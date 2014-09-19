using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class CommandList
    { 
        #region 属性成员定义

        public SDLinkedList<BaseCommand> Commands { get; set; }

        /// <summary>
        /// 当前命令,待撤销之操作
        /// </summary>
        public LinkedListNode<BaseCommand> CurCommand { get; set; }

        #endregion

        #region 构造函数

        public CommandList()
        {
            Commands = new SDLinkedList<BaseCommand>();
        }

        #endregion

        #region 公共函数成员接口

        /// <summary>
        /// 添加新命令并执行之
        /// </summary>
        /// <param name="command"></param>
        public void AddCommand(BaseCommand command)
        {
            LinkedListNode<BaseCommand> commandNode = new LinkedListNode<BaseCommand>(command);
            if (CurCommand!=null)
            {
                Commands.AddAfter(CurCommand, commandNode);
            }
            else
            {
                Commands.AddFirst(commandNode);
            }
            CurCommand = commandNode;

            ///断掉commandNode之后结点
            Commands.RemoveAfter(commandNode);
        }

        /// <summary>
        /// 重做命令
        /// </summary>
        public void ReDo()
        {
            if (IsExistRedo())
            {
                LinkedListNode<BaseCommand> reDoCommandNode = CurCommand;
                if (CurCommand == null)
                {
                    reDoCommandNode = Commands.First;
                }
                else
                {
                    reDoCommandNode = CurCommand.Next;
                }
                if (reDoCommandNode != null)///执行重做命令
                {
                    reDoCommandNode.Value.Execute();
                    CurCommand = reDoCommandNode;
                }
            }
        }

        /// <summary>
        /// 撤销命令
        /// </summary>
        public void UnDo()
        {
            if (IsExistUndo())
            {
                CurCommand.Value.UnExecute();
                CurCommand = CurCommand.Previous;
            }
        }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        public bool IsExistUndo()
        {
            return CurCommand != null ? true : false;
        }

        /// <summary>
        /// 是否可以重做
        /// </summary>
        public bool IsExistRedo()
        {
            return CurCommand == Commands.Last ? false : true;
        }

        #endregion
    }
}
