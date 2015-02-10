using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby.Builtins;
using DataEditor.Control;
using DataEditor.Help;

namespace DataEditor.Ruby
{
    public class RubyBuilder
    {
        public enum ControlOrder { Row, Column };
        protected int max_x = 0, max_y = 0;
        protected int now_x = 0, now_y = 0;
        protected int now_w = 0, now_h = 0;
        protected int head_x = 0, head_y = 0;
        protected ControlOrder mode = ControlOrder.Row;
        protected System.Windows.Forms.Control last;
        protected System.Windows.Forms.Control.ControlCollection target;
        protected DataContainer container;
        protected System.Windows.Forms.Padding margin;

        protected static RubyBuilder StackTop = null;

        protected RubyBuilder(DataContainer container, int default_head_x = 0, int default_head_y = 0)
        {
            if (default_head_x == 0) default_head_x = container.start_x;
            if (default_head_y == 0) default_head_y = container.start_y;
            max_x = head_x = now_x = default_head_x;
            max_y = head_y = now_y = default_head_y;
            this.container = container;
            this.target = container.Controls;
            this.last = new System.Windows.Forms.Control();
            this.last.Size = new System.Drawing.Size(1, 1);
            this.margin = new System.Windows.Forms.Padding(0, 0, 6, 3);
        }
        protected virtual Control.ObjectEditor SearchControl(RubySymbol type)
        {
            string name = type.ToString();
            return Help.Collector.Instance[name] as Control.ObjectEditor;
        }
        protected virtual Control.ObjectEditor NotNamedControl(RubySymbol tpye) { return null; }
        protected virtual void NotBindingControl(Control.ObjectEditor Editor) { }
        protected virtual System.Windows.Forms.Label GetLabel(int x, int y, string text)
        {
            var Label = new System.Windows.Forms.Label();
            Label.Location = new System.Drawing.Point(x, y);
            Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label.AutoSize = false;
            Label.Text = text;
            Label.Size = Label.PreferredSize;
            return Label;
        }
        protected virtual System.Drawing.Size CalcLabel(int label_value, System.Windows.Forms.Label label, int width, int height)
        {
            label.Size = label.PreferredSize;
            int extra_w = 0, extra_h = 0;
            switch (label_value)
            {
                // Label 放在上边
                case 1:
                    extra_h = 2 + label.Height; break;
                // Label 放在左边
                case 2:
                    extra_w = 2 + label.Width; break;
            }
            return new System.Drawing.Size(extra_w, extra_h);
        }
        protected virtual void CalcCoodinate(System.Windows.Forms.Control control, int w, int h)
        {
            if (now_x + w > max_x)
                max_x = now_x + w;
            if (now_y + h > max_y)
                max_y = now_y + h;
            if (mode == ControlOrder.Row)
            {
                now_y += h + this.margin.Bottom;
                if (w > now_w) now_w = w;
            }
            else
            {
                now_x += w + this.margin.Right;
                if (h > now_h) now_h = h;
            }
        }
        protected virtual void AddLabel(System.Windows.Forms.Label label)
        {
            if (container.CanAdd(label)) target.Add(label);
        }
        protected virtual void AddControl(System.Windows.Forms.Control control)
        {
            if (container.CanAdd(control)) target.Add(control);
        }


