using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Control;

namespace DataEditor.Help
{
    public class Window
    {
        static public Window Instance { get; set; }
        static Window() { Instance = new Window(); }
        protected Window() { }
        protected Dictionary<string, WrapBaseWindow> windows = new Dictionary<string, WrapBaseWindow>();
        public WrapBaseWindow this[string key]
        {
            get 
            {
                key = key.ToUpper();
                WrapBaseWindow form = null;
                windows.TryGetValue(key, out form); 
                return form;
            }
            set 
            {
                key = key.ToUpper();
                if (windows.ContainsKey(key)) windows[key] = value; 
                else windows.Add(key, value); 
            }
        }
    }
}
