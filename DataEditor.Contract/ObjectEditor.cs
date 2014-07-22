using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Contract
{
    public interface ObjectEditor
    {
        void Push();
        void Pull();
        void Reset();
    }
}
