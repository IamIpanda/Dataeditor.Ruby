using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataEditor.FuzzyData;

namespace DataEditor.Arce
{
    public partial class Titan : DataEditor.Control.Window.EditorWindow
    {
        public Titan ()
        {
            InitializeComponent();
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Window.EditorWindow).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Wrapper.Text).Assembly);
        }

        private void Titan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Help.Log.Flush();
            (Help.Window.Instance["lead"] as WrapLead).Window.Close();
        }
    }
    public class WrapTitan : DataEditor.Control.Window.EditorWindow.WrapEditorWindow<Titan> { }
}
