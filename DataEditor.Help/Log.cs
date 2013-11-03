using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    static public class Log
    {
        public const long MaxLogSize = 5000000;
        public const string Name = "Log";
        public const string Pattern = ".log";
        static System.IO.TextWriter Text;
        static Log()
        {
            if (!(System.IO.File.Exists(Name + Pattern)))
                System.IO.File.CreateText(Name + Pattern).Close();
            System.IO.FileInfo info = new System.IO.FileInfo(Name + Pattern);
            if (info.Length > MaxLogSize)
            {
                int ans = 1;
                while (System.IO.File.Exists(Name + ans.ToString() + Pattern)) ans++;
                info.CopyTo(Name + ans.ToString() + Pattern);
                System.IO.File.Create(Name + Pattern).Close();
            }
            Text = new System.IO.StreamWriter(Name + Pattern,true);
            Text.WriteLine("==================================================================");
            Text.WriteLine(" Initialized On " + DateTime.Now.ToString());
            Text.WriteLine("==================================================================");
            Text.Flush();
        }
        static public void log(string info)
        {
            Text.WriteLine("[" + DateTime.Now.ToString()+ "] " + info);
            Flush();
        }
        static public void Flush ()
        {
            Text.Flush();
        }
    }
}
