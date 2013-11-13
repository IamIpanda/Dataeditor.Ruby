using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby;

namespace DataEditor.Ruby
{
    public class RubyEngine
    {
       
        protected Microsoft.Scripting.Hosting.ScriptEngine engine;
        public Microsoft.Scripting.Hosting.ScriptEngine Engine { get { return engine; } }
        public RubyEngine ()
        {
            engine = IronRuby.Ruby.CreateEngine();
            LoadAssembly(typeof(RubyEngine).Assembly);
            LoadAssembly(typeof(Help.Log).Assembly);
            LoadAssembly(typeof(FuzzyData.FuzzyObject).Assembly);
            LoadAssembly(typeof(Control.ObjectEditor).Assembly);
            LoadAssembly(typeof(System.Windows.Forms.Form).Assembly);
        }
        public object this[string key]
        {
            get { return engine.Runtime.Globals.GetVariable(key); }
            set { engine.Runtime.Globals.SetVariable(key, value); }
        }
        public void LoadAssembly (System.Reflection.Assembly assembly)
        {
            Help.Log.log("Ruby 引擎加载了程序集 " + assembly.ToString());
            engine.Runtime.LoadAssembly(assembly);
        }
        public dynamic Execute (string code)
        {
            try
            {
                return engine.Execute(code);
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
        public dynamic ExecuteFile (string path)
        {
            try
            {
                return engine.ExecuteFile(path);
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
    }
}
