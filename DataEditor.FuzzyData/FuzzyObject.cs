using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{ 
    [System.Diagnostics.DebuggerTypeProxy(typeof(FuzzyObjectDebugView))]
    [Serializable]
    public partial class FuzzyObject : ICloneable
    {
        [Serializable]
        internal class FuzzyObjectDebugView
        {
            internal FuzzyObject obj;

            public FuzzyObjectDebugView(FuzzyObject obj)
            {
                this.obj = obj;
            }

            public FuzzySymbol ClassName
            {
                get { return obj.ClassName; }
            }

            [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
            public KeyValuePair<FuzzySymbol, object>[] Keys
            {
                get
                {
                    KeyValuePair<FuzzySymbol, object>[] keys = new KeyValuePair<FuzzySymbol, object>[obj.InstanceVariables.Count];

                    int i = 0;
                    foreach (KeyValuePair<FuzzySymbol, object> key in obj.InstanceVariables)
                    {
                        keys[i] = key;
                        i++;
                    }
                    return keys;
                }
            }
        }
        private FuzzySymbol className;
        private Dictionary<FuzzySymbol, object> variables;
        private FuzzyObjectInstanceVariableProxy variableproxy;
        private List<FuzzyModule> extmodules;

        public override string ToString()
        {
            return ("#<" + this.ClassName + ">");
        }

        public virtual FuzzySymbol ClassName
        {
            get { return this.className ?? (this.className = FuzzySymbol.GetSymbol("Object")); }
            set { this.className = value; }
        }

        public FuzzyClass Class
        {
            get { return FuzzyClass.GetClass(this.ClassName); }
        }

        public Dictionary<FuzzySymbol, object> InstanceVariables
        {
            get { return variables ?? (variables = new Dictionary<FuzzySymbol, object>()); }
        }

        public FuzzyObjectInstanceVariableProxy InstanceVariable
        {
            get { return variableproxy ?? (variableproxy = new FuzzyObjectInstanceVariableProxy(this)); }
        }

        public List<FuzzyModule> ExtendModules
        {
            get { return extmodules ?? (extmodules = new List<FuzzyModule>()); }
        }

        public virtual Encoding Encoding
        {
            get;
            set;
        }
        [Serializable]
        public class FuzzyObjectInstanceVariableProxy
        {
            FuzzyObject obj;
            internal FuzzyObjectInstanceVariableProxy(FuzzyObject obj)
            {
                this.obj = obj;
            }

            public object this[FuzzySymbol key]
            {
                get
                {
                    return obj.InstanceVariables.ContainsKey(key) ?
                        obj.InstanceVariables[key] is FuzzyNil ?
                            null :
                            obj.InstanceVariables[key] :
                        null;
                }
                set
                {
                    if (obj.InstanceVariables.ContainsKey(key))
                        obj.InstanceVariables[key] = value;
                    else
                        obj.InstanceVariables.Add(key, value);
                }
            }

            public object this[string key]
            {
                get { return this[FuzzySymbol.GetSymbol(key)]; }
                set { this[FuzzySymbol.GetSymbol(key)] = value; }
            }

            public object this[FuzzyString key]
            {
                get { return this[FuzzySymbol.GetSymbol(key)]; }
                set { this[FuzzySymbol.GetSymbol(key)] = value; }
            }
        }
        protected object Dup(object origin)
        {
            if (origin is ICloneable)
                return (origin as ICloneable).Clone();
            else
                return origin;
        }
        public virtual object Clone() 
        {
            FuzzyObject fo = new FuzzyObject();
            fo.className = this.className;
            object Value;
            foreach (FuzzySymbol key in this.InstanceVariables.Keys)
            {
                Value = this.InstanceVariables[key];
                fo.InstanceVariables.Add(Dup(key) as FuzzySymbol, Dup(Value));
            }
            return fo;
        }
        /// <summary>
        /// 从source中将自身变成目标的拷贝
        /// </summary>
        /// <param name="source">拷贝源</param>
        public virtual void Clone(FuzzyObject source)
        {
            this.className = source.className; object Value;
            this.InstanceVariables.Clear();
            foreach (FuzzySymbol key in source.InstanceVariables.Keys)
            {
                Value = source.InstanceVariables[key];
                this.InstanceVariables.Add(Dup(key) as FuzzySymbol, Dup(Value));
            }
        }
        /// <summary>
        /// 注意！这个操作符变更了它的原本含义。
        /// 将左操作数变成右操作数的拷贝。
        /// </summary>
        /// <param name="self">将被拷贝的对象</param>
        /// <param name="source">拷贝源</param>
        /// <returns></returns>
        public static FuzzyObject operator &(FuzzyObject self, FuzzyObject source)
        {
            if (source == null)
                return self;
            self.Clone(source);
            return self;
        }
        public object this[string str]
        {
            get 
            {
                FuzzySymbol sym = FuzzySymbol.GetSymbol(str);
                return this[sym];    
            }
            set 
            {
                FuzzySymbol sym = FuzzySymbol.GetSymbol(str);
                this[sym] = value;
            }
        }
        public object this[FuzzySymbol sym]
        {
            get
            {
                if (InstanceVariables.ContainsKey(sym))
                    return InstanceVariables[sym];
                else return null;
            }
            set
            {
                if (InstanceVariables.ContainsKey(sym))
                    InstanceVariables[sym] = value;
                else InstanceVariables.Add(sym, value);
            }
        }
    }
}
