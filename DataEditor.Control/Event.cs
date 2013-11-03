using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public static class Event
    {
        public static void OnLeave(object sender, EventArgs e)
        {
            Control.ObjectEditor editor =  sender as Control.ObjectEditor;
            if (editor == null) return;
            if (!(editor.CheckValue())) return;
            
            editor.Push();
        }
    }
}
