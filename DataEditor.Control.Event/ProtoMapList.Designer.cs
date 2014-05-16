namespace DataEditor.Control.Prototype
{
    partial class ProtoMapList
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.protoListBox1 = new DataEditor.Control.Prototype.ProtoListBox();
            this.SuspendLayout();
            // 
            // protoListBox1
            // 
            this.protoListBox1.DisappearRectLosingFocus = true;
            this.protoListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox1.FormattingEnabled = true;
            this.protoListBox1.Location = new System.Drawing.Point(0, 0);
            this.protoListBox1.Name = "protoListBox1";
            this.protoListBox1.Size = new System.Drawing.Size(157, 495);
            this.protoListBox1.TabIndex = 1;
            this.protoListBox1.SelectedIndexChanged += new System.EventHandler(this.protoListBox1_SelectedIndexChanged);
            // 
            // ProtoMapList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.protoListBox1);
            this.Name = "ProtoMapList";
            this.Size = new System.Drawing.Size(157, 495);
            this.Load += new System.EventHandler(this.MapList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public ProtoListBox protoListBox1;


    }
}
