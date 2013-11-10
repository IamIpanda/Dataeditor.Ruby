using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Titan
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent();
            var engine = new Ruby.RubyEngine();
            Help.Serialization.LoadFile("Test/PjVATst/Data/Actors.rvdata2");
            dynamic x = engine.ExecuteFile("Ruby/main.rb");
         }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
