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
        public string this[string name, string serialize = ""]
        {
            set
            {
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
            }
        }
    }
}