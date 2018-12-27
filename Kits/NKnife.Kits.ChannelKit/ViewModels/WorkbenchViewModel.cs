using Ninject;
using NKnife.Interface;
using NKnife.IoC;
using ReactiveUI;

namespace NKnife.Kits.ChannelKit.ViewModels
{
    public class WorkbenchViewModel : ReactiveObject
    {
        private string _applicationTitle;
        private readonly IAbout _about;

        [Inject]
        public WorkbenchViewModel(IAbout about)
        {
            _applicationTitle = "SerialPort And Socket Debugger";
            _about = about;
        }
        public string ApplicationTitle
        {
            get => _applicationTitle;
            set => this.RaiseAndSetIfChanged(ref _applicationTitle, value);
        }

        public string SoftwareVersion => _about.AssemblyVersion.ToString();
    }
}