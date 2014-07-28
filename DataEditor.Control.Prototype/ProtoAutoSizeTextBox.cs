using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Prototype
{
    public class ProtoAutoSizeTextBox : System.Windows.Forms.TextBox
    {
        int origin_width, origin_height;
        bool SizeChanging = false;
        public ProtoAutoSizeTextBox()
        {
            this.Multiline = true;
            this.TextChanged += OnValueChanging;
            this.SizeChanged += OnSizeChanging;
            this.Leave += OnLeaving;
            this.Enter += OnEntering;
        }
        private void OnSizeChanging(object sender,EventArgs e)
        {
            if (!SizeChanging)
            {
                origin_width = this.Width;
                origin_height = this.Height;
            }
        }
        private void OnValueChanging(object sender, EventArgs e)
        {
            if (Focused)
                CheckSize();
        }
        private void OnLeaving(object sender, EventArgs e)
        {
            ResetSize();
        }
        private void OnEntering(object sender, EventArgs e)
        {
            CheckSize();
        }
        protected void CheckSize()
        {
            System.Drawing.Size size = this.PreferredSize;
            int width = origin_width;
            int height = origin_height;
            if (size.Width > width) width = size.Width;
            if (size.Height > height) height = size.Height;
            if (Help.Environment.Instance.EnableFloatWindow && OverSize(size))
            {
                var window = new ProtoAutoSizeTextbox_PopupWindow();
                window.Location = this.PointToScreen(new System.Drawing.Point(0, 0));
                window.FormClosed += window_FormClosed;
                window.Value = this.Text;
                window.MinSize = new System.Drawing.Size(this.origin_width, this.origin_height);
                window.Size = new System.Drawing.Size(width, height);
                window.SetColor(this.ForeColor, this.BackColor);
                window.SetIndex(this.SelectionStart, this.SelectionLength);
                window.Show();
                window.Focus();
                ResetSize();
            }
            else
            {
                SizeChanging = true;
                this.Size = new System.Drawing.Size(width, height);
                SizeChanging = false;
            }
        }

        void window_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            var window  = sender as ProtoAutoSizeTextbox_PopupWindow;
            if (window == null) return;
            SizeChanging = true;
            this.Text = window.Value;
            SizeChanging = false;
            window.Dispose();
        }
        protected void ResetSize()
        {
            this.Size = new System.Drawing.Size(origin_width, origin_height); 
        }
        protected bool OverSize(System.Drawing.Size size)
        {
            return ((size.Width + this.Location.X) > Parent.ClientSize.Width)
                || ((size.Height + this.Location.Y) > Parent.ClientSize.Height);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

}
