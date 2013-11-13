using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoIntegerDisplayer : UserControl, ProtoFrameControl
    {
        [Browsable(true)] public int MaxNumber { get; set; }                                            // 允许显示的最大数值
        [Browsable(true)] public List<int> Value { get { return value; } set { SetValue(value); } }     // 数据接口
        [Browsable(true)] public Color DataColor { get ;set; }                                          // 设置颜色
        [Browsable(true)] public Brush ForeBrush { get; set; }                                          // 或者不设置颜色直接设置笔刷
        [Browsable(true)] public new string Text { get; set; }

        protected List<int> value = new List<int>();

        public ProtoIntegerDisplayer()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, true);
            InitializeComponent();
            this.MaxNumber = 3000;
            this.ForeBrush = null;
            this.DataColor = Color.Gray;
            this.Text = "";
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            DrawBorder(e);
            if (Value.Count == 0)
                return;
            DrawData(e);
            if (this.Focused)
                DrawFocusRectangle(e.Graphics);
            DrawText(e.Graphics);
        }
        protected virtual void DrawBorder(PaintEventArgs e)
        {
            ProtoFrameControlHelp.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black);
        }
        protected virtual void DrawData(PaintEventArgs e)
        {
            float zoomx = (this.ClientSize.Width - 2F) / Value.Count;
            float zoomy = (this.ClientSize.Height - 2F) / MaxNumber;
            for (int i = 0; i < Value.Count; i++)
                DrawSingleData(e.Graphics,GetBrush(),value[i], i, zoomx, zoomy);
        }
        protected virtual void DrawSingleData(Graphics graphics, Brush brush, int value, int index, float zoomx, float zoomy)
        {
            float x = zoomx * index + 1;
            float h = zoomy * value + 1;
            float y = this.ClientSize.Height - h - 1;
            float w = zoomx;
            if (y < 0) { y = 1; h = this.ClientSize.Height - 2; }
            graphics.FillRectangle(brush, x, y, w, h);
        }
        protected virtual void SetValue(List<int> value)
        {
            this.value = value;
        }
        protected virtual Brush GetBrush()
        {
            if (ForeBrush != null)
                return ForeBrush;
            if (DataColor != default(Color))
                return new SolidBrush(ProtoListControlHelp.CheckEnabled(DataColor, Enabled));
            return new SolidBrush(Color.Gray);
        }
        protected virtual void DrawFocusRectangle(Graphics graphics)
        {
            ControlPaint.DrawFocusRectangle(graphics, new Rectangle(3, 3, ClientSize.Width - 6, ClientSize.Height - 6));
        }
        protected virtual void DrawText(Graphics graphics)
        {
            graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 4F, 4F);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProtoIntegerDisplayer
            // 
            this.Name = "ProtoIntegerDisplayer";
            this.Enter += new System.EventHandler(this.ProtoIntegerDisplayer_Enter);
            this.Leave += new System.EventHandler(this.ProtoIntegerDisplayer_Leave);
            this.ResumeLayout(false);

        }

        protected virtual void ProtoIntegerDisplayer_Enter(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected virtual void ProtoIntegerDisplayer_Leave(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
