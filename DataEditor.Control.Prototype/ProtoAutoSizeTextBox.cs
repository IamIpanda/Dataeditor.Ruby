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
            this.SelectAll();
        }
        protected void CheckSize()
        {
            System.Drawing.Size size = this.PreferredSize;
            int width = origin_width;
            int height = origin_height;
            if (size.Width > width) width = size.Width;
            if (size.Height > height) height = size.Height;
            SizeChanging = true;
            this.Size = new System.Drawing.Size(width, height);
            SizeChanging = false;
        }
        protected void ResetSize()
        {
            this.Size = new System.Drawing.Size(origin_width, origin_height); 
        }
    }

}
