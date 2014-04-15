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
    public partial class ProtoMapList : UserControl
    {
        public FuzzyData.FuzzySymbol NameSymbol { get; set; }
        public Dictionary<int, int> Links { get; set; }
        public ProtoMapList()
        {
            InitializeComponent();
            NameSymbol = FuzzyData.FuzzySymbol.GetSymbol("@name");
            Links = new Dictionary<int,int>();
            this.SetStyle(ControlStyles.ContainerControl, true);
        }
        private void MapList_Load(object sender, EventArgs e)
        {
            Links.Clear();
            var info = Help.Data.Instance[-1]; // Mapinfo
            var map_info = info as FuzzyData.FuzzyHash;
            if (map_info == null) return;
            int index;
            FuzzyData.FuzzyObject obj;
            foreach (var key in map_info.Keys)
            {
                obj = map_info[key] as FuzzyData.FuzzyObject;
                if (!(key is FuzzyData.FuzzyFixnum) || obj == null) continue;
                index = Convert.ToInt32((key as FuzzyData.FuzzyFixnum).Value);
                Links.Add(protoListBox1.Items.Count, index);
                protoListBox1.Items.Add(obj[NameSymbol].ToString());
            }
        }
        public FuzzyData.FuzzyObject SelectedValue
        {
            get
            {
                int index = protoListBox1.SelectedIndex;
                if (index < 0) return null;
                int key = Links[index];
                return Help.Data.Instance[key];
            }
        }

        public event EventHandler SelectedValueChanged;
        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedValueChanged != null) SelectedValueChanged(this, e);
        }
    }
}
