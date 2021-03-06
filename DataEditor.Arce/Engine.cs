﻿using DataEditor.Ruby;
using System;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    static public class Engine
    {
        static public RubyEngine engine { get; set; }

        static Engine()
        {
            engine = new RubyEngine();
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Window.EditorWindow).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Wrapper.Text).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Event.DataModel.Command).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.ShapeShifter.ShapeShifter).Assembly);
            engine.LoadAssembly(typeof(DataEditor.Control.Window.EditorWindow).Assembly);
            engine.LoadAssembly(typeof(DataEditor.Control.Event.DataModel.Command).Assembly);
            Help.Bash.RubyEngine = ((str) => engine.Execute(str));
            Help.Bash.RequestAbout = About;
            Engine.engine.Execute(@"require ""Ruby/main.rb""");
        }

        static public void OpenProject(string Path)
        {
            try
            {
                var proc = (IronRuby.Builtins.Proc)engine.Execute(@"Lead.Open_project");
                Help.Path.Instance["project"] = System.IO.Path.GetDirectoryName(Path);
                IronRuby.Builtins.MutableString str = IronRuby.Builtins.MutableString.Create(Path, IronRuby.Builtins.RubyEncoding.UTF8);
                proc.Call(proc, str);
                (Help.Window.Instance["Main"] as WrapUranus).Window.Show();
            }
            catch (Exception ex)
            {
                Help.Log.log(ex.ToString());
                MessageBox.Show("在构筑窗口过程中发生了一个错误。" + Environment.NewLine + ex.ToString());
            }
            finally
            {
                Help.Loading.EndLoading();
            }
        }

        static public void OpenFile(string Path)
        {
            engine.Execute(@"Lead.open_file (""" + Path + @""")");
        }

        static public void OpenRubyFile(string Path)
        {
            engine.ExecuteFile(Path);
        }

        static public void OpenOption()
        {
            if (new Option().ShowDialog() == DialogResult.OK)
                Conquer();
        }

        static public void Exit()
        {
            System.Windows.Forms.Application.Exit();
        }

        private static SaveDialog saveDialog = new SaveDialog();

        static public void Save()
        {
            saveDialog.ShowDialog();
        }

        private static SaveAsDialog saveAsDialog = new SaveAsDialog();

        static public void SaveAs()
        {
            saveAsDialog.ShowDialog();
        }

        static public void Undo()
        {
            Help.Action.Instance.Undo();
        }

        static public void Redo()
        {
            Help.Action.Instance.Redo();
        }

        private static DataEditor.Control.ShapeShifter.ShapeShifter sp
            = new Control.ShapeShifter.ShapeShifter();

        static public void CallEditor()
        {
            if (sp == null || sp.IsDisposed) sp = new Control.ShapeShifter.ShapeShifter();
            sp.Show();
            sp.Focus();
        }

        static public void Run()
        {
            var dialog = new DataEditor.Control.Prototype.ProtoExecuteDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                object ans = Help.Bash.Call(dialog.Value);
                if (ans != null && ans is FuzzyData.FuzzyObject)
                    Help.ShapeShifter.ShowObject(ans as FuzzyData.FuzzyObject, "执行结果");
            }
        }

        static public void Execute()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择文件";
            dialog.Filter = "Ruby 文件|*.rb|所有文件|*.*";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() != DialogResult.OK) return;
            var result = MessageBox.Show("您将要执行下述文件：" + Environment.NewLine + dialog.FileName + Environment.NewLine
            + "请！清！楚！的！确认自己在做什么之后，点击确定。", "执行确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result != DialogResult.OK) return;
            engine.ExecuteFile(dialog.FileName);
        }

        private static ChangeInWaveDialog wave = new ChangeInWaveDialog();

        static public void Wave()
        {
            wave.ShowDialog();
        }
        static public void Conquer()
        {
            var main = Help.Window.Instance["main"] as WrapUranus;
            if (main != null) main.Conquer();
            Help.Environment.Instance.Conquer();
        }
        static public void SaveAll()
        {
            Help.Data.Instance.Save();
            Help.Bash.SetStatus("已保存。");
            new System.Threading.Thread(() => {
                System.Threading.Thread.Sleep(2000);
                if (Help.Bash.GetStatus() == "已保存。")
                    Help.Bash.SetStatus("已就绪。");
            }).Start();
        }
        static public void About()
        {
            new AboutDialog().ShowDialog();
        }
    }
}