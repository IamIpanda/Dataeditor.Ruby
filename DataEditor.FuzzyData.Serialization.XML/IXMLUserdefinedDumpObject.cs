using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData.Serialization.XML
{
    public interface IXMLUserdefinedDumpObject
    {
        System.Xml.XmlNode Dump();
    }
}
