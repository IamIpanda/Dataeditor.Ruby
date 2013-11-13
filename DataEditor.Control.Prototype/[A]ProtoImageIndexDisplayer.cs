using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Control.Prototype;
using System.Windows.Forms;
using System.Drawing;

namespace DataEditor.Control.Prototype
{
    // TODO : Rebuild it
    public class ProtoImageIndexDisplayer : ProtoImageBackgroundDisplayer
    {
        public event EventHandler<EventArgs> SelectedIndexChanged;
        Pen WhitePen, BlackPen;
        [System.ComponentModel.Browsable(false)]
        public int Index {
            get
            {
                if ( Rect == null ) return 0;
                return Rect[Rect.X, Rect.Y];
            }
            set { if ( Rect == null ) return; Rect.SetIndex(value); OnSelectedIndexChanged(); }
        }
        public Adapter.AdvanceImage.AdvanceImageRect Rect { get; set; }
        public Adapter.AdvanceImage Value { get; set; }

        public ProtoImageIndexDisplayer ()
        {
            this.WhitePen = new Pen(Color.White, 2F);
            this.BlackPen = new Pen(Color.Black, 0.2F);
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
            if ( bitmap == null || Rect == null ) return;
            Rectangle r = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle ans = Rect.Split(r);
            if ( Blocks > 1 ) e.Graphics.DrawRectangle(WhitePen, ans.X + 1, ans.Y + 1, ans.Width - 2.5F, ans.Height - 2.5F);
        }

        private void WrapImageDisplayer_MouseClick(object sender, MouseEventArgs e)
        {
            if (base.bitmap == null) return;
            if ( Rect == null ) return;
            Rect.SetIndex(e.X, e.Y);
            this.Index = Rect[Rect.X, Rect.Y];
            OnSelectedIndexChanged();
        }

        public int Blocks
        {
            get 
            {
                if (base.bitmap == null) return 0;
                if ( Rect == null ) return 0;
                return Rect.Blocks;
            }
        }

        private void ProtoImageIndexDisplayer_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("SHIT");
        }

        protected void ChangeIndex (int x, int y)
        {
            if ( Rect == null ) return;
            int index = this.Index + Rect[x, y];
            if ( index < 0 ) while ( index < 0 ) index += Rect.Blocks;
            index %= Rect.Blocks;
            this.Index = index;
            OnSelectedIndexChanged();
        }

        protected virtual void OnSelectedIndexChanged()
        {
            if ( SelectedIndexChanged != null )
                SelectedIndexChanged(this, new EventArgs());
        }


        private void InitializeComponent ()
        {
            this.SuspendLayout();
            // 
            // ProtoImageIndexDisplayer
            // 
            this.Name = "ProtoImageIndexDisplayer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProtoImageIndexDisplayer_KeyDown_1);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProtoImageIndexDisplayer_KeyDown);
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
