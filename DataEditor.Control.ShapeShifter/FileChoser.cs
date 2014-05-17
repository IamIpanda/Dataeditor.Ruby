using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class FileChoser : UserControl
    {
        public FuzzyData.FuzzyObject Value { get; set; }
        public FuzzyData.FuzzyObject SelectedValue { get; set; }
        public FileChoser()
        {
            InitializeComponent();
        }
    }
}
