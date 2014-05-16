using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoEventPaper: UserControl
    {
        public FuzzyData.FuzzySymbol EventsSymbol { get; set; }
        public FuzzyData.FuzzySymbol NameSymbol { get; set; }
        public FuzzyData.FuzzySymbol IDSymbol { get; set; }
        public Dictionary<int, int> Links { get; set; }
        public ProtoEventPaper()
        {
            InitializeComponent();
            EventsSymbol = FuzzyData.FuzzySymbol.GetSymbol("@events");
            NameSymbol = FuzzyData.FuzzySymbol.GetSymbol("@name");
            IDSymbol = FuzzyData.FuzzySymbol.GetSymbol("@id");
            Links = new Dictionary<int, int>();

        }
        FuzzyData.FuzzyObject map;
        FuzzyData.FuzzyHash event_hash;
        public FuzzyData.FuzzyObject Map
        {
            get { return map; }
            set 
            {
                map = value;
                OnSetMap();
            }
        }
        public void OnSetMap()
        {
            if (map == null) return;
            protoListBox1.Items.Clear();
            event_hash = map[EventsSymbol] as FuzzyData.FuzzyHash;
            if (event_hash == null) return;
            int index;
            FuzzyData.FuzzyObject obj;
            foreach(var key in event_hash.Keys)
            {
                obj = event_hash[key]  as FuzzyData.FuzzyObject;
                if (!(key is FuzzyData.FuzzyFixnum)) continue;
                index = Convert.ToInt32((key as FuzzyData.FuzzyFixnum).Value);
                Links.Add(protoListBox1.Items.Count, index);
                protoListBox1.Items.Add(obj[NameSymbol].ToString());
            }
        }
        public FuzzyData.FuzzyObject SelectedValue
        { 
            get 
            {
                if (event_hash == null) return null;
                int index = protoListBox1.SelectedIndex;
                if (index < 0) return null;
                int key = Links[index];
                return event_hash[key] as FuzzyData.FuzzyObject;
            } 
        }
        public event EventHandler SelectedValueChanged;

        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedValueChanged != null) SelectedValueChanged(this, e);
        }
    }
}
