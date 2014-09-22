using System;

namespace Jeelu.SimplusD
{
    public interface IGetPropertiesForPanelable
    {
        object[] GetPropertiesForPanel();
        event EventHandler PropertiesChanged;
    }

}
