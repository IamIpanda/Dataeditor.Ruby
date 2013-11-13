using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DataEditor.Control.Prototype
{
    public class ProtoIntegerEditor : ProtoIntegerDisplayer 
    {
        protected bool Editing = false;
        protected int LastIndex = -1;
        protected int LastValue = -1;
        protected float zoomx = 0F;
        protected float zoomy = 0F;

        protected override void DrawFocusRectangle(System.Drawing.Graphics graphics) { }
        protected override void DrawText(System.Drawing.Graphics graphics) { }
        protected override void ProtoIntegerDisplayer_Enter(object sender, EventArgs e) { }
        protected override void ProtoIntegerDisplayer_Leave(object sender, EventArgs e) { }

        public event EventHandler<ProtoIntegerEditorValueChangedEventArgs> SingleValueChanged;
        public event EventHandler PartValueChanged;
        public event EventHandler FullValueChanged;

        [Browsable(true)] public int MaxAdmitValue { get; set; }
        [Browsable(true)] public int MinAdmitValue { get; set; }


        public ProtoIntegerEditor()
        {
            InitializeComponent();
            MaxAdmitValue = 9999;
            MinAdmitValue = 0;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProtoIntegerEditor
            // 
            this.Name = "ProtoIntegerEditor";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProtoIntegerEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProtoIntegerEditor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ProtoIntegerEditor_MouseUp);
            this.ResumeLayout(false);

        }

        protected override void SetValue(List<int> value)
        {
            base.SetValue(value);
            if (FullValueChanged != null)
                FullValueChanged(this, new EventArgs());
        }
        private void ProtoIntegerEditor_MouseDown(object sender, MouseEventArgs e)
        {
            Editing = true;
            StartEditing();
        }

        private void ProtoIntegerEditor_MouseUp(object sender, MouseEventArgs e)
        {
            Editing = false;
            if (PartValueChanged != null)
                PartValueChanged(this, new EventArgs());
        }

        private void ProtoIntegerEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Editing)
                return;
            if (LastIndex == -1 && LastValue == -1)
            {
                int index = CalculateX(e.X);
                int value = CalculateY(e.Y);
                SetBaseValue(index,value);
                LastIndex = index;
                LastValue = value;
            }
            else
            {
                int index = CalculateX(e.X);
                int value = CalculateY(e.Y);
                FillData(LastIndex, LastValue, index, value, true);
                LastIndex = index;
                LastValue = value;
            }
            Invalidate();
        }

        protected void StartEditing()
        {
            LastIndex = LastValue = -1;
            zoomx = (this.ClientSize.Width - 2F) / Value.Count;
            zoomy = (this.ClientSize.Height - 2F) / MaxNumber;
        }

        protected void FillData(int StartIndex, int StartValue, int EndIndex, int EndValue)
        {
            double Value = StartValue;
            double Step = (EndValue - StartValue + 0.0) / (EndIndex - StartIndex);
            for (int i = StartIndex; i < EndIndex; i++)
            {
                Value += Step;
                SetBaseValue(i, (int)Value);
            }
        }

        protected void FillData(int StartIndex, int StartValue, int EndIndex, int EndValue, bool Check)
        {
            if (Check)
                if (StartIndex > EndIndex)
                {
                    FillData(EndIndex, EndValue, StartIndex, StartValue);
                    return;
                }
            FillData(StartIndex, StartValue, EndIndex, EndValue);
        }
        protected void SetBaseValue(int index, int value)
        {
            if (index < 0)
                return;
            if (index >= base.value.Count)
                return;
            if (value > MaxAdmitValue)
                value = MaxAdmitValue;
            if (value < MinAdmitValue)
                value = MinAdmitValue;
            base.value[index] = value;
            if (SingleValueChanged != null)
                SingleValueChanged(this, new ProtoIntegerEditorValueChangedEventArgs(index, value));
        }
        protected int CalculateX(int X)
        {
            return (int)((X - 1F) / zoomx);
        }
        protected int CalculateY(int Y)
        {
            return (int)((this.ClientSize.Height - Y - 1F) / zoomy);
        }

        public class ProtoIntegerEditorValueChangedEventArgs : EventArgs
        {
            public int Index { get; set; }
            public int Value { get; set; }
            public ProtoIntegerEditorValueChangedEventArgs(int Index, int Value)
            {
                this.Index = Index;
                this.Value = Value;
            }
        }
    }
}
