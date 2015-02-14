using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class EventShopItemWindow : DataEditor.Control.Window.WindowWithOK
    {
        public EventShopItemWindow()
        {
            InitializeComponent();
            base.pnMain.Controls.Add(mainLv);
        }
        public Contract.Runable GenerateChildWindow { get; set; }
        public List<FuzzyData.FuzzyArray> AllItems { get; set; }
    }
    public class Dialog_Shop : WindowWithOK.WrapWindowWithOK<EventShopItemWindow>
    {
        public override string Flag { get { return "dialog_shop"; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("child_window", null, Help.Parameter.ArgumentType.Must);
        }
    }
}
