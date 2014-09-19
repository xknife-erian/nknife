using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusOM.Client
{
    public interface IOMCommand
    {
        void NewCmd();
        void EditCmd();
        void FrozedCmd();
        void DeleteCmd();
        void CancelCmd();
        void SaveCmd();

        void ChargeCmd();
        void ReturnSetCmd();
        void ReturnCmd();
        void CheckCmd();

        void CloseCmd();

    }
}