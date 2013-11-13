using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoIntegerText : UserControl
    {
        Bitmap TextImage;
        Graphics ImageGraphics;
        ProtoIntegerEditor Bound;

        public int RowCount { get; set; }
        public int ItemWidth { get; set; }
        public int ItemHeight { get; set; }
        public bool QuickMode { get; set; }

        private int EditingIndex = -1;
        private List<bool> Changes = new List<bool>();
        private Color oldBackGroundColor,
                      newBackGroundColor,
                      oldForeColor,
                      newForeColor;
        private Brush OldBackGroundBrush,
                      NewBackGroundBrush,
                      OldForeBrush,
                      NewForeBrush;
        StringFormat FormatA = new StringFormat(StringFormatFlags.NoClip);
        StringFormat FormatB = new StringFormat(StringFormatFlags.NoClip);
        public ProtoIntegerText()
        {
            InitializeComponent();
            this.RowCount = 10;
            this.ItemHeight = 12;
            this.ItemWidth = 20;
            this.QuickMode = true;
            FormatA.Alignment = StringAlignment.Near;
            FormatA.LineAlignment = StringAlignment.Center;
            FormatB.Alignment = StringAlignment.Far;
            FormatB.LineAlignment = StringAlignment.Center;
            oldBackGroundColor = this.BackColor;
            newBackGroundColor = Color.Purple;
            oldForeColor = this.ForeColor;
            newForeColor = Color.LightGreen;
            OldBackGroundBrush = new SolidBrush(oldBackGroundColor);
            NewBackGroundBrush = new SolidBrush(newBackGroundColor);
            OldForeBrush = new SolidBrush(oldForeColor);
            NewForeBrush = new SolidBrush(newForeColor);
            this.MainPictureBox.Dock = DockStyle.Fill;
        }
        [Browsable(true)]
        public ProtoIntegerEditor BoundEditor
        {
            get { return Bound; }
            set 
            {
                UnListenToBound();
                Bound = value;
                ListenToBound();
                OnBound();
            }
        }

        protected void OnSingleValueChanged(object sender, ProtoIntegerEditor.ProtoIntegerEditorValueChangedEventArgs e)
        {
            DrawSingleTextNew(e.Index);
            if (!QuickMode)
            {
                ImageGraphics.Save();
                MainPictureBox.Invalidate();
            }
        }
        protected void OnMultiValueChanged(object sender,EventArgs e)
        {
            if (QuickMode)
            {
                ImageGraphics.Save();
                MainPictureBox.Invalidate();
            }
        }
        protected void OnFullValueChanged(object sender, EventArgs e)
        {
            if (Bound == null)
                return;
            Changes.Clear();
            foreach (int i in Bound.Value)
                Changes.Add(false);
            SetTextImage();
            DrawFullText();
        }
        protected void OnBound()
        {
            OnFullValueChanged(this, null);
        }
        protected void ListenToBound()
        {
            if (Bound == null)
                return;
            Bound.SingleValueChanged += this.OnSingleValueChanged;
            Bound.PartValueChanged += this.OnMultiValueChanged;
            Bound.FullValueChanged += this.OnFullValueChanged;
            MainNumeric.Maximum = Bound.MaxAdmitValue;
            MainNumeric.Minimum = Bound.MinAdmitValue;
        }
        protected void UnListenToBound()
        {
            if (Bound == null)
                return;
            Bound.SingleValueChanged -= this.OnSingleValueChanged;
            Bound.PartValueChanged -= this.OnMultiValueChanged;
            Bound.FullValueChanged -= this.OnFullValueChanged;
        }
        protected void SetTextImage()
        {
            if (Bound == null)
                return;
            if (Bound.Value.Count == 0)
                return;
            int width = ItemWidth * RowCount;
            int height = ItemHeight * ((Bound.Value.Count - 1) / RowCount + 1);
            TextImage = new Bitmap(width, height);
            ImageGraphics = Graphics.FromImage(TextImage);
            MainPictureBox.Image = TextImage;
            this.SetClientSizeCore(this.Width, height + 15);
        }
        protected Rectangle CalculateRectangle(int index)
        {
            int x = index % RowCount;
            int y = index / RowCount;
            return new Rectangle(x * ItemWidth, y * ItemHeight, ItemWidth, ItemHeight);
        }
        protected void DrawFullText()
        {
            if (Bound == null || Bound.Value == null || Bound.Value.Count == 0)
                return;
            for (int i = 0; i < Bound.Value.Count; i++)
                DrawSingleTextOld(i);
            ImageGraphics.Save();
        }
        protected void DrawSingleTextNew(int index)
        {
            DrawSingleText(index, NewBackGroundBrush, NewForeBrush);
        }
        protected void DrawSingleTextOld(int index)
        {
            DrawSingleText(index, OldBackGroundBrush, OldForeBrush);
        }
        /// <summary>
        /// 描绘单个的数值单元。
        /// 注意，此方法不会保存Graphics的目前状态。
        /// </summary>
        /// <param name="index">数值ID</param>
        /// <param name="BackColor">背景色</param>
        /// <param name="ForeColor">前景色</param>
        protected void DrawSingleText(int index, Brush BackColor, Brush ForeColor)
        {
            Rectangle r = CalculateRectangle(index);
            ImageGraphics.FillRectangle(BackColor, r);
            ImageGraphics.DrawString("[" + index + "]", Font, ForeColor, r, FormatA);
            ImageGraphics.DrawString(Bound.Value[index].ToString(), Font, ForeColor, r, FormatB);
        }

        protected void SubmitData()
        {
            if (EditingIndex < 0 || EditingIndex >= Bound.Value.Count)
                return;
            Bound.Value[EditingIndex] = Convert.ToInt32(MainNumeric.Value);
            DrawSingleTextNew(EditingIndex);
            Bound.Invalidate();   
        }

        protected void SetNumeric()
        {
            MainNumeric.Size = new Size(ItemWidth, ItemHeight);
            MainNumeric.Location = new Point(EditingIndex % RowCount * ItemWidth, EditingIndex / RowCount * ItemHeight);
            MainNumeric.Value = Bound.Value[EditingIndex];
            MainNumeric.Visible = true;
            MainNumeric.Focus();
        }

        private void MainPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Bound == null)
                return;
            int x = e.X / ItemWidth;
            int y = e.Y / ItemHeight;
            if (x < 0 || x > RowCount)
                return;
            int index = y * RowCount + x;
            if (index < 0 || index > Bound.Value.Count)
                return;
            SubmitData();
            EditingIndex = index;
            SetNumeric();
        }

        private void MainNumeric_Leave(object sender, EventArgs e)
        {
            MainNumeric.Visible = false;
        }

        private void MainNumeric_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                MainNumeric.Visible = false;
            else if (e.KeyData == Keys.Right)
            {
                if (Bound == null)
                    return;
                EditingIndex++;
                if (EditingIndex >= Bound.Value.Count)
                    EditingIndex = EditingIndex % Bound.Value.Count;
                SetNumeric();
            }
            else if (e.KeyData == Keys.Left)
            {
                if (Bound == null)
                    return;
                EditingIndex--;
                while (EditingIndex < 0)
                    EditingIndex += Bound.Value.Count;
                SetNumeric();
            }
            else if (e.KeyData == Keys.PageUp)
            {
                if (Bound == null)
                    return;
                EditingIndex -= RowCount;
                while (EditingIndex < 0)
                    EditingIndex += Bound.Value.Count;
                SetNumeric();                
            }
            else if (e.KeyData == Keys.PageDown)
            {
                if (Bound == null)
                    return;
                EditingIndex += RowCount;
                if (EditingIndex >= Bound.Value.Count)
                    EditingIndex = EditingIndex % Bound.Value.Count;
                SetNumeric();
            }
            else if (e.KeyData == Keys.Enter)
            {
                if (Bound == null)
                    return;
                SubmitData();
                EditingIndex++;
                if (EditingIndex >= Bound.Value.Count)
                    EditingIndex = EditingIndex % Bound.Value.Count;
                SetNumeric();
            }
        }
        public Color NewForeColor
        {
            get { return newForeColor; }
            set { newForeColor = value; NewForeBrush = new SolidBrush(newForeColor); }
        }

        public Color OldForeColor
        {
            get { return oldForeColor; }
            set { oldForeColor = value; OldForeBrush = new SolidBrush(oldForeColor); }
        }

        public Color NewBackGroundColor
        {
            get { return newBackGroundColor; }
            set { newBackGroundColor = value; NewBackGroundBrush = new SolidBrush(newBackGroundColor); }
        }

        public Color OldBackGroundColor
        {
            get { return oldBackGroundColor; }
            set { oldBackGroundColor = value; OldBackGroundBrush = new SolidBrush(oldBackGroundColor); }
        }
    }
}