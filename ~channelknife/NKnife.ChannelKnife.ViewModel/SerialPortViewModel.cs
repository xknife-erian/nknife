using System;
using System.Text;
using Common.Logging;
using NKnife.ChannelKnife.Model;
using NKnife.ChannelKnife.ViewModel.Common;
using NKnife.IoC;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class SerialPortViewModel : ReactiveObject
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialPortViewModel>();

        //private SerialChannel _BindChannel;

        private long _RxCount;
        private long _TxCount;
        private bool _HexShowEnable = true;
        private bool _IsFormatText = false;
        private readonly SerialChannelService _ChannelService = DI.Get<SerialChannelService>();

        public SerialPortViewModel()
        {
            Config.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(Config.IsHexShow):
                    {
                        _HexShowEnable = Config.IsHexShow;
                        break;
                    }
                    case nameof(Config.IsFormatText):
                    {
                        _IsFormatText = Config.IsFormatText;
                        break;
                    }
                }
            };
        }

        public ushort Port { get; set; }

        public bool IsSync { get; set; } = false;

        public ConfigPanelViewModel Config { get; } = new ConfigPanelViewModel();

        public QuestionsEditorPanelViewModel Questions { get; } = new QuestionsEditorPanelViewModel();

        public ChannelDataViewModel DataView { get; } = new ChannelDataViewModel();

        /// <summary>
        /// 是否在数据区中显示发送的数据
        /// </summary>
        public bool IsDisplayQuestion { get; set; } = true;

        public long RxCount
        {
            get { return _RxCount; }
            set { this.RaiseAndSetIfChanged(ref _RxCount, value); }
        }

        public long TxCount
        {
            get { return _TxCount; }
            set { this.RaiseAndSetIfChanged(ref _TxCount, value); }
        }

        public void SerialOpen(bool isPause)
        {
            /*if (!isPause)
            {
                _BindChannel = _ChannelService.AddSerial(Config.Export(Port));
                _BindChannel.IsSynchronous = IsSync;
                if (!IsSync)
                {
                    _BindChannel.DataArrived += OnAsyncDataArrived;
                }
                _BindChannel.Open();
            }
            else
            {
                _BindChannel.DataArrived += OnAsyncDataArrived;
            }
            Questions.SerialEnable = true;*/
        }

        public void SerialPause()
        {
            /*if (!IsSync)
                _BindChannel.DataArrived -= OnAsyncDataArrived;
            _BindChannel.Break();
            Questions.SerialEnable = false;*/
        }

        public void SerialClose()
        {
            /*_BindChannel?.Break();
            _BindChannel?.Close();
            Questions.SerialEnable = false;*/
        }

        public void Ask()
        {
            /*switch (Questions.Mode)
            {
                case AskMode.Single:
                {
                    //将打算发出的问题绑定到channel上
                    var questionGroup = new SerialQuestionGroup();
                    var content = Questions.SingleQuestions.Content;
                    var question = new SerialQuestion(_BindChannel, null, null, Questions.SingleQuestions.IsLoop, content);
                    questionGroup.Add(question);
                    _BindChannel.UpdateQuestionGroup(questionGroup);

                    //根据channel的同异步状态执行
                    if (!IsSync)
                    {
                        //当异步时
                        if (Questions.SingleQuestions.IsLoop)
                            _BindChannel.TalkTotalTimeout = Questions.SingleQuestions.Time;
                        _BindChannel.AutoSend(OnAsyncDataSend);
                    }
                    else
                    {
                        //当同步时
                        _BindChannel.SendReceiving(OnSyncSend, OnSyncReceived);
                    }
                    break;
                }
                case AskMode.Multiterm:
                case AskMode.User:
                    break;
            }*/
        }

        public void EndAsk()
        {
            //_BindChannel.Break();
        }

/*

        private void OnAsyncDataArrived(object sender, ChannelAnswerDataEventArgs<byte[]> e)
        {
            var exhibitData = (SerialAnswer) (e.Answer);
            DisplayReceivedData(exhibitData);
        }

        private ushort _SyncReceivedCount = 0;

        private bool OnSyncReceived(IAnswer<byte[]> obj)
        {
            var exhibitData = (SerialAnswer) (obj);
            DisplayReceivedData(exhibitData);
            if (_SyncReceivedCount < 3)
            {
                _SyncReceivedCount++;
                return false;
            }
            _SyncReceivedCount = 0;
            return true;
        }

        private void OnAsyncDataSend(IQuestion<byte[]> question)
        {
            DisplaySendData(((SerialQuestion) question).Data);
        }

        private void OnSyncSend(IQuestion<byte[]> question)
        {
            DisplaySendData(((SerialQuestion) question).Data);
        }

        private readonly object _DataViewDatasLock = new object();

        private void DisplaySendData(byte[] data)
        {
            TxCount = TxCount + data.Length;
            if (IsDisplayQuestion)
            {
                var viewData = new ChannelData();
                viewData.IsAsk = true;
                viewData.Content = _HexShowEnable ? data.ToHexString() : Encoding.Default.GetString(data);
                lock (_DataViewDatasLock)
                {
                    try
                    {
                        DataView.Datas.Add(viewData);
                    }
                    catch (Exception e)
                    {
                        _logger.Error($"添加数据时异常:{e.Message}, DataView.Datas.Count:{DataView.Datas.Count}", e);
                    }
                }
            }
        }

        private void DisplayReceivedData(SerialAnswer answer)
        {
            RxCount = RxCount + answer.Data.Length;
            var viewData = new ChannelData();
            viewData.IsAsk = false;
            viewData.Content = _HexShowEnable ? answer.Data.ToHexString() : Encoding.Default.GetString(answer.Data);
            lock (_DataViewDatasLock)
            {
                try
                {
                    DataView.Datas.Add(viewData);
                }
                catch (Exception e)
                {
                    _logger.Error($"添加数据时异常:{e.Message}, DataView.Datas.Count:{DataView.Datas.Count}", e);
                }
            }
        }*/
    }
}