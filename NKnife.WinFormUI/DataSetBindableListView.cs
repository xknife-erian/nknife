using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace NKnife.Draws.WinForm
{
    /// <summary>
    ///     This class inherits from the ListView control
    ///     and allows binding the control to a datasource
    /// </summary>
    public class DataSetBindableListView : ListView
    {
        #region --METHODS--

        /// <summary>
        ///     This method is called everytime the DataMember property is set
        /// </summary>
        private void Bind()
        {
            Items.Clear(); //clear the existing list

            if (DataSource is DataSet)
            {
                var ds = DataSource as DataSet;
                var dt = ds.Tables[0];

                if (dt != null)
                {
                    _CurrencyManager = (CurrencyManager) BindingContext[ds, ds.Tables[0].TableName];
                    _CurrencyManager.CurrentChanged += CurrencyManager_CurrentChanged;
                    _DataView = (DataView) _CurrencyManager.List;

                    Columns.Add(DataMember, ClientRectangle.Width - 17, HorizontalAlignment.Left);

                    foreach (DataRow dr in dt.Rows)
                    {
                        var lvi = new ListViewItem(dr[DataMember].ToString());
                        lvi.Tag = dr;
                        Items.Add(lvi);
                    }
                    Sorting = SortOrder.Ascending;
                    _DataView.Sort = Columns[0].Text + " ASC";
                }
            }
            else
            {
                _CurrencyManager = null;
            }
        }

        #endregion

        #region --SYSTEM CODE--

        private readonly Container _Components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            // 
            // DataSetBindableListView
            // 
            this.Name = "DataSetBindableListView";
        }

        #endregion

        #endregion

        #region --VARIABLES--

        private CurrencyManager _CurrencyManager;
        private DataView _DataView;

        #endregion

        #region --CONSTRUCTOR & DESTRUCTOR--

        public DataSetBindableListView()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            SelectedIndexChanged += DataSetBindableListView_SelectedIndexChanged;
            ColumnClick += DataSetBindableListView_ColumnClick;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region --PROPERTIES--

        #region --DATASOURCE--

        [Bindable(true)]
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [DefaultValue(null)]
        public object DataSource { get; set; }

        #endregion

        #region --DATAMEMBER--

        private string data;

        [Category("Data"), Bindable(false)]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("")]
        public string DataMember
        {
            get { return data; }
            set
            {
                data = value;
                Bind();
            }
        }

        #endregion

        #region --SORTING--

        [Browsable(false)]
        public new SortOrder Sorting
        {
            get { return base.Sorting; }
            set { base.Sorting = value; }
        }

        #endregion

        [Browsable(false)]
        protected new bool MultiSelect
        {
            get { return base.MultiSelect; }
            set { base.MultiSelect = false; }
        }

        #endregion

        #region --EVENTS--

        private void DataSetBindableListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndices.Count > 0)
            {
                if (_CurrencyManager != null)
                {
                    _CurrencyManager.Position = SelectedIndices[0];
                }
            }
        }

        private void DataSetBindableListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Sorting == SortOrder.None || Sorting == SortOrder.Descending)
            {
                Sorting = SortOrder.Ascending;
                _DataView.Sort = Columns[0].Text + " ASC";
            }
            else if (Sorting == SortOrder.Ascending)
            {
                Sorting = SortOrder.Descending;
                _DataView.Sort = Columns[0].Text + " DESC";
            }
        }

        private void CurrencyManager_CurrentChanged(object sender, EventArgs e)
        {
            Items[_CurrencyManager.Position].Selected = true;
        }

        #endregion
    }
}