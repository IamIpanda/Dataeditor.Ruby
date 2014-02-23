using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Ruby
{
    public class Proc : Contract.Runable
    {
        protected IronRuby.Builtins.Proc proc;
        public Proc(IronRuby.Builtins.Proc proc)
        {
            this.proc = proc;
            if (proc == null) Help.Log.log("程序正在尝试初始化一个 proc 为空的过程件。");
        }
        public object call(params object[] arguments)
        {
            try
            {
                return proc.Call(arguments);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Arke meets Iris: On Line "
                    + proc.SourceLine + "\n In File : "
                    + proc.SourcePath + "\n With"
                    + arguments.ToString() + "\n Report As :"
                    + ex.ToString());
                return null;
            }
        }
    }
}
