namespace DataEditor.Control.Event
{
    partial class Main
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbMap = new DataEditor.Control.Prototype.ProtoListBox();
            this.lbEvent = new DataEditor.Control.Prototype.ProtoListBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbPage = new DataEditor.Control.Prototype.ProtoListBox();
            this.peclMain = new DataEditor.Control.Prototype.ProtoEventCommandList();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(824, 529);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbMap);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbEvent);
            this.splitContainer2.Size = new System.Drawing.Size(292, 529);
            this.splitContainer2.SplitterDistance = 131;
            this.splitContainer2.TabIndex = 0;
            // 
            // lbMap
            // 
            this.lbMap.DisappearRectLosingFocus = false;
            this.lbMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbMap.FormattingEnabled = true;
            this.lbMap.ItemHeight = 12;
            this.lbMap.Location = new System.Drawing.Point(0, 0);
            this.lbMap.Name = "lbMap";
            this.lbMap.Size = new System.Drawing.Size(131, 529);
            this.lbMap.TabIndex = 0;
            this.lbMap.SelectedIndexChanged += new System.EventHandler(this.lbMap_SelectedIndexChanged);
            // 
            // lbEvent
            // 
            this.lbEvent.DisappearRectLosingFocus = false;
            this.lbEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbEvent.FormattingEnabled = true;
            this.lbEvent.ItemHeight = 12;
            this.lbEvent.Location = new System.Drawing.Point(0, 0);
            this.lbEvent.Name = "lbEvent";
            this.lbEvent.Size = new System.Drawing.Size(157, 529);
            this.lbEvent.TabIndex = 0;
            this.lbEvent.SelectedIndexChanged += new System.EventHandler(this.lbEvent_SelectedIndexChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbPage);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.peclMain);
            this.splitContainer3.Size = new System.Drawing.Size(528, 529);
            this.splitContainer3.SplitterDistance = 176;
            this.splitContainer3.TabIndex = 0;
            // 
            // lbPage
            // 
            this.lbPage.DisappearRectLosingFocus = false;
            this.lbPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPage.FormattingEnabled = true;
            this.lbPage.ItemHeight = 12;
            this.lbPage.Location = new System.Drawing.Point(0, 0);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(176, 529);
            this.lbPage.TabIndex = 0;
            this.lbPage.SelectedIndexChanged += new System.EventHandler(this.lbPage_SelectedIndexChanged);
            // 
            // peclMain
            // 
            this.peclMain.DisappearRectLosingFocus = true;
            this.peclMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peclMain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.peclMain.FormattingEnabled = true;
            this.peclMain.Location = new System.Drawing.Point(0, 0);
            this.peclMain.Name = "peclMain";
            this.peclMain.Size = new System.Drawing.Size(348, 529);
            this.peclMain.TabIndex = 0;
            this.peclMain.With = null;
            this.peclMain.Leave += new System.EventHandler(this.peclMain_Leave);
            // 
            // OFD
            // 
            this.OFD.Filter = "RMXP 工程|*.rxproj";
            this.OFD.Title = "打开工程";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 529);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Main";
            this.Text = "Main";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Prototype.ProtoEventCommandList peclMain;
        private DataEditor.Control.Prototype.ProtoListBox lbMap;
        private DataEditor.Control.Prototype.ProtoListBox lbEvent;
        private DataEditor.Control.Prototype.ProtoListBox lbPage;
        private System.Windows.Forms.OpenFileDialog OFD;
    }
}