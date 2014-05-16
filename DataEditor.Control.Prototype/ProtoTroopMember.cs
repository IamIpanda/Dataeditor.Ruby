using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoTroopMember : UserControl
    {
        // 入口契约
        public delegate Bitmap GetBitmap(int index);
        public GetBitmap Bitmaps { get; set; }
        // 出口数据
        List<int> indecis = new List<int>();
        public List<int> Indecis { get { return indecis; } }
        public List<Point> Coodinates { get { return troopMain.Coodinates; } }
        public List<bool> Flash { get; set; }
        public List<bool> Undead { get; set; }
        public ProtoTroopMember()
        {
            InitializeComponent();
        }

        bool isAuto = true;
        private void btClarify_Click(object sender, EventArgs e)
        {
            isAuto = true;
            Clarify();
        }
        void Clarify()
        {
            if (Main.Background == null || Main.Components.Count == 0) return;
            Bitmap bit = null; Point pos;
            int w = Main.Background.Width, h = Main.Background.Height;
            int margin = 16;
            int allw = 0, k = 0;
            for (int i = 0; i < Main.Components.Count; i++)
            {
                bit = Main.Components[i];
                pos = Main.Coodinates[i];
                if (bit.Height > h - margin * 2)
                    pos.Y = (h - bit.Height) / 2 + bit.Height;
                else pos.Y = h - margin;
                Main.Coodinates[i] = pos;
                allw += bit.Width;
            }
            int start_x, possible_w = w - 2 * margin - bit.Width / 2;
            if (allw > possible_w)
            {
                k = (allw - possible_w) / Main.Components.Count + 1;
                start_x = margin;
            }
            else start_x = (w - allw) / 2;
            for (int i = 0; i < Main.Components.Count; i++)
            {
                bit = Main.Components[i];
                pos = Main.Coodinates[i];
                pos.X = start_x + bit.Width / 2;
                start_x += bit.Width - k;
                Main.Coodinates[i] = pos;
            }
            Main.Invalidate();
        }

        private void btBackground_Click(object sender, EventArgs e)
        {
            if (BackgroundClicked != null) BackgroundClicked(this, e);
        }

        private void btBattleTest_Click(object sender, EventArgs e)
        {
            if (BattleTestClicked != null) BattleTestClicked(this, e);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (Bitmaps == null) return;
            if (lbList.SelectedIndex < 0) return;
            var bitmap = Bitmaps(lbList.SelectedIndex);
            if (bitmap == null) return;
            indecis.Add(lbList.SelectedIndex);
            troopMain.Components.Add(bitmap);
            troopMain.Coodinates.Add(new Point(Main.Background.Width / 2, Main.Background.Height - 16));
            troopMain.Invalidate();
            Flash.Add(false);
            Undead.Add(false);
            if (isAuto) Clarify();
            Main.SelectedIndex = Main.Components.Count - 1;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            int index = troopMain.SelectedIndex;
            if (index < 0) return;
            Indecis.RemoveAt(index);
            Coodinates.RemoveAt(index);
            Flash.RemoveAt(index);
            Undead.RemoveAt(index);
            troopMain.SelectedIndex = -1;
            troopMain.Components.RemoveAt(index);
            troopMain.Invalidate();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            Indecis.Clear();
            Coodinates.Clear();
            Flash.Clear();
            Undead.Clear();
            troopMain.Components.Clear();
            troopMain.Invalidate();
        }

        private void btAuto_Click(object sender, EventArgs e)
        {
            if (NameClicked != null) NameClicked(this, new EventArgs());
        }

        private void troopMain_SelectedBitmapMoved(object sender, EventArgs e)
        {
            isAuto = false;
            if (troopMain.SelectedIndex > 0) Moveto(troopMain.SelectedIndex);
        }

        private void troopMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Enabled = troopMain.SelectedIndex >= 0;
            if (panel1.Enabled) Moveto(troopMain.SelectedIndex);
        }
        protected void Moveto(int index)
        {
            nudX.Value = troopMain.Coodinates[index].X;
            nudY.Value = troopMain.Coodinates[index].Y;
            cbFlash.Checked = Flash[index];
            cbUndead.Checked = Undead[index];
            cbType.Items.Add(lbList.Items[Indecis[Main.SelectedIndex]]);
            cbType.SelectedIndex = cbType.Items.Count - 1;
        }
        protected void Set(List<Bitmap> bitmap, List<Point> point)
        {
            troopMain.Components.Clear();
            troopMain.Coodinates.Clear();
            troopMain.Components.AddRange(bitmap);
            troopMain.Coodinates.AddRange(point);
            Main.SelectedIndex = -1;
        }
        public void Set(List<int> index, List<Point> point)
        {
            var bitmap = new List<Bitmap>();
            indecis = index;
            foreach (int i in index)
                bitmap.Add(Bitmaps(i));
            Set(bitmap, point);
        }

        public ListBox.ObjectCollection Items { get { return lbList.Items; } }
        public ProtoTroopBitmap Main { get { return troopMain; } }
        public event EventHandler NameClicked;
        public event EventHandler BattleTestClicked;
        public event EventHandler BackgroundClicked;
        public override string Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.Focused)
            {
                Indecis[Main.SelectedIndex] = cbType.SelectedIndex;
                Main.Components[Main.SelectedIndex] = Bitmaps(cbType.SelectedIndex);
                Main.Invalidate();
                if (isAuto) Clarify();
            }
        }

        private void cbType_Enter(object sender, EventArgs e)
        {
            cbType.Items.Clear();
            foreach (var obj in lbList.Items)
                cbType.Items.Add(obj);
            cbType.SelectedIndex = Indecis[Main.SelectedIndex];
        }

        private void ProtoTroopMember_Leave(object sender, EventArgs e)
        {
            lbList.SelectedIndex = -1;
            lbList.Invalidate();
        }

        private void cbUndead_CheckedChanged(object sender, EventArgs e)
        {
            Undead[Main.SelectedIndex] = cbUndead.Checked;
        }

        private void cbFlash_CheckedChanged(object sender, EventArgs e)
        {
            Flash[Main.SelectedIndex] = cbFlash.Checked;
        }
    }
}
