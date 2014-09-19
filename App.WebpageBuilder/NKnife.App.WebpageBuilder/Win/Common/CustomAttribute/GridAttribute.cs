using System;

namespace Jeelu.Win
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class GridAttribute : Attribute
    {
        /// <summary>
        /// ΪDataGridView׼������
        /// </summary>
        /// <param name="menuLevel">�˵����𣬶����ַ�����ʽ, ��һ���ģ�����Ϊ"0"; �����ĸ���������Ϊ"1"; �������Ӽ�������Ϊ"1,1""1,2""1,3"</param>
        /// <param name="IsDisplayInGrid">���б����뵽���У��Ƿ���ʾ</param>
        /// <param name="columnDisplayIndex">������ʾʱ������</param>
        /// <param name="HeaderText">�б��⣬����Ϊ��</param>
        /// <param name="columnInMenuText">���ڲ˵��еı��⣬������Ϊ�գ����Ժ��б���һ��</param>
        /// <param name="columnWidth">�еĿ��</param>
        public GridAttribute(
            string menuLevel, bool IsDisplayInGrid, int columnDisplayIndex, 
            string HeaderText, string columnInMenuText, int columnWidth) 
        {
            this._menuLevel = menuLevel;
            this._isDisplayInGrid = IsDisplayInGrid;
            this._columnDisplayIndex = columnDisplayIndex;
            this._headerText = HeaderText;
            this._columnInMenuText = columnInMenuText;
            this._columnWidth = columnWidth;
        }

        private string _menuLevel;
        /// <summary>
        /// �˵����𣬶����ַ�����ʽ
        /// ��һ���ģ�����Ϊ"0"
        /// �������Ӽ�������Ϊ"parentName,1"   "parentName,2"  "parentName,3"
        /// </summary>
        public string MenuLevel
        {
            get { return _menuLevel; }
            set { _menuLevel = value; }
        }

        /// <summary>
        /// ����Ҫ��ʾʱ��ͼƬ(14*14)
        /// </summary>
        private string _showImage;
        /// <summary>
        /// ����Ҫ��ʾʱ��ͼƬ(14*14)
        /// </summary>
        public string ShowImage
        {
            get { return _showImage; }
            set { _showImage = value; }
        }

        /// <summary>
        /// ��ʾ�ַ������ַ�����ʽ
        /// </summary>
        private string _stringFormating;
        /// <summary>
        /// ��ʾ�ַ������ַ�����ʽ
        /// </summary>
        public string StringFormating
        {
            get { return _stringFormating; }
            set { _stringFormating = value; }
        }

        /// <summary>
        /// �е���ʾ����
        /// </summary>
        private int _columnDisplayIndex;
        /// <summary>
        /// �е���ʾ����
        /// </summary>
        public int ColumnDisplayIndex
        {
            get { return _columnDisplayIndex; }
            set { _columnDisplayIndex = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ�ڱ����
        /// </summary>
        private bool _isDisplayInGrid = true;
        /// <summary>
        /// �Ƿ���ʾ�ڱ����
        /// </summary>
        public bool IsDisplayInGrid
        {
            get { return _isDisplayInGrid; }
            set { _isDisplayInGrid = value; }
        }

        /// <summary>
        /// �б���
        /// </summary>
        private string _headerText;
        /// <summary>
        /// �б���
        /// </summary>
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        /// <summary>
        /// ���ڲ˵��е���ʾ�ַ���
        /// </summary>
        private string _columnInMenuText;
        /// <summary>
        /// ���ڲ˵��е���ʾ�ַ���
        /// </summary>
        public string ColumnInMenuText
        {
            get { return _columnInMenuText; }
            set { _columnInMenuText = value; }
        }

        /// <summary>
        /// �Ƿ��ǲ��ɸı��С���п��
        /// </summary>
        private bool _isFixColumn = false;
        /// <summary>
        /// �Ƿ��ǲ��ɸı��С���п��
        /// </summary>
        public bool IsFixColumn
        {
            get { return _isFixColumn; }
            set { _isFixColumn = value; }
        }
        /// <summary>
        /// �п��
        /// </summary>
        private int _columnWidth;
        /// <summary>
        /// �п��
        /// </summary>
        public int ColumnWidth
        {
            get { return _columnWidth; }
            set { _columnWidth = value; }
        }

        /// <summary>
        /// ���Ƿ������
        /// </summary>
        private bool _isOrderColumn;
        /// <summary>
        /// ���Ƿ������
        /// </summary>
        public bool IsOrderColumn
        {
            get { return _isOrderColumn; }
            set { _isOrderColumn = value; }
        }

        /// <summary>
        /// �е��Զ��������ģʽ
        /// </summary>
        private bool _isAutoColumn;
        /// <summary>
        /// �е��Զ��������ģʽ,falseʱ����ΪĬ�ϣ�trueʱ����ΪFill
        /// </summary>
        public bool IsAutoColumn
        {
            get { return _isAutoColumn; }
            set { _isAutoColumn = value; }
        }

    }


}