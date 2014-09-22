using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public interface IPartContainer
    {
        /// <summary>
        /// ��ǰContainer�ĸ�Container
        /// </summary>
        IPartContainer ParentContainer { get;}

        /// <summary>
        /// ������SnipPageDesigner(���ܾ��ǵ�ǰContainer����)
        /// </summary>
        SnipPageDesigner Designer { get;}

        /// <summary>
        /// ��parts
        /// </summary>
        SnipPagePartCollection ChildParts { get;}

        /// <summary>
        /// ��ȵ�Css
        /// </summary>
        string Width_Css { get;}

        /// <summary>
        /// ��ȵ���ֵ���
        /// </summary>
        int WidthPixel { get;set;}

        /// <summary>
        /// ��ʵ�Ŀ�����
        /// </summary>
        int FactLines { get;}

        /// <summary>
        /// �ڲ�����
        /// </summary>
        int InnerLines { get;}

        /// <summary>
        /// ��ʵ���
        /// </summary>
        int FactWidth { get;}

        /// <summary>
        /// ��ռ�ľ���
        /// </summary>
        Rectangle Bounds { get;}

        /// <summary>
        /// �㼶
        /// </summary>
        int Level { get;}

        /// <summary>
        /// ��ʾ���ı�
        /// </summary>
        string Text{get;set;}

        /// <summary>
        /// �ڱ߾��С
        /// </summary>
        System.Windows.Forms.Padding Padding { get;}

        /// <summary>
        /// �����µ���Parts
        /// </summary>
        /// <param name="g"></param>
        void PaintChildParts(Graphics g);

        /// <summary>
        /// ���²���
        /// </summary>
        void LayoutParts();

        /// <summary>
        /// ͨ��point������ȡ�˵����ڵ�SnipPagePart
        /// </summary>
        SnipPagePart GetPartAt(Point point, bool isRecursiveChilds);

        /// <summary>
        /// ����InterLines����
        /// </summary>
        void ResetInnerLines(int innerLines);
    }
}
