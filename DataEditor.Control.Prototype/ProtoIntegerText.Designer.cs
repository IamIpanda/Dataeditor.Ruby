namespace DataEditor.Control.Prototype
{
    partial class ProtoIntegerText
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
            this.MainNumeric = new System.Windows.Forms.NumericUpDown();
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainNumeric
            // 
            this.MainNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainNumeric.Location = new System.Drawing.Point(102, 272);
            this.MainNumeric.Name = "MainNumeric";
            this.MainNumeric.Size = new System.Drawing.Size(72, 21);
            this.MainNumeric.TabIndex = 3;
            this.MainNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MainNumeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.MainNumeric.Visible = false;
            this.MainNumeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainNumeric_KeyUp);
            this.MainNumeric.Leave += new System.EventHandler(this.MainNumeric_Leave);
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.BackColor = System.Drawing.Color.White;
            this.MainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(283, 260);
            this.MainPictureBox.TabIndex = 2;
            this.MainPictureBox.TabStop = false;
            this.MainPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainPictureBox_MouseClick);
            // 
            // ProtoIntegerText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.MainNumeric);
            base.Controls.Add(this.MainPictureBox);
            this.Name = "ProtoIntegerText";
            this.Size = new System.Drawing.Size(283, 303);
            ((System.ComponentModel.ISupportInitialize)(this.MainNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown MainNumeric;
        private System.Windows.Forms.PictureBox MainPictureBox;
    }
}
