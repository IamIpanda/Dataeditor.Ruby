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
            this.protoAutoSizeTextBox1 = new DataEditor.Control.Prototype.ProtoAutoSizeTextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // protoAutoSizeTextBox1
            // 
            this.protoAutoSizeTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoAutoSizeTextBox1.Location = new System.Drawing.Point(0, 0);
            this.protoAutoSizeTextBox1.Multiline = true;
            this.protoAutoSizeTextBox1.Name = "protoAutoSizeTextBox1";
            this.protoAutoSizeTextBox1.Size = new System.Drawing.Size(435, 242);
            this.protoAutoSizeTextBox1.TabIndex = 0;
            this.protoAutoSizeTextBox1.Resize += new System.EventHandler(this.protoAutoSizeTextBox1_Resize);
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
            this.btCancel.Resize += new System.EventHandler(this.btCancel_Resize);
            // 
            // ProtoArticleInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(435, 242);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.protoAutoSizeTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProtoArticleInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "输入文章";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProtoAutoSizeTextBox protoAutoSizeTextBox1;
        private System.Windows.Forms.Button btCancel;
    }
}