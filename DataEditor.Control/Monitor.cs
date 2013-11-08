using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Control;

namespace DataEditor.Help
{
    static public partial class Monitor
    {
        static Dictionary<FuzzyData.FuzzyObject, List<ObjectEditor>> verse
            = new Dictionary<FuzzyData.FuzzyObject, List<ObjectEditor>>();
        static Dictionary<ObjectEditor, List<FuzzyData.FuzzyObject>> reverse
            = new Dictionary<ObjectEditor, List<FuzzyData.FuzzyObject>>();
        static Dictionary<ObjectEditor, EventHandler> Events = new Dictionary<ObjectEditor, EventHandler>();
        public static void Watch(Control.ObjectEditor editor, EventHandler trigger, params FuzzyData.FuzzyObject[] target)
        {
            if (trigger == null || editor == null || target.Length == 0) return;
            if (Events.ContainsKey(editor)) Events[editor] = trigger;
            else Events.Add(editor, trigger);
            if (reverse.ContainsKey(editor)) { reverse[editor].AddRange(target); }
            else reverse.Add(editor, new List<FuzzyData.FuzzyObject>(target));
            foreach (var value in target)
                if (verse.ContainsKey(value)) verse[value].Add(editor);
                else verse.Add(value, new List<ObjectEditor>() { editor });
        }
        public static void Remove(ObjectEditor editor)
        {
            List<FuzzyData.FuzzyObject> watches = null;
            reverse.TryGetValue(editor, out watches);
            if (watches == null) return;
            foreach (var target in watches)
                verse[target].Remove(editor);
            reverse.Remove(editor);
        }
        public static void Trigger(Control.ObjectEditor changed_editor)
        {
            Trigger(changed_editor, Link.Instance[changed_editor]);
        }
        public static void Trigger(Control.ObjectEditor changed_editor, FuzzyData.FuzzyObject changed_value)
        {
            List<ObjectEditor> recall = null;
            verse.TryGetValue(changed_value, out recall);
            if (recall == null) return;
            foreach (var editor in recall)
                Events[editor](changed_editor, new EventArgs());
        }
    }
}
