using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DataEditor.Help
{
    [Serializable]
    public struct Rtp
    {
        public string Path, Version, Name;
        public System.Drawing.Color Color;
        public bool IsFromReg;
        public Rtp(string Path, string Version, String Name, System.Drawing.Color Color, bool IsFromReg)
        {
            this.Path = Path;
            this.Version = Version;
            this.Name = Name;
            this.Color = Color;
            this.IsFromReg = IsFromReg;
        }
        public override string ToString()
        {
            return Name == "" ? Path : (Name + (Version == "" ? "" : ("（" + Version + "）")));
        }
        public string FullString()
        {
            return "[" + Name + "(" + Version + ")" + "]" + Path;
        }
        public bool SearchFile(string file,out string full_path)
        {
            full_path = "";
            string temp_full_path = System.IO.Path.Combine(this.Path, file);
            string directory_path = System.IO.Path.GetDirectoryName(temp_full_path);
            string file_name = System.IO.Path.GetFileName(temp_full_path);
            var dir = new System.IO.DirectoryInfo(directory_path);
            if (!dir.Exists) return false;
            var ans = dir.GetFiles(file_name + "*");
            if (ans.Length == 0) return false;
            full_path = ans[0].FullName;
            return true;

        }
        static public Rtp FromString(String s, Color c)
        {
            Regex r = new Regex("\\[.+\\]");
            Regex r2 = new Regex("\\(.+\\)");
            Match m = r.Match(s);
            Match m2;
            String path, version, name;
            if (m.Success)
            {
                path = s.Remove(m.Index, m.Length);
                string s2 = m.Value;
                s2 = s2.Remove(0, 1);
                s2 = s2.Remove(s2.Length - 1, 1);
                m2 = r2.Match(s2);
                if (m2.Success)
                {
                    name = s2.Remove(m2.Index, m2.Length);
                    version = m2.Value;
                    version = version.Remove(0, 1);
                    version = version.Remove(version.Length - 1, 1);
                }
                else
                {
                    name = s2;
                    version = "";
                }
            }
            else
            {
                version = "";
                name = "";
                path = s;
            }
            return new Rtp(path, version, name, c, false);
        }
    }
}
