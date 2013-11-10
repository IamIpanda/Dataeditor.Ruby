﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData.Serialization.RubyMarshal
{
    public class FuzzyUserdefinedDumpObject : FuzzyObject, IRubyUserdefinedDumpObject
    {
        private byte[] dumpedObject;

        public override string ToString()
        {
            return "#<" + this.ClassName.ToString() + ", dumped object: " + this.dumpedObject.ToString() + ">";
        }

        public byte[] DumpedObject
        {
            get { return dumpedObject; }
            set { dumpedObject = value; }
        }

        public byte[] Dump()
        {
            return this.dumpedObject;
        }
    }
}
