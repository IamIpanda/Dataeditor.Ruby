﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataEditor.Help;

namespace DataEditor.Control.Wrapper
{
    /*
    public partial class Image_Choser : Form
    {
        public FuzzyData.FuzzyComplex Value { get; set; }
        public string Path { get; set; }
        public override string Text
        {
            get { return TitleBox.Text; }
            set { TitleBox.Text = value; }
        }
        public Size BlockSize { get { return MainImage.ClipSize; } set { MainImage.ClipSize = value; } }
        protected void InitializeRTP()
        {
            RTPChoser.Items.Clear();
            RTPChoser.Items.Add("全部");
            foreach (Rtp rtp in Help.Path.RTPManager.RtpList)
                RTPChoser.Items.Add(rtp);
            RTPChoser.SelectedIndex = 0;
        }

        private void RTPChoser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            // 搜寻图像
            Help.Path.Instance.SearchFiles(System.IO.Path.Combine(Path, "*.*"), "project", "rtp");
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btOK_Click(object sender, EventArgs e)
        {
             // push()
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void Image_Choser_Shown(object sender, EventArgs e)
        {
            RTPChoser.Items.Clear();
            RTPChoser.Items.Add("全部");
            foreach (Rtp rtp in Help.Path.RTPManager.RtpList)
                RTPChoser.Items.Add(rtp);
            RTPChoser.SelectedIndex = 0;
        }
    }
     */

    public partial class Image_Choser : Form
    {
        public FuzzyData.FuzzyComplex Value { get; set; }
        public string Path { get; set; }
        public Image.SplitManager Split { get; set; }
        public new Image.SplitManager Show { get; set; }
        public Image_Choser()
        {
            InitializeComponent();
        }
        public new string Text
        {
            get { return TitleBox.Text; }
            set { TitleBox.Text = value; base.Text = value; }
        }

        public string FileName
        {
            get
            {
                if (fileList.Items.Count == 0) return "";
                System.IO.FileInfo file = fileList.ChosenFile;
                if (file == null) return "";
                return System.IO.Path.GetFileNameWithoutExtension(file.FullName);
            }
            set
            {
                if (fileList.Items.Count == 0) return;
                foreach (object ob in fileList.Items)
                    if (ob.ToString() == value)
                    { fileList.SelectedItem = ob; break; }
            }
        }

        protected void InitializeRTP()
        {
            RTPChoser.Items.Clear();
            RTPChoser.Items.Add("全部");
            foreach (Rtp rtp in Help.Path.RTPManager.RtpList)
                RTPChoser.Items.Add(rtp);
            RTPChoser.SelectedIndex = 0;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (Value != null)
            {
                 if (Value["FILE"] != null) (Value["FILE"] as FuzzyData.FuzzyString).Text = FileName;
                 if (Value["INDEX"] != null) (Value["INDEX"] as FuzzyData.FuzzyFixnum).Value = MainImage.Index;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void Image_Choser_Shown(object sender, EventArgs e)
        {
            InitializeRTP();
            /*
            if (value != null)
            {
                if (value.ImageName != null)
                    this.FileName = value.ImageName.Text;
                if (value.ImageIndex != null)
                    MainImage.Index = (int)value.ImageIndex.Value;
            }
             * */
        }

        private void RTPChoser_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetListbox();
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex == 0)
            {
                MainImage.Bitmap = null;
                return;
            }
            System.IO.FileInfo file = fileList.ChosenFile;
            if (file == null) return;
            if (!file.Exists) return;
            Bitmap bitmap = new Bitmap(file.FullName);
            string name = file.Name;
            Show.SearchSplit(ref name);
            Help.Parameter.Split split = Split.SearchSplit(ref name);
            Rectangle rect = split[0, 0, bitmap.Width, bitmap.Height];
            MainImage.ClipSize = rect.Size;
            MainImage.Bitmap = bitmap;
            SetText();
        }

        private void MainImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btOK_Click(sender, e);
        }

        protected void SearchFile(Rtp rtp, string path)
        {
            string full = System.IO.Path.Combine(rtp.Path, path);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(full);
            if (!dir.Exists) return;
            fileList.AddChildFile(dir, rtp.Color);
        }
        protected void SetListbox()
        {
            fileList.Items.Clear();
            fileList.Clear();
            fileList.AddFlag("（无）", Color.Black);
            int index = RTPChoser.SelectedIndex;
            if (index == -1) return;
            else if (index == 0)
                foreach (Rtp rtp in Help.Path.RTPManager.RtpList)
                    SearchFile(rtp, Path);
            else
                SearchFile(Help.Path.RTPManager.RtpList[index - 1], Path);
        }
        protected void SetBitmap(Bitmap bitmap)
        {
            MainImage.Bitmap = bitmap;
            MainImage.Index = 0;
        }

        protected void SetText()
        {
            int bs = MainImage.Blocks;
            if (bs == 1)
                btIndex.Text = FileName;
            else
                btIndex.Text = FileName + " ( " + (MainImage.Index + 1) + " / " + bs + " ) ";
        }

        private void MainImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetText();
        }
    }
}