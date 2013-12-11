using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Win32;

namespace DataEditor.Help
{
    public class Path
    {
        public static Path Instance { get; set; }
        static Path()
        {
            Instance = new Path();
            RTPManager.GetRegistryRtps();
            Instance["Program"] = System.Windows.Forms.Application.StartupPath;
        }
        protected Path() { }
        protected Dictionary<string, string> paths = new Dictionary<string, string>();
        public string this[string name]
        {
            get 
            {
                name = name.ToUpper();
                string answer = ""; 
                paths.TryGetValue(name, out answer); 
                return answer; 
            }
            set 
            {
                name = name.ToUpper();
                if (paths.ContainsKey(name)) paths[name] = value;
                else paths.Add(name, value); 
            }
        }
        public string RequestPath(string name, string description)
        {
            if (paths.ContainsKey(name.ToUpper())) return paths[name.ToUpper()];
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "程序向您请求短名为 " + name + " 的文件夹。\n" + description;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return dialog.SelectedPath;
            else return "";
        }
        protected bool SearchFileByFull(string file, out string answer, string path)
        {
            string full_path = System.IO.Path.Combine(path, file);
            var file_info = new System.IO.FileInfo(full_path);
            if (file_info.Exists) { answer = file_info.FullName; return true; }
            answer = "";
            return false;
        }
        public bool SearchFile(string file, out string answer, params string[] paths)
        {
            foreach (string shorts in paths)
            {
                string up_shorts = shorts.ToUpper();
                if (up_shorts == "RTP")
                    if (RTPManager.SearchFile(file,out answer))
                        return true;
                string path = this[up_shorts];
                if (path != "")
                    if (SearchFileByFull(file, out answer, path))
                        return true;
            }
            answer = ""; return false;
        }
        public string[] SearchFiles(string file, params string[] paths)
        {
            List<string> answers = new List<string>();
            string answer;
            foreach (string shorts in paths)
            {
                string up_shorts = shorts.ToUpper();
                if (up_shorts == "RTP")
                    answers.AddRange(RTPManager.SearchFiles(file));
                else
                {
                    string path = this[up_shorts];
                    if (path != "")
                        if (SearchFileByFull(file, out answer, path))
                            answers.Add(answer);
                }
            }
            return answers.ToArray();
        }



        static public class RTPManager
        {
            static private List<Color> colors = new List<Color>()
            {
                Color.DarkRed, 
                Color.Orange,
                Color.YellowGreen,
                Color.BlueViolet,
                Color.Purple 
            };
            public static List<Rtp> RtpList { get { return rtp_list; } set { rtp_list = value; Save(); } }
            static List<Rtp> rtp_list = new List<Rtp>();
            static public void GetRegistryRtps()
            {
                RegistryKey l = Registry.LocalMachine;
                RegistryKey s = l.OpenSubKey("SOFTWARE");
                RegistryKey E = s.OpenSubKey("Enterbrain");
                if (E == null) return;
                int count = 0;
                foreach (string t in E.GetSubKeyNames())
                    if (t == "RGSS" || t == "RGSS2" || t == "RGSS3")
                    {
                        RegistryKey d = E.OpenSubKey(t).OpenSubKey("RTP");
                        foreach (string r in d.GetValueNames())
                        {
                            rtp_list.Add(new Rtp(d.GetValue(r).ToString(), t, r, GetColor(count), true));
                            count++;
                        }
                    }
            }
            static Color GetColor(int count)
            {
                if (count < colors.Count)
                    return colors[count];
                int index = count % colors.Count;
                int pow = count / colors.Count;
                int alpha = (int)(255 / Math.Pow(2.0, pow));
                return Color.FromArgb(alpha, colors[index]);
            }
            static void Load()
            {
                object ob = Option.GetOption(typeof(RTPManager));
                List<Rtp> rtps = ob as List<Rtp>;
                if (rtps == null) return;
                foreach (Rtp rtp in rtps)
                    if (!(rtp.IsFromReg))
                        rtp_list.Add(rtp);
            }
            static void Save()
            {
                Option.SetOption(typeof(RTPManager), rtp_list);
            }
            static public bool SearchFile(string file, out string answer)
            {
                foreach (Rtp rtp in rtp_list)
                    if (rtp.SearchFile(file, out answer))
                        return true;
                answer = "";
                return false;
            }
            static public string[] SearchFiles(string file)
            {
                List<string> answers = new List<string>();
                string answer;
                foreach (Rtp rtp in rtp_list)
                    if (rtp.SearchFile(file, out answer))
                        answers.Add(answer);
                return answers.ToArray();
            }
        }
    }
}
