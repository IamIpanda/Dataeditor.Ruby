using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataEditor.FuzzyData.Serialization.XML
{
    public class XMLAdapter : Contract.Serialization, Contract.Iconic
    {
        public void Dump(Stream stream, object ob)
        {
            XML.Dump(stream, ob);
        }
        public object Load(Stream stream)
        {
            return XML.Load(stream);
        }
        class XMLReadStealer : XMLReader { public XMLReadStealer () { } }
        public object Load (System.Xml.XmlNode Node)
        {
            XMLReader reader = new XMLReadStealer();
            return reader.Load(Node);
        }
        public string Flag
        {
            get { return "[x]"; }
        }
    }
}
