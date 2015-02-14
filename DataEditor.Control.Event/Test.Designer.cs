namespace DataEditor.Control.Event
{
    partial class Test
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
            this.protoEventCommandList1 = new DataEditor.Control.Prototype.ProtoEventCommandList();
            this.SuspendLayout();
            // 
            // protoEventCommandList1
            // 
            this.protoEventCommandList1.DisappearRectLosingFocus = false;
            this.protoEventCommandList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoEventCommandList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.protoEventCommandList1.FormattingEnabled = true;
            this.protoEventCommandList1.Location = new System.Drawing.Point(0, 0);
            this.protoEventCommandList1.Name = "protoEventCommandList1";
            this.protoEventCommandList1.Size = new System.Drawing.Size(303, 481);
            this.protoEventCommandList1.TabIndex = 0;
            this.protoEventCommandList1.With = null;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 481);
            this.Controls.Add(this.protoEventCommandList1);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private Prototype.ProtoEventCommandList protoEventCommandList1;
    }
}