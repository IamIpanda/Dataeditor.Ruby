using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    public partial class Lead : DataEditor.Control.Window.ChoiceWindow
    {
        public Lead()
        {
            InitializeComponent();


            string[] loads = new string[] { @"Program\Serialization\DataEditor.FuzzyData.Serialization.RubyMarshal.dll"
                , @"Program\Serialization\UserDefined\DataEditor.FuzzyData.Extra.dll" };
            Array.ForEach(loads, (string ass) =>
            {
                System.Reflection.Assembly.LoadFile(System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath),
                        ass
                    ));
            });
        }
        protected override void protoListBox1_DoubleClick(object sender, EventArgs e)
        {
            RunResult(protoListBox1.SelectedIndex);
        }

        protected override void protoListBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                RunResult(protoListBox1.SelectedIndex);
        }
        public void RunResult(int i)
        {
            switch (i)
            {
                case 0: RunOpenProject(); break;
                case 1: RunOpenFile(); break;
                case 2: RunExecuteFile(); break;
                case 3: RunOption(); break;
                case 4: RunExit(); break;
            }
        }
        void RunOpenProject()
        {
            OFD.Filter = "RMXP 工程文件|*.rxproj|RMVX 工程文件|*.rvproj|RMVA 工程文件|*.rvproj2|所有文件|*.*";
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string Path = OFD.FileName;
                Engine.OpenProject(Path);
                var sp = new DataEditor.Control.ShapeShifter.ShapeShifter();
                sp.Show();
                this.Hide();
            }
        }
        void RunOpenFile()
        {
            OFD.Filter = "RMXP 数据文件|*.rxdata|RMVX 数据文件|*.rvdata|RMVA 数据文件|*.*rvdata2|所有文件|*.*";
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string Path = OFD.FileName;
                Engine.OpenFile(Path);
                this.Hide();
            }
        }
        void RunExecuteFile()
        {
            OFD.Filter = "|Ruby 脚本文件|*.rb|所有文件|*.*";
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string Path = OFD.FileName;
                Engine.OpenRubyFile(Path);
                this.Hide();
            }
        }
        void RunOption()
        {
            DataEditor.Control.Wrapper.TroopMember t = new Control.Wrapper.TroopMember();
            Engine.OpenOption();
        }
        void RunExit()
        {
            Engine.Exit();
        }

        private void Lead_Load(object sender, EventArgs e)
        {
        }
    }
    public class WrapLead : DataEditor.Control.WrapBaseWindow<Lead> { }
}
