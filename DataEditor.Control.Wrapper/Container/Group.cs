using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Group : WrapControlContainer<System.Windows.Forms.GroupBox>
    {
        public override string Flag { get { return "group"; } }
        public override int start_x { get { return 4; } }
        public override int start_y { get { return 12; } }
        public override int end_x { get { return 4; } }
        public override int end_y { get { return 8; } }
    }
}
