namespace DataEditor.Arce
{
    partial class Lead
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
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Text = "欢迎使用 Fux 的节操\r\n您想要做什么？";
            // 
            // protoListBox1
            // 
            this.protoListBox1.Items.AddRange(new object[] {
            "打开工程",
            "打开文件",
            "执行文件",
            "选项",
            "退出"});
            this.protoListBox1.Size = new System.Drawing.Size(158, 160);
            // 
            // OFD
            // 
            this.OFD.FileName = "Game.rxproj";
            // 
            // Lead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 203);
            this.Name = "Lead";
            this.Text = "引导窗口";
            this.Load += new System.EventHandler(this.Lead_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OFD;
    }
}