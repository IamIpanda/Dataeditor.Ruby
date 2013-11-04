using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public interface ObjectEditor : Contract.ObjectEditor, Contract.Iconic
    {
        ObjectEditor Container { get; set; }
        System.Windows.Forms.Label Label { get; set; }
        System.Windows.Forms.Control Binding { get; }
        bool CheckValue();
        /// <summary>
        /// 编辑器的实际值。
        /// 原则上，set 不予使用。
        /// </summary>
        FuzzyData.FuzzyObject Value { get; set; }
        FuzzyData.FuzzyObject Parent { set; }

    }
}
