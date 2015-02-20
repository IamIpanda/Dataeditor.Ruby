using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Change : ICloneable
    {
        public FuzzyData.FuzzyObject New { get; set; }
        public FuzzyData.FuzzyObject Old { get; set; }
        public FuzzyData.FuzzyObject Pos { get; set; }
        public Change(FuzzyData.FuzzyObject old, FuzzyData.FuzzyObject _new)
        {
            Old = old ?? FuzzyData.FuzzyNil.Instance;
            New = _new ?? FuzzyData.FuzzyNil.Instance;
            Pos = New;
        }
        public Change(FuzzyData.FuzzyObject old, FuzzyData.FuzzyObject _new, FuzzyData.FuzzyObject pos)
        {
            Old = old ?? FuzzyData.FuzzyNil.Instance;
            New = _new ?? FuzzyData.FuzzyNil.Instance;
            Pos = pos;
        }
        public object Clone()
        {
            return new Change(Old.Clone() as FuzzyData.FuzzyObject, New.Clone() as FuzzyData.FuzzyObject, Pos);
        }
        public virtual void Undo() { var temp = Pos & Old; }
        public virtual void Redo() { var temp = Pos & New; }
    }
    public class Action
    {
        public static Action Instance { get; set; }
        protected Action() { }
        static Action() { Instance = new Action(); Instance.Enabled = true; }
        protected LinkedList<Change> Record = new LinkedList<Change>();
        Stack<Change> Redid = new Stack<Change>();
        public bool Enabled { get; set; }
        public void Do(FuzzyData.FuzzyObject old,FuzzyData.FuzzyObject _new)
        {
            if (Enabled != true) return;
            Log.log("Action 记录了一个动作，来自" + old.GetType().ToString()); 
            while (Record.Count >= 99) Record.RemoveFirst();
            Change change = new Change(old, _new);
            Record.AddLast(change);
            Redid.Clear();
            if (Act != null) Act(this, new ActionEventArgs(change, ActionType.Do));
        }

        public void Do(Change change)
        {
            if (Enabled != true) return;
            Log.log("Action 记录了一个动作，类型为 " + change.GetType().ToString());
            while(Record.Count > 99) Record.RemoveFirst();
            Record.AddLast(change);
            Redid.Clear();
            if (Act != null) Act(this, new ActionEventArgs(change, ActionType.Do));
        }
        public void Undo()
        {
            if (Enabled != true) return;
            if (!CanUndo) return;
            Log.log("Action 弹回了一个动作");
            Change c = Record.Last.Value;
            Redid.Push(c.Clone() as Change);
            c.Undo();
            Record.RemoveLast();
            if (Act != null) Act(this, new ActionEventArgs(c, ActionType.Undo));
        }
        public void Redo()
        {
            if (Enabled != true) return;
            if (!CanRedo) return;
            Log.log("Action 重做了一个动作");
            var change = Redid.Pop();
            Record.AddLast(change.Clone() as Change);
            change.Redo();
            if (Act != null) Act(this, new ActionEventArgs(change, ActionType.Redo));
        }
        public bool CanRedo { get { return Redid.Count > 0; } }
        public bool CanUndo { get { return Record.Count > 0; } }
        public enum ActionType { Do, Undo, Redo };
        public class ActionEventArgs :EventArgs
        {
            public Change Change { get; set; }
            public ActionType Type { get; set; }
            public ActionEventArgs(Change change, ActionType type)
            {
                this.Change = change;
                this.Type = type;
            }
        }
        public event EventHandler<ActionEventArgs> Act;
    }
}
