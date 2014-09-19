using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;

namespace NKnife.App.SocketKit.Commands
{
    class Class1 : DelegateCommand
    {
        public Class1(Action executeMethod) : base(executeMethod)
        {
        }

        public Class1(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }
    }
}
