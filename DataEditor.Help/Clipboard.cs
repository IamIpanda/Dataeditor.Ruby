using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Help
{
    public class Clipboard
    {
        static Dictionary<string, Clipboard> clips = new Dictionary<string, Clipboard>();
        public static Clipboard GetClip(string format = null)
        {
            Clipboard clip;
            if (format != null && clips.TryGetValue(format, out clip)) return clip;
            clip = new Clipboard(format);
            clips.Add(clip.ClipboardFormat, clip);
            return clip;
        }
        public string ClipboardFormat { get; set; }
        protected Clipboard(string format = null)
        {
            if (format == null) format = "CLIP" + this.GetHashCode().ToString();
            ClipboardFormat = format;
        }
        public void Set(FuzzyData.FuzzyObject obj)
        {
            var target = Help.Serialization.TrySetValue(obj, "[m]");
            System.Windows.Forms.DataObject db = new System.Windows.Forms.DataObject(ClipboardFormat, target);
            System.Windows.Forms.Clipboard.SetDataObject(db, true, 5, 100);
        }
        public bool CanGet() 
        {
            return ClipboardFormat.Contains(ClipboardFormat);
        }
        public FuzzyData.FuzzyObject Get()
        {
            if (!CanGet()) return null;
            var db = System.Windows.Forms.Clipboard.GetDataObject();
            var str = (byte[])db.GetData(ClipboardFormat);
            return Help.Serialization.TryGetValue(str, "[m]") as FuzzyData.FuzzyObject;
        }
    }
}
