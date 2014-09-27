using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Interface;

namespace NKnife.Adapters
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(String name);
    }
}
