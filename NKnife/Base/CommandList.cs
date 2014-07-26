using System.Collections.Generic;
using NKnife.Interface;

namespace NKnife.Base
{
    /// <summary>
    /// 命令模式中命令接口的集合类
    /// </summary>
    public class CommandList
    {
        #region 属性成员定义

        public LinkedList<ICommand> Commands { get; set; }

        /// <summary>
        ///     当前命令,待撤销之操作
        /// </summary>
        public LinkedListNode<ICommand> CurrentCommand { get; set; }

        #endregion

        #region 构造函数

        public CommandList()
        {
            Commands = new LinkedList<ICommand>();
        }

        #endregion

        #region 公共函数成员接口

        /// <summary>
        ///     添加新命令并执行之
        /// </summary>
        /// <param name="command"></param>
        public ICommand AddCommand(ICommand command)
        {
            var commandNode = new LinkedListNode<ICommand>(command);
            if (CurrentCommand != null)
            {
                Commands.AddAfter(CurrentCommand, commandNode);
            }
            else
            {
                Commands.AddFirst(commandNode);
            }
            CurrentCommand = commandNode;
            return command;
        }

        /// <summary>
        ///     重做命令
        /// </summary>
        public void Execute()
        {
            if (CanExecute())
            {
                var reDoCommandNode = CurrentCommand == null ? Commands.First : CurrentCommand.Next;
                if (reDoCommandNode != null) //执行重做命令
                {
                    reDoCommandNode.Value.Execute();
                    CurrentCommand = reDoCommandNode;
                }
            }
        }

        /// <summary>
        ///     撤销命令
        /// </summary>
        public void Cancel()
        {
            if (CanCancel())
            {
                CurrentCommand.Value.Cancel();
                CurrentCommand = CurrentCommand.Previous;
            }
        }

        /// <summary>
        ///     是否可以撤销
        /// </summary>
        public bool CanCancel()
        {
            return CurrentCommand != null;
        }

        /// <summary>
        ///     是否可以重做
        /// </summary>
        public bool CanExecute()
        {
            return CurrentCommand != Commands.Last;
        }

        #endregion
    }
}