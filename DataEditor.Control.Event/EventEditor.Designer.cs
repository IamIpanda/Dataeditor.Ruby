namespace DataEditor.Control.Event
{
    partial class EventEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventEditor));
            this.protoMapList1 = new DataEditor.Control.Prototype.ProtoMapList();
            this.protoEventPaper1 = new DataEditor.Control.Prototype.ProtoEventPaper();
            this.SuspendLayout();
            // 
            // protoMapList1
            // 
            this.protoMapList1.Dock = System.Windows.Forms.DockStyle.Left;
            this.protoMapList1.Links = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("protoMapList1.Links")));
            this.protoMapList1.Location = new System.Drawing.Point(0, 0);
            this.protoMapList1.Name = "protoMapList1";
            this.protoMapList1.Size = new System.Drawing.Size(138, 462);
            this.protoMapList1.TabIndex = 0;
            // 
            // protoEventPaper1
            // 
            this.protoEventPaper1.Dock = System.Windows.Forms.DockStyle.Left;
            this.protoEventPaper1.Links = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("protoEventPaper1.Links")));
            this.protoEventPaper1.Location = new System.Drawing.Point(138, 0);
            this.protoEventPaper1.Map = null;
            this.protoEventPaper1.Name = "protoEventPaper1";
            this.protoEventPaper1.Size = new System.Drawing.Size(166, 462);
            this.protoEventPaper1.TabIndex = 1;
            // 
            // EventEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 462);
            this.Controls.Add(this.protoEventPaper1);
            this.Controls.Add(this.protoMapList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventEditor";
            this.Text = "事件";
            this.ResumeLayout(false);

        }

        #endregion

        private DataEditor.Control.Prototype.ProtoMapList protoMapList1;
        private Prototype.ProtoEventPaper protoEventPaper1;
    }
}