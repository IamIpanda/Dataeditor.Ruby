using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Metro : Control.WrapControlContainer<Control.Prototype.ProtoMetroContainer>
    {
        public override System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                return Control.PanelCollection;
            }
        }
    }
}
