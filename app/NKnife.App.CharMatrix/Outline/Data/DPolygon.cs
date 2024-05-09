using System.Collections.Generic;

namespace NKnife.App.CharMatrix.Outline.Data
{
    /// <summary>
    /// ���������
    /// </summary>
    public class DPolygon
    {
        private int type;
        private POINTFX start;
        private IList<DLine> lines;

        public DPolygon(int type)
        {
            this.type = type;
            this.lines = new List<DLine>();
        }
        /// <summary>
        /// ��ȡ������
        /// </summary>
        public POINTFX Start
        {
            get { return start; }
            set { start = value; }
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public int Type
        {
            get { return type; }
        }
        /// <summary>
        /// ��ȡ�߶��б�
        /// </summary>
        public IList<DLine> Lines
        {
            get { return lines; }
        }
    }
}
