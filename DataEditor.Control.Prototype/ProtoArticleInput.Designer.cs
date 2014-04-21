namespace DataEditor.Control.Prototype
{
    partial class ProtoArticleInput
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
            this.btCancel = new System.Windows.Forms.Button();
            this.protoLinedTextbox1 = new DataEditor.Control.Prototype.ProtoLinedTextbox();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(350, 216);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(85, 25);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // protoLinedTextbox1
            // 
            this.protoLinedTextbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoLinedTextbox1.Location = new System.Drawing.Point(0, 0);
            this.protoLinedTextbox1.Name = "protoLinedTextbox1";
            this.protoLinedTextbox1.Size = new System.Drawing.Size(435, 242);
            this.protoLinedTextbox1.TabIndex = 2;
            // 
            // ProtoArticleInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(435, 242);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.protoLinedTextbox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProtoArticleInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "输入文章";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private ProtoLinedTextbox protoLinedTextbox1;
    }
}