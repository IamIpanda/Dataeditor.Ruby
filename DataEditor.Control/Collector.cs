using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Help;
using System.IO;
using System.Reflection;

namespace DataEditor.Help
{
    public class Collector
    {
        static Dictionary<string, Type> Types = new Dictionary<string, Type>();
        static DirectoryInfo Directory;
        static public Collector Instance { get; set; }
        static Collector()
        {
            String sDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string Dir = System.IO.Path.Combine(sDir, "Program/Control/Wrapper");
            Directory = new DirectoryInfo(Dir);
            Log.log("正在从下列目录加载控件：" + Dir);
            if (!(Directory.Exists)) { Directory.Create(); Directory = new DirectoryInfo(Dir); }
            List<Assembly> la = Help.Reflect.GetDirectory(Directory);

            foreach (Assembly ass in la)
                AddAssembly(ass);
            Instance = new Collector();
        }
        static public void AddAssembly(Assembly ass)
        {
            Help.Log.log("正在导入库：" + ass.FullName);
            Type basetype = typeof(DataEditor.Control.ObjectEditor);
            var buffer = AddAssemblyFromBuffer(ass);
            if (buffer == null)
            {
                buffer = new Dictionary<string, Type>();
                foreach ( Type t in ass.GetExportedTypes() )
                {
                    // Log.log("正在扫描类型：" + t.ToString());
                    if (!t.IsClass || t.IsAbstract || t.IsGenericType) continue;
                    foreach ( Type Inter in t.GetInterfaces() )
                        if ( Inter == basetype )
                        {
                            object o = ass.CreateInstance(t.FullName);
                            string key = (o as DataEditor.Control.ObjectEditor).Flag;
                            if ( !Types.ContainsKey(key) )
                            {
                                Types.Add(key, t);
                                buffer.Add(key, t);
                                Log.log("导入了类型：[" + key + "]" + t.ToString()); 
                            }
                        }
                }
                WriteAssemblyToBuffer(ass, buffer);
            }
            else
            {
                Log.log("读入了缓存：" + Environment.NewLine + ass.FullName);
                foreach (var key in buffer.Keys)
                    if (!Types.ContainsKey(key))
                        Types.Add(key, buffer[key]);
            }
        }
        static public Dictionary<string, Type> AddAssemblyFromBuffer(Assembly ass)
        {
            var path = GetAssemblyBufferPath(ass);
            var fileinfo = new System.IO.FileInfo(path);
            if (!fileinfo.Exists) return null;
            var stream = fileinfo.OpenRead();
            object ans = Help.Option.GetOption(stream);
            stream.Close();
            var buffer = ans as TypeBuffer;
            if (buffer == null) return null;
            DateTime LastTime = buffer.LastTime;
            DateTime NowTime = Help.Reflect.LastModifiedTime(ass);
            if (LastTime < NowTime) return null;
            return buffer.Types;
        }
        static public void WriteAssemblyToBuffer(Assembly ass, Dictionary<string, Type> value)
        {
            DateTime NowTime = Help.Reflect.LastModifiedTime(ass);
            TypeBuffer buffer = new TypeBuffer();
            buffer.LastTime = NowTime;
            buffer.Types = value;
            string name = GetAssemblyBufferPath(ass);
            FileStream stream = new FileStream(name, FileMode.Create);
            Help.Option.SetOption(stream, buffer);
        }
        static public string GetAssemblyBufferPath(Assembly ass)
        {
            var fullpath = ass.Location;
            var filename = System.IO.Path.GetFileNameWithoutExtension(fullpath);
            return System.IO.Path.Combine(Directory.FullName, filename + ".buffer");
        }
        protected Collector() { }
        public object this[string s]
        {
            get
            {
                if (Types.ContainsKey(s))
                {
                    Type type = Types[s];
                    Assembly ass = Assembly.GetAssembly(type);
                    object target = ass.CreateInstance(type.FullName);
                    return target;
                }
                else
                   return null;
            }
        }
        [Serializable]
        class TypeBuffer
        {
            public DateTime LastTime {get;set;}
            public Dictionary<string, Type> Types { get; set; }
            public TypeBuffer()
            {
                LastTime = new DateTime();
                Types = new Dictionary<string, Type>();
            }
        }

    }
}
