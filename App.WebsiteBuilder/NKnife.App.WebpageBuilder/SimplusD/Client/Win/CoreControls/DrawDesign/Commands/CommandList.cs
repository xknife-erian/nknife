using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class CommandList
    { 
        #region ���Գ�Ա����

        public SDLinkedList<BaseCommand> Commands { get; set; }

        /// <summary>
        /// ��ǰ����,������֮����
        /// </summary>
        public LinkedListNode<BaseCommand> CurCommand { get; set; }

        #endregion

        #region ���캯��

        public CommandList()
        {
            Commands = new SDLinkedList<BaseCommand>();
        }

        #endregion

        #region ����������Ա�ӿ�

        /// <summary>
        /// ��������ִ��֮
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

            ///�ϵ�commandNode֮����
            Commands.RemoveAfter(commandNode);
        }

        /// <summary>
        /// ��������
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
                if (reDoCommandNode != null)///ִ����������
                {
                    reDoCommandNode.Value.Execute();
                    CurCommand = reDoCommandNode;
                }
            }
        }

        /// <summary>
        /// ��������
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
        /// �Ƿ���Գ���
        /// </summary>
        public bool IsExistUndo()
        {
            return CurCommand != null ? true : false;
        }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsExistRedo()
        {
            return CurCommand == Commands.Last ? false : true;
        }

        #endregion
    }
}
