using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Configuring.Interfaces;

namespace Gean.Configuring.Common
{
    public class OptionPanelPrecondition : IOptionPanelPrecondition
    {
        #region Implementation of IOptionPanelPrecondition

        public bool Check()
        {
            return true;
        }

        #endregion
    }
}
