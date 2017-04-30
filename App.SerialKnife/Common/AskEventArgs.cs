using System;
using NKnife.Channels.SerialKnife.Views.Controls;

namespace NKnife.Channels.SerialKnife.Common
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