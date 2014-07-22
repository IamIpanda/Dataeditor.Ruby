using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Contract
{
    /// <summary>
    /// 当控件自行处理控件获得和失去焦点的信息时
    /// 继承此接口。
    /// </summary>
    public interface FocusDependent
    {
        void OnEnter(object sender,EventArgs e);
        void OnLeave(object sender, EventArgs e);
    }
}
