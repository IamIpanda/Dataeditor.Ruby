using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public struct Change
    {
        public FuzzyData.FuzzyObject New { get; set; }
        public FuzzyData.FuzzyObject Old { get; set; }
        public Change(FuzzyData.FuzzyObject old, FuzzyData.FuzzyObject _new)
        { Old = old; New = _new; }
    }
    public class Action
    {
        public static Action Instance { get; set; }
        protected Action() { }
        static Action() { Instance = new Action(); }
        protected LinkedList<Change> Record = new LinkedList<Change>();
        public void Do(FuzzyData.FuzzyObject old,FuzzyData.FuzzyObject _new)
        {
            while (Record.Count >= 99) Record.RemoveFirst();
            Record.AddLast(new Change(old, _new));
        }
        public void Undo()
        {
            Change c = Record.Last.Value;
            var temp = c.New & c.Old;
            Record.RemoveLast();
        }
    }
}
