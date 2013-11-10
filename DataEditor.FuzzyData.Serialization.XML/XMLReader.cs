using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace DataEditor.FuzzyData.Serialization.XML
{
    
    class XMLReader
    {
        private bool x_ignoreSymbolEncoding;
        private Stream x_stream;
        private XmlReader x_reader;
        private XmlDocument x_document;
        private XmlNode x_node;
        private Dictionary<int, object> x_objects;
        private Dictionary<int, FuzzySymbol> x_symbols;
        private Dictionary<object, object> x_compat_tbl;

        public XMLReader(Stream input)
        {
            if (input == null)
                throw new ArgumentNullException("instance of IO needed");
            if (!input.CanRead)
                throw new ArgumentException("instance of IO needed");
            this.x_stream = input;
            this.x_objects = new Dictionary<int, object>();
            this.x_symbols = new Dictionary<int, FuzzySymbol>();
            this.x_document = new XmlDocument();
            this.x_compat_tbl = new Dictionary<object, object>();
            this.x_reader = XmlReader.Create(input);
            this.x_ignoreSymbolEncoding = false;
            try
            {
                x_document.Load(x_reader);
            }
            catch (Exception e)
            {
                throw e;
            }
            this.x_node = x_document; 
        }

        protected XMLReader () 
        {
            this.x_objects = new Dictionary<int, object>();
            this.x_symbols = new Dictionary<int, FuzzySymbol>();
        }

        public object Load()
        {
            x_node = x_document.FirstChild.NextSibling;
            x_node = x_node.FirstChild;
            int major = XML.XMLMajor;
            int minor = XML.XMLMinor;
            if (ReadAttribute("IgnoreSymbolEncoding") == "Yes")
                x_ignoreSymbolEncoding = true;
            if (int.TryParse(ReadAttribute("MajorVersion"), out major))
                if (int.TryParse(ReadAttribute("MinorVersion"), out minor))
                    if (major != XML.XMLMajor || minor > XML.XMLMinor)
                        throw new InvalidDataException(string.Format("incompatible xml file format (can't be read)\n\tformat version {0}.{1} required; {2}.{3} given", XML.MarshalMajor, XML.MarshalMinor, major, minor));
            x_node = x_node.FirstChild;
            return ReadObject();
        }

        public object Load(XmlNode Node)
        {
            x_node = Node;
            return ReadObject();
        }

        protected string ReadAttribute(string key)
        {
            foreach (XmlAttribute a in x_node.Attributes)
                if (a.Name == key)
                    return a.InnerText;
            return "";
        }

        protected string ReadText()
        {
            foreach (XmlNode node in x_node.ChildNodes)
                if (node is XmlText)
                    return node.Value;
            return "";
        }

        protected long ReadInt(string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return i;
            throw new Exception("Not able to get int : " + str);
        }

        protected double ReadFloat(string str)
        {
            double d;
            if (str == "nan")
                return double.NaN;
            else if (str == "inf")
                return double.PositiveInfinity;
            else if (str == "-inf")
                return double.NegativeInfinity;
            else
                if (double.TryParse(str,out d))
                    return d;
            throw new Exception("Not able to get double : " + str);
        }

        protected string ReadString(string str)
        {
            return str;
        }

        protected FuzzyString GetString()
        {
            string str = ReadText();
            return new FuzzyString(str);
        }
        /// <summary>
        /// 从文档中读取一个 object
        /// 注意：焦点没有发生移动。
        /// </summary>
        /// <returns></returns>
        protected FuzzyObject GetObject()
        {
            FuzzyObject fobj = new FuzzyObject();
            x_node = x_node.FirstChild;
            if (x_node.Name == "Name")
            {
                x_node = x_node.FirstChild;
                fobj.ClassName = ReadObject() as FuzzySymbol;
                if (fobj.ClassName == null)
                    fobj.ClassName = FuzzySymbol.GetSymbol("");
                x_node = x_node.ParentNode.NextSibling;
            }
            FuzzySymbol name = null;
            if (x_ignoreSymbolEncoding)
            {
                name = FuzzySymbol.GetSymbol(ReadAttribute("Name"));
                fobj.InstanceVariables.Add(name, ReadObject());
                while (x_node.NextSibling != null)
                {
                    x_node = x_node.NextSibling;
                    name = FuzzySymbol.GetSymbol(ReadAttribute("Name"));
                    fobj.InstanceVariables.Add(name, ReadObject());
                }
            }
            else
            {
                name = ReadObject() as FuzzySymbol;
                while (x_node.NextSibling != null)
                {
                    x_node = x_node.NextSibling;
                    if (name == null)
                        name = ReadObject() as FuzzySymbol;
                    else
                    {
                        fobj.InstanceVariables.Add(name, ReadObject());
                        name = null;
                    }
                }
            }
            x_node = x_node.ParentNode;
            return fobj;
        }
        /// <summary>
        /// 从文档中读取一个 Hash
        /// 注意：焦点没有发生移动。
        /// </summary>
        /// <returns></returns>
        protected FuzzyHash GetHash()
        {
            FuzzyHash fhash = new FuzzyHash();
            x_node = x_node.FirstChild;
            if (x_node.Name == "Default")
            {
                XmlNode parent = x_node;
                x_node = x_node.FirstChild;
                fhash.DefaultValue = ReadObject();
                x_node = parent.NextSibling;
            }
            object key = ReadObject();
            while (x_node.NextSibling != null)
            {
                x_node = x_node.NextSibling;
                if (key == null)
                    key = ReadObject();
                else
                {
                    fhash.Add(key, ReadObject());
                    key = null;
                }
            }
            x_node = x_node.ParentNode;
            return fhash;
        }
        /// <summary>
        /// 从文档中读取一个 Array。
        /// 注意：焦点没有发生移动。
        /// </summary>
        /// <returns></returns>
        protected FuzzyArray GetArray()
        {
            FuzzyArray farray = new FuzzyArray();
            x_node = x_node.FirstChild;
            farray.Add(ReadObject());
            while (x_node.NextSibling != null)
            {
                x_node = x_node.NextSibling;
                farray.Add(ReadObject());
            }
            x_node = x_node.ParentNode;
            return farray;
        }
        public object ReadObject()
        {
            object v = null;
            string type = x_node.Name;
            switch (type)
            {
                case XML.Types.Link:
                    int index = (int)ReadInt(ReadText());
                    return x_objects[index];
                case XML.Types.Nil:
                    return FuzzyNil.Instance;
                case XML.Types.True:
                    return FuzzyBool.True;
                case XML.Types.False:
                    return FuzzyBool.False;
                case XML.Types.Fixnum:
                    return new FuzzyFixnum(ReadInt(ReadText()));
                case XML.Types.Symbol:
                    // TODO : 编码解析
                    string str = ReadText();
                    return FuzzySymbol.GetSymbol(str);
                case XML.Types.Bignum:
                    return new FuzzyBignumAdapter(ReadText());
                // ============== 引用类 ==============
                case XML.Types.Float:
                    double dou = ReadFloat(ReadText());
                    v = new FuzzyFloat(dou);
                    break;
                case XML.Types.String:
                    v = GetString();
                    break;
                case XML.Types.Regexp:
                    // TODO : Finish this.
                    break;
                case XML.Types.Array:
                    v = GetArray();
                    break;
                case XML.Types.Hash:
                case XML.Types.HashWithDefault:
                    v = GetHash();
                    break;
                case XML.Types.Struct:
                    // TODO : Finish this.
                    break;
                case XML.Types.UserDefined:
                    break;
                case XML.Types.UserMarshal:
                    break;
                case XML.Types.Object:
                    v = GetObject();
                    break;
                default:
                    throw new InvalidDataException(string.Format("dump format error({0:X2})", type));
            }
            x_objects.Add(x_objects.Count, v);
            
            return v;
        }
        public object ReadObject0(XmlNode node,bool IgnoreSymbolEncoding = true)
        {
            x_node = node;
            x_ignoreSymbolEncoding = IgnoreSymbolEncoding;
            return ReadObject();
        }
    }
}
