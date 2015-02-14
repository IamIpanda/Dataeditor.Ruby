namespace DataEditor.Control.Window
{
    partial class EventShopItemWindow
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
            this.mainLv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // mainLv
            // 
            this.mainLv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.mainLv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLv.FullRowSelect = true;
            this.mainLv.Location = new System.Drawing.Point(0, 0);
            this.mainLv.Name = "mainLv";
            this.mainLv.Size = new System.Drawing.Size(265, 391);
            this.mainLv.TabIndex = 0;
            this.mainLv.UseCompatibleStateImageBehavior = false;
            this.mainLv.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "商品";
            this.columnHeader1.Width = 139;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "价格";
            this.columnHeader2.Width = 76;
            // 
            // EventShopItemWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 431);
            this.Name = "EventShopItemWindow";
            this.Text = "商店的处理";
            this.ResumeLayout(false);

        }
        System.Windows.Forms.ListView mainLv;

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}