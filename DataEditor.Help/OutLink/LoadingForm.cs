using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            //this.Shown += LoadingForm_Shown;
        }
        static LoadingForm()
        {
            LoadingForm.CheckForIllegalCrossThreadCalls = false;
        }
        /*

        void LoadingForm_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }*/
        public new string Text 
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        /*
        public System.Threading.ThreadStart Work = null;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Work != null)
                Work();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }*/
    }
}
