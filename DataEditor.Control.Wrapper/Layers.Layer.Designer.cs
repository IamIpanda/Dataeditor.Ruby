namespace DataEditor.Control.Wrapper
{
    partial class Layers_layer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Layers_layer));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.protoTitleBox1 = new DataEditor.Control.Prototype.ProtoTitleBox();
            this.protoRtpViewList1 = new DataEditor.Control.Prototype.ProtoRtpViewList();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.protoTitleBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.protoRtpViewList1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(170, 373);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // protoTitleBox1
            // 
            this.protoTitleBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoTitleBox1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.protoTitleBox1.Location = new System.Drawing.Point(7, 7);
            this.protoTitleBox1.Margin = new System.Windows.Forms.Padding(7);
            this.protoTitleBox1.Name = "protoTitleBox1";
            this.protoTitleBox1.Size = new System.Drawing.Size(156, 30);
            this.protoTitleBox1.TabIndex = 1;
            // 
            // protoRtpViewList1
            // 
            this.protoRtpViewList1.DisappearRectLosingFocus = true;
            this.protoRtpViewList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoRtpViewList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoRtpViewList1.Extands = ((System.Collections.Generic.List<string>)(resources.GetObject("protoRtpViewList1.Extands")));
            this.protoRtpViewList1.FormattingEnabled = true;
            this.protoRtpViewList1.Location = new System.Drawing.Point(3, 47);
            this.protoRtpViewList1.Name = "protoRtpViewList1";
            this.protoRtpViewList1.Size = new System.Drawing.Size(164, 323);
            this.protoRtpViewList1.TabIndex = 2;
            // 
            // Layers_layer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Layers_layer";
            this.Size = new System.Drawing.Size(170, 373);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Prototype.ProtoTitleBox protoTitleBox1;
        private Prototype.ProtoRtpViewList protoRtpViewList1;
    }
}
