using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Contract
{
    public interface MenuCommand
    {
        string Name { get; }
        System.Windows.Forms.Keys Key { get; }
    }
}
