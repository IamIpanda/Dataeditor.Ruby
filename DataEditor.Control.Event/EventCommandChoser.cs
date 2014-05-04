using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Event
{
    public partial class EventCommandChoser : Form
    {
        // 入口约定
        Dictionary<int, EventCommand> commands;
        public Dictionary<int, EventCommand> Commands { set { commands = value; } }
        List<CommandGroup> groups;
        public List<CommandGroup> Groups { set { groups = value; } }
        public EventCommandChoser()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            protoListBox1.GetEventItemColor = GroupGetColor;
            protoListBox2.GetEventItemColor = CommandGetColor;
            protoListBox1.Invalidate();
            protoListBox2.Invalidate();
        }

        public int Code { get; set; }
        public FuzzyData.FuzzyArray Value { get; set; }
        public List<FuzzyData.FuzzyObject> With { get; set; }
        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (protoListBox1.SelectedIndex < 0) return;
            protoListBox2.Items.Clear();
            var group = protoListBox1.SelectedItem as CommandGroup;
            if (group == null) return;
            foreach (var command in group.Lists)
                if (command.Follow < 0)
                    protoListBox2.Items.Add(command);
        }
        private void protoListBox2_DoubleClick(object sender, EventArgs e)
        {
            var command = protoListBox2.SelectedItem as EventCommand;
            if (command == null) return;
            DataEditor.Control.WrapBaseWindow window;
            window = new DataEditor.Control.Window.WindowWithOK.WrapWindowWithOK<DataEditor.Control.Window.WindowWithOK>();
            if (command.Window != null)
            {
                FuzzyData.FuzzyArray parameter;
                List<FuzzyData.FuzzyObject> with;
                if (Code != command.Code) { parameter = command.GetParameter(); with = null; }
                else { parameter = Value; with = With; }
                window = command.Window.call(window, parameter, with) as WrapBaseWindow;
                if (window == null) return;
                window.Binding.Text = command.Name;
                window.Value = command.GetParameter();
                if (window.Show() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Code = command.Code;
                    IEnumerable<object> withobj = command.With.call(parameter) as IEnumerable<object>;
                    if (withobj != null)
                    {
                        this.With = new List<FuzzyData.FuzzyObject>();
                        foreach (var obj in withobj)
                            if (obj is FuzzyData.FuzzyObject)
                                this.With.Add(obj as FuzzyData.FuzzyObject);
                    }
                    this.Value = parameter;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) return;
            protoListBox2.Items.Clear();
            foreach (var command in commands.Values)
                if (command.Name.Contains(textBox1.Text))
                    protoListBox2.Items.Add(command);
            if (protoListBox2.Items.Count == 0)
                protoListBox2.Items.Add("没有找到结果...");
        }
        public void Initialize()
        {
            protoListBox1.Items.Clear();
            foreach (var group in groups)
                protoListBox1.Items.Add(group);
            protoListBox1.SelectedIndex = 0;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        Color GroupGetColor(int index)
        {
            if (index < 0) return protoListBox2.ForeColor;
            return (protoListBox1.Items[index] as CommandGroup).Color;
        }
        Color CommandGetColor(int index)
        {
            if (index < 0) return protoListBox2.ForeColor;
            var command = protoListBox2.Items[index] as EventCommand;
            if (command == null) return protoListBox2.ForeColor;
            return command.Color;
        }

        private void protoListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}