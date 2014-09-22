using System;

namespace Jeelu.Win
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class GridAttribute : Attribute
    {
        /// <summary>
        /// 为DataGridView准备数据
        /// </summary>
        /// <param name="menuLevel">菜单级别，定义字符串格式, 仅一级的：定义为"0"; 两级的父级：定义为"1"; 两级的子级：定义为"1,1""1,2""1,3"</param>
        /// <param name="IsDisplayInGrid">本列被加入到列中，是否显示</param>
        /// <param name="columnDisplayIndex">列在显示时的索引</param>
        /// <param name="HeaderText">列标题，可以为空</param>
        /// <param name="columnInMenuText">列在菜单中的标题，不可以为空，可以和列标题一致</param>
        /// <param name="columnWidth">列的宽度</param>
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
        /// 菜单级别，定义字符串格式
        /// 仅一级的：定义为"0"
        /// 两级的子级：定义为"parentName,1"   "parentName,2"  "parentName,3"
        /// </summary>
        public string MenuLevel
        {
            get { return _menuLevel; }
            set { _menuLevel = value; }
        }

        /// <summary>
        /// 如需要显示时的图片(14*14)
        /// </summary>
        private string _showImage;
        /// <summary>
        /// 如需要显示时的图片(14*14)
        /// </summary>
        public string ShowImage
        {
            get { return _showImage; }
            set { _showImage = value; }
        }

        /// <summary>
        /// 显示字符串的字符串格式
        /// </summary>
        private string _stringFormating;
        /// <summary>
        /// 显示字符串的字符串格式
        /// </summary>
        public string StringFormating
        {
            get { return _stringFormating; }
            set { _stringFormating = value; }
        }

        /// <summary>
        /// 列的显示索引
        /// </summary>
        private int _columnDisplayIndex;
        /// <summary>
        /// 列的显示索引
        /// </summary>
        public int ColumnDisplayIndex
        {
            get { return _columnDisplayIndex; }
            set { _columnDisplayIndex = value; }
        }

        /// <summary>
        /// 是否显示在表格中
        /// </summary>
        private bool _isDisplayInGrid = true;
        /// <summary>
        /// 是否显示在表格中
        /// </summary>
        public bool IsDisplayInGrid
        {
            get { return _isDisplayInGrid; }
            set { _isDisplayInGrid = value; }
        }

        /// <summary>
        /// 列标题
        /// </summary>
        private string _headerText;
        /// <summary>
        /// 列标题
        /// </summary>
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        /// <summary>
        /// 列在菜单中的显示字符串
        /// </summary>
        private string _columnInMenuText;
        /// <summary>
        /// 列在菜单中的显示字符串
        /// </summary>
        public string ColumnInMenuText
        {
            get { return _columnInMenuText; }
            set { _columnInMenuText = value; }
        }

        /// <summary>
        /// 是否是不可改变大小的列宽度
        /// </summary>
        private bool _isFixColumn = false;
        /// <summary>
        /// 是否是不可改变大小的列宽度
        /// </summary>
        public bool IsFixColumn
        {
            get { return _isFixColumn; }
            set { _isFixColumn = value; }
        }
        /// <summary>
        /// 列宽度
        /// </summary>
        private int _columnWidth;
        /// <summary>
        /// 列宽度
        /// </summary>
        public int ColumnWidth
        {
            get { return _columnWidth; }
            set { _columnWidth = value; }
        }

        /// <summary>
        /// 列是否可排序
        /// </summary>
        private bool _isOrderColumn;
        /// <summary>
        /// 列是否可排序
        /// </summary>
        public bool IsOrderColumn
        {
            get { return _isOrderColumn; }
            set { _isOrderColumn = value; }
        }

        /// <summary>
        /// 列的自动宽度缩放模式
        /// </summary>
        private bool _isAutoColumn;
        /// <summary>
        /// 列的自动宽度缩放模式,false时设置为默认，true时设置为Fill
        /// </summary>
        public bool IsAutoColumn
        {
            get { return _isAutoColumn; }
            set { _isAutoColumn = value; }
        }

    }


}