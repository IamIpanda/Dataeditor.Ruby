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


    static public partial class Monitor
    {
        static Dictionary<FuzzyData.FuzzyObject, List<Parameter.Text>> text_verse
            = new Dictionary<FuzzyData.FuzzyObject, List<Parameter.Text>>();
        static Dictionary<Parameter.Text, List<FuzzyData.FuzzyObject>> text_reverse
            = new Dictionary<Parameter.Text, List<FuzzyData.FuzzyObject>>();
        static Dictionary<Parameter.Text, EventHandler> text_events = new Dictionary<Parameter.Text, EventHandler>();
        public static void Watch(Parameter.Text text, EventHandler trigger, params FuzzyData.FuzzyObject[] target)
        {
            if (trigger == null || text == null || target.Length == 0) return;
            if (text_events.ContainsKey(text)) text_events[text] = trigger;
            else text_events.Add(text, trigger);
            if (text_reverse.ContainsKey(text)) { text_reverse[text].AddRange(target); }
            else text_reverse.Add(text, new List<FuzzyData.FuzzyObject>(target));
            foreach (var value in target)
                if (text_verse.ContainsKey(value)) text_verse[value].Add(text);
                else text_verse.Add(value, new List<Parameter.Text>() { text });
        }
        public static void Remove(Parameter.Text editor)
        {
            List<FuzzyData.FuzzyObject> watches = null;
            text_reverse.TryGetValue(editor, out watches);
            if (watches == null) return;
            foreach (var target in watches)
                text_verse[target].Remove(editor);
            text_reverse.Remove(editor);
        }
        public static void Trigger(Parameter.Text changed_text, FuzzyData.FuzzyObject changed_value)
        {
            List<Parameter.Text> recall = null;
            text_verse.TryGetValue(changed_value, out recall);
            if (recall == null) return;
            foreach (var editor in recall)
                text_events[editor](changed_text, new EventArgs());
        }
    }
}
