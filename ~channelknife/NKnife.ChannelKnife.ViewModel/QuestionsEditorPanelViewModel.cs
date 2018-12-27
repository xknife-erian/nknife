using System.Collections.Generic;
using NKnife.ChannelKnife.ViewModel.Common;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class QuestionsEditorPanelViewModel : ReactiveObject
    {
        private bool _SerialEnable;

        public bool SerialEnable
        {
            get => _SerialEnable;
            set => this.RaiseAndSetIfChanged(ref _SerialEnable, value);
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