        static Stack<RubyBuilder> Builders = new Stack<RubyBuilder>();
        public static void In(DataContainer container)
        {
            Builders.Push(new RubyBuilder(container));
            StackTop = Builders.Peek();
        }
        public static System.Drawing.Size Out()
        {
            if (Builders.Count == 0) return default(System.Drawing.Size);
            var builder = Builders.Pop();
            var size = new System.Drawing.Size(builder.max_x + builder.container.end_x, builder.max_y + builder.container.end_y);
            builder.container.SetSize(size);
            if (Builders.Count > 0) StackTop = Builders.Peek(); else StackTop = null;
            return size;
        }
        public static void Space(int space = 20)
        {
            var builder = StackTop;
            if (builder.mode == ControlOrder.Row) builder.now_y += space;
            else builder.now_x += space;
        }
        public static void Space(int x_space = 20, int y_space = 20)
        {
            var builder = StackTop;
            builder.now_x += x_space;
            builder.now_y += y_space;
        }
        public static void Next(int extra = 0)
        {
            var builder = StackTop;
            if (builder.mode == ControlOrder.Row)
            {
                builder.now_y = builder.head_y;
                builder.head_x = builder.now_x + builder.now_w + builder.margin.Right + extra;
                builder.now_x = builder.head_x;
                builder.now_w = 0;
            }
            else
            {
                builder.now_x = builder.head_x;
                builder.head_y = builder.now_y + builder.now_h + builder.margin.Bottom + extra;
                builder.now_y = builder.head_y;
                builder.now_h = 0;
            }
        }
        public static void OrderAndNext()
        {
            var builder = StackTop;
            if (builder.mode == ControlOrder.Column)
            {
                builder.head_x = builder.now_x = builder.max_x + builder.margin.Right * 2;
                builder.now_y = builder.container.start_y;
                builder.now_w = builder.now_h = 0;
            }
            else
            {
                builder.head_y = builder.now_y = builder.max_y;
                builder.head_x = builder.max_x;
                builder.now_x = builder.container.start_x;
                builder.now_w = builder.now_h = 0;
            }
        }
        public static void Order(int i = -1)
        {
            var builder = StackTop;
            if (i == 0)
                builder.mode = ControlOrder.Row;
            else if (i == 1)
                builder.mode = ControlOrder.Column;
            else
                builder.mode = builder.mode == ControlOrder.Row ? ControlOrder.Column : ControlOrder.Row;
        }
        public static System.Windows.Forms.Label Text(IronRuby.Builtins.MutableString text, int extra_w = 0, int extra_h = 0)
        {
            return Text(text.ToString(System.Text.Encoding.UTF8), extra_w, extra_h);
        }
        public static System.Windows.Forms.Label Text(string text = "", int extra_w = 0, int extra_h = 0)
        {
            var builder = StackTop;
            var label = new System.Windows.Forms.Label();
            label.Text = text;
            label.Size = label.PreferredSize;
            if (extra_w < 0)
            {
                extra_w = builder.last.Width - label.Width + extra_w + 1;
                var object_editor = builder.last.Tag as Control.ObjectEditor;
                int label_parameter = object_editor.Argument.GetArgument<int>("label");
                if (label_parameter == 2)
                    extra_w += object_editor.Label.Width;
            }
            if (extra_h < 0)
            {
                extra_h = builder.last.Height - label.Height + extra_h + 1;
                var object_editor = builder.last.Tag as Control.ObjectEditor;
                int label_parameter = object_editor.Argument.GetArgument<int>("label");
                if (label_parameter == 1)
                    extra_h += object_editor.Label.Height;
            }
            label.Location = new System.Drawing.Point(builder.now_x + extra_w, builder.now_y + extra_h);
            var size = new System.Drawing.Size(label.Width + extra_w, label.Height + extra_h);
            label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            builder.AddLabel(label);
            builder.CalcCoodinate(label, size.Width, size.Height);
            return label;
        }
        public static ObjectEditor Push(RubySymbol type, Hash parameters, IronRuby.Builtins.Proc after)
        {
            // 获得目前在顶的构造器
            var builder = StackTop;
            // 检索此名称的控件
            var editor = builder.SearchControl(type);
            if (editor == null)
            {
                // 找不到时，记录并返回
                Help.Log.log("找不到下述关键字的控件：" + type.ToString());
                return null;
            }
            // 生成控件参数
            // 这里认为，控件一开始就生成好了默认的参数。
            var argument = editor.Argument;
            // 如果没有，那么新建一个
            if (argument == null) argument = new Parameter();
            argument.LoadFromRubyHash(parameters);
            // 校验参数
            string ArgumentError;
            if (argument.CheckArgument(out ArgumentError) == false)
                Help.Log.log(type + "型的参数不准确：\r" + ArgumentError);
            // 上传控件参数
            editor.Argument = argument;
            // 标记其父亲控件
            editor.Container = builder.container;
            // 转换成控件形式
            System.Windows.Forms.Control control = editor.Binding;
            if (control == null) { builder.NotBindingControl(editor); return null; }
            // 绑定默认事件
            control.Leave += Help.Event.OnLeave;
            control.Enter += Help.Event.OnEnter;
            // 绑定关系
            control.Tag = editor;
            // 生成和计算 Label
            string label_text= argument.GetArgument<string>("text");
            int label_argument = argument.GetArgument<int>("label");
            var label = builder.GetLabel(builder.now_x, builder.now_y, label_text);
            var size = builder.CalcLabel(label_argument, label, control.Width, control.Height);
            // 上传 Label
            if (label_argument != 0) builder.AddLabel(label);
            editor.Label = label;
            // 标定位置
            control.Location = new System.Drawing.Point(builder.now_x + size.Width, builder.now_y + size.Height);
            // 执行块
            // 若这是一个容器，则装载它，执行块，然后卸装
            if (editor is DataContainer)
            {
                In(editor as DataContainer);
                try
                {
                    if (after != null) after.Call(after, editor);
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Zenus is watching you!" + System.Environment.NewLine +
                        "In building " + type.String + System.Environment.NewLine +
                        "Report As :" + System.Environment.NewLine +
                        RubyEngine.LastEngine.FormatException(ex),
                        "Error on Building", System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error
                        );
                }
                Out();
            }
            // 结算坐标
            builder.CalcCoodinate(control, size.Width + control.Width, size.Height + control.Height);
            // 若 Label 是横向的，那么置中
            if (label_argument == 2) label.Height = control.Height;
            // 上传控件
            builder.AddControl(control);
            // 记忆控件
            builder.last = control;
            return editor;
        }
    }
}
