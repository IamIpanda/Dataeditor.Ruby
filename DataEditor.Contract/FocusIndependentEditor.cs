using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Contract
{
    interface FocusIndependentEditor
    {
        void TriggerGetFocus(object sender,EventArgs e);
        void TriggerLostFocus(object sender,EventArgs e);
    }
}
