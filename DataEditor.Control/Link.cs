using System;
using System.Text;
using System.Collections.Generic;
using DataEditor.FuzzyData;

namespace DataEditor.Help
{
	public class Link
	{
        public static Link Instance { get; set; }
        public Dictionary<FuzzyObject, Control.ObjectEditor> Collection { get; set; }
        static Link()
        { Instance = new Link(); }
        protected Link()
        { Collection = new Dictionary<FuzzyObject,Control.ObjectEditor>(); }
        public FuzzyObject this[Control.ObjectEditor editor]
        {
            get { return editor.Value; }
            set { editor.Value = value; }
        }
        public Control.ObjectEditor this[FuzzyObject key]
        {
            get 
            {
                Control.ObjectEditor ans;
                return Collection.TryGetValue(key, out ans) ? ans : null;
            }
            set 
            {
                if (Collection.ContainsKey(key))
                    Collection[key] = value;
                else Collection.Add(key, value);
            }
        }
        
	}
}