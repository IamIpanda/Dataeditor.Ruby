using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class MapList : UserControl
    {
        public MapList()
        {
            InitializeComponent();
        }

        private void MapList_Load(object sender, EventArgs e)
        {
            var maps = Help.Data.Instance.Map;
            var info = maps[-1];
            var map_info = info as FuzzyData.FuzzyHash;
            if (map_info == null) return;
            // TODO FINISH
        }
    }
}
