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

        private void Titan_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (MessageBox.Show("您是否要保存您的数据？", "Fux 的节操", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                string target = System.IO.Path.Combine(DataEditor.Help.Path.Instance["project"], "Data/Actors.rxdata");
                var stream = new System.IO.FileStream(target, System.IO.FileMode.Create);
                FuzzyData.Serialization.RubyMarshal.RubyMarshal.Dump(stream, Help.Data.Instance["actor"]);
                stream.Close();
            }
             * */
        }

        private void pnMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class WrapTitan : DataEditor.Control.Window.EditorWindow.WrapEditorWindow<Titan> 
    {
        public override void SetSize(Size size)
        {
            base.SetSize(new Size(size.Width + 15, size.Height + 40));
        }
    }
}
