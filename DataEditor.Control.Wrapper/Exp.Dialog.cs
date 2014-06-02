using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public partial class Exp_Dialog : Form
    {
        public Contract.Runable Value;
        public FuzzyData.FuzzyComplex Complex;
        public int MinValue = 0;
        public int MaxValue = 50;
        public int MaxLevel = 99;
        protected List<Prototype.ProtoScrollIntBar> controls = new List<Prototype.ProtoScrollIntBar>();
        public Exp_Dialog()
        {
            InitializeComponent();
            this.Shown += Exp_Dialog_Shown;
            exp_Text1.ForeBrush = new SolidBrush(Color.ForestGreen);
            exp_Text2.ForeBrush = new SolidBrush(Color.OrangeRed);
        }

        void Exp_Dialog_Shown(object sender, EventArgs e)
        {
            psi_ValueChanged(this, e);
            numericUpDown1.Value = exp_Text1.Max;
        }
        public new void Load()
        {
            GroupBox gr;
            Prototype.ProtoScrollIntBar psi;
            int count = 0;
            FuzzyData.FuzzyFixnum target;
            foreach(var sym in Complex.AllKeys)
            {
                target = Complex[sym] as FuzzyData.FuzzyFixnum;
                if (target == null) continue;
                gr = new GroupBox();
                psi = new Prototype.ProtoScrollIntBar();
                gr.Controls.Add(psi);
                gr.Size = new System.Drawing.Size(this.flowLayoutPanel1.ClientSize.Width - 4, 66);
                gr.Text = sym;
                gr.Tag = target;
                gr.Dock = DockStyle.Top;
                psi.Dock = System.Windows.Forms.DockStyle.Top;
                psi.MaxValue = MaxValue;
                psi.MinValue = MinValue;
                psi.value = Convert.ToInt32(target.Value);
                psi.ValueChanged += psi_ValueChanged;
                if (count++ % 2 == 0)
                    this.flowLayoutPanel1.Controls.Add(gr);
                else this.flowLayoutPanel2.Controls.Add(gr);
                controls.Add(psi);
            }
            int size = (count - 1) / 2 + 1;
            this.SetClientSizeCore(this.ClientSize.Width, 420 + 72 * size);
        }

        void psi_ValueChanged(object sender, EventArgs e)
        {
            if (Value == null) return;
            List<int> value = new List<int>(), sum = new List<int>() { 0 };
            int[] paras = new int[controls.Count];
            int i = 0;
            foreach(Prototype.ProtoScrollIntBar psi in controls)
            {
                paras[i++] = psi.value;
            }
            int su = 0, part;
            for (int j = 1; j < MaxLevel; j++)
            {
                part = Convert.ToInt32(Value.call(j, paras));
                su += part;
                value.Add(part);
                sum.Add(su);
            }
            exp_Text1.Value = value;
            exp_Text2.Value = sum;
            exp_Text1.Invalidate();
            exp_Text2.Invalidate();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            FuzzyData.FuzzyFixnum num;
             foreach(var psi in controls)
            {
                num = psi.Tag as FuzzyData.FuzzyFixnum;
                if (num != null) num.Value = psi.value; 
             }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                if (exp_Text1.Max != numericUpDown1.Value)
                {
                    exp_Text1.Max = Convert.ToInt32(numericUpDown1.Value);
                    exp_Text1.Invalidate();
                }
            if (tabControl1.SelectedIndex == 1)
                if (exp_Text2.Max != numericUpDown1.Value)
                {
                    exp_Text2.Max = Convert.ToInt32(numericUpDown1.Value);
                    exp_Text1.Invalidate();
                }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0) return;
            numericUpDown1.Value = tabControl1.SelectedIndex == 0 ? exp_Text1.Max : exp_Text2.Max;
        }
    }
    public class Exp_Text : UserControl
    {
        List<int> value = null;
        public List<int> Value { get { return value; } set { this.value = value; OnValueChanged(); } }
        int lineCount = 5;
        public int LineCount { get { return lineCount; } set { this.lineCount = value; Invalidate(); } }
        public Brush BackBrush { get; set; }
        public Brush ForeBrush { get; set; }
        protected StringFormat left, right;
        protected Brush LevelBrush;
        public int Max { get; set; }
        public Exp_Text()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.BackBrush = new SolidBrush(Color.LightGray);
            this.ForeBrush = new SolidBrush(Color.Black);
            this.LevelBrush = new SolidBrush(Color.Black);
            this.BackColor = Color.White;
            left = new StringFormat();
            left.LineAlignment = StringAlignment.Center;
            left.Alignment = StringAlignment.Near;
            right = new StringFormat();
            right.LineAlignment = StringAlignment.Center;
            right.Alignment = StringAlignment.Far;
            this.Max = 999999;
        }
        protected virtual void OnValueChanged()
        {
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (value == null) return;
            int x_szie = e.ClipRectangle.Width, y_size = e.ClipRectangle.Height;
            float inner = (x_szie + 0F) / Value.Count;
            float zoom = (y_size + 0F) / Max;
            float h;
            for(int i = 0; i < Value.Count; i++)
            {
                h = zoom * value[i];
                e.Graphics.FillRectangle(this.BackBrush, inner * i, y_size - h, inner, h);
            }
            inner = (x_szie + 0F) / lineCount;
            int rowcount =  ((Value.Count - 1) / lineCount + 1);
            zoom = (y_size + 0F) / rowcount;
            RectangleF rect;
            for (int i = 0; i < Value.Count; i++)
            {
                rect = new RectangleF(i / rowcount * inner + 5, i % rowcount * zoom, inner - 10, zoom);
                e.Graphics.DrawString(
                    String.Format("L {0}:", i + 1),
                    this.Font, this.LevelBrush, rect, left);
                e.Graphics.DrawString(Value[i].ToString(), this.Font, this.ForeBrush, rect, right);
            }
        }
    }
}
