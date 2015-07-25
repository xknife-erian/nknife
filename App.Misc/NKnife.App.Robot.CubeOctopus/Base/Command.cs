using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.Tools.Robot.CubeOctopus.Base
{
    /// <summary>描述舵机的指令
    /// </summary>
    class Command
    {
        /// <summary>描述舵机的指令
        /// </summary>
        public Command()
        {
            Velocity = 100;
            _Steerings = new List<Steering>(1);
        }

        protected List<Steering> _Steerings;

        /// <summary>添加新的舵机指令。（不允许对同一舵机进行操作）
        /// </summary>
        public void SetSteering(params Steering[] newSteerings)
        {
            foreach (var newSteering in newSteerings)
            {
                SetStreeing(newSteering);
            }
        }

        private void SetStreeing(Steering newSteering)
        {
            foreach (var steering in _Steerings)
            {
                if (steering.Index == newSteering.Index)
                {
                    throw new ArgumentException(string.Format("单条命令中不允许对同一舵机({0}号舵机)进行操作", steering.Index));
                }
            }
            _Steerings.Add(newSteering);
        }

        /// <summary>返回所有的指令。
        /// </summary>
        /// <returns></returns>
        public Steering[] GetSteerings()
        {
            return _Steerings.ToArray();
        }

        /// <summary>速度。默认值:100
        /// </summary>
        public uint Velocity { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var steering in _Steerings)
            {
                sb.Append(steering.ToString());
            }
            sb.Append('T').Append(Velocity);
            return sb.ToString();
        }
    }
}