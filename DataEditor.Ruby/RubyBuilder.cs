using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby.Builtins;
using DataEditor.Control;

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
        protected System.Windows.Forms.Control.ControlCollection target;
        protected DataContainer container;

        protected RubyBuilder(DataContainer container, int default_head_x = 0, int default_head_y = 0)
        {
            max_x = head_x = now_x = default_head_x;
            max_y = head_y = now_y = default_head_y;
            this.container = container;
            this.target = container.Controls;
        }
        protected virtual DataEditor SearchControl(RubySymbol type)
        {
            throw new NotImplementedError();
        }
        protected virtual DataEditor NotNamedControl(RubySymbol tpye) { return null; }
        protected virtual void NotBindingControl(DataEditor Editor) { }
        protected virtual System.Windows.Forms.Label GetLabel(int x, int y, string text)
        {
            var Label = new System.Windows.Forms.Label();
            Label.Location = new System.Drawing.Point(x, y);
            Label.Text = text;
            return Label;
        }
        protected virtual System.Drawing.Size CalcLabel(int label_value, System.Windows.Forms.Label label, int width, int height)
        {
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
                now_y += h + control.Margin.Bottom;
                if (w > now_w) now_w = w;
            }
            else
            {
                now_w += w + control.Margin.Right;
                if (h > now_h) now_h = h;
            }
        }
        protected virtual void AddLabel(System.Windows.Forms.Label label)
        {
            target.Add(label);
        }
        protected virtual void AddControl(System.Windows.Forms.Control control)
        {
            target.Add(control);
        }


        static Stack<RubyBuilder> Builders = new Stack<RubyBuilder>();
        public static void In(DataContainer container)
        {
            Builders.Push(new RubyBuilder(container));
        }
        public static System.Drawing.Size Out()
        {
            if (Builders.Count == 0) return default(System.Drawing.Size);
            var builder = Builders.Pop();
            var size = new System.Drawing.Size(builder.max_x, builder.max_y);
            return size;
        }
        public static void Space(int space = 20)
        {
            var builder = Builders.Peek();
            if (builder.mode == ControlOrder.Row) builder.now_y += space;
            else builder.now_x += space;
        }
        public static void Next()
        {
            var builder = Builders.Peek();
            if (builder.mode == ControlOrder.Row)
            {
                builder.now_y = builder.head_y;
                builder.head_x = builder.now_x + builder.now_w;
                builder.now_x = builder.head_x;
                builder.now_w = 0;
            }
            else
            {
                builder.now_x = builder.head_x;
                builder.head_y = builder.now_y + builder.now_h;
                builder.now_y = builder.head_y;
                builder.now_h = 0;
            }
        }
        public static void Order(int i = -1)
        {
            var builder = Builders.Peek();
            if (i == 0)
                builder.mode = ControlOrder.Row;
            else if (i == 1)
                builder.mode = ControlOrder.Column;
            else
                builder.mode = builder.mode == ControlOrder.Row ? ControlOrder.Column : ControlOrder.Row;
        }
        public static void Text(string text = "")
        {
            // TODO : Finish it.
        }
        public static void Push(RubySymbol type, Hash parameters, Proc after)
        {
            // 获得目前在顶的构造器
            var builder = Builders.Peek();
            // 检索此名称的控件
            var editor = builder.SearchControl(type);
            if (editor == null) return;
            // 生成控件参数
            var argument = editor.GetParameter(parameters);
            // 标记其父亲控件
            editor.Container = builder.container;
            // 转换成控件形式
            System.Windows.Forms.Control control = editor.Binding;
            if (control == null) { builder.NotBindingControl(editor); return; }
            // 绑定默认事件
            control.Leave += Help.Event.OnLeave;
            // 绑定关系
            control.Tag = editor;
            // 生成和计算 Label
            var label = builder.GetLabel(builder.now_x, builder.now_y, argument.GetAegument<string>("text"));
            var size = builder.CalcLabel(argument.GetAegument<int>("label"), label, control.Width, control.Height);
            // 上传 Label
            builder.AddLabel(label);
            // 标定位置
            control.Location = new System.Drawing.Point(builder.now_x + size.Width, builder.now_y + size.Height);
            // 结算坐标
            builder.CalcCoodinate(control, size.Width + control.Width, size.Height + control.Height);
            // 上传控件
            builder.AddControl(control);
        }
    }
}
