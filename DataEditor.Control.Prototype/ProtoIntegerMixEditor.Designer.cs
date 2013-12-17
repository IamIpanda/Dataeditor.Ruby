namespace DataEditor.Control.Prototype
{
    partial class ProtoIntegerMixEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtoIntegerMixEditor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.protoComboBox1 = new DataEditor.Control.Prototype.ProtoComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.protoIntegerText1 = new DataEditor.Control.Prototype.ProtoIntegerText();
            this.protoIntegerEditor1 = new DataEditor.Control.Prototype.ProtoIntegerEditor();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.protoIntegerEditor1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 396F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(867, 396);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.protoComboBox1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(436, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(428, 390);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // protoComboBox1
            // 
            this.protoComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoComboBox1.ForeColor = System.Drawing.Color.Silver;
            this.protoComboBox1.FormattingEnabled = true;
            this.protoComboBox1.ItemHeight = 12;
            this.protoComboBox1.Location = new System.Drawing.Point(3, 367);
            this.protoComboBox1.Name = "protoComboBox1";
            this.protoComboBox1.Size = new System.Drawing.Size(422, 18);
            this.protoComboBox1.TabIndex = 2;
            this.protoComboBox1.SelectedIndexChanged += new System.EventHandler(this.protoComboBox1_SelectedIndexChanged);
            this.protoComboBox1.TextChanged += new System.EventHandler(this.protoComboBox1_TextChanged);
            this.protoComboBox1.Enter += new System.EventHandler(this.protoComboBox1_Enter);
            this.protoComboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.protoComboBox1_KeyPress);
            this.protoComboBox1.Leave += new System.EventHandler(this.protoComboBox1_Leave);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.protoIntegerText1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 358);
            this.panel1.TabIndex = 0;
            // 
            // protoIntegerText1
            // 
            this.protoIntegerText1.BoundEditor = this.protoIntegerEditor1;
            this.protoIntegerText1.Dock = System.Windows.Forms.DockStyle.Top;
            this.protoIntegerText1.ItemHeight = 20;
            this.protoIntegerText1.ItemWidth = 60;
            this.protoIntegerText1.Location = new System.Drawing.Point(0, 0);
            this.protoIntegerText1.Name = "protoIntegerText1";
            this.protoIntegerText1.NewBackGroundColor = System.Drawing.Color.Purple;
            this.protoIntegerText1.NewForeColor = System.Drawing.Color.LightGreen;
            this.protoIntegerText1.OldBackGroundColor = System.Drawing.Color.White;
            this.protoIntegerText1.OldForeColor = System.Drawing.SystemColors.ControlText;
            this.protoIntegerText1.QuickMode = false;
            this.protoIntegerText1.RowCount = 4;
            this.protoIntegerText1.Size = new System.Drawing.Size(422, 358);
            this.protoIntegerText1.TabIndex = 0;
            // 
            // protoIntegerEditor1
            // 
            this.protoIntegerEditor1.DataColor = System.Drawing.Color.Gray;
            this.protoIntegerEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoIntegerEditor1.ForeBrush = null;
            this.protoIntegerEditor1.Location = new System.Drawing.Point(3, 3);
            this.protoIntegerEditor1.MaxAdmitValue = 9999;
            this.protoIntegerEditor1.MaxNumber = 3000;
            this.protoIntegerEditor1.MinAdmitValue = 0;
            this.protoIntegerEditor1.Name = "protoIntegerEditor1";
            this.protoIntegerEditor1.Size = new System.Drawing.Size(427, 390);
            this.protoIntegerEditor1.TabIndex = 1;
            this.protoIntegerEditor1.Value = ((System.Collections.Generic.List<int>)(resources.GetObject("protoIntegerEditor1.Value")));
            // 
            // ProtoIntegerMixEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProtoIntegerMixEditor";
            this.Size = new System.Drawing.Size(867, 396);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private ProtoIntegerText protoIntegerText1;
        private ProtoIntegerEditor protoIntegerEditor1;
        private ProtoComboBox protoComboBox1;
    }
}
