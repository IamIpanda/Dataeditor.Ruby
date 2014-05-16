using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace DataEditor.Help
{
    public static class Reflect
    {
        static Dictionary<FileInfo, Assembly> Assemblies = new Dictionary<FileInfo, Assembly>();
        public static Assembly GetAssembly(FileInfo info)
        {
            if (info.Exists)
            {
                if (Assemblies.ContainsKey(info))
                    return Assemblies[info];
                Assembly ass = Assembly.LoadFrom(info.FullName);
                Assemblies.Add(info, ass);
                return ass;
            }
            throw new ArgumentException("File Not Exists : " + info.FullName);
        }
        public static List<Assembly> GetDirectory(DirectoryInfo info)
        {
            if (!info.Exists)
            {
                info.Create();
                Log.log("目录不存在：" + info.FullName + "，已创建之");
            }
            FileInfo[] files = info.GetFiles("*.dll");
            List<Assembly> ass = new List<Assembly>();
            foreach (FileInfo file in files)
                ass.Add(GetAssembly(file));
            return ass;
        }
    }
}
