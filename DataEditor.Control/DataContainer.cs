using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public interface DataContainer : Control.ObjectEditor
    {
        System.Windows.Forms.Control.ControlCollection Controls { get; }
    }
}
