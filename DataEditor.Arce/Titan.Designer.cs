namespace DataEditor.Arce
{
    partial class Titan
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose (bool disposing)
        {
            if ( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent ()
        {
            this.SuspendLayout();
            // 
            // pnMain
            // 
            this.pnMain.Size = new System.Drawing.Size(1134, 681);
            this.pnMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnMain_Paint);
            // 
            // Titan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 727);
            this.Name = "Titan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Titan_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Titan_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion







    }
}

