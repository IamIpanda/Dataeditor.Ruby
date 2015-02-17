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

namespace DataEditor.Control.Event
{
    public partial class Main : Form
    {
        private FuzzyObject Map, Event, Page;
        private static FuzzySymbol EventSymbol = FuzzySymbol.GetSymbol("@events"),
            PagesSymbol = FuzzySymbol.GetSymbol("@pages"),
            ListSymbol = FuzzySymbol.GetSymbol("@list"),
            NameSymbol = FuzzySymbol.GetSymbol("@name");

        private List<object> MapKeys = new List<object>(), EventKeys = new List<object>(); 
        private bool EditingCommonEvents = false;

        public Main()
        {
            InitializeComponent();
            if (OFD.ShowDialog() != DialogResult.OK) { Application.Exit(); return; }
            Help.Path.Instance["project"] = System.IO.Path.GetDirectoryName(OFD.FileName);
            Program.DebugAddingOn();
            SetMapListBox();
        }

        void SetMapListBox()
        {
            var info = Help.Data.Instance["mapinfo"] as FuzzyHash;
            if (info == null) return;
            MapKeys.Clear();
            lbMap.Items.Clear();
            foreach (var key in info.Keys)
            {
                var mapinfo = info[key] as FuzzyObject;
                var FuzzyName = mapinfo == null ? null : mapinfo[NameSymbol] as FuzzyString;
                var name = FuzzyName == null ? null : FuzzyName.Text;
                if (name == null) name = "[" + key.ToString() + "]";
                MapKeys.Add(key);
                lbMap.Items.Add(name);
            }
            MapKeys.Add(null);
            lbMap.Items.Add("公共事件");
        }

        void SetEventListBox()
        {
            if (EditingCommonEvents) return;
            var events = Map[EventSymbol] as FuzzyHash;
            if (events == null) return;
            lbEvent.Enabled = true;
            EventKeys.Clear();
            foreach (var key in events.Keys)
            {
                var _event = events[key] as FuzzyObject;
                var FuzzyName = _event == null ? null : _event[NameSymbol] as FuzzyString;
                var name = FuzzyName == null ? null : FuzzyName.Text;
                if (name == null) name = "[" + key.ToString() + "]";
                EventKeys.Add(key);
                lbEvent.Items.Add(name);
            }
            if (lbEvent.Items.Count > 0) lbEvent.SelectedIndex = 0;
        }

        void SetPageListBox()
        {
            if (EditingCommonEvents) return;
            var FuzzyPages = Event[PagesSymbol] as FuzzyArray;
            if(FuzzyPages == null) return;
            lbPage.Enabled = true;
            lbPage.Items.Clear();
            for (int i = 0; i < FuzzyPages.Count; i++)
                lbPage.Items.Add("第 " + (i + 1).ToString() + " 页");
            if (lbPage.Items.Count > 0) lbPage.SelectedIndex = 0;
        }

        void SetCommandListBox()
        {
            peclMain.Items.Clear();
            var FuzzyPage = Page[ListSymbol] as FuzzyArray;
            if(FuzzyPage == null) return;
            peclMain.Enabled = true;
            foreach (var command in FuzzyPage.OfType<FuzzyObject>())
                peclMain.Items.Add(new Event.DataModel.Command(command));
        }

        private void lbMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMap.SelectedIndex < 0) return;
            lbEvent.Enabled = lbPage.Enabled = peclMain.Enabled = false;
            var key = MapKeys[lbMap.SelectedIndex];
            EditingCommonEvents = key == null;
            if (EditingCommonEvents)
            {
                lbPage.Items.Clear();
                lbEvent.Items.Clear();
                lbEvent.Enabled = false;
                var commonList = Help.Data.Instance["commonevent"] as FuzzyArray;
                Map = Event = commonList;
                if (Map == null) return;
                for (int i = 0; i < commonList.Length - 1; i++)
                    lbPage.Items.Add("公共事件 " + (i + 1).ToString());
                lbPage.Enabled = true;
                if (lbPage.Items.Count > 0) lbPage.SelectedIndex = 0;
            }
            else
            {
                lbEvent.Items.Clear();
                lbEvent.Enabled = true;
                var FuzzyKey = key as FuzzyFixnum;
                if (FuzzyKey == null) return;
                Map = Help.Data.Instance[Convert.ToInt32(FuzzyKey.Value)];
                SetEventListBox();
            }

        }

        private void lbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EditingCommonEvents) return;
            if (lbEvent.SelectedIndex < 0) return;
            lbPage.Enabled = peclMain.Enabled = false;
            var Key = EventKeys[lbEvent.SelectedIndex];
            Event = (Map[EventSymbol] as FuzzyHash)[Key] as FuzzyObject;
            if (Event == null) return;
            SetPageListBox();
        }

        private void lbPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPage.SelectedIndex < 0) return;
            peclMain.Enabled = false;
            if (Event == null) return;
            if (EditingCommonEvents)
                Page = (Event as FuzzyArray)[lbPage.SelectedIndex + 1] as FuzzyObject;
            else Page = (Event[PagesSymbol] as FuzzyArray)[lbPage.SelectedIndex] as FuzzyObject;
            if (Page != null) SetCommandListBox();
        }

        private void peclMain_Leave(object sender, EventArgs e)
        {
            var FuzzyPage = Page[ListSymbol] as FuzzyArray;
            if (FuzzyPage == null) return;
            FuzzyPage.Clear();
            foreach (var command in peclMain.Items.OfType<Command>())
                FuzzyPage.Add(command.Link);
        }
    }
}
