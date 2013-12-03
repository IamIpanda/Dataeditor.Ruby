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
    }
}