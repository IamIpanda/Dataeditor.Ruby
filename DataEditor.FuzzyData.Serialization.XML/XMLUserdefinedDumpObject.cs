using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData.Serialization.XML
{
    public class FuzzyUserdefinedDumpObject : FuzzyObject, IXMLUserdefinedDumpObject
    {
        private System.Xml.XmlNode dumpedObject;

        public override string ToString()
        {
            return "#<" + this.ClassName.ToString() + ", dumped object: " + this.dumpedObject.InnerText + ">";
        }

        public System.Xml.XmlNode DumpedObject
        {
            get { return dumpedObject; }
            set { dumpedObject = value; }
        }

        public System.Xml.XmlNode Dump()
        {
            return this.dumpedObject;
        }
    }
}
