using System;
using System.Collections.ObjectModel;
using DynamicData;
using Ninject;
using NKnife.ChannelKnife.Model;
using NKnife.Interface;
using NKnife.IoC;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class WorkbenchViewModel : ReactiveObject
    {
        private IAbout _about;

        [Inject]
        public WorkbenchViewModel(IAbout about)
        {
            _about = about;
            Version = _about.AssemblyVersion.ToString();
        }

        [Inject]
        public SerialChannelService SerialChannelService { get; }

        public ChangeAwareCache<SerialPortViewModel, ushort> SerialPorts { get; set; } = new ChangeAwareCache<SerialPortViewModel, ushort>();

        public string Version { get; }
    }
}
