using DataEditor.Help;
using DataEditor.Ruby;
using System;
using System.Windows.Forms;

namespace DataEditor.Control.Event
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
#if DEBUG
            string[] loads = new string[] { @"Program\Serialization\DataEditor.FuzzyData.Serialization.RubyMarshal.dll"
                , @"Program\Serialization\UserDefined\DataEditor.FuzzyData.Extra.dll" };
            Array.ForEach(loads, (string ass) =>
            {
                System.Reflection.Assembly.LoadFile(System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath),
                    ass));
            });
#endif
            Initialize();
            Application.Run(new Main());
        }

        private static Ruby.RubyEngine engine = new RubyEngine();

        public static void Initialize()
        {
            // Load this Assembly self into Engine.
            engine.LoadAssembly(typeof(Program).Assembly);
            Collector.AddAssembly(typeof(Program).Assembly);
            Help.Collector.AddAssembly(typeof(Control.Wrapper.Check).Assembly);
            Help.Collector.AddAssembly(typeof(Control.Window.WindowWithOK).Assembly);
            Help.Collector.AddAssembly(typeof(Control.Window.WindowWithOK).Assembly);
            FuzzyData.Serialization.Factory<byte[]>.SearchFactor(typeof(FuzzyData.FuzzyTable).Assembly);
        }

        public static void DebugAddingOn()
        {
#if DEBUG
            // DEBUG ONLY CODE
            engine.Execute("$LOAD_PATH << \"\\\\\\\\psf\\\\FILE\\\\VSProjects\\\\dataeditor.ruby\\\\DataEditor.Arce\"");
            engine.Execute("Dir.chdir \"\\\\\\\\psf\\\\FILE\\\\VSProjects\\\\dataeditor.ruby\\\\DataEditor.Arce\" ");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\main.rb");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\XP\\File - xp.rb");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\XP\\Event - xp.rb");
#endif
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < currentAssemblies.Length; i++)
                if (currentAssemblies[i].FullName == args.Name)
                    return currentAssemblies[i];
            Help.Log.log("Can't search named assembly: " + args.Name);
            return null;
        }
    }
}