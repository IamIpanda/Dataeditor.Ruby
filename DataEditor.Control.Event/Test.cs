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
    public partial class Test : Form
    {
        DataEditor.Ruby.RubyEngine engine = new Ruby.RubyEngine();
        public Test()
        {
            InitializeComponent();
            engine.LoadAssembly(typeof(Test).Assembly);
            Help.Collector.AddAssembly(typeof(Control.Wrapper.Choose).Assembly);
            Help.Collector.AddAssembly(typeof(Test).Assembly);
            Help.Collector.AddAssembly(typeof(Control.Window.WindowWithOK).Assembly);
            engine.Execute("$LOAD_PATH << \"\\\\\\\\psf\\\\FILE\\\\VSProjects\\\\dataeditor.ruby\\\\DataEditor.Arce\"");
            engine.Execute("Dir.chdir \"\\\\\\\\psf\\\\FILE\\\\VSProjects\\\\dataeditor.ruby\\\\DataEditor.Arce\" ");
            Help.Path.Instance["project"] = "\\\\psf\\FILE\\RM\\XP\\pj2015-02-02";
            FuzzyData.Serialization.Factory<byte[]>.SearchFactor(typeof(FuzzyData.FuzzyTable).Assembly);
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\main.rb");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\XP\\File - xp.rb");
            engine.ExecuteFile("\\\\psf\\FILE\\VSProjects\\dataeditor.ruby\\DataEditor.Arce\\Ruby\\XP\\Event - xp.rb");
            Ruby.RubyBuilder.In(new DataEditor.Control.Wrapper.Container.Group());
            foreach (var item in protoEventCommandList1.Items)
                item.ToString();
        }
    }
}
