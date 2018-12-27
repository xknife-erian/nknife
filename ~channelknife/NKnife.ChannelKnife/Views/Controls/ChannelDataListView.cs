using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using NKnife.ChannelKnife.Controller;
using NKnife.ChannelKnife.ViewModel;
using NKnife.ChannelKnife.ViewModel.Common;

namespace NKnife.ChannelKnife.Views.Controls
{
    public partial class ChannelDataListView : UserControl
    {
        private ChannelDataViewModel _SerialDataListViewViewData;

        public ChannelDataViewModel SerialDataListViewViewData
        {
            get { return _SerialDataListViewViewData; }
            set
            {
                _SerialDataListViewViewData = value;
                if (_SerialDataListViewViewData != null)
                {
                    _SerialDataListViewViewData.Datas.CollectionChanged+= DatasOnCollectionChanged;
                }
            }
        }

        private void DatasOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (ChannelData newItem in e.NewItems)
                AddData(newItem);
            _SerialDataListViewViewData.Datas.CollectionChanged -= DatasOnCollectionChanged;
            foreach (ChannelData newItem in e.NewItems)
                _SerialDataListViewViewData.Datas.Remove(newItem);
            _SerialDataListViewViewData.Datas.CollectionChanged += DatasOnCollectionChanged;
        }

        public ChannelDataListView()
        {
            InitializeComponent();
        }

        private void AddData(ChannelData channelData)
        {
            var item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss,fff")) {UseItemStyleForSubItems = false};
            var subItem = new ListViewItem.ListViewSubItem(item, channelData.Content) {};
            if (channelData.IsAsk)
                subItem.BackColor = Color.LightSteelBlue;
            item.SubItems.Add(subItem);
            _ListView.ThreadSafeInvoke(() =>
            {
                var count = _ListView.Items.Count;
                if (count > 500)
                {
                    _ListView.Items.RemoveAt(count - 1);
                }
                _ListView.Items.Insert(0, item);
            });
        }

        public void Clear()
        {
            _ListView.Items.Clear();
        }
    }
}