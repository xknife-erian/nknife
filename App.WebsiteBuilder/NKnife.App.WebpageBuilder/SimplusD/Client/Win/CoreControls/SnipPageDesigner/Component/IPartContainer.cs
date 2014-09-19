using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public interface IPartContainer
    {
        /// <summary>
        /// 当前Container的父Container
        /// </summary>
        IPartContainer ParentContainer { get;}

        /// <summary>
        /// 所属的SnipPageDesigner(可能就是当前Container本身)
        /// </summary>
        SnipPageDesigner Designer { get;}

        /// <summary>
        /// 子parts
        /// </summary>
        SnipPagePartCollection ChildParts { get;}

        /// <summary>
        /// 宽度的Css
        /// </summary>
        string Width_Css { get;}

        /// <summary>
        /// 宽度的数值表达
        /// </summary>
        int WidthPixel { get;set;}

        /// <summary>
        /// 真实的跨行数
        /// </summary>
        int FactLines { get;}

        /// <summary>
        /// 内部行数
        /// </summary>
        int InnerLines { get;}

        /// <summary>
        /// 真实宽度
        /// </summary>
        int FactWidth { get;}

        /// <summary>
        /// 所占的矩形
        /// </summary>
        Rectangle Bounds { get;}

        /// <summary>
        /// 层级
        /// </summary>
        int Level { get;}

        /// <summary>
        /// 显示的文本
        /// </summary>
        string Text{get;set;}

        /// <summary>
        /// 内边距大小
        /// </summary>
        System.Windows.Forms.Padding Padding { get;}

        /// <summary>
        /// 画其下的子Parts
        /// </summary>
        /// <param name="g"></param>
        void PaintChildParts(Graphics g);

        /// <summary>
        /// 重新布局
        /// </summary>
        void LayoutParts();

        /// <summary>
        /// 通过point参数获取此点所在的SnipPagePart
        /// </summary>
        SnipPagePart GetPartAt(Point point, bool isRecursiveChilds);

        /// <summary>
        /// 重置InterLines属性
        /// </summary>
        void ResetInnerLines(int innerLines);
    }
}
