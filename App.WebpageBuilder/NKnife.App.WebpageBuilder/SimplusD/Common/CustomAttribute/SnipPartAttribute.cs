using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    sealed public class SnipPartAttribute : Attribute
    {
        /// <summary>
        /// Page的派生类型中属性的定制特性
        /// </summary>
        /// <param name="text">工具的显示文本</param>
        /// <param name="toolTipCaption">工具的提示文本的标题</param>
        /// <param name="toolTipText">工具的提标文本</param>
        /// <param name="index">图片在ImageList中的索引</param>
        public SnipPartAttribute(string name, string text, string toolTipCaption, string toolTipText, int index, float width)
        {
            this.Name = name;
            this.Text = text;
            this.ToolTipCaption = toolTipCaption;
            this.ToolTipText = toolTipCaption;
            this.Index = index;
            this.Width = width;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public string ToolTipCaption { get; set; }
        public string ToolTipText { get; set; }
        public float Width { get; set; }
        public int Index { get; set; }
    }
}