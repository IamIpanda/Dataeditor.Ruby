namespace DataEditor.Control.Prototype
{
    partial class ProtoLeftListBox
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.protoTitleBox1 = new DataEditor.Control.Prototype.ProtoTitleBox();
            this.protoListBox1 = new DataEditor.Control.Prototype.ProtoListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.protoTitleBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.protoListBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(157, 481);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // protoTitleBox1
            // 
            this.protoTitleBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoTitleBox1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.protoTitleBox1.Location = new System.Drawing.Point(7, 7);
            this.protoTitleBox1.Margin = new System.Windows.Forms.Padding(7);
            this.protoTitleBox1.Name = "protoTitleBox1";
            this.protoTitleBox1.Size = new System.Drawing.Size(143, 34);
            this.protoTitleBox1.TabIndex = 0;
            // 
            // protoListBox1
            // 
            this.protoListBox1.DisappearRectLosingFocus = false;
            this.protoListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox1.FormattingEnabled = true;
            this.protoListBox1.Location = new System.Drawing.Point(3, 51);
            this.protoListBox1.Name = "protoListBox1";
            this.protoListBox1.Size = new System.Drawing.Size(151, 427);
            this.protoListBox1.TabIndex = 1;
            this.protoListBox1.SelectedIndexChanged += new System.EventHandler(this.protoListBox1_SelectedIndexChanged);
            // 
            // ProtoLeftListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProtoLeftListBox";
            this.Size = new System.Drawing.Size(157, 481);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ProtoTitleBox protoTitleBox1;
        private ProtoListBox protoListBox1;
    }
}
