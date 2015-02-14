﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DataEditor.Control.Event.DataModel;
using System.Windows.Forms;
using DataEditor.FuzzyData;

// ReSharper disable CheckNamespace

namespace DataEditor.Control.Window
{
    public partial class MoveRouteWindow : Window.WindowWithOK
    {
        public MoveRouteWindow()
        {
            InitializeComponent();
            base.pnMain.Controls.Add(tableLayoutPanel1);
            var group = Event.DataModel.CommandType.TargetGroup("Move");
            foreach (var type in group.Values)
                if (type.Code > 0)
                    listBox1.Items.Add(type);
        }

        private void protoEventCommandList1_EditCalled(object sender, EventArgs e)
        {

        }

        private void protoEventCommandList1_InsertCalled(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var commandType = listBox1.SelectedItem as Event.DataModel.CommandType;
            if (commandType == null) return;
            var move = new MoveCommand(commandType);
            if (commandType.Window != null)
            {
                var window = move.GenerateWindow(new List<Command>());
                if (window.Show() != DialogResult.OK) return;
            }
            protoEventCommandList1.Insert(move);
        }

        List<MoveCommand> _commands = null;
        public List<MoveCommand> Value
        {
            get { return _commands; }
            set
            {
                _commands = value;
                protoEventCommandList1.Items.Clear();
                value.ForEach((x) => protoEventCommandList1.Items.Add(x));
                if (protoEventCommandList1.Items.Count > 0) protoEventCommandList1.SelectedIndex = 0;
            }
        }

        public class MoveDialog : Window.WindowWithOK.WrapWindowWithOK<MoveRouteWindow>
        {
            public override string Flag
            {
                get { return "dialog_move"; }
            }

            public override void Pull()
            {
                base.Pull();
                var list = this.value["@list"] as FuzzyData.FuzzyArray;
                if (list == null) return;
                var fuzzyList = list.Select(command => new MoveCommand(command as FuzzyData.FuzzyObject)).ToList();
                Window.Value = fuzzyList;
            }

            public override void Push()
            {
                base.Push();
                var list = this.value["@list"] as FuzzyData.FuzzyArray;
                list.Clear();
                Window.Value.ForEach((move) => list.Add(move.Link));
            }

            public override void SetSize(Size size) { }
        }

        class MoveCommandList : Prototype.ProtoEventCommandList
        {
            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                if (e.Index < 0) return;
                if (Items.Count == 0 || e.Index >= Items.Count) return;
                if (Items.Count == 0 && Focused)
                { DrawFocusRectangle(e.Graphics, e.Bounds); return; }
                var command = GetCommand(e.Index);
                if (command == null) return;
                if (e.Index == Items.Count - 1) DrawRestItems(e);
                var focused = GetFocused(e.State);
                // 获取背景
                Brush BackBrush = focused ? GetFocusBrush(e) : GetBackColor(e);
                e.Graphics.FillRectangle(BackBrush, e.Bounds);
                // 焦点框
                if (focused)
                    DrawFocusRectangle(e.Graphics, e.Bounds);
                // 开始描绘
                Brush ForeBrush = GetEventBrush(e, command, focused);
                var str = command.ToString();
                // 生成的内容
                e.Graphics.DrawString(str, Font, ForeBrush, 0, e.Bounds.Y);
            }
        }
    }
}
