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
    public class Call : Contract.Runable
    {
        public delegate object Callable(params object[] args);
        Callable method;
        public Call(Callable method) { this.method = method; }
        public object call(params object[] args) { return method.Invoke(args); }
    }
}
