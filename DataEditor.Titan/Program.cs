using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataEditor.Titan
{
    class WrapTitan : DataEditor.Control.Window.EditorWindow.WrapEditorWindow<Titan> { }
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
            WrapTitan titan = new WrapTitan();
            DataEditor.Help.Window.Instance["main"] = titan;
            Application.Run(titan.Window);
        }
    }
}
