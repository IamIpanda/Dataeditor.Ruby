using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData.Serialization.RubyMarshal
{
    public interface IRubyUserdefinedDumpObject
    {
        byte[] Dump();
    }
}
