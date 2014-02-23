using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Event
{
    public class CommandGroup
    {
        public List<EventCommand> Lists { get; set; }
        public string Text { get; set; }
        public CommandGroup(string text, params EventCommand[] commands)
        {
            this.Text = text;
            Lists = new List<EventCommand>();
            Lists.AddRange(commands);
        }
        public CommandGroup(string text, Dictionary<object, object> list, IEnumerable<object> commands)
        {
            this.Text = text;
            Lists = new List<EventCommand>();
            foreach (object obj in commands)
            {
                int i;
                if (obj is int) i = Convert.ToInt32(obj); else continue;
                if (list.ContainsKey(i) && list[i] is EventCommand)
                    Lists.Add(list[i] as EventCommand);
            }
 
        }
        public override string ToString()
        {
            return Text;
        }
        public System.Drawing.Color Color
        {
            set { foreach (var command in Lists) command.Color = value; }
        }
    }
}
