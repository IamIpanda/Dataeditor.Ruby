namespace DataEditor.Control.Window
{
    partial class MoveRouteWindow
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.protoEventCommandList1 = new DataEditor.Control.Window.MoveRouteWindow.MoveCommandList();
            this.listBox1 = new DataEditor.Control.Prototype.ProtoListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.protoEventCommandList1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 591);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // protoEventCommandList1
            // 
            this.protoEventCommandList1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.protoEventCommandList1.DisappearRectLosingFocus = false;
            this.protoEventCommandList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoEventCommandList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoEventCommandList1.FormattingEnabled = true;
            this.protoEventCommandList1.Location = new System.Drawing.Point(3, 3);
            this.protoEventCommandList1.Name = "protoEventCommandList1";
            this.protoEventCommandList1.Size = new System.Drawing.Size(235, 585);
            this.protoEventCommandList1.TabIndex = 0;
            this.protoEventCommandList1.With = null;
            this.protoEventCommandList1.InsertCalled += new System.EventHandler(this.protoEventCommandList1_InsertCalled);
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.DisappearRectLosingFocus = true;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(244, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(235, 585);
            this.listBox1.TabIndex = 1;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // MoveRouteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 631);
            this.Name = "MoveRouteWindow";
            this.Text = "编辑移动路线";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MoveCommandList protoEventCommandList1;
        private Prototype.ProtoListBox listBox1;
    }
}