namespace DataEditor.Control.Window
{
    partial class CommandChooseWindow
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
            this.lb = new DataEditor.Control.Window.CommandChooseWindow.CommandTypeWindow();
            this.cbMain = new DataEditor.Control.Window.CommandChooseWindow.GroupTypeWindow();
            this.btCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbMain, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 461);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lb
            // 
            this.lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb.DisappearRectLosingFocus = true;
            this.lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lb.Location = new System.Drawing.Point(3, 27);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(282, 431);
            this.lb.TabIndex = 0;
            this.lb.DoubleClick += new System.EventHandler(this.lb_DoubleClick);
            this.lb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lb_KeyDown);
            // 
            // cbMain
            // 
            this.cbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMain.FormattingEnabled = true;
            this.cbMain.ItemHeight = 12;
            this.cbMain.Location = new System.Drawing.Point(3, 3);
            this.cbMain.Name = "cbMain";
            this.cbMain.Size = new System.Drawing.Size(282, 18);
            this.cbMain.TabIndex = 1;
            this.cbMain.DropDown += new System.EventHandler(this.cbMain_DropDown);
            this.cbMain.SelectedIndexChanged += new System.EventHandler(this.cbMain_SelectedIndexChanged);
            this.cbMain.DropDownClosed += new System.EventHandler(this.cbMain_DropDownClosed);
            this.cbMain.TextChanged += new System.EventHandler(this.cbMain_TextChanged);
            this.cbMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbMain_KeyDown);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(240, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(0, 0);
            this.btCancel.TabIndex = 3;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // CommandChooseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(288, 461);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommandChooseWindow";
            this.Text = "选择指令";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CommandChooseWindow_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CommandTypeWindow lb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private GroupTypeWindow cbMain;
        private System.Windows.Forms.Button btCancel;
    }
}