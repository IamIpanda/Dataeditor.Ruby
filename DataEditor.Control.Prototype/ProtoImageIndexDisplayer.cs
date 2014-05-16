using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Control.Prototype;
using System.Windows.Forms;
using System.Drawing;

namespace DataEditor.Control.Prototype
{
    public class ProtoImageIndexDisplayer : ProtoImageBackgroundDisplayer
    {
        public event EventHandler<EventArgs> SelectedIndexChanged;
        Pen WhitePen, BlackPen;
        public Size ClipSize { get; set; }
        public int Index { get; set; }
        
        public ProtoImageIndexDisplayer ()
        {
            this.WhitePen = new Pen(Color.White, 2F);
            this.BlackPen = new Pen(Color.Black, 0.2F);
            this.ClipSize = new System.Drawing.Size(32, 32);
            this.Index = 0;
            InitializeComponent();
            this.SelectedIndexChanged += ProtoImageIndexDisplayer_SelectedIndexChanged;
            this.SetStyle(ControlStyles.Selectable, true);
        }

        void ProtoImageIndexDisplayer_SelectedIndexChanged (object sender, EventArgs e)
        {
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if ( bitmap == null ) return;
            int x_count = bitmap.Width / ClipSize.Width;
            int x = Index % x_count * ClipSize.Width;
            int y = Index / x_count * ClipSize.Height;
            Rectangle ans = new Rectangle(x, y, ClipSize.Width, ClipSize.Height);
            if ( Blocks > 1 ) e.Graphics.DrawRectangle(WhitePen, ans.X + 1, ans.Y + 1, ans.Width - 2.5F, ans.Height - 2.5F);
        }

        private void WrapImageDisplayer_MouseClick(object sender, MouseEventArgs e)
        {
            if (base.bitmap == null) return;
            int x_count = bitmap.Width / ClipSize.Width;
            int y_count = bitmap.Height / ClipSize.Height;
            int x = e.X / ClipSize.Width;
            int y = e.Y / ClipSize.Height;
            if (x >= x_count || y >= y_count) return;
            Index = y * x_count + x;
            OnSelectedIndexChanged();
        }

        public int Blocks
        {
            get 
            {
                if (Bitmap == null) return 0;
                return Bitmap.Width / ClipSize.Width * Bitmap.Height / ClipSize.Height;
            }
        }

        protected void ChangeIndex (int x, int y)
        {
            if (base.bitmap == null) return;
            int x_count = bitmap.Width / ClipSize.Width;
            int index = Index + y * x_count + x;
            while (index < 0) index += Blocks;
            while (index > Blocks) index -= Blocks;
            this.Index = index;
            OnSelectedIndexChanged();
        }

        protected virtual void OnSelectedIndexChanged()
        {
            if ( SelectedIndexChanged != null )
                SelectedIndexChanged(this, new EventArgs());
            Invalidate();
        }


        private void InitializeComponent ()
        {
            this.SuspendLayout();
            // 
            // ProtoImageIndexDisplayer
            // 
            this.Name = "ProtoImageIndexDisplayer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProtoImageIndexDisplayer_KeyDown_1);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WrapImageDisplayer_MouseClick);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ProtoImageIndexDisplayer_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        private void ProtoImageIndexDisplayer_KeyDown_1 (object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Right || e.KeyCode == Keys.D ) ChangeIndex(1, 0);
            else if ( e.KeyCode == Keys.Left || e.KeyCode == Keys.A ) ChangeIndex(-1, 0);
            else if ( e.KeyCode == Keys.Up || e.KeyCode == Keys.W ) ChangeIndex(0, -1);
            else if ( e.KeyCode == Keys.Down || e.KeyCode == Keys.S ) ChangeIndex(0, 1);
        }

        private void ProtoImageIndexDisplayer_PreviewKeyDown (object sender, PreviewKeyDownEventArgs e)
        {
            if ( e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right )
                e.IsInputKey = true;
        }


    }
}
