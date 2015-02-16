using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataEditor.Control.Event.DataModel;
using DataEditor.FuzzyData;
using DataEditor.Help;

namespace DataEditor.Control.Window
{
    public partial class CommandChooseWindow : Form
    {
        private const string all_types = "全部";

        public CommandChooseWindow()
        {
            InitializeComponent();
            cbMain.Items.Add(all_types);
            cbMain.Items.AddRange(Event.DataModel.CommandGroup.Groups.ToArray());
            cbMain.SelectedIndex = 0;
            StartingIndent = 0;
        }

        private void cbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMain.SelectedIndex < 0) return;
            lb.Items.Clear();
            var obj = cbMain.Items[cbMain.SelectedIndex];
            if (obj.ToString() == all_types)
                foreach (var command in Event.DataModel.CommandType.TargetGroup().Values)
                    if (!(command.isChildCommand) && command.Code > 0)
                        lb.Items.Add(command);
                    else { }
            else foreach (var command in (obj as Event.DataModel.CommandGroup).Components)
                    if (!(command.isChildCommand) && command.Code > 0)
                        lb.Items.Add(command);
        }
        public class CommandTypeWindow : Prototype.ProtoListBox
        {
            protected override Brush GetForeColor(DrawItemEventArgs e)
            {
                var type = this.Items[e.Index] as Event.DataModel.CommandType;
                if (type == null || GetFocused(e.State)) return base.GetForeColor(e);
                else return Prototype.ProtoListControlHelp.GetBrush(CheckEnabled(type.Color));
            }
        }
        public class GroupTypeWindow:Prototype.ProtoComboBox
        {
            protected override Brush GetForeBrush(DrawItemEventArgs e)
            {
                var group = this.Items[e.Index] as Event.DataModel.CommandGroup;
                if (group == null || (e.State & DrawItemState.Selected) != 0) return base.GetForeBrush(e);
                else return Prototype.ProtoListControlHelp.GetBrush(Prototype.ProtoListControlHelp.CheckEnabled(group.Color, this.Enabled));
            }
        }

        private void cbMain_TextChanged(object sender, EventArgs e)
        {
            if (cbMain.Items.Contains(cbMain.Text)) return;
            lb.Items.Clear();
            if (cbMain.Text == "")
                foreach (var command in Event.DataModel.CommandType.TargetGroup().Values)
                    if (!(command.isChildCommand) && command.Code > 0)
                        lb.Items.Add(command);
                    else { }
            else foreach (var command in Event.DataModel.CommandType.TargetGroup().Values)
                    if (!(command.isChildCommand) && command.Code > 0)
                        if (command.Name.Contains(cbMain.Text) || command.Code.ToString().Contains(cbMain.Text))
                            lb.Items.Add(command);
            if (lb.Items.Count == 0)
                lb.Items.Add("没有搜索到指令....");
        }

        private void cbMain_DropDown(object sender, EventArgs e)
        {
            lb.Enabled = false;
        }

        private void cbMain_DropDownClosed(object sender, EventArgs e)
        {
            lb.Enabled = true;
        }

        private void CommandChooseWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        public int StartingIndent { get; set; }

        private void lb_DoubleClick(object sender, EventArgs e)
        {
            var type = SelectedCommandType;
            selectedCommands = new List<Command>();
            var command = new Command(type, StartingIndent);
            selectedCommands.Add(command);
            var window = command.GenerateWindow(new List<Command>());
            this.Hide();
            DialogResult? result = null;
            if (type.Window != null) result = window.Show();
            if (result == null || result == DialogResult.OK)
            {
                command.FuzzyParameters = window.Parent as FuzzyArray;
                if (type.With != null)
                {
                    var with = type.With.call(window, new List<object>());
                    var list = with as IEnumerable<object>;
                    if (list != null)
                        foreach (var obj in list.OfType<Command>())
                            selectedCommands.Add(obj);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else this.Show();
        }

        public Event.DataModel.CommandType SelectedCommandType
        {
            get { return lb.SelectedItem as Event.DataModel.CommandType; }
        }

        private List<Event.DataModel.Command> selectedCommands = null;
        public List<Event.DataModel.Command> SelectedCommands
        {
            get { return selectedCommands; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
