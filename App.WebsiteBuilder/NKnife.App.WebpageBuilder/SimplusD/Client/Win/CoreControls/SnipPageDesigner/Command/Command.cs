using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ����������Ļ���
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
