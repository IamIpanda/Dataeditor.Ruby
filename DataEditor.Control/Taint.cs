using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Taint
    {
        public static Taint Instance { get; set; }
        static Taint() { Instance = new Taint(); }
        protected Taint() { }
        protected Dictionary<FuzzyData.FuzzyObject, Contract.TaintState> records 
            = new Dictionary<FuzzyData.FuzzyObject, Contract.TaintState>();
        public Contract.TaintState this[FuzzyData.FuzzyObject key]
        {
            get
            {
                var state = Contract.TaintState.UnTainted;
                records.TryGetValue(key, out state);
                return state;
            }
            set
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
            Control.ObjectEditor container = target.Container;
            switch (e.NewState)
            {
                case Contract.TaintState.Undo:
                case Contract.TaintState.Added:
                case Contract.TaintState.Tainted:
                case Contract.TaintState.FullReplaced:
                case Contract.TaintState.ChildTainted:
                    if (container != null)
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
        public void Save()
        {
            foreach (var key in records.Keys)
                records[key] = Contract.TaintState.Saved;
        }
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
        {/*
            switch(Instance[editor.Value])
            {
                case Contract.TaintState.Tainted
                    

            }*/
        }
    }
}
