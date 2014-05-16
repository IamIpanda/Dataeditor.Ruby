using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class EditorWindow : Form
    {
        public EditorWindow()
        {
            InitializeComponent();
        }

        private void WindowEditor_Load(object sender, EventArgs e)
        {

        }

        private void EditorWindow_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (!(e.Alt)) return;
            msMain.Visible = !(msMain.Visible);
            if (!msMain.Visible) return;
            msMain.Focus();
            msMain.Items[0].Select();
             * */
        }

        private void msMain_Leave(object sender, EventArgs e)
        {
            msMain.Visible = false;
        }
        public class WrapEditorWindow<T> : WrapBaseWindow<T> where T : EditorWindow, new()
        {
            public override System.Windows.Forms.Control.ControlCollection Controls { get { return Window.pnMain.Controls;  } }
            public override string Flag { get { return "editor"; } }
        }

        private void msMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
