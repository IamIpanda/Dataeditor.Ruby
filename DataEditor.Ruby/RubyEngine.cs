using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby;
using Microsoft.Scripting.Hosting;

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
            LoadAssembly(typeof(System.Drawing.Color).Assembly);
            LoadAssembly(typeof(System.Text.Encoding).Assembly);
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
                ExceptionOperations eo = engine.GetService<ExceptionOperations>();
                System.Windows.Forms.MessageBox.Show("Titan Rock the Olympic！\n" + eo.FormatException(ex), "Ruby 程序执行错误",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
        public dynamic ExecuteFile (string path)
        {
            try
            {
                Help.Log.log("正在通过 Ruby 引擎执行文件 : " + path);
                return engine.ExecuteFile(path);
            }
            catch ( Exception ex )
            {
                System.Windows.Forms.MessageBox.Show("Titan Rock the Ground！\n" + ex.ToString(), "Ruby 文件执行错误",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
