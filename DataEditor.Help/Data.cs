using System;
using System.Text;
using DataEditor.FuzzyData;
using System.Collections.Generic;

namespace DataEditor.Help
{
    public class Data
    {
        public static Data Instance { get; set; }
        static Data() { Instance = new Data(); }
        protected Data () { }
        protected Dictionary<string, FuzzyObject> Datas = new Dictionary<string, FuzzyObject>();
        public FuzzyObject this[string name]
        {
            get
            {
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
                    string description = "������������һ������Ϊ " + name + " ���ļ���\n��Ĭ�ϵĵ��ļ�����û���ҵ���Ϊ " + value + " ���ļ�����ָ��֮��";
                    System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                    dialog.Title = "���� " + name;
                    System.Windows.Forms.MessageBox.Show(description, dialog.Title,System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Question); 
                    while (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) ;
                    ans = dialog.FileName;
                }
                else if (files.Length > 1)
                {
                    string description = "������������һ������Ϊ " + name + " ���ļ���\n�����ҵ����������ļ�����ѡ��Ҫƥ�����һ��";
                    DataEditor.Control.Window.SelectWindow window = new Control.Window.SelectWindow();
                    int i = -1;
                    while (i < 0) { i = window.ShowDialog("ѡ���ļ�", description, 0, false, files); }
                    ans = files[i];
                }
                else ans = files[0];
                object content = Serialization.LoadFile(ans, serialize);
                Datas.Add(name, content as FuzzyObject);
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
                else Log.log("�Ҳ�����ͼ��ʼ��·����" + FullPath);
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