using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataEditor.Control.ShapeShifter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var window = new ObjectChecker();
            var ofd = new OpenFileDialog();
            ofd.Title = "打开文件";
            ofd.Filter = "Ruby 文件|*.*";
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var ser = Help.Serialization.TryGetSerialization("[m]");
                    var stream = new System.IO.FileStream(ofd.FileName, System.IO.FileMode.Open);
                    var arr = new FuzzyData.FuzzyArray();
                    try
                    {
                        while (true)
                            arr.Add(ser.Load(stream));
                    }
                    catch { }
                    window.Value = arr;
                    Application.Run(window);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序遇到了一个错误。程序即将退出。" + ex.ToString(), "消息");
            }
        }
    }
}
