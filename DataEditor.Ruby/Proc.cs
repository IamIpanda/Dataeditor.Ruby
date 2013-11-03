using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Ruby
{
    public class Proc : Contract.Runable
    {
        protected IronRuby.Builtins.Proc proc;
        public Proc(IronRuby.Builtins.Proc proc) { this.proc = proc; }
        public object call(params object[] arguments)
        {
            return proc.Call(arguments);
        }
    }
}
