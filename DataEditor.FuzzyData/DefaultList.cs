using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class DefaultList<T> : List<T>
    {
        public new T this[int index]
        {
            get 
            {
                if (index < 0) return default(T);
                if (index >= this.Count) return default(T);
                return base[index];
            }
            set 
            {
                if (index < 0) return;
                while (index >= this.Count) Add(default(T));
                base[index] = value;
            }
        }
    }
}
