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
            engine.LoadAssembly(typeof(Form1).Assembly);
            dynamic x = 0;
            object y = engine.Engine.Execute(
                @":ass
            ");
            x = engine.ExecuteFile(@"X:/VS Projects/DataEditor/空想科学/Initialize.rb");
         }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
