using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataEditor.FuzzyData.Serialization.RubyMarshal
{
    public class RubyMarshalAdapter : Contract.Serialization,Contract.Iconic
    {
        public void Dump(Stream stream, object ob)
        {
            RubyMarshal.Dump(stream, ob);
        }
        public object Load(Stream stream)
        {
            return RubyMarshal.Load(stream);
        }
        public string Flag
        {
            get { return "[m]"; }
        }
    }
}
