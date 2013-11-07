using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Ruby
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent();
            RubyEngine engine = new RubyEngine();
            engine.ExecuteFile("Ruby/main.rb");
         }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
