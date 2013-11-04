using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Window
    {
        static public Window Instance { get; set; }
        static Window() { Instance = new Window(); }
        protected Window() { }
        protected Dictionary<string, System.Windows.Forms.Form> windows = new Dictionary<string, System.Windows.Forms.Form>();
        public System.Windows.Forms.Form this[string key]
        {
            get 
            {
                key = key.ToUpper();
                System.Windows.Forms.Form form = null;
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
