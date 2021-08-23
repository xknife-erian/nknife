using System.Collections.Generic;

namespace NKnife.Pattern
{
    /// <summary>
    ///     ����ģʽ������ӿڵļ�����
    /// </summary>
    public class CommandList
    {
        #region ���Գ�Ա����

        public LinkedList<ICommandPattern> Commands { get; set; }

        /// <summary>
        ///     ��ǰ����,������֮����
        /// </summary>
        public LinkedListNode<ICommandPattern> CurrentCommand { get; set; }

        #endregion

        #region ���캯��

        public CommandList()
        {
            Commands = new LinkedList<ICommandPattern>();
        }

        #endregion

        #region ����������Ա�ӿ�

        /// <summary>
        ///     ��������ִ��֮
        /// </summary>
        /// <param name="command"></param>
        public ICommandPattern AddCommand(ICommandPattern command)
        {
            var commandNode = new LinkedListNode<ICommandPattern>(command);
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
        public void Execute(object parameter)
        {
            if (CanExecute())
            {
                LinkedListNode<ICommandPattern> reDoCommandNode = CurrentCommand == null
                    ? Commands.First
                    : CurrentCommand.Next;
                if (reDoCommandNode != null) //ִ����������
                {
                    reDoCommandNode.Value.Execute(parameter);
                    CurrentCommand = reDoCommandNode;
                }
            }
        }

        /// <summary>
        ///     ��������
        /// </summary>
        public void Cancel(object parameter)
        {
            if (CanCancel())
            {
                CurrentCommand.Value.Cancel(parameter);
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