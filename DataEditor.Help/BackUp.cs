using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public static class Backup
    {
        public static readonly string DIRECTORY = "Program/Backup";
        public static readonly string FILENAME = "Backup.arce";
        public static readonly int HINT_TIME = 2000;
        public static readonly string HINT_STRING = "备份文件已保存。";
        // public static readonly string FILEBACK = "Backup.arce";
        static object loc = new object();
        static Backup()
        {
            System.IO.DirectoryInfo Directory = new System.IO.DirectoryInfo(DIRECTORY);
            if (!(Directory.Exists)) Directory.Create();
        }
        public static void Save()
        {
            lock (loc)
            {
                var fullpath = System.IO.Path.Combine(DIRECTORY, FILENAME);
                var stream = new System.IO.FileStream(fullpath, System.IO.FileMode.Create);
                Option.SetOption(stream, Help.Data.Instance);
                stream.Close();
            }
            var temp = Help.Bash.GetStatus();
            Help.Bash.SetStatus(HINT_STRING);
            System.Threading.Thread.Sleep(HINT_TIME);
            Help.Bash.SetStatus(temp);
        }
        static System.Threading.Thread save_thread = null;
        public static void SaveOnAnotherThread()
        {
            if (save_thread != null) return;
            save_thread = new System.Threading.Thread(Save);
            save_thread.Start();
            save_thread = null;
        }
        public static bool Load()
        {
            lock (loc)
            {
                var fullpath = System.IO.Path.Combine(DIRECTORY, FILENAME);
                var file = new System.IO.FileInfo(fullpath);
                if (!(file.Exists)) return false;
                var stream = file.OpenRead();
                var obj = Help.Option.GetOption(stream);
                if (!(obj is Help.Data)) return false;
                Help.Data.Instance = obj as Help.Data;
                stream.Close();
            }
            return true;
        }


        static System.Threading.Thread main_thread;
        public static void Start()
        {
            main_thread = new System.Threading.Thread(ThreadDo);
            main_thread.Start();
        }
        static void ThreadDo()
        {
            while (true)
            {
                Save();
                System.Threading.Thread.Sleep(1000 * 10);
            }
        }
        public static void End()
        {
            if (main_thread != null)
                main_thread.Abort();
        }
    }
}
