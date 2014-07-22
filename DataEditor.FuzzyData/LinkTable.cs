using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class LinkTable<T1,T2>
    {
        Dictionary<T1, T2> Plus = new Dictionary<T1, T2>();
        Dictionary<T2, T1> Minus = new Dictionary<T2, T1>();
        LinkInterface<T1, T2> verse;
        LinkInterface<T2, T1> reverse;
        public LinkTable()
        {
            verse = new LinkInterface<T1, T2>(Plus);
            reverse = new LinkInterface<T2, T1>(Minus);
        }
        public void Add(T1 key,T2 value)
        {
            if (Plus.ContainsKey(key))
                Plus[key] = value;
            else
                Plus.Add(key, value);
            if (Minus.ContainsKey(value))
                Minus[value] = key;
            else
                Minus.Add(value, key);
        }
        public void Clear()
        {
            Plus.Clear();
            Minus.Clear();
        }
        public T2 this[T1 Key] { get { return verse[Key]; } }
        public T1 this[T2 Key] { get { return reverse[Key]; } }
        public LinkInterface<T1, T2> Verse { get { return verse; } }
        public LinkInterface<T2, T1> Reverse { get { return reverse; } }
        public Dictionary<T1, T2> RawVerse { get { return Plus; } }
        public Dictionary<T2, T1> RawReverse { get { return Minus; } }
        public class LinkInterface<T3, T4>
        {
            Dictionary<T3, T4> dictionary;
            internal LinkInterface(Dictionary<T3,T4> dictionary)
            {
                this.dictionary = dictionary;
            }
            public T4 this[T3 argument]
            {
                get 
                {
                    T4 value = default(T4);
                    dictionary.TryGetValue(argument, out value);
                    return value;
                }
            }
            public bool TryGetValue(T3 key, out T4 value)
            {
                return dictionary.TryGetValue(key, out value);
            }
        }
    }
}
