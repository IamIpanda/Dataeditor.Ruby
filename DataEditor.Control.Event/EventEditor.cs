using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Event
{
    public partial class EventEditor : Form
    {
        public EventEditor()
        {
            InitializeComponent();
            this.protoMapList1.SelectedValueChanged += protoMapList1_SelectedValueChanged;
            this.protoEventPaper1.SelectedValueChanged += protoEventPaper1_SelectedValueChanged;
        }

        void protoEventPaper1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        void protoMapList1_SelectedValueChanged(object sender, EventArgs e)
        {
            protoEventPaper1.Map = protoMapList1.SelectedValue;
        }
    }
}
