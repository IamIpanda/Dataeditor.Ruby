using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataEditor.Help;
using DataEditor.Ruby;

namespace DataEditor.Control.Event
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Initialize();
            Application.Run(new Main());
        }

        private static Ruby.RubyEngine engine = new RubyEngine();

        public static void Initialize()
        {
            // Load this Assembly self into Engine.
            engine.LoadAssembly(typeof(Program).Assembly);
            Collector.AddAssembly(typeof(Program).Assembly);
            Help.Collector.AddAssembly(typeof (Control.Wrapper.Check).Assembly);
            Help.Collector.AddAssembly(typeof (Control.Window.WindowWithOK).Assembly);
            Help.Collector.AddAssembly(typeof(Control.Window.WindowWithOK).Assembly);
            FuzzyData.Serialization.Factory<byte[]>.SearchFactor(typeof(FuzzyData.FuzzyTable).Assembly);
        }

        public static void DebugAddingOn()
        {
            // DEBUG ONLY CODE
            engine.Execute("$LOAD_PATH << \"\\\\\\\\psf\\\\FILE\\\\VSProjects\\\\dataeditor.ruby\\\\DataEditor.Arce\"");
            engine.Execute("Dir.chdir \"\\\\\\\\psf\\\\FILE\\\\VSProjects\\\\dataeditor.ruby\\\\DataEditor.Arce\" ");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\main.rb");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\XP\\File - xp.rb");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\XP\\Event - xp.rb");
            Ruby.RubyBuilder.In(new DataEditor.Control.Wrapper.Container.Group());
        }
    }
}
