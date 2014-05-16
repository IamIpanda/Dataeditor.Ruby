namespace DataEditor.Control.Event
{
    partial class EventCommandChoser
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.protoListBox1 = new DataEditor.Control.Prototype.ProtoEventList();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.protoListBox2 = new DataEditor.Control.Prototype.ProtoEventList();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.protoListBox1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.protoListBox2);
            this.splitContainer1.Size = new System.Drawing.Size(400, 552);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 1;
            // 
            // protoListBox1
            // 
            this.protoListBox1.DisappearRectLosingFocus = false;
            this.protoListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox1.FormattingEnabled = true;
            this.protoListBox1.Location = new System.Drawing.Point(0, 0);
            this.protoListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.protoListBox1.Name = "protoListBox1";
            this.protoListBox1.Size = new System.Drawing.Size(132, 531);
            this.protoListBox1.TabIndex = 1;
            this.protoListBox1.SelectedIndexChanged += new System.EventHandler(this.protoListBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 531);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // protoListBox2
            // 
            this.protoListBox2.DisappearRectLosingFocus = true;
            this.protoListBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoListBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox2.FormattingEnabled = true;
            this.protoListBox2.Location = new System.Drawing.Point(0, 0);
            this.protoListBox2.Margin = new System.Windows.Forms.Padding(0);
            this.protoListBox2.Name = "protoListBox2";
            this.protoListBox2.Size = new System.Drawing.Size(264, 552);
            this.protoListBox2.TabIndex = 0;
            this.protoListBox2.SelectedIndexChanged += new System.EventHandler(this.protoListBox2_SelectedIndexChanged);
            this.protoListBox2.DoubleClick += new System.EventHandler(this.protoListBox2_DoubleClick);
            // 
            // EventCommandChoser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 552);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventCommandChoser";
            this.Text = "选取指令";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Prototype.ProtoEventList protoListBox1;
        private System.Windows.Forms.TextBox textBox1;
        private Prototype.ProtoEventList protoListBox2;


    }
}
