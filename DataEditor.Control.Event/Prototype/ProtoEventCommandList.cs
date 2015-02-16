using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using DataEditor.Control.Event.DataModel;
using DataEditor.Control.Window;
using DataEditor.FuzzyData;

namespace DataEditor.Control.Prototype
{
    public class ProtoEventCommandList : Prototype.ProtoListBox
    {
        #region 自动生成的代码
        private ContextMenuStrip RightMenu;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem MenuItemInsert;
        private ToolStripMenuItem MenuItemEdit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem MenuItemCut;
        private ToolStripMenuItem MenuItemCopy;
        private ToolStripMenuItem MenuItemPaste;
        private ToolStripMenuItem MenuItemDelete;
        #endregion

        const int IndentShift = 18;
        readonly SolidBrush SignBrush = new SolidBrush(Color.Black);

        public ProtoEventCommandList()
        {
            InitializeComponent();
        }

        void ProtoEventCommandList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
        }

        void ProtoEventCommandList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                {
                    var index = this.SelectedIndex - 1;
                    if (index < 0) index = this.Items.Count - 1;
                    var command = GetCommand(index);
                    while (index >= 0 && (command == null || command.Type.isChildCommand))
                        command = GetCommand(--index);
                    if (this.SelectedIndex >= 0)
                        this.SetSelected(this.SelectedIndex, false);
                    this.SetSelected(index, true);
                    e.Handled = true;
                }
                    break;
                case Keys.Down:
                {
                    var index = this.SelectedIndex + 1;
                    if (index >= Items.Count) index = 0;
                    var command = GetCommand(index);
                    while (index <= Items.Count - 1 && (command == null || command.Type.isChildCommand))
                        command = GetCommand(++index);
                    if (this.SelectedIndex >= 0)
                        this.SetSelected(this.SelectedIndex, false);
                    this.SetSelected(index, true);
                    e.Handled = true;
                }
                    break;
                case Keys.Space:
                    OnEditCalled();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    OnInsertCalled();
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    OnDeleteCalled();
                    e.Handled = true;
                    break;
            }
        }

        int EndingIndex = -1;
        private int last_start_index = 0, last_end_index = 0;
        void ProtoEventCommandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EndingIndex = -1;
            if (last_end_index >= this.Items.Count) last_end_index = this.Items.Count - 1;
            if (last_start_index >= this.Items.Count) last_start_index = this.Items.Count - 1;
            var rect_top = this.GetItemRectangle(last_start_index);
            var rect_end = this.GetItemRectangle(last_end_index);
            if (rect_top.Top < 0) rect_top.Y = 0;
            if (rect_end.Bottom > this.ClientRectangle.Height)
                rect_end.Y = this.ClientRectangle.Bottom - rect_end.Height;
            this.Invalidate(new Rectangle(rect_top.X, rect_top.Y, rect_end.Width, rect_end.Bottom - rect_top.X));
            if (SelectedIndex == this.Items.Count - 1 || SelectedIndex < 0) return;
            var command = GetCommand(this.SelectedIndex);
            if (command == null) return;
            if (command.Type.isTextCommand || command.Type.isStartCommand)
            {
                var search_code = command.Code;
                var search_ending = command.Type.Ends;
                var search_indent = command.Indent;
                var index = this.SelectedIndex;
                var isText = command.Type.isTextCommand;
                command = GetCommand(++index);
                while (isText ? (command.Type.Follow == search_code) : (command.Code != search_ending || command.Indent != search_indent))
                {
                    if (index == this.Items.Count - 1) { index++; break; }
                    command = GetCommand(++index);
                }
                while (!isText && command.Code == search_ending)
                {
                    if (index == this.Items.Count - 1) { index++; break; }
                    command = GetCommand(++index);
                }
                EndingIndex = index - 1;
                rect_top = this.GetItemRectangle(last_start_index = this.SelectedIndex);
                rect_end = this.GetItemRectangle(last_end_index = this.EndingIndex);
                this.Invalidate(new Rectangle(rect_top.X, rect_top.Y, rect_end.Width, rect_end.Bottom - rect_top.X));
            }
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // 各种情况的处理
            if (e.Index < 0) return;
            if (Items.Count == 0 || e.Index >= Items.Count) return;
            if (Items.Count == 0 && Focused)
            { DrawFocusRectangle(e.Graphics, e.Bounds); return; }
            var command = GetCommand(e.Index);
            if (command == null) return;
            if (e.Index == Items.Count - 1) DrawRestItems(e);
            bool focused = GetFocused(e.State);
            bool handled_focused = (focused && !(command.Type.isChildCommand)) || GetRanged(e.Index);
            // 获取背景
            Brush BackBrush = handled_focused ? GetFocusBrush(e) : GetBackColor(e);
            e.Graphics.FillRectangle(BackBrush, e.Bounds);
            // 焦点框
            if (focused)
                DrawFocusRectangle(e.Graphics, e.Bounds);
            // 开始描绘
            Brush ForeBrush = GetEventBrush(e, command, handled_focused);
            float shift = IndentShift * command.Indent;
            // 总会描绘的标记（Sign）
            e.Graphics.DrawString(command.Type.isChildCommand ? Command.ChildSign : Command.FocusSign, Font, handled_focused ? ForeBrush : SignBrush, shift, e.Bounds.Y);
            if (command.Code <= 0) return;
            shift += IndentShift;
            // 指令名称
            if (!(command.Type.isChildCommand))
            {
                e.Graphics.DrawString(command.Type.Name, Font, ForeBrush, shift, e.Bounds.Y);
                shift += command.Type.NameLength ?? command.Type.GenerateNameLength(e.Graphics, Font);
            }
            // 指令顺延
            if (command.Type.isProlongCommand) 
                shift += command.Type.ParentNameLength ?? command.Type.GetParentTextCommandLength(e.Graphics, Font);
            var str = command.ToString();
            // “ ： ”
            if (((!(command.Type.isChildCommand) || command.Type.isProlongCommand)) && str.Length > 0)
            {
                e.Graphics.DrawString(": ", Font, ForeBrush, shift, e.Bounds.Y);
                shift += 14;
            }
            // 生成的内容
            e.Graphics.DrawString(str, Font, ForeBrush, shift, e.Bounds.Y);
        }

        protected void DrawRestItems(DrawItemEventArgs e)
        {
            int cy = e.Bounds.Y + e.Bounds.Height, count = e.Index % ProtoListControlHelp.DefaultBackColors.Count;
            while (cy <= this.ClientRectangle.Height)
            {
                count = (count + 1) % ProtoListControlHelp.DefaultBackColors.Count;
                SolidBrush ExtraBackBrush = ProtoListControlHelp.GetBrush(CheckEnabled(ProtoListControlHelp.DefaultBackColors[count]));
                e.Graphics.FillRectangle(ExtraBackBrush, new Rectangle(e.Bounds.X, cy, e.Bounds.Width, e.Bounds.Height));
                cy += ItemHeight;
            }
        }
        protected DataEditor.Control.Event.DataModel.Command GetCommand(int index)
        {
            return this.Items[index] as DataEditor.Control.Event.DataModel.Command;
        }


        protected override bool GetFocused(DrawItemState state)
        {
            if (DisappearRectLosingFocus)
                return (state & DrawItemState.Focus) != 0;
            return (state & DrawItemState.Selected) != 0;
        }

        protected virtual bool GetRanged(int index)
        {
            if (EndingIndex < 0 || this.SelectedIndex < 0) return false;
            return (this.SelectedIndex < index && index <= EndingIndex);
        }
        protected Brush GetEventBrush(DrawItemEventArgs e, Command command, bool handled_focused)
        {
            if (handled_focused)
                return ProtoListControlHelp.GetBrush(CheckEnabled(ProtoListControlHelp.DefaultForeColorOnFocus));
            Color color;
            if (ForeColors.TryGetValue(e.Index, out color))
                return ProtoListControlHelp.GetBrush(CheckEnabled(color));
            return ProtoListControlHelp.GetBrush(CheckEnabled(command.Type.Color));
        }

        public List<Command> With
        {
            get
            {
                if (this.SelectedIndex < 0 || this.EndingIndex < 0) return null;
                var ans = new List<Command>();
                for (var i = this.SelectedIndex + 1; i <= this.EndingIndex; i++)
                    ans.Add(this.Items[i] as Command);
                return ans;
            }
            set
            {
                if (this.SelectedIndex < 0 || this.EndingIndex < 0) return;
                var selected_index = this.SelectedIndex;
                for (var i = this.EndingIndex; i > selected_index; i--)
                    this.Items.RemoveAt(i);
                for (var i = 0; i < value.Count; i++)
                    this.Items.Insert(i + selected_index + 1, value[i]);
                this.SelectedIndex = -1;
                this.SelectedIndex = selected_index;
            }
        }

        public Command SelectedCommand
        {
            get { return SelectedIndex < 0 ? null : GetCommand(this.SelectedIndex); }
        }

        private void ProtoEventCommandList_DoubleClick(object sender, EventArgs e)
        {
            OnInsertCalled();
        }

        public event EventHandler InsertCalled;
        public event EventHandler EditCalled;
        public event EventHandler CopyCalled;
        public event EventHandler CutCalled;
        public event EventHandler PasteCalled;
        public event EventHandler DeleteCalled;

        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            OnCopyCalled();
        }

        private void MenuItemCut_Click(object sender, EventArgs e)
        {
            OnCutCalled();
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            OnDeleteCalled();
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            OnEditCalled();
        }

        private void MenuItemInsert_Click(object sender, EventArgs e)
        {
            OnInsertCalled();
        }

        private void MenuItemPaste_Click(object sender, EventArgs e)
        {
            OnPasteCalled();
        }

        public void OnInsertCalled()
        {
            if (InsertCalled != null)
                InsertCalled(this, new EventArgs());
            else Insert();
        }
        public void OnEditCalled()
        {
            if (EditCalled != null)
                EditCalled(this, new EventArgs());
            else Edit();
        }
        public void OnCopyCalled()
        {
            if (CopyCalled != null)
                CopyCalled(this, new EventArgs());
            else Copy();
        }
        public void OnCutCalled()
        {
            if (CutCalled != null)
                CutCalled(this, new EventArgs());
            else Cut();
        }
        public void OnPasteCalled()
        {
            if (PasteCalled != null)
                PasteCalled(this, new EventArgs());
            else Paste();
        }
        public void OnDeleteCalled()
        {
            if (DeleteCalled != null)
                DeleteCalled(this, new EventArgs());
            else Delete();
        }

        public bool CanOperate
        {
            get { return CanInsert && SelectedCommand.Code > 0; }
        }

        public bool CanInsert
        {
            get
            {
                if (this.SelectedIndex < 0) return false;
                var selected = SelectedCommand;
                return !selected.Type.isChildCommand;
            }
        }
        private CommandChooseWindow cm = null;
        public void Insert()
        {
            if (CanInsert == false) return;
            if (cm == null) cm = new CommandChooseWindow();
            if (cm.ShowDialog() != DialogResult.OK) return;
            var index = SelectedIndex;
            for (var i = 0; i < cm.SelectedCommands.Count; i++)
                this.Items.Insert(i + index, cm.SelectedCommands[i]);
            this.SelectedIndex = -1;
            this.SelectedIndex = index;
        }

        public void Insert(Command command)
        {
            this.Items.Insert(SelectedIndex, command);
        }

        public void Edit()
        {
            if (CanOperate == false) return;
            var command = SelectedCommand;
            if (command.Type.isChildCommand) return;
            var window = SelectedCommand.GenerateWindow(this.With);
            if (window == null) return;
            if (window.Show() != DialogResult.OK) return;
            var with = command.Type.With == null ? null : command.Type.With.call(window, this.With) as IEnumerable<object>;
            command.FuzzyParameters = window.Parent as FuzzyArray;
            command.SyncToLink();
            command.GenerateString();
            if (with != null)
                With = with.OfType<Command>().Select(obj => obj).ToList();
            ProtoEventCommandList_SelectedIndexChanged(this, new EventArgs());
        }

        private static DataEditor.Help.Clipboard clip = DataEditor.Help.Clipboard.GetClip();

        public FuzzyArray GetFuzzySelectedCommands()
        {
            var arr = new FuzzyArray { this.SelectedCommand.Link };
            var with = this.With;
            if (with == null) return arr;
            foreach (var command in with)
                arr.Add(command.Link);
            return arr;
        }
        public void Copy()
        {
            if (CanOperate == false) return;
            clip.Set(GetFuzzySelectedCommands());
        }
        public void Cut()
        {
            if (CanOperate == false) return;
            clip.Set(GetFuzzySelectedCommands());
            OnDeleteCalled();
        }
        public void Paste()
        {
            if (CanOperate == false) return;
            if (!(clip.CanGet())) return;
            var answer = clip.Get() as FuzzyArray;
            if (answer == null) return;
            var indent = SelectedCommand.Indent;
            var commands = answer.OfType<FuzzyObject>().Select(command => new Command(command as FuzzyObject)).ToList();
            var origin_command = commands[0];
            var sub_indent = indent - origin_command.Indent;
            var index = SelectedIndex;
            foreach (var command in commands)
            {
                command.Indent += sub_indent;
                command.SyncToLink();
                this.Items.Insert(index, command);
                index++;
            }
            SelectedIndex = -1;
            SelectedIndex = index;
        }
        public void Delete()
        {
            var command = SelectedCommand;
            if (command.Code <= 0 || command.Type.isChildCommand) return;
            var index = this.SelectedIndex;
            var to = this.EndingIndex;
            if (to >= 0)
                for (int i = to; i >= index; i--)
                    this.Items.RemoveAt(i);
            else this.Items.RemoveAt(index);
            if (index >= this.Items.Count) index = this.Items.Count - 1;
            SelectedIndex = -1;
            this.SelectedIndex = index;
        }


        #region 自动生成的代码
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.RightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // RightMenu
            // 
            this.RightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemInsert,
            this.MenuItemEdit,
            this.toolStripSeparator1,
            this.MenuItemCut,
            this.MenuItemCopy,
            this.MenuItemPaste,
            this.MenuItemDelete});
            this.RightMenu.Name = "RightMenu";
            this.RightMenu.Size = new System.Drawing.Size(101, 142);
            // 
            // MenuItemInsert
            // 
            this.MenuItemInsert.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.MenuItemInsert.Name = "MenuItemInsert";
            this.MenuItemInsert.Size = new System.Drawing.Size(100, 22);
            this.MenuItemInsert.Text = "插入";
            this.MenuItemInsert.Click += new System.EventHandler(this.MenuItemInsert_Click);
            // 
            // MenuItemEdit
            // 
            this.MenuItemEdit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MenuItemEdit.Name = "MenuItemEdit";
            this.MenuItemEdit.Size = new System.Drawing.Size(100, 22);
            this.MenuItemEdit.Text = "编辑";
            this.MenuItemEdit.Click += new System.EventHandler(this.MenuItemEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // MenuItemCut
            // 
            this.MenuItemCut.Name = "MenuItemCut";
            this.MenuItemCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.MenuItemCut.ShowShortcutKeys = false;
            this.MenuItemCut.Size = new System.Drawing.Size(100, 22);
            this.MenuItemCut.Text = "剪切";
            this.MenuItemCut.Click += new System.EventHandler(this.MenuItemCut_Click);
            // 
            // MenuItemCopy
            // 
            this.MenuItemCopy.Name = "MenuItemCopy";
            this.MenuItemCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.MenuItemCopy.ShowShortcutKeys = false;
            this.MenuItemCopy.Size = new System.Drawing.Size(100, 22);
            this.MenuItemCopy.Text = "复制";
            this.MenuItemCopy.Click += new System.EventHandler(this.MenuItemCopy_Click);
            // 
            // MenuItemPaste
            // 
            this.MenuItemPaste.Name = "MenuItemPaste";
            this.MenuItemPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.MenuItemPaste.ShowShortcutKeys = false;
            this.MenuItemPaste.Size = new System.Drawing.Size(100, 22);
            this.MenuItemPaste.Text = "粘贴";
            this.MenuItemPaste.Click += new System.EventHandler(this.MenuItemPaste_Click);
            // 
            // MenuItemDelete
            // 
            this.MenuItemDelete.Name = "MenuItemDelete";
            this.MenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.MenuItemDelete.ShowShortcutKeys = false;
            this.MenuItemDelete.Size = new System.Drawing.Size(100, 22);
            this.MenuItemDelete.Text = "删除";
            this.MenuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // ProtoEventCommandList
            // 
            this.ContextMenuStrip = this.RightMenu;
            this.SelectedIndexChanged += new System.EventHandler(this.ProtoEventCommandList_SelectedIndexChanged);
            this.DoubleClick += new System.EventHandler(this.ProtoEventCommandList_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProtoEventCommandList_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProtoEventCommandList_KeyPress);
            this.RightMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}
