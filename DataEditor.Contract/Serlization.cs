using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataEditor.Contract
{
    public interface Serialization
    {
        void Dump(Stream stream, object ob);
        object Load(Stream stream);
    }
    public interface Iconic
    {
        string Flag { get; }
    }
}
