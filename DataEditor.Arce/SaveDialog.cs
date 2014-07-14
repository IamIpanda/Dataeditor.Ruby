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
    public partial class SaveDialog : System.Windows.Forms.Form
    {
        public SaveDialog()
        {
            InitializeComponent();
            this.Shown += SaveDialog_Shown;
        }
        void SaveDialog_Shown(object sender, EventArgs e)
        {
            protoListBox1.Items.Clear();
            FuzzyData.FuzzyObject obj;
            Contract.TaintState state;
            foreach(string key in Help.Data.Instance.Names)
            {
                obj = Help.Data.Instance[key];
                state = Help.Taint.Instance[obj];
                if (state!= Contract.TaintState.UnTainted && state != Contract.TaintState.Saved)
                {
                    protoListBox1.Items.Add(key);
                }
            }
        }

        private void SaveDialog_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.Alt)
            {
                e.IsInputKey = true;
                if (protoListBox1.SelectedIndex > 0)
                {
                    btDiscard.Text = "丢弃当前";
                    btSave.Text = "保存当前";
                    isSingle = true;
                }
            }
        }

        private void SaveDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Menu) > 0)
            {
                btSave.Text = "保存";
                btDiscard.Text = "丢弃";
                isSingle = false;
            }
        }
        bool isSingle = false;
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (isSingle)
            {
                string name = protoListBox1.SelectedItem.ToString();
                Help.Taint.Instance.Save(Help.Data.Instance[name]);
                Help.Data.Instance.Save(name);
                protoListBox1.Items.RemoveAt(protoListBox1.SelectedIndex);
                if (protoListBox1.Items.Count == 0) this.Close();
            }
            else
            {
                Help.Data.Instance.Save();
                Help.Taint.Instance.Save();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btDiscard_Click(object sender, EventArgs e)
        {
            if (isSingle)
            {
                Help.Data.Instance.Discard(protoListBox1.SelectedItem.ToString());
                protoListBox1.Items.RemoveAt(protoListBox1.SelectedIndex);
                if (protoListBox1.Items.Count == 0) this.Close();
            }
            else
            {
                Help.Data.Instance.Discard();
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Close();
            }
        }

        private void SaveDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                if (protoListBox1.SelectedIndex >= 0)
                {
                    btDiscard.Text = "丢弃当前";
                    btSave.Text = "保存当前";
                    isSingle = true;
                }
            }
        }

    }
}
