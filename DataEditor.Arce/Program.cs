using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WrapLead lead = new WrapLead();
            WrapTitan titan = new WrapTitan();
            Help.Window.Instance["lead"] = lead;
            Help.Window.Instance["Main"] = titan;
            Application.Run(lead.Window);
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < currentAssemblies.Length; i++)
                if (currentAssemblies[i].FullName == args.Name)
                    return currentAssemblies[i];
            throw new NotImplementedException();
        }
    }
}
