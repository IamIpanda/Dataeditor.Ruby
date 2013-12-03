using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public interface DataContainer : Control.ObjectEditor
    {
        System.Windows.Forms.Control.ControlCollection Controls { get; }
        void SetSize(System.Drawing.Size size);
        bool CanAdd(System.Windows.Forms.Control control);
        int start_x { get; }
        int start_y { get; }
        int end_x { get; }
        int end_y { get; }
    }
}
