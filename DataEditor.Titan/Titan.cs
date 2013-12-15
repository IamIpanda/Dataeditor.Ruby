using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataEditor.FuzzyData;

namespace DataEditor.Titan
{
    public partial class Titan : DataEditor.Control.Window.EditorWindow
    {
        public Titan ()
        {
            InitializeComponent();
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            new DataEditor.Control.Wrapper.Container.List();
            Help.Path.Instance["project"] = "Test/PjVATst/";
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Window.EditorWindow).Assembly);
            Help.Collector.AddAssembly(typeof(DataEditor.Control.Wrapper.Text).Assembly);

            var engine = new Ruby.RubyEngine();
            object x = Help.Serialization.LoadFile("Test/PjVATst/Data/Actors.rvdata2");
            engine["FData"] = x;
            engine.ExecuteFile("Ruby/main.rb");
        }
    }
}
