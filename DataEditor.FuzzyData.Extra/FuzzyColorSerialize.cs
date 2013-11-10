using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DataEditor.FuzzyData.Serialization;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyColor
    {
        protected void ToBytes(Stream stream)
        {
            StreamWriter w = new StreamWriter(stream);
            w.Write((Byte)37);
            w.Write((double)r);
            w.Write((double)g);
            w.Write((double)b);
            w.Write((double)a);
        }
        protected static FuzzyColor GetBytes(Stream stream)
        {
            BinaryReader r = new BinaryReader(stream);
            int R, G, B, A;
            r.ReadByte();
            R = Convert.ToInt32(r.ReadDouble());
            G = Convert.ToInt32(r.ReadDouble());
            B = Convert.ToInt32(r.ReadDouble());
            A = Convert.ToInt32(r.ReadDouble());
            return new FuzzyColor(R, G, B, A);
        }
        protected XmlNode ToDocument(XmlDocument document)
        {
            XmlNode Parent = document.CreateNode(XmlNodeType.Element, "Color", "");
            XmlNode R = document.CreateNode(XmlNodeType.Element, "R", ""); R.InnerText = r.ToString();
            XmlNode G = document.CreateNode(XmlNodeType.Element, "G", ""); G.InnerText = g.ToString();
            XmlNode B = document.CreateNode(XmlNodeType.Element, "B", ""); B.InnerText = b.ToString();
            XmlNode A = document.CreateNode(XmlNodeType.Element, "A", ""); A.InnerText = a.ToString();
            Parent.AppendChild(R);
            Parent.AppendChild(G);
            Parent.AppendChild(B);
            Parent.AppendChild(A);
            return Parent;
        }
        protected static FuzzyColor GetDocument(XmlNode node)
        {
            int r = 0, g = 0, b = 0, a = 0;
            foreach (XmlNode n in node.ChildNodes)
                if (n.Name == "R")
                    r = GetInt(n.InnerText);
                else if (n.Name == "G")
                    g = GetInt(n.InnerText);
                else if (n.Name == "B")
                    b = GetInt(n.InnerText);
                else if (n.Name == "A")
                    a = GetInt(n.InnerText);
            return new FuzzyColor(r, g, b, a);
        }
        protected static int GetInt(string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return i;
            return 0;
        }
        public class FuzzyColorFactoty : ISerializationFactory<byte[]>, 
            ISerializationFactory<XmlNode>
        {
            public byte[] dump(Stream stream, object color, byte[] Tag)
            {
                (color as FuzzyColor).ToBytes(stream);
                return new byte[0];
            }
            public XmlNode dump(Stream stream, object color, XmlNode Node)
            {
                return (color as FuzzyColor).ToDocument(new XmlDocument());
            }
            public object _dump(Stream stream, byte[] bytes)
            {
                return FuzzyColor.GetBytes(stream);
            }
            public object _dump(Stream stream, XmlNode node)
            {
                return FuzzyColor.GetDocument(node);
            }
            public string Type
            {
                get { return "Color"; }
            }
            public Type Actual
            {
                get { return typeof(FuzzyColor); }
            }
        }
    }
}
