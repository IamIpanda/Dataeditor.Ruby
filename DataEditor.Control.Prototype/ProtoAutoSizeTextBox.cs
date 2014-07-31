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
            this.DoubleClick += ProtoAutoSizeTextBox_DoubleClick;
            this.ForeColorChanged += ProtoAutoSizeTextBox_ForeColorChanged;
            this.BackColorChanged += ProtoAutoSizeTextBox_BackColorChanged;
        }

        void ProtoAutoSizeTextBox_BackColorChanged(object sender, EventArgs e)
        {
            if (window != null && !window.IsDisposed) window.BackColor = this.BackColor;
        }

        void ProtoAutoSizeTextBox_ForeColorChanged(object sender, EventArgs e)
        {
            if (window != null && !window.IsDisposed) window.ForeColor = this.ForeColor;
        }

        void ProtoAutoSizeTextBox_DoubleClick(object sender, EventArgs e)
        {
            ProtoLinedPaperDialog dialog = new ProtoLinedPaperDialog();
            dialog.Value = this.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.Text = dialog.Value;
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
        ProtoAutoSizeTextbox_PopupWindow window = null;
        protected void CheckSize()
        {
            System.Drawing.Size size = this.PreferredSize;
            int width = origin_width;
            int height = origin_height;
            if (size.Width > width) width = size.Width;
            if (size.Height > height) height = size.Height;
            if (Help.Environment.Instance.EnableFloatWindow && OverSize(size))
            {
                window = new ProtoAutoSizeTextbox_PopupWindow();
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
            this.window = null;
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
