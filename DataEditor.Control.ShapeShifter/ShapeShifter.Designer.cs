namespace DataEditor.Control.ShapeShifter
{
    partial class ShapeShifter
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.protoShapeShifterData1 = new DataEditor.Control.Prototype.ProtoShapeShifterData();
            this.protoShapeShifterValue1 = new DataEditor.Control.Prototype.ProtoShapeShifterValue();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.protoShapeShifterData1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.protoShapeShifterValue1);
            this.splitContainer1.Size = new System.Drawing.Size(850, 457);
            this.splitContainer1.SplitterDistance = 283;
            this.splitContainer1.TabIndex = 0;
            // 
            // protoShapeShifterData1
            // 
            this.protoShapeShifterData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoShapeShifterData1.Location = new System.Drawing.Point(0, 0);
            this.protoShapeShifterData1.Name = "protoShapeShifterData1";
            this.protoShapeShifterData1.Size = new System.Drawing.Size(283, 457);
            this.protoShapeShifterData1.TabIndex = 0;
            this.protoShapeShifterData1.ValueChanged += new System.EventHandler(this.protoShapeShifterData1_ValueChanged);
            // 
            // protoShapeShifterValue1
            // 
            this.protoShapeShifterValue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoShapeShifterValue1.FullRowSelect = true;
            this.protoShapeShifterValue1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.protoShapeShifterValue1.Location = new System.Drawing.Point(0, 0);
            this.protoShapeShifterValue1.MultiSelect = false;
            this.protoShapeShifterValue1.Name = "protoShapeShifterValue1";
            this.protoShapeShifterValue1.OwnerDraw = true;
            this.protoShapeShifterValue1.Size = new System.Drawing.Size(563, 457);
            this.protoShapeShifterValue1.TabIndex = 0;
            this.protoShapeShifterValue1.UseCompatibleStateImageBehavior = false;
            this.protoShapeShifterValue1.Value = null;
            this.protoShapeShifterValue1.View = System.Windows.Forms.View.Details;
            // 
            // ShapeShifter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 457);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ShapeShifter";
            this.Text = "通用编辑器";
            this.Load += new System.EventHandler(this.ShapeShifter_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Prototype.ProtoShapeShifterData protoShapeShifterData1;
        private Prototype.ProtoShapeShifterValue protoShapeShifterValue1;
    }
}