using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public interface ObjectEditor : Contract.ObjectEditor, Contract.Iconic
    {
        ObjectEditor Parent { get; set; }
        System.Windows.Forms.Label Label { get; set; }
        System.Windows.Forms.Control Binding { get; }

    }
}
