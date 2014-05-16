namespace DataEditor.Control.Prototype
{
    partial class ProtoLinedTextbox
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtRow = new System.Windows.Forms.TextBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.txtContent = new DataEditor.Control.Prototype.zzyTextBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtRow
            // 
            this.txtRow.BackColor = System.Drawing.Color.LightGray;
            this.txtRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRow.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtRow.Location = new System.Drawing.Point(0, 0);
            this.txtRow.Margin = new System.Windows.Forms.Padding(0);
            this.txtRow.MaxLength = 65535;
            this.txtRow.Multiline = true;
            this.txtRow.Name = "txtRow";
            this.txtRow.ReadOnly = true;
            this.txtRow.Size = new System.Drawing.Size(26, 255);
            this.txtRow.TabIndex = 8;
            this.txtRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRow.WordWrap = false;
            this.txtRow.SizeChanged += new System.EventHandler(this.txtRow_SizeChanged);
            this.txtRow.TextChanged += new System.EventHandler(this.txtRow_TextChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.LargeChange = 13;
            this.vScrollBar1.Location = new System.Drawing.Point(410, 0);
            this.vScrollBar1.Maximum = 15;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 255);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Visible = false;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // txtContent
            // 
            this.txtContent.BackColor = System.Drawing.Color.White;
            this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(26, 0);
            this.txtContent.Margin = new System.Windows.Forms.Padding(0);
            this.txtContent.MaxLength = 65535;
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(401, 255);
            this.txtContent.TabIndex = 1;
            this.txtContent.WordWrap = false;
            this.txtContent.SizeChanged += new System.EventHandler(this.txtContent_SizeChanged);
            this.txtContent.TextChanged += new System.EventHandler(this.txtContect_TextChanged);
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            this.txtContent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyUp);
            this.txtContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtContent_MouseDown);
            // 
            // ProtoLinedTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtRow);
            this.Name = "ProtoLinedTextbox";
            this.Size = new System.Drawing.Size(427, 255);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtRow;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private zzyTextBox txtContent;
    }
}
