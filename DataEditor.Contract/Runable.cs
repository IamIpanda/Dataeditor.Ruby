using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Contract
{
    public interface Runable
    {
        object call(params object[] args);
    }
}
