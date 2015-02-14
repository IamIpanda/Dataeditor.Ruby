using System;
using System.Collections.Generic;

namespace DataEditor.Control.Event.DataModel
{
	public class CommandGroup
	{
		public string Name { get; set; }
        public System.Drawing.Color Color { get; set; }
		public CommandGroup(string name, System.Drawing.Color color)
		{
            this.Name = name;
            this.Color = color;
            this.Components = new List<CommandType>();
            Groups.Add(this);
		}
        public CommandGroup(string name, int color)
        {
            this.Name = name;
            this.Color = System.Drawing.Color.FromArgb(color);
            this.Color = System.Drawing.Color.FromArgb(255, this.Color);
            this.Components = new List<CommandType>();
            Groups.Add(this);
        }
        public CommandGroup(string name) : this(name, System.Drawing.Color.Black) { }
        public List<CommandType> Components { get; set; }
        public bool AddCommand(CommandType type)
        {
            if (Components.Contains(type)) return true;
            else Components.Add(type);
            type.Group = this;
            return false;
        }
        public void AddCommand(IEnumerable<CommandType> types)
        {
            foreach (var type in types)
                AddCommand(type);
        }
        public override string ToString()
        {
            return this.Name;
        }
        static public List<CommandGroup> Groups = new List<CommandGroup>();
	}
}