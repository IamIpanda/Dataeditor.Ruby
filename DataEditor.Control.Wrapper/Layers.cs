using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Layers : WrapControlEditor<FuzzyData.FuzzyComplex, Prototype.ProtoDropItem>
    {
        public struct LayerData
        {
            public string name;
            public string dir;
            public LayerData(string name, string dir)
            {
                this.name = name;
                this.dir = dir;
            }
        }
        bool isChanged = false;
        List<LayerData> layers = new List<LayerData>();
        
        public override string Flag { get { return "layers"; } }
        public override void Push()
        {
            isChanged = true;
        }
        public override void Pull()
        {
            object[] obs = new List<FuzzyData.FuzzyObject>(value.AllValues).ToArray();
            Control.Text = string.Concat(obs);
        }
        public override bool ValueIsChanged()
        {
            return isChanged;   
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("names", null, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("dir", null, Help.Parameter.ArgumentType.Must);
            argument.SetArgument("rtp", "RPGVXAce", Help.Parameter.ArgumentType.HardlyEver);
        }
        public override void Reset()
        {
            base.Reset();
            var names = argument.GetArgument<Dictionary<object, object>>("names");
            var dir = argument.GetArgument<Dictionary<object, object>>("dir");
            if (names == null)
            {
                names = new Dictionary<object,object>();
                foreach (string key in value.AllKeys)
                    names.Add(key, key);
            }
            layers.Clear();
            foreach(string key in value.AllKeys)
                layers.Add(new LayerData(names[key].ToString(), dir[key].ToString()));
            
        }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {
            Layers_Dialog dialog = new Layers_Dialog();
            
        }
    }
}
