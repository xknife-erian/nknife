using System;
using SocketKnife.Controls;
using SocketKnife.Views.Controls;

namespace SocketKnife.Common
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