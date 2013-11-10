using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace DataEditor.FuzzyData.Serialization.XML
{
    class XMLWriter
    {
        private XmlDocument x_document;
        private XmlWriter x_writer;
        private Dictionary<object, int> x_objects;

        private Stream x_stream;

        private Stack<XmlNode> x_parent_node = new Stack<XmlNode>();

        public XMLWriter(Stream output)
        {
            if (output == null)
                throw new ArgumentNullException("output");
            if (!output.CanWrite)
                throw new ArgumentException("stream cannot write");
            this.x_stream = output;
            this.x_document = new XmlDocument();
            this.x_objects = new Dictionary<object, int>();
            this.x_writer = XmlWriter.Create(output);
            this.x_parent_node.Clear();
            this.x_parent_node.Push(x_document);
        }
        public XmlNode WriteNode(string name, string value = "")
        {
            XmlNode Node = x_document.CreateNode(XmlNodeType.Element, name, "");
            Node.InnerText = value;
            x_parent_node.Peek().AppendChild(Node);
            return Node;
        }
        public XmlNode WritePopNode(string name, string value = "")
        {
            XmlNode node = WriteNode(name, value);
            x_parent_node.Push(node);
            return node;
        }
        public void WriteText(string text)
        {
            x_parent_node.Peek().InnerText = text;
        }
        public XmlAttribute WriteAttribute(string name, string value)
        {
            XmlAttribute attribute = x_document.CreateAttribute(name, "");
            attribute.InnerText = value;
            x_parent_node.Peek().Attributes.Append(attribute);
            return attribute;
        }
        public XmlNode WriteSymbol(FuzzySymbol id)
        {
            string sym;
            System.Text.Encoding encidx = null;

            sym = id.Name;
            encidx = id.Encoding;
            if (encidx == Encoding.ASCII || encidx == Encoding.Default || encidx == Encoding.UTF8)
                encidx = null;
            XmlNode node = WritePopNode(XML.Types.Symbol, sym);
            if (encidx != null)
                WriteAttribute("Encoding", encidx.EncodingName);
            x_parent_node.Pop();
            return node;
        }
        public void WriteString(String s,System.Text.Encoding encidx)
        {
            WriteText(s);
            if (encidx == Encoding.ASCII || encidx == Encoding.Default || encidx == Encoding.UTF8)
                return;
            WriteNode("Encoding", encidx.EncodingName);
        }
        public XmlNode WriteFloat(double value)
        {
            XmlNode node = WritePopNode(XML.Types.Float);
            if (double.IsInfinity(value))
            {
                if (double.IsPositiveInfinity(value))
                    WriteText("inf");
                else
                    WriteText("-inf");
            }
            else if (double.IsNaN(value))
                WriteText("nan");
            else
                WriteText(string.Format("{0:g}", value));
            x_parent_node.Pop();
            return node;
        }
        public XmlNode WriteFloat(float value)
        {
            return WriteFloat((double)value);
        }
        public bool WriteUserClass(object obj, FuzzyClass super)
        {
            FuzzyObject fobj = obj as FuzzyObject;
            if (fobj != null)
            {
                FuzzyClass klass = fobj.Class;
            }
            return false;
        }
        public void WriteObject(object obj)
        {
            int num;
            if (this.x_objects.TryGetValue(obj, out num))
            {
                WriteNode(XML.Types.Link, num.ToString());
                return;
            }
            if (obj == null || obj == FuzzyNil.Instance)
                WriteNode(XML.Types.Nil, "");
            else if (obj is bool && (bool)obj == true)
                WriteNode(XML.Types.True, "");
            else if (obj is bool && (bool)obj == false)
                WriteNode(XML.Types.False, "");
            else if (obj is int || obj is long || obj is FuzzyFixnum)
            {
                long v;
                if (obj is FuzzyFixnum)
                    v = (obj as FuzzyFixnum).Value;
                else
                    v = (long)obj;
                if (v <= 1073741823 && v >= -1073741824)
                    WriteNode(XML.Types.Fixnum, obj.ToString());
                else
                    WriteObject(FuzzyBignum.Create(v));
            }
            else if (obj is FuzzySymbol)
                WriteSymbol(obj as FuzzySymbol);
            else
            {
                FuzzyObject fobj = obj as FuzzyObject;
                bool hasiv = false; 
                if (fobj != null)
                    hasiv = (obj as FuzzyObject).InstanceVariables.Count > 0 || fobj.Encoding != null;
                if (obj is IXMLUserdefinedDumpObject)
                {
                    this.x_objects.Add(obj, this.x_objects.Count);
                    object result = (obj as IXMLUserdefinedMarshalDumpObject).Dump();
                    WritePopNode(XML.Types.UserMarshal, "");
                    WriteObject(result);
                    x_parent_node.Pop();
                    return;
                }
                if (obj is IXMLUserdefinedDumpObject)
                {
                    XmlNode result = (obj as IXMLUserdefinedDumpObject).Dump();
                    WritePopNode(XML.Types.UserDefined, "").AppendChild(result);
                    this.x_objects.Add(obj, this.x_objects.Count);
                    return;
                }
                this.x_objects.Add(obj, this.x_objects.Count);
                if (obj is FuzzyClass)
                    WriteNode(XML.Types.Class, (obj as FuzzyClass).Name);
                else if (obj is FuzzyModule)
                    WriteNode(XML.Types.Module, (obj as FuzzyClass).Name);
                else if (obj is float)
                    WriteFloat((float)obj);
                else if (obj is double)
                    WriteFloat((double)obj);
                else if (obj is FuzzyFloat)
                    WriteFloat((obj as FuzzyFloat).Value);
                else if (obj is FuzzyBignum)
                    WriteNode(XML.Types.Bignum, obj.ToString());
                else if (obj is FuzzyBignumAdapter)
                    WriteNode(XML.Types.Bignum, (obj as FuzzyBignumAdapter).Value.ToString());
                else if (obj is FuzzyString || obj is string)
                {
                    FuzzyString v;
                    if (obj is string)
                        v = new FuzzyString(obj as string);
                    else
                        v = (FuzzyString)obj;
                    WritePopNode(XML.Types.String);
                    WriteString(v.Text, v.Encoding);
                    x_parent_node.Pop();
                }
                else if (obj is FuzzyRegexp)
                {
                    FuzzyRegexp v = (FuzzyRegexp)obj;
                    WritePopNode(XML.Types.Regexp);
                    WriteString(v.Pattern.Text, v.Pattern.Encoding);
                    WriteNode("Options", ((int)v.Options).ToString());
                }
                else if (obj is FuzzyArray || obj is List<object>)
                {
                    FuzzyArray v;
                    if (obj is List<object>)
                        v = new FuzzyArray(obj as List<object>);
                    else
                        v = (FuzzyArray)obj;
                    WritePopNode(XML.Types.Array);
                    for (int i = 0; i < v.Count; i++)
                        WriteObject(v[i]);
                    x_parent_node.Pop();
                }
                else if (obj is FuzzyHash)
                {
                    FuzzyHash v = (FuzzyHash)obj;
                    if (v.DefaultValue == null)
                        WritePopNode(XML.Types.Hash);
                    else
                    {
                        WritePopNode(XML.Types.HashWithDefault);
                        WritePopNode("Default");
                        WriteObject(v.DefaultValue);
                        x_parent_node.Pop();
                    }
                    foreach (KeyValuePair<object, object> item in v)
                    {
                        WriteObject(item.Key);
                        WriteObject(item.Value);
                    }
                    x_parent_node.Pop();
                }

                else if (obj is FuzzyObject || obj is FuzzyStruct)
                {
                    FuzzyObject v = fobj;
                    if (obj is FuzzyObject)
                        WritePopNode(XML.Types.Object);
                    else
                        WritePopNode(XML.Types.Struct);
                    if (XML.Options.IgnoreSymbolEncoding)
                    {
                        WriteAttribute("Name", v.ClassName.Name);
                        foreach (KeyValuePair<FuzzySymbol, object> item in v.InstanceVariables)
                        {
                            WritePopNode("Data");
                            WriteAttribute("Name", item.Key.Name);
                            WriteObject(item.Value);
                            x_parent_node.Pop();
                        }
                    }
                    else
                    {
                       var factor = Serialization.Factory<XmlNode>.Factor(fobj.GetType());
                       if (factor == null) factor = Serialization.Factory<XmlNode>.Factor(fobj.ClassName.Name);
                       if (factor != null)
                       {
                           WritePopNode(XML.Types.UserDefined);
                           XmlNode node = factor.dump(x_stream, fobj, null);
                           node = x_document.ImportNode(node, true);
                           x_parent_node.Pop().AppendChild(node);
                       }
                       else
                       {
                           WritePopNode("Name");
                           WriteObject(v.ClassName);
                           x_parent_node.Pop();
                           foreach (KeyValuePair<FuzzySymbol, object> item in v.InstanceVariables)
                           {
                               WriteSymbol(item.Key);
                               WriteObject(item.Value);
                           }
                       }
                    }
                    x_parent_node.Pop();
                }
                else
                {

                    throw new InvalidDataException(string.Format("can't dump {0}", obj.GetType().FullName));
                }
            }
        }

        public void Dump(object obj)
        {
            WritePopNode("Data");
            WritePopNode("Version");
            if (XML.Options.IgnoreSymbolEncoding)
                WriteAttribute("IgnoreSymbolEncoding", "Yes");
            WriteAttribute("MajorVersion", XML.XMLMajor.ToString());
            WriteAttribute("MinorVersion", XML.XMLMinor.ToString());
            WriteObject(obj);
            x_document.Save(x_writer);
        }
    }
}