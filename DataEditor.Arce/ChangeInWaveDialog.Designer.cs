namespace DataEditor.Arce
{
    partial class ChangeInWaveDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.protoShapeShifterFullList1 = new DataEditor.Control.Prototype.ProtoShapeShifterFullList();
            this.protoAutoSizeTextBox1 = new DataEditor.Control.Prototype.ProtoAutoSizeTextBox();
            this.protoListBox1 = new DataEditor.Control.Prototype.ProtoListBox();
            this.protoListBox2 = new DataEditor.Control.Prototype.ProtoListBox();
            this.protoListBox3 = new DataEditor.Control.Prototype.ProtoListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "更改区域";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(404, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(143, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "使用内置计算器来算值";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.protoListBox2);
            this.groupBox1.Controls.Add(this.protoListBox1);
            this.groupBox1.Controls.Add(this.protoAutoSizeTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(404, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 187);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计算目标";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(404, 227);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(143, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "使用 Ruby 脚本来算值";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(404, 261);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(292, 140);
            this.textBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "提示：请给出一个关于obj的函数返回值";
            // 
            // protoShapeShifterFullList1
            // 
            this.protoShapeShifterFullList1.Location = new System.Drawing.Point(12, 24);
            this.protoShapeShifterFullList1.Name = "protoShapeShifterFullList1";
            this.protoShapeShifterFullList1.Size = new System.Drawing.Size(184, 377);
            this.protoShapeShifterFullList1.TabIndex = 0;
            // 
            // protoAutoSizeTextBox1
            // 
            this.protoAutoSizeTextBox1.Location = new System.Drawing.Point(6, 20);
            this.protoAutoSizeTextBox1.Multiline = true;
            this.protoAutoSizeTextBox1.Name = "protoAutoSizeTextBox1";
            this.protoAutoSizeTextBox1.Size = new System.Drawing.Size(280, 21);
            this.protoAutoSizeTextBox1.TabIndex = 0;
            // 
            // protoListBox1
            // 
            this.protoListBox1.DisappearRectLosingFocus = true;
            this.protoListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox1.FormattingEnabled = true;
            this.protoListBox1.Location = new System.Drawing.Point(6, 47);
            this.protoListBox1.Name = "protoListBox1";
            this.protoListBox1.Size = new System.Drawing.Size(137, 134);
            this.protoListBox1.TabIndex = 1;
            // 
            // protoListBox2
            // 
            this.protoListBox2.DisappearRectLosingFocus = true;
            this.protoListBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox2.FormattingEnabled = true;
            this.protoListBox2.Location = new System.Drawing.Point(152, 47);
            this.protoListBox2.Name = "protoListBox2";
            this.protoListBox2.Size = new System.Drawing.Size(134, 134);
            this.protoListBox2.TabIndex = 2;
            // 
            // protoListBox3
            // 
            this.protoListBox3.DisappearRectLosingFocus = true;
            this.protoListBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoListBox3.FormattingEnabled = true;
            this.protoListBox3.Location = new System.Drawing.Point(202, 24);
            this.protoListBox3.Name = "protoListBox3";
            this.protoListBox3.Size = new System.Drawing.Size(196, 381);
            this.protoListBox3.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "更改目标";
            // 
            // ChangeInWaveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 413);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.protoListBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.protoShapeShifterFullList1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeInWaveDialog";
            this.ShowIcon = false;
            this.Text = "批量更改";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Control.Prototype.ProtoShapeShifterFullList protoShapeShifterFullList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private Control.Prototype.ProtoAutoSizeTextBox protoAutoSizeTextBox1;
        private Control.Prototype.ProtoListBox protoListBox2;
        private Control.Prototype.ProtoListBox protoListBox1;
        private Control.Prototype.ProtoListBox protoListBox3;
        private System.Windows.Forms.Label label3;
    }
}