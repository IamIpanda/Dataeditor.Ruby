using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoRtpViewList : ProtoColoredListBox
    {
        static public List<string> FileExtands = new List<string>() { ".jpg", ".jpeg", ".png", ".bmp" };
        public List<string> Extands { get; set; }
        public List<FileSystemInfo> Files = new List<FileSystemInfo>();
        public ProtoRtpViewList()
        {
            Extands = FileExtands;
        }
        protected Color ChangeColorForDirectory(Color Origin)
        {
            return System.Drawing.Color.FromArgb(200, Origin.R / 2, Origin.G / 2, Origin.B / 2);
        }
        protected bool CheckFile(FileInfo info)
        {
            string Extand = info.Extension;
            return Extands.Contains(Extand);
        }
        public void AddDirectory(DirectoryInfo dir, System.Drawing.Color color,string name = "")
        {
            Items.Add(name == "" ? dir.Name : name);
            Files.Add(dir);
            Color.Add(color);
        }
        public void AddChildDirectory(DirectoryInfo dir,System.Drawing.Color color,bool WithSelf = false)
        {
            System.Drawing.Color TrueColor = ChangeColorForDirectory(color);
            if (WithSelf)
                AddDirectory(dir.Parent, TrueColor, "..");
            foreach (DirectoryInfo child in dir.GetDirectories())
                AddDirectory(child, TrueColor);
        }
        public void AddFile(FileInfo file, System.Drawing.Color color)
        {
            Items.Add(System.IO.Path.GetFileNameWithoutExtension(file.Name));
            Files.Add(file);
            Color.Add(color);
        }
        public void AddChildFile(DirectoryInfo dir, System.Drawing.Color color)
        {
            foreach (FileInfo file in dir.GetFiles())
                if (CheckFile(file))
                    AddFile(file, color);
        }
        public void AddFlag(string flag, Color color)
        {
            Items.Add(flag);
            Files.Add(null);
            Color.Add(color);
        }
        public void Clear()
        {
            Items.Clear();
            Color.Clear();
            Files.Clear();
        }
        public FileInfo ChosenFile
        {
            get { return SelectedIndex >= 0 ? Files[SelectedIndex] as FileInfo : null; }
        }
        public DirectoryInfo ChosenDirectory
        {
            get { return SelectedIndex >= 0 ? Files[SelectedIndex] as DirectoryInfo : null; }
        }
    }
}