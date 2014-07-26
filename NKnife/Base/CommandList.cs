using System.Collections.Generic;
using NKnife.Interface;

namespace NKnife.Base
{
    /// <summary>
    /// ����ģʽ������ӿڵļ�����
    /// </summary>
    public class CommandList
    {
        #region ���Գ�Ա����

        public LinkedList<ICommand> Commands { get; set; }

        /// <summary>
        ///     ��ǰ����,������֮����
        /// </summary>
        public LinkedListNode<ICommand> CurrentCommand { get; set; }

        #endregion

        #region ���캯��

        public CommandList()
        {
            Commands = new LinkedList<ICommand>();
        }

        #endregion

        #region ����������Ա�ӿ�

        /// <summary>
        ///     ��������ִ��֮
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
        ///     ��������
        /// </summary>
        public void Execute()
        {
            if (CanExecute())
            {
                var reDoCommandNode = CurrentCommand == null ? Commands.First : CurrentCommand.Next;
                if (reDoCommandNode != null) //ִ����������
                {
                    reDoCommandNode.Value.Execute();
                    CurrentCommand = reDoCommandNode;
                }
            }
        }

        /// <summary>
        ///     ��������
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
        ///     �Ƿ���Գ���
        /// </summary>
        public bool CanCancel()
        {
            return CurrentCommand != null;
        }

        /// <summary>
        ///     �Ƿ��������
        /// </summary>
        public bool CanExecute()
        {
            return CurrentCommand != Commands.Last;
        }

        #endregion
    }
}