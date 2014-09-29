﻿using System.ComponentModel;

namespace NKnife.App.TouchIme.TouchInput.Controls.Keys
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogicalKey : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// 
        /// </summary>
        void Press();
        /// <summary>
        /// 
        /// </summary>
        event LogicalKeyPressedEventHandler LogicalKeyPressed;
    }
}