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
    public partial class SaveAsDialog : System.Windows.Forms.Form
    {
        public SaveAsDialog()
        {
            InitializeComponent();
            this.Shown += SaveAsDialog_Shown;
        }
        void SaveAsDialog_Shown(object sender, EventArgs e)
        {
            protoListBox1.Items.Clear();
            foreach(string key in Help.Data.Instance.Names)
            {
                protoListBox1.Items.Add(key);
            }
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Help.Serialization.TryGetSerialization("").Dump(SFD.OpenFile(), Help.Data.Instance[this.protoListBox1.SelectedItem.ToString()]);
            }
        }
    }
}
