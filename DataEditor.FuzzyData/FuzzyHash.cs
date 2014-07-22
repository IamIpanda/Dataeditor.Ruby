using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("FuzzyHash: Count = {Count}")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(FuzzyHashDebugView))]
    public partial class FuzzyHash : FuzzyObject, IEnumerable<KeyValuePair<object, object>>,ICloneable
    {
        private Dictionary<object, object> dict = new Dictionary<object, object>();
        private object defaultValue;

        public FuzzyHash()
            : this(null)
        { }

        public FuzzyHash(object DefaultValue)
            : base()
        {
            this.defaultValue = DefaultValue;
            this.ClassName = FuzzySymbol.GetSymbol("Hash");
        }

        public object DefaultValue
        {
            get { return this.defaultValue; }
            set {
                this.defaultValue = value;
            }
        }

        public object this[object key]
        {
            get {
                object result;
                if (dict.TryGetValue(key, out result))
                {
                    return result;
                }
                return defaultValue;
            }
            set {
                dict[key] = value;
            }
        }

        public IEqualityComparer<Object> Comparer { get { return dict.Comparer; } }
        public int Count { get { return dict.Count; } }
        public int Size { get { return dict.Count; } }
        public int Length { get { return dict.Count; } }
        public Dictionary<Object, Object>.KeyCollection Keys { get { return dict.Keys; } }
        public Dictionary<Object, Object>.ValueCollection Values { get { return dict.Values; } }
        public void Add(Object key, Object value) { dict.Add(key, value); }
        public void Clear() { dict.Clear(); }
        public bool ContainsKey(Object key) { return dict.ContainsKey(key); }
        public bool ContainsValue(Object value) { return dict.ContainsValue(value); }
        public Dictionary<Object, Object>.Enumerator GetEnumerator() { return dict.GetEnumerator(); }
        public bool Remove(Object key) { return dict.Remove(key); }
        public bool TryGetValue(Object key, out Object value) { return dict.TryGetValue(key, out value); }

        IEnumerator<KeyValuePair<object, object>> IEnumerable<KeyValuePair<object, object>>.GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }


        internal class FuzzyHashDebugView
        {
            private FuzzyHash hashtable;
            public FuzzyHashDebugView(FuzzyHash hashtable)
            {
                this.hashtable = hashtable;
            }

            [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
            public KeyValuePair<object, object>[] Keys
            {
                get
                {
                    KeyValuePair<object, object>[] keys = new KeyValuePair<object, object>[hashtable.Count];

                    int i = 0;
                    foreach (KeyValuePair<object, object> key in hashtable)
                    {
                        keys[i] = key;
                        i++;
                    }
                    return keys;
                }
            }
        }
        public override object Clone()
        {
            FuzzyHash fh = new FuzzyHash(defaultValue);
            object Value;
            foreach (object Key in fh.Keys)
            {
                Value = fh[Key];
                fh.Add(Dup(Key), Dup(Value));
            }
            return fh;
        }
        public override void Clone(FuzzyObject source)
        {
            base.Clone(source);
            FuzzyHash S = source as FuzzyHash;
            if (S != null)
            {
                Clear();
                foreach (object key in S.Keys)
                    Add(Dup(key), Dup(S[key]));
            }
        }
    }
}
