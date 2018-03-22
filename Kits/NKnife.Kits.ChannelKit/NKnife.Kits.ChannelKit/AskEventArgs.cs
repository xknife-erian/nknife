using System;
using NKnife.Channels.SerialKnife.Views.Controls;

namespace NKnife.Kits.ChannelKit
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