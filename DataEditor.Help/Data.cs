using System;
using System.Text;
using DataEditor.FuzzyData;
using System.Collections.Generic;

namespace DataEditor.Help
{
    [Serializable]
    public class Data
    {
        public static Data Instance { get; set; }
        static Data() { Instance = new Data(); }
        protected Data () { }
        protected Dictionary<string, FuzzyObject> Datas = new Dictionary<string, FuzzyObject>();
        protected Dictionary<string, System.IO.FileInfo> Origins = new Dictionary<string, System.IO.FileInfo>();
        public FuzzyObject this[string name]
        {
            get
            {
                if (name.ToLower() == "map")
                {
                    FuzzyArray arr = new FuzzyArray();
                    foreach (var map in Map.Values) arr.Add(map);
                    return arr;
                }
                if (Datas.ContainsKey(name))
                    return Datas[name];
                else
                    return null;
            }
            set
            {
                if (Datas.ContainsKey(name))
                    Datas[name] = value;
                else
                    Datas.Add(name, value);
            }
        }
        public FuzzyObject this[int index]
        { 
            get
            {
                if (MapComponent.Maps.ContainsKey(index))
                    return MapComponent.Maps[index];
                else
                    return null;
            }
        }
        public Dictionary<int, FuzzyObject> Map { get { return MapComponent.Maps; } }
        public Dictionary<string, FuzzyObject>.KeyCollection Names { get { return Datas.Keys; } }
        public string this[string name, string serialize = ""]
        {
            set
            {
                if (name.ToLower() == "map")
                {
                    MapComponent.Initialize(value, serialize);
                    return;
                }
                string ans;
                string[] files = Path.Instance.SearchFiles(value, "project", "rtp");
                if (files.Length == 0)
                {
                    string description = "程序正在请求一个短名为 " + name + " 的文件。\n在默认的的文件夹中没有找到名为 " + value + " 的文件，清指定之：";
                    System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                    dialog.Title = "请求 " + name;
                    System.Windows.Forms.MessageBox.Show(description, dialog.Title,System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Question); 
                    while (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) ;
                    ans = dialog.FileName;
                }
                else if (files.Length > 1)
                {
                    string description = "程序正在请求一个短名为 " + name + " 的文件。\n程序找到了下述的文件，请选择要匹配的其一：";
                    DataEditor.Control.Window.SelectWindow window = new Control.Window.SelectWindow();
                    int i = -1;
                    while (i < 0) { i = window.ShowDialog("选择文件", description, 0, false, files); }
                    ans = files[i];
                }
                else ans = files[0];
                object content = Serialization.LoadFile(ans, serialize);
                Datas.Add(name, content as FuzzyObject);
                Origins.Add(name, new System.IO.FileInfo(ans));
            }
        }
        public void Save(string name = null, string type = "")
        {
            if (name == null)
            {
                foreach (string str in Datas.Keys)
                    Save(str);
                return;
            }
            System.IO.FileInfo file;
            System.IO.FileStream stream;
            if (Origins.TryGetValue(name, out file))
            {
                try 
                {
                    stream = file.OpenWrite();
                    Serialization.TryGetSerialization(type).Dump(stream, Instance[name]);

                }
                catch(Exception ex)
                {
                    var message = "An error occured when saving data for " + name + ":" + System.Environment.NewLine + ex.ToString();
                    Log.log(message);
                    System.Windows.Forms.MessageBox.Show(message, "Save Failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else 
            {
                var message = "Discarded data named " + name + " for its unexistance.";
                Log.log(message);
            }
        }
        public void Discard(string name = null, string type = "")
        {
            if (name == null)
            {
                foreach (string str in Datas.Keys)
                    Discard(str);
                return;
            }
            System.IO.FileInfo file;
            System.IO.FileStream stream;
            object obj;
            FuzzyObject target;
            if (Origins.TryGetValue(name, out file))
            {
                try
                {
                    stream = file.OpenRead();
                    obj = Serialization.TryGetSerialization(type).Load(stream);
                    if (obj is FuzzyObject) target = obj as FuzzyObject;
                }
                catch (Exception ex)
                {
                    var message = "An Error occured when reloading data for " + name + ":" + System.Environment.NewLine + ex.ToString();
                    Log.log(message);
                }
            }
            else
            {
                var message = "No file named " + name;
                Log.log(message);
            }
        }

        internal static class MapComponent
        {
            static public Dictionary<int, FuzzyData.FuzzyObject> Maps = new Dictionary<int,FuzzyObject>();
            static public void Initialize(string Path,string Name, string Serialize = "")
            {
                Maps.Clear();
                string From = DataEditor.Help.Path.Instance["project"];
                string FullPath = System.IO.Path.Combine(From, Path);
                if (System.IO.Directory.Exists(FullPath))
                {
                    string partName;
                    int index;
                    foreach (var file in System.IO.Directory.GetFiles(FullPath, Name))
                    {
                        partName = System.IO.Path.GetFileName(file);
                        index = SearchNumber(partName);
                        object content = Serialization.LoadFile(file, Serialize);
                        Maps[index] = content as FuzzyObject;
                    }
                }
                else Log.log("找不到地图初始化路径：" + FullPath);
            }
            static public void Initialize(string Path, string Serialize = "")
            {
                string pathPart = System.IO.Path.GetDirectoryName(Path);
                string namePart = System.IO.Path.GetFileName(Path);
                Initialize(pathPart, namePart, Serialize);
            }
            static public int SearchNumber(string name)
            {
                int ans = -1;
                foreach(char c in name)
                {
                    if (c > '0' && c < '9')
                        if (ans < 0) ans = c - '0';
                        else ans = ans * 10 + c - '0';
                    else if (ans >= 0)
                        return ans;
                }
                return ans;
            }
        }
    }
}