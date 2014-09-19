using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ToolStripItem�ؼ�����
    /// </summary>
    internal enum ToolStripItemType
    {
        Button, Label, SplitButton, DropDownButton, ComboBox, TextBox, ProgressBar, MenuItem, StatusLabel, Separator, Nothing
    }
    /// <summary>
    /// ToolStripMenuItem�ؼ�����
    /// </summary>
    internal enum MenuItemType
    {
        ComboBox, TextBox, MenuItem, Separator, Nothing
    }
    /// <summary>
    /// �����������Ե�Item
    /// </summary>
    internal enum DropControlType
    {
        MenuItem, SplitButton, DropDownButton, Nothing
    }
    /// <summary>
    /// ToolStrip�ؼ�����
    /// </summary>
    public enum StripType
    {
        Menu, Status, ToolBar, ContextMenu, Nothing
    }
    /// <summary>
    /// �ؼ���ѡ��״̬
    /// </summary>
    internal enum ControlCheckState 
    { 
        NoSetting, isChecked, isNotChecked 
    }
    /// <summary>
    /// ToolStripItem��λ��
    /// </summary>
    internal enum ControlPlace
    { 
        Menu, ToolBar, MenuAndToolbar, Nothing
    }
}
