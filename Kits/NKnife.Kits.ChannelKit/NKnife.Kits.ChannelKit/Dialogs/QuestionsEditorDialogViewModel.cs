﻿using System.Collections.Generic;
using GalaSoft.MvvmLight;
using NKnife.Kits.ChannelKit.Commons;

namespace NKnife.Kits.ChannelKit.Dialogs
{
    public class QuestionsEditorDialogViewModel : ViewModelBase
    {
        private bool _serialEnable;

        public bool SerialEnable
        {
            get { return _serialEnable; }
            set { Set(() => SerialEnable, ref _serialEnable, value); }
        }

        public AskMode Mode { get; set; }
        public Single SingleQuestions { get; set; } = new Single();
        public Multiterm MultitermQuestions { get; set; } = new Multiterm();
        public User UserQuestions { get; set; } = new User();

        public class Single
        {
            public byte[] Content { get; set; }
            public uint Time { get; set; } = 2;
            public bool IsLoop { get; set; } = false;
        }

        public class Multiterm
        {
            public List<byte[]> Content { get; set; }
        }

        public class User
        {
            public List<byte[]> Content { get; set; }
        }
    }
}