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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WrapLead lead = new WrapLead();
            WrapTitan titan = new WrapTitan();
            Help.Window.Instance["lead"] = lead;
            Help.Window.Instance["Main"] = titan;
            Application.Run(lead.Window);
        }
    }
}
