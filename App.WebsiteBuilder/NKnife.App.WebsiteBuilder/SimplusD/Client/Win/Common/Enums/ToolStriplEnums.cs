using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ToolStripItem控件类型
    /// </summary>
    internal enum ToolStripItemType
    {
        Button, Label, SplitButton, DropDownButton, ComboBox, TextBox, ProgressBar, MenuItem, StatusLabel, Separator, Nothing
    }
    /// <summary>
    /// ToolStripMenuItem控件类型
    /// </summary>
    internal enum MenuItemType
    {
        ComboBox, TextBox, MenuItem, Separator, Nothing
    }
    /// <summary>
    /// 具有下拉特性的Item
    /// </summary>
    internal enum DropControlType
    {
        MenuItem, SplitButton, DropDownButton, Nothing
    }
    /// <summary>
    /// ToolStrip控件类型
    /// </summary>
    public enum StripType
    {
        Menu, Status, ToolBar, ContextMenu, Nothing
    }
    /// <summary>
    /// 控件的选中状态
    /// </summary>
    internal enum ControlCheckState 
    { 
        NoSetting, isChecked, isNotChecked 
    }
    /// <summary>
    /// ToolStripItem的位置
    /// </summary>
    internal enum ControlPlace
    { 
        Menu, ToolBar, MenuAndToolbar, Nothing
    }
}
