namespace DataEditor.Control.Prototype
{
    partial class ProtoEventTone
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
            this.nud_gray = new System.Windows.Forms.NumericUpDown();
            this.nud_blue = new System.Windows.Forms.NumericUpDown();
            this.nud_green = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_red = new System.Windows.Forms.TrackBar();
            this.tb_green = new System.Windows.Forms.TrackBar();
            this.tb_blue = new System.Windows.Forms.TrackBar();
            this.tb_gray = new System.Windows.Forms.TrackBar();
            this.protoColorSpectrum1 = new DataEditor.Control.Prototype.ProtoColorSpectrum();
            this.nud_red = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_gray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_gray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_red)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.nud_gray, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.nud_blue, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.nud_green, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tb_red, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_green, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tb_blue, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_gray, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.protoColorSpectrum1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.nud_red, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(375, 135);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nud_gray
            // 
            this.nud_gray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_gray.Location = new System.Drawing.Point(238, 102);
            this.nud_gray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_gray.Name = "nud_gray";
            this.nud_gray.Size = new System.Drawing.Size(64, 21);
            this.nud_gray.TabIndex = 12;
            this.nud_gray.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // nud_blue
            // 
            this.nud_blue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_blue.Location = new System.Drawing.Point(238, 69);
            this.nud_blue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_blue.Name = "nud_blue";
            this.nud_blue.Size = new System.Drawing.Size(64, 21);
            this.nud_blue.TabIndex = 11;
            this.nud_blue.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // nud_green
            // 
            this.nud_green.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_green.Location = new System.Drawing.Point(238, 36);
            this.nud_green.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_green.Name = "nud_green";
            this.nud_green.Size = new System.Drawing.Size(64, 21);
            this.nud_green.TabIndex = 10;
            this.nud_green.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "红：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 33);
            this.label2.TabIndex = 1;
            this.label2.Text = "绿：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "蓝：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 36);
            this.label4.TabIndex = 3;
            this.label4.Text = "灰：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_red
            // 
            this.tb_red.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_red.Location = new System.Drawing.Point(43, 3);
            this.tb_red.Maximum = 255;
            this.tb_red.Name = "tb_red";
            this.tb_red.Size = new System.Drawing.Size(189, 27);
            this.tb_red.TabIndex = 4;
            this.tb_red.TickFrequency = 17;
            this.tb_red.Scroll += new System.EventHandler(this.tb_scroll);
            // 
            // tb_green
            // 
            this.tb_green.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_green.Location = new System.Drawing.Point(43, 36);
            this.tb_green.Maximum = 255;
            this.tb_green.Name = "tb_green";
            this.tb_green.Size = new System.Drawing.Size(189, 27);
            this.tb_green.TabIndex = 5;
            this.tb_green.TickFrequency = 17;
            this.tb_green.Scroll += new System.EventHandler(this.tb_scroll);
            // 
            // tb_blue
            // 
            this.tb_blue.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_blue.Location = new System.Drawing.Point(43, 69);
            this.tb_blue.Maximum = 255;
            this.tb_blue.Name = "tb_blue";
            this.tb_blue.Size = new System.Drawing.Size(189, 27);
            this.tb_blue.TabIndex = 6;
            this.tb_blue.TickFrequency = 17;
            this.tb_blue.Scroll += new System.EventHandler(this.tb_scroll);
            // 
            // tb_gray
            // 
            this.tb_gray.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_gray.Location = new System.Drawing.Point(43, 102);
            this.tb_gray.Maximum = 255;
            this.tb_gray.Name = "tb_gray";
            this.tb_gray.Size = new System.Drawing.Size(189, 30);
            this.tb_gray.TabIndex = 7;
            this.tb_gray.TickFrequency = 17;
            this.tb_gray.Scroll += new System.EventHandler(this.tb_scroll);
            // 
            // protoColorSpectrum1
            // 
            this.protoColorSpectrum1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoColorSpectrum1.Location = new System.Drawing.Point(306, 1);
            this.protoColorSpectrum1.Margin = new System.Windows.Forms.Padding(1);
            this.protoColorSpectrum1.Name = "protoColorSpectrum1";
            this.tableLayoutPanel1.SetRowSpan(this.protoColorSpectrum1, 4);
            this.protoColorSpectrum1.Size = new System.Drawing.Size(68, 133);
            this.protoColorSpectrum1.TabIndex = 8;
            // 
            // nud_red
            // 
            this.nud_red.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_red.Location = new System.Drawing.Point(238, 3);
            this.nud_red.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_red.Name = "nud_red";
            this.nud_red.Size = new System.Drawing.Size(64, 21);
            this.nud_red.TabIndex = 9;
            this.nud_red.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // ProtoEventTone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProtoEventTone";
            this.Size = new System.Drawing.Size(375, 135);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_gray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_gray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_red)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tb_red;
        private System.Windows.Forms.TrackBar tb_green;
        private System.Windows.Forms.TrackBar tb_blue;
        private System.Windows.Forms.TrackBar tb_gray;
        private ProtoColorSpectrum protoColorSpectrum1;
        private System.Windows.Forms.NumericUpDown nud_gray;
        private System.Windows.Forms.NumericUpDown nud_blue;
        private System.Windows.Forms.NumericUpDown nud_green;
        private System.Windows.Forms.NumericUpDown nud_red;
    }
}
