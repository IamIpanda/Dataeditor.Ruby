namespace DataEditor.Control.Wrapper.Event
{
    partial class SwitchChoser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.protoListBox1 = new DataEditor.Control.Prototype.ProtoListBox();
            this.protoLinedPaper1 = new DataEditor.Control.Prototype.ProtoLinedPaper();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.protoListBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.protoLinedPaper1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btOK, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btCancel, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 391);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // protoListBox1
            // 
            this.protoListBox1.DisappearRectLosingFocus = true;
            this.protoListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox1.FormattingEnabled = true;
            this.protoListBox1.Location = new System.Drawing.Point(3, 3);
            this.protoListBox1.Name = "protoListBox1";
            this.protoListBox1.Size = new System.Drawing.Size(122, 356);
            this.protoListBox1.TabIndex = 0;
            // 
            // protoLinedPaper1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.protoLinedPaper1, 2);
            this.protoLinedPaper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoLinedPaper1.Location = new System.Drawing.Point(131, 3);
            this.protoLinedPaper1.Name = "protoLinedPaper1";
            this.protoLinedPaper1.Size = new System.Drawing.Size(154, 356);
            this.protoLinedPaper1.TabIndex = 1;
            this.protoLinedPaper1.textbook = null;
            this.protoLinedPaper1.Value = null;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(131, 365);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(74, 23);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(211, 365);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(74, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // SwitchChoser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 391);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SwitchChoser";
            this.Text = "SwitchChoser";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Prototype.ProtoListBox protoListBox1;
        private Prototype.ProtoLinedPaper protoLinedPaper1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
    }
}