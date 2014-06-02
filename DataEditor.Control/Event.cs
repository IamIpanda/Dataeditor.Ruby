using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public static class Event
    {
        public static void OnLeave(object sender, EventArgs e)
        {
            // 获取 control
            System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
            // 获取 editor
            Control.ObjectEditor editor = control.Tag as Control.ObjectEditor;
            // 重置颜色标记
            if (editor.HighLight)
                Help.Painter.Instance.PopColor(control);
            if (editor == null) return;
            if (!(editor.ValueIsChanged())) return;
            // 储存旧值，以与 Action 交换
            var old = editor.Value.Clone() as FuzzyData.FuzzyObject;
            // 将控件值上传
            editor.Push();
            var now = editor.Value;
            // 与 Action 交换
            Help.Action.Instance.Do(old, now);
            // 与 Taint 标记
            // 进一步考虑中
            Help.Taint.Instance.SetTaint(editor);
            // 与 Monitor 触发
            Help.Monitor.Trigger(editor);
        }
        public static void OnEnter(object sender, EventArgs e)
        {
            // 获取 control
            System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
            // 获取 editor
            Control.ObjectEditor editor = control.Tag as Control.ObjectEditor;
            if (editor.HighLight)
                Help.Painter.Instance.PushColor(control);
        }
    }
}
