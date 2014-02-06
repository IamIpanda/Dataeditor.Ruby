using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public static class Event
    {
        public static void OnLeave(object sender, EventArgs e)
        {
            // 获取 editor
            Control.ObjectEditor editor =  (sender as System.Windows.Forms.Control).Tag as Control.ObjectEditor;
            if (editor == null) return;
            if (!(editor.CheckValue())) return;
            // 储存旧值，以与 Action 交换
            var old = editor.Value.Clone() as FuzzyData.FuzzyObject;
            // 将控件值上传
            editor.Push();
            var now = editor.Value;
            // 与 Action 交换
            Help.Action.Instance.Do(old, now);
            // 与 Taint 标记
            // 进一步考虑中
            Help.Taint.Instance[now] = Contract.TaintState.Tainted;
            // 与 Monitor 触发
            Help.Monitor.Trigger(editor);
        }
    }
}
