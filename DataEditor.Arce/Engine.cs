using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataEditor.Ruby;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    static public class Engine
    {
        static public RubyEngine engine { get; set; }
        static Engine()
        {
            engine = new RubyEngine();
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Window.EditorWindow).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Wrapper.Text).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Event.EventCommand).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.ShapeShifter.ShapeShifter).Assembly);
            engine.LoadAssembly(typeof(DataEditor.Control.Event.EventCommand).Assembly);
            engine.LoadAssembly(typeof(DataEditor.Control.Window.EditorWindow).Assembly);
            Help.Bash.RubyEngine = ((str) => engine.Execute(str));
            Engine.engine.Execute(@"require ""Ruby/main.rb""");
        }
        static public void OpenProject(string Path)
        {
            try
            {
                var proc = (IronRuby.Builtins.Proc)engine.Execute(@"Lead.Open_project");
                Help.Path.Instance["project"] = System.IO.Path.GetDirectoryName(Path);
                IronRuby.Builtins.MutableString str = IronRuby.Builtins.MutableString.Create(Path, IronRuby.Builtins.RubyEncoding.UTF8);
                proc.Call(str);
                (Help.Window.Instance["Main"] as WrapTitan).Window.Show();
            }
            catch (Exception ex)
            { 
            }
            finally
            {
                Help.Loading.EndLoading();
            }
        }
        static public void OpenFile(string Path)
        {
            engine.Execute(@"Lead.open_file (""" + Path + @""")");
        }
        static public void OpenRubyFile(string Path)
        {
            engine.ExecuteFile(Path);
        }
        static public void OpenOption()
        {
            MessageBox.Show("Not usable now.");
        }
        static public void Exit()
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
