using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoTroopBitmap : UserControl
    {
        protected Bitmap backGround, frontGround;
        public Bitmap Background { get { return backGround; }
            set { backGround = value; if (value != null) { zoom(); this.Invalidate(); } }
        }
        public Bitmap Frontground { get { return frontGround; } set { frontGround = value; this.Invalidate(); } }
        public List<Bitmap> Components { get; set; }
        public List<Point> Coodinates { get; set; }
        public ProtoTroopBitmap()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.Components = new List<Bitmap>();
            this.Coodinates = new List<Point>();
            this.Selected = new Pen(Color.Red, 1.5F);
            this.KeyUp += ProtoTroopBitmap_KeyUp;
        }
        public Pen Selected { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (backGround == null) return;
            var graphics = e.Graphics;
            graphics.DrawImage(backGround, e.ClipRectangle);
            
            if (frontGround != null) graphics.DrawImage(frontGround, e.ClipRectangle);
            float x, y,w,h;
            for (int i = 0; i < Components.Count; i++)
            {
                var point = Coodinates[i];
                var bit = Components[i];
                x = (point.X - bit.Width / 2) / zoom_x;
                y = (point.Y - bit.Height) / zoom_y;
                w = bit.Width / zoom_x;
                h = bit.Height / zoom_y;
                graphics.DrawImage(bit, new RectangleF(x, y, w, h));
                if (i == selectedIndex)
                    graphics.DrawRectangle(Selected, x, y, w, h);
            } 
        }
        float zoom_x = 1F, zoom_y = 1F;
        void zoom()
        {
            zoom_x = (Background.Width + 0F) / this.ClientSize.Width;
            zoom_y = (Background.Height + 0F) / this.ClientSize.Height;
        }
        int selectedIndex = -1;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectedBitmapMoved;
        public int SelectedIndex 
        {
            get { return selectedIndex; } 
            set 
            {
                selectedIndex = value;
                if (SelectedIndexChanged != null) SelectedIndexChanged(this, new EventArgs());
            }
        }
        Point? pos = null, origin = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            float x, y, w, h;
            List<int> index = new List<int>();
            for (int i = 0; i < Components.Count; i++)
            {
                var point = Coodinates[i];
                var bit = Components[i];
                x = (point.X - bit.Width / 2) / zoom_x;
                y = (point.Y - bit.Height) / zoom_y;
                w = bit.Width / zoom_x;
                h = bit.Height / zoom_y;
                if (e.X > x && e.X < x + w && e.Y > y && e.Y < y + h)
                    index.Add(i);
            }
            if (index.Count == 0)
                return;
            else if (index.Contains(selectedIndex))
            {
                int now = index.IndexOf(selectedIndex);
                now++;
                if (now == index.Count) now = 0;
                selectedIndex = index[now];
            }
            else selectedIndex = index[0];
            pos = new Point(e.X, e.Y);
            origin = Coodinates[selectedIndex];
            Invalidate();
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, new EventArgs());
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            pos = null;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (pos == null || selectedIndex < 0) return;
            int dx = (int)((e.X - pos.Value.X) * zoom_x);
            int dy = (int)((e.Y - pos.Value.Y * zoom_y));
            Coodinates[selectedIndex] = new Point(origin.Value.X + dx, origin.Value.Y + dy);
            // Help.Log.log("Move - " + dx.ToString() + " , " + dy.ToString() + "  & Now " + Coodinates[selectedIndex].ToString());
            if (SelectedBitmapMoved != null)
                SelectedBitmapMoved(this, new EventArgs());
            Invalidate();
        }
        void ProtoTroopBitmap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            { }
        }
    }
}
