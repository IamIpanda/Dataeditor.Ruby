using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoImageDisplayer : UserControl, ProtoImageControl, ProtoFrameControl
    {
        protected Bitmap bitmap = null;
        protected Rectangle src_rect = new Rectangle(0, 0, 0, 0);

        public new bool Scale { get; set; }
        public bool ImageAlignCenter { get; set; }
        public bool UseRectangleFocus { get; set; }
        
        public Bitmap Bitmap 
        {
            get { return bitmap; }
            set { bitmap = value; Invalidate(); }
        }
        
        public Rectangle SrcRect
        {
            get { return src_rect; }
            set { src_rect = value; Invalidate(); }
        }

        public ProtoImageDisplayer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Scale = true;
            this.ImageAlignCenter = false;
            this.UseRectangleFocus = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProtoImageDisplayer
            // 
            this.Name = "ProtoImageDisplayer";
            this.Enter += new System.EventHandler(this.ProtoImageDisplayer_Enter);
            this.Leave += new System.EventHandler(this.ProtoImageDisplayer_Leave);
            this.ResumeLayout(false);

        }

        private void ProtoImageDisplayer_Enter(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ProtoImageDisplayer_Leave(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawBackGround(e.Graphics);
            ProtoFrameControlHelp.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black);
            if (Bitmap == null)
                return;
            Rectangle rect = SrcRect;
            if (SrcRect.Width == 0 && SrcRect.Height == 0)
                rect = new Rectangle(new Point(0, 0), bitmap.Size);
            int x = 0, y = 0;
            if (ImageAlignCenter)
            {
                x = (int)(e.Graphics.ClipBounds.Width - rect.Width) / 2;
                y = (int)(e.Graphics.ClipBounds.Height - rect.Height) / 2;
            }
            else 
            {
                x = ((bitmap.Width) - bitmap.Width) / 2;
                y = ((bitmap.Height) - bitmap.Height) / 2;
            }
            if ( Scale )
                e.Graphics.DrawImage(bitmap, e.ClipRectangle, rect, GraphicsUnit.Pixel);
            else
                e.Graphics.DrawImage(bitmap, new Rectangle(x, y, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            if (this.UseRectangleFocus && this.Focused)
                ProtoFrameControlHelp.DrawFocusRectangle(e.Graphics, e.ClipRectangle);
        }

        protected virtual void DrawBackGround(Graphics graphics)
        {
            graphics.Clear(Color.White);
        }
    }
    public class ProtoImageBackgroundDisplayer : ProtoImageDisplayer
    {
        static protected List<Color> DefaultBackColors = new List<Color>()
        {
            Color.FromArgb(0, 0, 128),
            Color.FromArgb(0, 0, 65)
        };

        int block_width = 12, block_height = 6;
        public int BlockWidth { get { return block_width; } set { block_width = value; Invalidate(); } }
        public int BlockHeight { get { return block_height; } set { block_height = value; Invalidate(); } }
        public bool FullBackgroundDraw { get; set; }
        public List<Color> BackColors { get; set; }
        
        public ProtoImageBackgroundDisplayer()
            : base()
        {
            BackColors = new List<Color>();
            BackColors.AddRange(DefaultBackColors);
            FullBackgroundDraw = false;
        }
        static ProtoImageBackgroundDisplayer()
        {
            LoadSettings();
        }
        static public void SaveSettings()
        {
            DataEditor.Help.Option.SetOption(typeof(ProtoImageBackgroundDisplayer), DefaultBackColors);
        }
        static public void LoadSettings()
        {
            try
            {
                DefaultBackColors = DataEditor.Help.Option.GetOption
                    (typeof(ProtoImageBackgroundDisplayer)) as List<Color>;
                if (DefaultBackColors == null)
                    DefaultBackColors = new List<Color>() { DefaultBackColor };
            }
            catch { }
        }
        protected override void DrawBackGround(Graphics graphics)
        {
            graphics.Clear(BackColor);
            if ( bitmap == null ) return;
            int w, h;
            if ( src_rect.Width == 0 && src_rect.Height == 0 )
            { w = bitmap.Width; h = bitmap.Height; }
            else { w = src_rect.Width; h = src_rect.Height; }
            Rectangle rect;
            if ( FullBackgroundDraw ) rect = new Rectangle(0, 0, (int)graphics.ClipBounds.Width, (int)graphics.ClipBounds.Height);
            else if ( ImageAlignCenter )
            {
                int x = ((int)graphics.ClipBounds.Width - w) / 2;
                int y = ((int)graphics.ClipBounds.Height - h) / 2;
                rect = new Rectangle(x, y, w, h);
            }
            else rect = new Rectangle(0, 0, w, h);
            DrawRectangeleBackGround(graphics, rect);
        }
        protected void DrawRectangeleBackGround(Graphics graphics, Rectangle rect)
        {
            if (BackColors.Count == 0)
                graphics.FillRectangle(new SolidBrush(BackColor), graphics.ClipBounds);
            else if (BackColors.Count == 1)
                graphics.FillRectangle(new SolidBrush(BackColors[0]), graphics.ClipBounds);
            else
            {
                int Length = BackColors.Count;
                int X = rect.X;
                int Y = rect.Y;
                int StartX = X, StartY = Y;
                int CountX = 0, CountY = 0;
                int Right = rect.Right;
                int Bottom = rect.Bottom;
                List<Brush> Brushes = new List<Brush>();
                foreach (Color c in BackColors)
                    Brushes.Add(new SolidBrush(c));
                int FinalX = 0, FinalCountX = 0;
                while (Y <= Bottom - BlockHeight)
                {
                    while (X <= Right - BlockWidth)
                    {
                        graphics.FillRectangle(Brushes[(CountX + CountY) % Length],
                            new Rectangle(X, Y, BlockWidth, BlockHeight));
                        X += BlockWidth;
                        CountX++;
                    }
                    FinalX = X;
                    FinalCountX = CountX;
                    X = StartX;
                    Y += BlockHeight;
                    CountY++;
                    CountX = 0;
                }
                int FinalY = Y, DeltaX = Right - FinalX, DeltaY = Bottom - FinalY;
                for ( int i = 0; i < CountY; i++ )
                    graphics.FillRectangle(Brushes[(FinalCountX + i) % Length], FinalX, StartY + BlockHeight * i, DeltaX, BlockHeight);
                for ( int i = 0; i < FinalCountX; i++ )
                    graphics.FillRectangle(Brushes[(i + CountY) % Length], StartX + BlockWidth * i, FinalY, BlockWidth, DeltaY);
                graphics.FillRectangle(Brushes[(FinalCountX + CountY) % Length], FinalX, FinalY, DeltaX, DeltaY);
            }
        }
    }
}
