using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Help
{
    public class Taint
    {
        public static Taint Instance { get; set; }
        static Taint() { Instance = new Taint(); }
        protected Taint() 
        {
            Help.Action.Instance.Act += Instance_Act;
            Tag = new TaintTag();
        }
        protected Dictionary<FuzzyData.FuzzyObject, Contract.TaintState> records 
            = new Dictionary<FuzzyData.FuzzyObject, Contract.TaintState>();
        protected Dictionary<FuzzyData.FuzzyObject, Contract.TaintCollection> books
            = new Dictionary<FuzzyData.FuzzyObject, Contract.TaintCollection>();
        public Contract.TaintState this[FuzzyData.FuzzyObject key]
        {
            get
            {
                if (key == null) return Contract.TaintState.UnTainted;
                var state = Contract.TaintState.UnTainted;
                records.TryGetValue(key, out state);
                return state;
            }
            internal set
            {
                if (records.ContainsKey(key)) records[key] = value;
                else records.Add(key, value);
            }
        }
        public void SetTaint(Control.ObjectEditor target, TaintEventArgs e)
        {
            e.OldState = this[e.Owner];
            this[e.Owner] = e.NewState;
            e.Editor = target;
            Control.ObjectEditor container = null;
            if (target != null) container = target.Container;
            switch (e.NewState)
            {
                case Contract.TaintState.Undo:
                case Contract.TaintState.Added:
                case Contract.TaintState.Tainted:
                case Contract.TaintState.FullReplaced:
                case Contract.TaintState.ChildTainted:
                    if (container != null && container.Value != null)
                    {
                        TaintEventArgs f = new TaintEventArgs(container.Value, 0, Contract.TaintState.ChildTainted, e);
                        SetTaint(container, f);
                    }
                    break;
                case Contract.TaintState.Saved:
                    // DO NOTHING
                    break;
            }
            if (target is Contract.TaintableEditor)
                (target as Contract.TaintableEditor).Putt();
        }
        public void SetTaint(Control.ObjectEditor target)
        {
            var arg = new TaintEventArgs(target.Value, 0, Contract.TaintState.Tainted);
            SetTaint(target, arg);

        }
        public void Save()
        {
            var list = new List<FuzzyObject>();
            foreach (var key in records.Keys)
                list.Add(key);
            foreach (var key in list)
                records[key] = Contract.TaintState.Saved;
        }
        public void Save(FuzzyData.FuzzyObject obj)
        {
            if (obj == null) return;
            if (records.ContainsKey(obj))
            {
                records[obj] = Contract.TaintState.Saved;
                if (obj.InstanceVariables.Count > 0)
                    foreach (var value in obj.InstanceVariables.Values)
                        Save(value as FuzzyData.FuzzyObject);
                if (obj is FuzzyData.FuzzyArray)
                    foreach (var value in obj as FuzzyData.FuzzyArray)
                        Save(value as FuzzyData.FuzzyObject);
                if (obj is FuzzyData.FuzzyHash)
                {
                    FuzzyData.FuzzyHash hash = obj as FuzzyData.FuzzyHash;
                    foreach(var key in hash.Keys)
                    {
                        Save(key as FuzzyData.FuzzyObject);
                        Save(hash[key] as FuzzyData.FuzzyObject);
                    }
                }
            }
        }

        public class TaintTag
        {
            private Dictionary<FuzzyObject, object> tag = new Dictionary<FuzzyObject, object>();

            public object this[FuzzyObject key]
            {
                get
                {
                    object ans;
                    return tag.TryGetValue(key, out ans) ? ans : null;
                }
                set
                {
                    if (tag.ContainsKey(key)) tag[key] = value;
                    else tag.Add(key, value);
                }
            }
        }
        public TaintTag Tag { get; set; }

        public class TaintEventArgs : EventArgs
        {
            public TaintEventArgs InnerEventArg { get; set; }
            public FuzzyData.FuzzyObject Owner { get; set; }
            public Contract.TaintState NewState { get; set; }
            public Contract.TaintState OldState { get; set; }
            public Control.ObjectEditor Editor { get; set; }
            public TaintEventArgs(/*Control.ObjectEditor editor, */FuzzyData.FuzzyObject owner, Contract.TaintState oldState, Contract.TaintState newState, TaintEventArgs inner = null)
            {
                // Editor = editor;
                Owner = owner;
                OldState = oldState;
                NewState = newState;
                InnerEventArg = inner;
            }
        }
        public class IndexTaintEventArgs : TaintEventArgs
        {
            public int Index { get; set; }
            public IndexTaintEventArgs(/*Control.ObjectEditor editor, */FuzzyData.FuzzyObject owner, Contract.TaintState oldState, Contract.TaintState newState, int index, TaintEventArgs inner = null)
                : base(/*editor, */owner, oldState, newState, inner)
            {
                Index = index;
            }
        }
        public static void DefaultPutt(Control.ObjectEditor editor)
        {
            // 单独部分
            // 抓取当前污染状态
            var state = Instance[editor.Value];
            // 获取对应颜色
            var color = DefaultColor(state);
            if (editor.Label == null) return;
            // 重置标签颜色
            editor.Label.ForeColor = System.Windows.Forms.Label.DefaultForeColor;
            // 给标签上色
            if (state != Contract.TaintState.UnTainted)
                editor.Label.ForeColor = color;
            if (editor is Contract.TaintableList)
            {
                // 列表部分
                Contract.TaintCollection list = null;
                if (Instance.books.TryGetValue(editor.Value, out list))
                    (editor as Contract.TaintableList).PuttList(list);
            }
        }
        public static Contract.TaintCollection RequireBook(Control.ObjectEditor editor)
        {
            Contract.TaintCollection collection = null;
            if (Instance.books.TryGetValue(editor.Value, out collection))
                return collection;
            else 
            {
                collection = new TaintList();
                Instance.books.Add(editor.Value, collection);
                return collection;
            }
        }
        public static System.Drawing.Color DefaultColor(Contract.TaintState state)
        {
            switch (state)
            {
                case Contract.TaintState.Added:
                    return Help.Painter.Instance[18];
                case Contract.TaintState.ChildTainted:
                    return Help.Painter.Instance[17];
                case Contract.TaintState.FullReplaced:
                    return Help.Painter.Instance[23];
                case Contract.TaintState.Saved:
                    return Help.Painter.Instance[19];
                case Contract.TaintState.Tainted:
                    return Help.Painter.Instance[16];
                case Contract.TaintState.Undo:
                    return Help.Painter.Instance[20];
                case Contract.TaintState.UnTainted:
                default:
                    return System.Windows.Forms.Control.DefaultForeColor;
            }
        }

        void Instance_Act(object sender, Action.ActionEventArgs e)
        {
            var target = e.Change.New;
            if (e.Type == Action.ActionType.Undo)
                SetTaint(null, new TaintEventArgs(target, this[target], Contract.TaintState.Undo));
            else if (e.Type == Action.ActionType.Redo)
                SetTaint(null, new TaintEventArgs(target, this[target], Contract.TaintState.Tainted));
            if (e.Type != Action.ActionType.Do)
            {
                var editor = Link.Instance[target];
                if (editor != null) editor.Pull();
            }
        }
        public class TaintList : List<Contract.TaintState>, Contract.TaintCollection
        {
            #region TaintCollection 成员
            public new Contract.TaintState this[int index]
            {
                get { return index < base.Count ? base[index] : Contract.TaintState.UnTainted; }
                set 
                {
                    while (index >= base.Count) Add(Contract.TaintState.UnTainted);
                    base[index] = value; 
                }
            }
            public new int Count { get { return base.Count; } }
            #endregion
        }
    }
}
