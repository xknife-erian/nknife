using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// ��ͼ�����Ľӿ�
    /// </summary>
    public interface IDrawingContext
    {
        /// <summary>
        /// ��ȡ����
        /// </summary>
        Graphics Graphics { get;}
        /// <summary>
        /// ��ȡ����
        /// </summary>
        Pen Pen { get;}
        /// <summary>
        /// ��ȡ��ˢ
        /// </summary>
        Brush Brush { get;}
    }
}
