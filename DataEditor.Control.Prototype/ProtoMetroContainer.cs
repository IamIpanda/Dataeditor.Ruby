using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Prototype
{
    public class ProtoMetroContainer : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbText;

        private void InitializeComponent()
        {
            this.lbText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbText.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbText.Location = new System.Drawing.Point(0, 0);
            this.lbText.Margin = new System.Windows.Forms.Padding(3, 0, 3, 7);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(150, 12);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "UNTITLED";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 138);
            this.panel1.TabIndex = 1;
            // 
            // ProtoMetroContainer
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbText);
            this.Name = "ProtoMetroContainer";
            this.BackColorChanged += new System.EventHandler(this.ProtoMetroContainer_BackColorChanged);
            this.ResumeLayout(false);

        }
        public override string Text
        {
            get
            {
                return lbText.Text;
            }
            set
            {
                lbText.Text = value;
                if (lbText.Text == "") lbText.Height = 0; else lbText.Height = 12;
            }
        }

        private void ProtoMetroContainer_BackColorChanged(object sender, EventArgs e)
        {
            panel1.BackColor = BackColor;
        }
        public System.Windows.Forms.Control.ControlCollection PanelCollection { get { return panel1.Controls; } }
        public ProtoMetroContainer()
        {
            InitializeComponent();
        }
    }
}
