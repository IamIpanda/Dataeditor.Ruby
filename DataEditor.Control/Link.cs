using System;
using System.Text;
using System.Collections.Generic;
using DataEditor.FuzzyData;

namespace DataEditor.Control
{
	public class Link
	{
        public static Link Instance { get; set; }
        public Help.LinkTable<FuzzyObject, Control.ObjectEditor> Collection { get; set; }
        static Link()
        { Instance = new Link(); }
        protected Link()
        { Collection = new Help.LinkTable<FuzzyObject, Control.ObjectEditor>(); }
        public FuzzyObject this[Control.ObjectEditor editor]
        {
            get { return Collection[editor]; }
            set { Collection.Add(value, editor); }
        }
        public Control.ObjectEditor this[FuzzyObject key]
        {
            get { return Collection[key]; }
            set { Collection.Add(key, value); }
        }
        
	}
}