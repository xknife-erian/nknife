﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace NKnife.Workbench
{
    public interface IAddin
    {
        MenuItem GetMenuItem();

        UserControl GetUserControl();
    }
}
