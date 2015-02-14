namespace DataEditor.Control.Event.Wrapper
{
    partial class ConsideringEventSwitch_Choser
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
            this.plp = new DataEditor.Control.Prototype.ProtoLinedPaper();
            this.SuspendLayout();
            // 
            // plp
            // 
            this.plp.Dock = System.Windows.Forms.DockStyle.Top;
            this.plp.Location = new System.Drawing.Point(0, 0);
            this.plp.Name = "plp";
            this.plp.SelectedIndex = -1;
            this.plp.Size = new System.Drawing.Size(319, 489);
            this.plp.TabIndex = 1;
            this.plp.textbook = null;
            this.plp.Value = null;
            // 
            // EventSwitch_Choser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 520);
            this.Controls.Add(this.plp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventSwitch_Choser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private Prototype.ProtoLinedPaper plp;
    }
}