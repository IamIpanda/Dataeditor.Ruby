using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DataEditor.Help
{
    public static class Loading
    {
        static Control.Window.LoadingForm Loads = new Control.Window.LoadingForm();
        static Thread thread = null;
        public static void StartLoading(/*System.Threading.ThreadStart Work*/)
        {
            /*
            Loads.Work = Work;
            Loads.ShowDialog();*/
            while (thread != null) Thread.Sleep(1);
            thread = new Thread(ThreadDo);
            thread.Start();
        }
        public static void SetLoading(string text)
        {
            if (Loads == null) return;
            Loads.Refresh();
            Loads.Text = text;
        }
        public static void EndLoading()
        {
            Loads.Close();
            thread.Abort();
            thread = null;
        }
        static void ThreadDo()
        {
            Loads.ShowDialog();
        }
    }
    
}
