using System;
using Gean.Configuring.Common;
using NKnife.Configuring.CoderSetting;

namespace Gean.Configuring
{
    public delegate void CoderSettingChangedEventHandler(object sender, CoderSettingChangeEventArgs e);
    public delegate void CoderSettingChangingEventHandler(object sender, CoderSettingChangeEventArgs e);
    public delegate void CoderSettingLoadedEventHandler(object sender, CoderSettingLoadedEventArgs e);

    public delegate void OptionManagerInitializedEventHandler(EventArgs e);
    public delegate void OptionLoadingEventHandler(object sender, OptionLoadEventArgs e);
    public delegate void OptionLoadedEventHandler(object sender, OptionLoadEventArgs e);

    public delegate void OptionTableChangedEventHandler(object sender, OptionTableChangedEventArgs e);
}