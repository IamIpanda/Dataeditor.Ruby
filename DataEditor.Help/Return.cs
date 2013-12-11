using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Return : Contract.Runable
    {
        protected object target;
        public Return(object target) { this.target = target; }
        public object call(params object[] args) { return target; }
    }
}
