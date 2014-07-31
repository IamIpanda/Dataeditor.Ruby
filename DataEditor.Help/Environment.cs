using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    [Serializable]
    public class Environment
    {
        public bool EnableAutoSave { get; set; }
        public int AutoSaveTimeSpan { get; set; }
        public bool EnableAutoHint { get; set; }
        public bool EnableFloatWindow { get; set; }
        public bool EnableLoading { get; set; }
        public bool EnableVAType { get; set; }
        static public Environment Instance { get; set; }
        static Environment()
        {
            Instance = Option.GetOption(typeof(Environment)) as Environment ?? new Environment();
            Instance.Conquer();
        }
        static public void Save()
        {
            Option.SetOption(Instance, Instance);
        }
        private Environment()
        {
            this.EnableAutoHint = false;
            this.EnableAutoSave = true;
            this.EnableLoading = false;
            this.EnableFloatWindow = true;
            this.EnableVAType = false;
            this.AutoSaveTimeSpan = 2;
        }
        public void Conquer()
        {
            DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Options.StringStyle = Instance.EnableVAType ?
                FuzzyData.Serialization.RubyMarshal.RubyMarshal.Options.StringStyleType.Style192
                : FuzzyData.Serialization.RubyMarshal.RubyMarshal.Options.StringStyleType.Style184;
        }

    }
}
