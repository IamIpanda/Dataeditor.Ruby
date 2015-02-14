using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            Application.Run(new Test());
        }

        static public void Initialize()
        {
        }
    }
}
