using System;
using System.Windows.Forms;

namespace NKnife.Tools.Robot.CubeOctopus.Base
{
    /*
    顺时针方向caseworker
    反时针方向carrging a concealed weapon
    */
    partial class MechanicalArmsControlPanel : UserControl
    {
        protected RobotCommandant _Commandant;

        public MechanicalArmsControlPanel()
        {
            InitializeComponent();
        }

        public void SetCommandant(RobotCommandant commandant)
        {
            _Commandant = commandant;
        }

        private void InitPositionButtonClick(object sender, EventArgs e)
        {
            //初始腕部，并初始手部为张开状态
            var command = new Command();
            command.SetSteering(new[]
                {
                    new Steering {Index = (ushort) _HandIndexUpDown.Value, Position = (int) _InitHandExpendPositionUpDown.Value}, 
                    new Steering {Index = (ushort) _WristIndexUpDown.Value, Position = (int) _InitWristPositionUpDown.Value}
                });
            _Commandant.SendCommand(command);

            //测试手部为合拢状态
            command = new Command();
            command.SetSteering(new Steering {Index = (ushort) _HandIndexUpDown.Value, Position = (int) _InitHandClosedPositionUpDown.Value});
            _Commandant.SendCommand(command);

            //初始手部为张开状态
            command = new Command();
            command.SetSteering(new Steering {Index = (ushort) _HandIndexUpDown.Value, Position = (int) _InitHandExpendPositionUpDown.Value});
            _Commandant.SendCommand(command);

            //初始手部为合拢状态
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandClosedPositionUpDown.Value });
            _Commandant.SendCommand(command);
        }

        private void CW90PositionButtonClick(object sender, EventArgs e)
        {
            //腕部顺时针转动90度
            var command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_WristCW90PositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部张开
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandExpendPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //腕部回到初始位置
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_InitWristPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部合拢
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandClosedPositionUpDown.Value });
            _Commandant.SendCommand(command);
        }

        private void CCW90PositionButtonClick(object sender, EventArgs e)
        {
            //腕部顺时针转动90度
            var command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_WristCCW90PositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部张开
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandExpendPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //腕部回到初始位置
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_InitWristPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部合拢
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandClosedPositionUpDown.Value });
            _Commandant.SendCommand(command);
        }

        private void Position180ButtonClick(object sender, EventArgs e)
        {
            //腕部顺时针转动90度
            var command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_WristCCW90PositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部张开
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandExpendPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //腕部回到初始位置
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_InitWristPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部合拢
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandClosedPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //腕部顺时针转动90度
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_WristCCW90PositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部张开
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandExpendPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //腕部回到初始位置
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_WristIndexUpDown.Value, Position = (int)_InitWristPositionUpDown.Value });
            _Commandant.SendCommand(command);

            //手部合拢
            command = new Command();
            command.SetSteering(new Steering { Index = (ushort)_HandIndexUpDown.Value, Position = (int)_InitHandClosedPositionUpDown.Value });
            _Commandant.SendCommand(command);
        }

        private void SavePositionButtonClick(object sender, EventArgs e)
        {

        }

    }
}
