namespace DataEditor.Control.Window
{
    partial class ChoiceWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.protoListBox1 = new DataEditor.Control.Prototype.ProtoListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 34);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // protoListBox1
            // 
            this.protoListBox1.DisappearRectLosingFocus = false;
            this.protoListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox1.FormattingEnabled = true;
            this.protoListBox1.Location = new System.Drawing.Point(0, 34);
            this.protoListBox1.Name = "protoListBox1";
            this.protoListBox1.Size = new System.Drawing.Size(158, 169);
            this.protoListBox1.TabIndex = 1;
            this.protoListBox1.DoubleClick += new System.EventHandler(this.protoListBox1_DoubleClick);
            this.protoListBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.protoListBox1_KeyUp);
            // 
            // ChoiceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 203);
            this.Controls.Add(this.protoListBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChoiceWindow";
            this.Load += new System.EventHandler(this.ChoiceWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label label1;
        protected Prototype.ProtoListBox protoListBox1;

    }
}