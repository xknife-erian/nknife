using System.Collections.Generic;

namespace NKnife.App.CharMatrix.Outline.Data
{
    /// <summary>
    /// ��������
    /// </summary>
    public class DOutline
    {
        private uint width;
        private uint height;
        private IList<DPolygon> polygons;

        public DOutline(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            this.polygons = new List<DPolygon>();
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        public uint Width
        {
            get { return width; }
        }
        /// <summary>
        /// ��ȡ�߶�
        /// </summary>
        public uint Height
        {
            get { return height; }
        }

        /// <summary>
        /// ��ȡ������б�
        /// </summary>
        public IList<DPolygon> Polygons
        {
            get { return polygons; }
        }
    }
}
