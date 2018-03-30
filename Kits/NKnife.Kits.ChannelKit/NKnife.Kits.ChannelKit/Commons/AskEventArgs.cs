using System;
using NKnife.Kits.ChannelKit.Dialogs;

namespace NKnife.Kits.ChannelKit.Commons
{
    public class AskEventArgs : EventArgs
    {
        public AskEventArgs(QuestionsEditorDialogViewModel model)
        {
            Model = model;
        }
        public QuestionsEditorDialogViewModel Model { get; set; }
    }
}