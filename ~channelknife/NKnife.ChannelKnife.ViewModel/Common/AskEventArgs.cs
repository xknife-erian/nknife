using System;

namespace NKnife.ChannelKnife.ViewModel.Common
{
    public class AskEventArgs : EventArgs
    {
        public AskEventArgs(QuestionsEditorPanelViewModel model)
        {
            Model = model;
        }
        public QuestionsEditorPanelViewModel Model { get; set; }
    }
}