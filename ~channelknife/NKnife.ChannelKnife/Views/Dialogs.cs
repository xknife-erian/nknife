using Ninject;

namespace NKnife.ChannelKnife.Views
{
    public class Dialogs
    {
        [Inject]
        public PortSelectorDialog PortSelectorDialog { get; set; }
    }
}
