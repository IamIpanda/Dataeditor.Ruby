using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

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
                return proc.Call(proc, arguments);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Arke meets Iris: On Line "
                    + proc.SourceLine + Environment.NewLine + "In File : "
                    + proc.SourcePath + Environment.NewLine + "With"
                    + arguments.ToString() + Environment.NewLine + "Report As :"
                    + RubyEngine.LastEngine.FormatException(ex),
                    "Error on execting Proc", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                    );
                return null;
            }
        }
    }
}
