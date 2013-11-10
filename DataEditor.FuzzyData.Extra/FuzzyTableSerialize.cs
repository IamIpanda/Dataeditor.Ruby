using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DataEditor.FuzzyData.Serialization;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyTable
    {
        protected void ToBytes(Stream stream)
        {
            BinaryWriter w = new BinaryWriter(stream);
            WriteLong((x_size * y_size * z_size * 2 + 20), w);
            w.Write((Int32)dimension);
            w.Write((Int32)x_size);
            w.Write((Int32)y_size);
            w.Write((Int32)z_size);
            w.Write((Int32)(x_size * y_size * z_size));
            for (int i = 0; i < z_size; i++)
                for (int j = 0; j < y_size; j++)
                    for (int k = 0; k < x_size; k++)
                        w.Write((Int16)value[k, j, i]);
        }
        protected static FuzzyTable GetBytes(Stream stream)
        {
            BinaryReader r = new BinaryReader(stream);
            int check1, check2, x, y, z, d;
            check1 = ReadLong(r);
            d = r.ReadInt32();
            x = r.ReadInt32();
            y = r.ReadInt32();
            z = r.ReadInt32();
            //Console.WriteLine("c1" + check1.ToString() + "d" + d.ToString() + "x" + x.ToString() + "y" + y.ToString() + "z" + z.ToString());
            check2 = r.ReadInt32();
            FuzzyTable ft = new FuzzyTable(x, y, z);
            ft.dimension = d;
            for (int i = 0; i < z; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < x; k++)
                        ft.value[k, j, i] = r.ReadInt16();
            return ft;
        }
        protected static int ReadLong(BinaryReader m_reader)
        {
            sbyte num = m_reader.ReadSByte();
            if (num <= -5)
                return num + 5;
            if (num < 0)
            {
                int output = 0;
                for (int i = 0; i < -num; i++)
                {
                    output += (0xff - m_reader.ReadByte()) << (8 * i);
                }
                return (-output - 1);
            }
            if (num == 0)
                return 0;
            if (num <= 4)
            {
                int output = 0;
                for (int i = 0; i < num; i++)
                {
                    output += m_reader.ReadByte() << (8 * i);
                }
                return output;
            }
            return (num - 5);
        }
        protected static void WriteLong(int value,BinaryWriter m_writer)
        {
            if (value == 0)
            {
                m_writer.Write((byte)0);
            }
            else if ((value > 0) && (value < 0x7b))
            {
                m_writer.Write((byte)(value + 5));
            }
            else if ((value < 0) && (value > -124))
            {
                m_writer.Write((sbyte)(value - 5));
            }
            else
            {
                sbyte num2;
                byte[] buffer = new byte[5];
                buffer[1] = (byte)(value & 0xff);
                buffer[2] = (byte)((value >> 8) & 0xff);
                buffer[3] = (byte)((value >> 0x10) & 0xff);
                buffer[4] = (byte)((value >> 0x18) & 0xff);
                int index = 4;
                if (value >= 0)
                {
                    while (buffer[index] == 0)
                    {
                        index--;
                    }
                    num2 = (sbyte)index;
                }
                else
                {
                    while (buffer[index] == 0xff)
                    {
                        index--;
                    }
                    num2 = (sbyte)-index;
                }
                buffer[0] = (byte)num2;
                m_writer.Write(buffer, 0, index + 1);
            }
        }
        protected XmlNode ToDocument(XmlDocument document)
        {
            XmlNode Parent = document.CreateNode(XmlNodeType.Element, "Table", "");
            XmlAttribute d, x, y, z;
            d = document.CreateAttribute("dimension", ""); d.InnerText = dimension.ToString();
            x = document.CreateAttribute("xsize", ""); d.InnerText = x_size.ToString();
            y = document.CreateAttribute("ysize", ""); d.InnerText = y_size.ToString();
            z = document.CreateAttribute("zsize", ""); d.InnerText = z_size.ToString();
            Parent.Attributes.Append(d);
            Parent.Attributes.Append(x);
            Parent.Attributes.Append(y);
            Parent.Attributes.Append(z);
            XmlNode z_Node, y_Node, x_Node;
            XmlAttribute note;
            string snote = "Note", inner;
            for (int i = 0; i < z_size; i++)
            {
                z_Node = document.CreateNode(XmlNodeType.Element, "Z", "");
                Parent.AppendChild(z_Node);
                inner = "[" + i.ToString() + "]";
                note = document.CreateAttribute(snote);
                note.InnerText = inner;
                z_Node.Attributes.Append(note);
                for (int j = 0; j < y_size; j++)
                {
                    y_Node = document.CreateNode(XmlNodeType.Element, "Y", "");
                    z_Node.AppendChild(y_Node);
                    inner = "[" + j.ToString() + "," + i.ToString() + "]";
                    note = document.CreateAttribute(snote);
                    note.InnerText = inner;
                    y_Node.Attributes.Append(note);
                    for (int k = 0; k < x_size; k++)
                    {
                        x_Node = document.CreateNode(XmlNodeType.Element, "X", "");
                        y_Node.AppendChild(x_Node); 
                        inner = "[" + k.ToString() + "," + j.ToString() + "," + i.ToString() + "]";
                        note = document.CreateAttribute(snote);
                        note.InnerText = inner;
                        x_Node.Attributes.Append(note);
                        x_Node.InnerText = value[k, j, i].ToString();
                    }
                }
            }
            return Parent;
        }
        static protected FuzzyTable GetDocument(XmlNode node)
        {
            int d = 3, x = 1, y = 1, z = 1;
            foreach (XmlAttribute a in node.Attributes)
                if (a.Name == "dimension")
                    d = GetInt(a);
                else if (a.Name == "xsize")
                    x = GetInt(a);
                else if (a.Name == "yszie")
                    y = GetInt(a);
                else if (a.Name == "zsize")
                    z = GetInt(a);
            int zc = -1, yc = 0, xc = 0;
            FuzzyTable ft = new FuzzyTable(x,y,z);
            ft.dimension = d;
            foreach (XmlNode z_Node in node.ChildNodes)
            {
                foreach (XmlNode y_Node in z_Node.ChildNodes)
                {
                    foreach (XmlNode x_Node in y_Node.ChildNodes)
                    {
                        ft[xc, yc, zc] = GetShort(x_Node);
                        xc += 1;
                    }
                    yc += 1;
                    xc = 0;
                }
                zc += 1;
                yc = 0;
                xc = 0;
            }
            return ft;
        }
        static protected int GetInt(XmlNode Node)
        {
            int i;
            if (int.TryParse(Node.InnerText, out i))
                return i;
            return 0;
        }
        static protected Int16 GetShort(XmlNode Node)
        {
            short s;
            if (Int16.TryParse(Node.InnerText, out s))
                return s;
            return 0;
        }
        public class FuzzyTableFactory : ISerializationFactory<XmlNode>, 
            ISerializationFactory<byte[]>
        {
            public byte[] dump(Stream stream, object table,byte[] Tag)
            {
                (table as FuzzyTable).ToBytes(stream);
                return new byte[0];
            }
            public XmlNode dump(Stream stream,object table,XmlNode Tag)
            {
                return (table as FuzzyTable).ToDocument(new XmlDocument());
            }
            public object _dump(Stream stream, byte[] bytes)
            {
                return FuzzyTable.GetBytes(stream);
            }
            public object _dump(Stream stream, XmlNode node)
            {
                return FuzzyTable.GetDocument(node);
            }
            public string Type
            {
                get { return "Table"; }
            }
            public Type Actual
            {
                get { return typeof(FuzzyTable); }
            }
        }
    }
}
