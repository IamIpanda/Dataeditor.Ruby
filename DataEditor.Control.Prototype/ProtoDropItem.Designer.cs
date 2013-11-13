namespace DataEditor.Control.Prototype
{
    partial class ProtoDropItem
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent ()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.protoSelectable1 = new DataEditor.Control.Prototype.ProtoSelectable();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(614, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 215);
            this.button1.TabIndex = 4;
            this.button1.Text = "∷";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // protoSelectable1
            // 
            this.protoSelectable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoSelectable1.Location = new System.Drawing.Point(0, 0);
            this.protoSelectable1.Margin = new System.Windows.Forms.Padding(6);
            this.protoSelectable1.Name = "protoSelectable1";
            this.protoSelectable1.Size = new System.Drawing.Size(614, 215);
            this.protoSelectable1.TabIndex = 6;
            this.protoSelectable1.DoubleClick += new System.EventHandler(this.protoSelectable1_DoubleClick);
            // 
            // ProtoDropItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.protoSelectable1);
            this.Controls.Add(this.button1);
            this.Name = "ProtoDropItem";
            this.Size = new System.Drawing.Size(681, 215);
            this.Resize += new System.EventHandler(this.ProtoDropItem_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private ProtoSelectable protoSelectable1;

    }
}
