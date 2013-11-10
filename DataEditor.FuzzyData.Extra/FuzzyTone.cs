using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using DataEditor.FuzzyData.Serialization;

namespace DataEditor.FuzzyData
{
    public class FuzzyTone
    {
        public int red { get; set; }
        public int green { get; set; }
        public int blue { get; set; }
        public int gray { get; set; }
        public FuzzyTone (int r = 0, int g = 0, int b = 0, int gray = 0)
        {
            red = r;
            green = g;
            blue = b;
            this.gray = gray;
        }


        protected void ToBytes (Stream stream)
        {
            StreamWriter w = new StreamWriter(stream);
            w.Write((Byte)37);
            w.Write((double)red);
            w.Write((double)green);
            w.Write((double)blue);
            w.Write((double)gray);
        }
        protected static FuzzyTone GetBytes (Stream stream)
        {
            BinaryReader r = new BinaryReader(stream);
            int R, G, B, A;
            r.ReadByte();
            R = Convert.ToInt32(r.ReadDouble());
            G = Convert.ToInt32(r.ReadDouble());
            B = Convert.ToInt32(r.ReadDouble());
            A = Convert.ToInt32(r.ReadDouble());
            return new FuzzyTone(R, G, B, A);
        }
        protected static FuzzyTone GetDocument (XmlNode node)
        {
            int r = 0, g = 0, b = 0, a = 0;
            foreach ( XmlNode n in node.ChildNodes )
                if ( n.Name == "R" )
                    r = GetInt(n.InnerText);
                else if ( n.Name == "G" )
                    g = GetInt(n.InnerText);
                else if ( n.Name == "B" )
                    b = GetInt(n.InnerText);
                else if ( n.Name == "A" )
                    a = GetInt(n.InnerText);
            return new FuzzyTone(r, g, b, a);
        }
        protected static int GetInt (string str)
        {
            int i;
            if ( int.TryParse(str, out i) )
                return i;
            return 0;
        }
        protected XmlNode ToDocument(XmlDocument document)
        {
            XmlNode Parent = document.CreateNode(XmlNodeType.Element, "Tone", "");
            XmlNode R = document.CreateNode(XmlNodeType.Element, "R", ""); R.InnerText = red.ToString();
            XmlNode G = document.CreateNode(XmlNodeType.Element, "G", ""); G.InnerText = green.ToString();
            XmlNode B = document.CreateNode(XmlNodeType.Element, "B", ""); B.InnerText = blue.ToString();
            XmlNode A = document.CreateNode(XmlNodeType.Element, "A", ""); A.InnerText = gray.ToString();
            Parent.AppendChild(R);
            Parent.AppendChild(G);
            Parent.AppendChild(B);
            Parent.AppendChild(A);
            return Parent;
        }
        public class FuzzyToneFactoty : ISerializationFactory<byte[]>,
    ISerializationFactory<XmlNode>
        {
            public byte[] dump(Stream stream, object color, byte[] Tag)
            {
                (color as FuzzyTone).ToBytes(stream);
                return new byte[0];
            }
            public XmlNode dump(Stream stream, object color, XmlNode Node)
            {
                return (color as FuzzyTone).ToDocument(new XmlDocument());
            }
            public object _dump(Stream stream, byte[] bytes)
            {
                return FuzzyTone.GetBytes(stream);
            }
            public object _dump(Stream stream, XmlNode node)
            {
                return FuzzyTone.GetDocument(node);
            }
            public string Type
            {
                get { return "Tone"; }
            }
            public Type Actual
            {
                get { return typeof(FuzzyTone); }
            }
        }
    }

}
