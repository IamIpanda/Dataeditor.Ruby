using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Event.Wrapper
{
    public partial class ConsideringEventSwitch_Choser : Form
    {
        public ConsideringEventSwitch_Choser()
        {
            InitializeComponent();
            plp.textbook = new Help.Parameter.Text(GetString);
        }
        public object GetString(params object[] args)
        {
            int index = Convert.ToInt32(args[0]);
            string value = args[1].ToString();
            return string.Format("{0:d3}: {1}", index, value);
        }
    }
}
