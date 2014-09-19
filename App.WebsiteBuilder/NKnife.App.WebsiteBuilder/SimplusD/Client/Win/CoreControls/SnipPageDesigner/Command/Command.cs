using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 所有命令类的基类
    /// </summary>
    abstract internal class Command
    {
        public Command()
        {
        }

        public virtual void Execute()
        {
        }
        public virtual void Unexecute()
        {
        }
    }
}
