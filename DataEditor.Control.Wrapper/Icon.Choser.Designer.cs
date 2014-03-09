namespace DataEditor.Control.Wrapper
{
    partial class Icon_Choser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Icon_Choser));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.protoImageIndexDisplayer1 = new DataEditor.Control.Prototype.ProtoImageIndexDisplayer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 552);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 27);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "INDEX :";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(74, 5);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(102, 21);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btOK
            // 
            this.btOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btOK.Location = new System.Drawing.Point(206, 0);
            this.btOK.Margin = new System.Windows.Forms.Padding(6);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(102, 27);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.button2_Click);
            // 
            // btCancel
            // 
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCancel.Location = new System.Drawing.Point(308, 0);
            this.btCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(96, 27);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btOK_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.protoImageIndexDisplayer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(404, 552);
            this.panel2.TabIndex = 1;
            // 
            // protoImageIndexDisplayer1
            // 
            this.protoImageIndexDisplayer1.BackColors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("protoImageIndexDisplayer1.BackColors")));
            this.protoImageIndexDisplayer1.Bitmap = null;
            this.protoImageIndexDisplayer1.BlockHeight = 6;
            this.protoImageIndexDisplayer1.BlockWidth = 12;
            this.protoImageIndexDisplayer1.ClipSize = new System.Drawing.Size(32, 32);
            this.protoImageIndexDisplayer1.FullBackgroundDraw = true;
            this.protoImageIndexDisplayer1.ImageAlignCenter = false;
            this.protoImageIndexDisplayer1.Index = 0;
            this.protoImageIndexDisplayer1.Location = new System.Drawing.Point(0, 0);
            this.protoImageIndexDisplayer1.Name = "protoImageIndexDisplayer1";
            this.protoImageIndexDisplayer1.Scale = false;
            this.protoImageIndexDisplayer1.Size = new System.Drawing.Size(150, 150);
            this.protoImageIndexDisplayer1.SrcRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.protoImageIndexDisplayer1.TabIndex = 0;
            this.protoImageIndexDisplayer1.UseRectangleFocus = false;
            this.protoImageIndexDisplayer1.SelectedIndexChanged += new System.EventHandler<System.EventArgs>(this.protoImageIndexDisplayer1_SelectedIndexChanged);
            // 
            // Icon_Choser
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(404, 579);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Icon_Choser";
            this.Text = " 选取图标";
            this.Shown += new System.EventHandler(this.Icon_Choser_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Panel panel2;
        private Prototype.ProtoImageIndexDisplayer protoImageIndexDisplayer1;
    }
}