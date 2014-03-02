using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class View : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyArray, Prototype.ProtoListView>
    {
        public override string Flag { get { return "view"; } }
        protected FuzzyData.FuzzyObject model = null;
        protected MultiCatalogue catalogue = null;

        public override void Push() { /* 已作废 */ }

        public override void Pull()
        {
            if (catalogue != null)
                catalogue.InitializeText(value);
            if (model != null)
                Control.Items.Add(new ListViewItem());
        }

        // FIXME : 设置污染标记
        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Reset()
        {
            SetColumns(argument.GetArgument<List<object>>("COLUMNS"), argument.GetArgument<List<object>>("COLUMN_WIDTH"));
            List<object> texts = argument.GetArgument<List<object>>("CATALOGUE");
            SetCatalogue(texts);
            // FIXME : 处理 NEW 节点以提供给 MODEL
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("window", null);
            argument.SetArgument("columns", new string[0]);
            argument.SetArgument("catalogue", new List<object>());
            argument.SetArgument("new", null, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("window_type", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("column_width", null, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("width", 500, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("height", 250, Help.Parameter.ArgumentType.Option);
        }
        public override void Bind()
        {
            base.Bind();
            Control.DoubleClick += Control_DoubleClick;
            Control.KeyDown += Control_KeyDown;
        }


        public FuzzyData.FuzzyObject SelectedValue
        {
            get 
            {
                if (Control.SelectedIndices[0] == value.Count) return model == null ? null : model.Clone() as FuzzyData.FuzzyObject;
                else return value[Control.SelectedIndices[0]] as FuzzyData.FuzzyObject;
            }
        }

        protected void SetColumns(IEnumerable<object> value,IEnumerable<object> list)
        {
            if (value == null) return;
            foreach (object target in value)
            {
                Control.Columns.Add(target.ToString());
            }
        }
        protected void SetCatalogue(List<object> value)
        {
            if (value == null) return;
            var text = new List<Help.Parameter.Text>();
            foreach (var target in value)
                if (target is Help.Parameter.Text) text.Add(target as Help.Parameter.Text);
            catalogue = new MultiCatalogue(Control.Items, text);
        }
        protected void SetModel()
        {
            model = argument.GetArgument<FuzzyData.FuzzyObject>("new");
        }
         

        void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { }
            else if (e.KeyCode == Keys.Space) { }
            else if (e.KeyCode == Keys.Delete) { }
            else if (e.KeyCode == Keys.Back) { }
        }

        void Control_DoubleClick(object sender, EventArgs e)
        {
            if (Control.SelectedIndices.Count == 0) return;
            var window_proc = argument.GetArgument<Contract.Runable>("window");
            if (window_proc == null) return;
            var window_type = argument.GetArgument<int>("window_type");
            DataEditor.Control.WrapBaseWindow window;
            if (window_type == 0 )
                window = new Control.Window.WindowWithOK.WrapWindowWithOK<Control.Window.WindowWithOK>() ;
                else window = new Control.Window.WindowWithRightOK.WrapWindowWithRightOK<Control.Window.WindowWithRightOK>();
            var value = SelectedValue;
            window_proc.call(window, value);
            if (window.Show() == DialogResult.OK)
            {
                if (Control.SelectedIndices[0] == base.value.Count)
                    base.value.Add(value);
                // 刷新文字
            }
        }

        public class MultiCatalogue
        {
            public ListView.ListViewItemCollection Items { get; set; }
            public List<Help.Parameter.Text> Text { get; set; }
            public Contract.Runable Filter { get; set; }
            public Help.LinkTable<int, int> Link { get; set; }
            List<List<Help.Parameter.Text>> catalogue = new List<List<Help.Parameter.Text>>();
            List<object> using_value = null;
            public MultiCatalogue(ListView.ListViewItemCollection items, List<Help.Parameter.Text> origin, Contract.Runable filter = null)
            {
                Items = items;
                Text = origin;
                Filter = filter;
                Link = new Help.LinkTable<int, int>();
            }
            public void InitializeText(List<object> value)
            {
                using_value = value;
                Items.Clear();
                Link.Clear();
                catalogue.Clear();
                int i = 0, j = 0; //  i 表示 List 上的 ID，j 表示 ListBox 的ID（显示顺序）
                for (; i < value.Count; i++)
                    if (IsFix(value[i]))
                    {
                        Link.Add(i, j);
                        var text = new List<Help.Parameter.Text>();
                        foreach (var t in Text) text.Add(new Help.Parameter.Text(t));
                        catalogue.Add(text);
                        string[] ans = new string[text.Count];
                        for (int k = 0; k < text.Count; k++)
                        {
                            var t = text[k];
                            ans[k] = t.ToString(value[i], t.Watch, i, j, k);
                            if (t.Watch != null && t.Watch.Count != 0)
                                Help.Monitor.Watch(t, TextChanged, t.Watch.ToArray());
                        }
                        j++;
                        Items.Add(new ListViewItem(ans));
                    }
            }
            void TextChanged(object sender, EventArgs e)
            {
                if (!(sender is Help.Parameter.Text)) return;
                var text = sender as Help.Parameter.Text;
                var trans = catalogue.Find(a => a.Contains(text));
                int j = catalogue.IndexOf(trans);
                int k = trans.IndexOf(text);
                int i = Link.Reverse[j];
                object target = using_value[i];
                string ans = (text.ToString(target, text.Watch, i, j));
                if (k == 0) Items[j].Text = ans; else Items[j].SubItems[k - 1].Text = ans;
                Help.Monitor.Remove(text);
                Help.Monitor.Watch(text, TextChanged, text.Watch.ToArray());
            }
            bool IsFix(object value)
            {
                if (Filter == null)
                    return (value != null && value != FuzzyData.FuzzyNil.Instance);
                else return Convert.ToBoolean(Filter.call(value));
            }
        }
    }
}
