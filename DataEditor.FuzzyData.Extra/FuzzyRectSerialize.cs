using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DataEditor.FuzzyData.Serialization;

namespace DataEditor.FuzzyData
{
    partial class FuzzyRect 
    {
        protected void ToBytes(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Write((byte)37);
            writer.Write((double)X);
            writer.Write((double)Y);
            writer.Write((double)w);
            writer.Write((double)h);
        }
        protected static FuzzyRect GetBytes(Stream stream)
        {
            BinaryReader r = new BinaryReader(stream);
            int x, y, width, height;
            byte b = r.ReadByte();
            x = Convert.ToInt32(r.ReadDouble());
            y = Convert.ToInt32(r.ReadDouble());
            width  = Convert.ToInt32(r.ReadDouble());
            height = Convert.ToInt32(r.ReadDouble());
            return new FuzzyRect(x, y, width, height);
        }
        protected XmlNode ToDocument(XmlDocument document)
        {
            XmlNode Parent = document.CreateNode(XmlNodeType.Element, "Color", "");
            XmlNode X = document.CreateNode(XmlNodeType.Element, "X", ""); X.InnerText = this.X.ToString();
            XmlNode Y = document.CreateNode(XmlNodeType.Element, "Y", ""); Y.InnerText = this.Y.ToString();
            XmlNode W = document.CreateNode(XmlNodeType.Element, "W", ""); W.InnerText = width.ToString();
            XmlNode H = document.CreateNode(XmlNodeType.Element, "H", ""); H.InnerText = height.ToString();
            Parent.AppendChild(X);
            Parent.AppendChild(Y);
            Parent.AppendChild(W);
            Parent.AppendChild(H);
            return Parent;
        }
        protected static FuzzyRect GetDocument(XmlNode node)
        {
            int x = 0, y = 0, w = 0, h = 0;
            foreach (XmlNode n in node.ChildNodes)
                if (n.Name == "X")
                    x = GetInt(n.InnerText);
                else if (n.Name == "Y")
                    y = GetInt(n.InnerText);
                else if (n.Name == "W")
                    w = GetInt(n.InnerText);
                else if (n.Name == "H")
                    h = GetInt(n.InnerText);
            return new FuzzyRect(x, y, w, h);
        }
        protected static int GetInt(string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return i;
            return 0;
        }
        public class FuzzyRectFactoty : ISerializationFactory<byte[]>,
            ISerializationFactory<XmlNode>
        {
            public byte[] dump(Stream stream, object rect, byte[] Tag)
            {
                (rect as FuzzyRect).ToBytes(stream);
                return new byte[0];
            }
            public XmlNode dump(Stream stream,object rect, XmlNode Tag)
            {
                return (rect as FuzzyRect).ToDocument(new XmlDocument());
            }
            public object _dump(Stream stream, byte[] bytes)
            {
                return FuzzyRect.GetBytes(stream);
            }
            public object _dump(Stream stream, XmlNode node)
            {
                return FuzzyRect.GetDocument(node);
            }
            public string Type
            {
                get { return "Rect"; }
            }
            public Type Actual
            {
                get { return typeof(FuzzyRect); }
            }
        }
    }
}
