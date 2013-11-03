using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Ruby
{
    public interface DataEditor : Control.ObjectEditor
    {
        Help.Parameter GetParameter(IronRuby.Builtins.Hash Hash);
    }
}
