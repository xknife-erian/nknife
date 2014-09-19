using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class CommandManager
    {
        List<Command> _commands = new List<Command>();

        private int _batchDepth= 0;
        List<Command> _batchCommand = new List<Command>();

        private int _currentStep = -1;

        private SnipPageDesigner _designer;
        public SnipPageDesigner Designer
        {
            get { return _designer; }
        }

        public CommandManager(SnipPageDesigner designer)
        {
            _designer = designer;
        }

        public bool CanUndo
        {
            get { return _currentStep >= 0; }
        }

        public bool CanRedo
        {
            get { return _commands.Count > (_currentStep + 1); }
        }

        public void Undo()
        {
            if (CanUndo)
            {
                Command cmd = _commands[_currentStep--];
                cmd.Unexecute();

                ///����ѡ�����������Ķ���Designer��IsModified������Ϊtrue
                if (!(cmd is SelectPartCommand || cmd is UnSelectPartCommand))
                {
                    Designer.IsModified = true;
                }
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                Command cmd = _commands[++_currentStep];
                cmd.Execute();

                ///����ѡ�����������Ķ���Designer��IsModified������Ϊtrue
                if (!(cmd is SelectPartCommand || cmd is UnSelectPartCommand))
                {
                    Designer.IsModified = true;
                }
            }
        }

        private void AddCommand(Command cmd)
        {
            ///���������������,���Ȳ�����_commands�У����Ƿ���_batchCommand�
            if (_batchDepth > 0)
            {
                _batchCommand.Add(cmd);
                return;
            }

            ///�����ǰ���費�����һ������ɾ����ǰ����֮���������
            if (_commands.Count > (_currentStep + 1))
            {
                _commands.RemoveRange(_currentStep + 1, _commands.Count - _currentStep - 1);
            }

            ///��ӵ�_commands�������ǰ�����1
            _commands.Add(cmd);
            _currentStep++;
        }

        /// <summary>
        /// ��ʼִ�������
        /// </summary>
        public void BeginBatchCommand()
        {
            _batchDepth++;
        }

        /// <summary>
        /// ����ִ�������
        /// </summary>
        public void EndBatchCommand()
        {
            _batchDepth--;

            if (_batchDepth == 0 && _batchCommand.Count > 0)
            {
                BatchCommand cmd = new BatchCommand(_batchCommand.ToArray());
                _batchCommand.Clear();
                AddCommand(cmd);
            }
            else if (_batchDepth < 0)
            {
                throw new Exception("�����ڴ���_batchDepth����С��0");
            }
        }

        /// <summary>
        /// ȡ�����������ִ�е����������
        /// </summary>
        public void CancenBatchCommand()
        {
            _batchDepth--;

            if (_batchDepth == 0 && _batchCommand.Count > 0)
            {
                BatchCommand cmd = new BatchCommand(_batchCommand.ToArray());
                _batchCommand.Clear();
                cmd.Unexecute();
            }
            else if (_batchDepth < 0)
            {
                throw new Exception("�����ڴ���_batchDepth����С��0");
            }
        }

        public void ClearCommand()
        {
            _commands.Clear();
            _currentStep = -1;
            _batchDepth = 0;
        }

        public void AddExecSetPropertyPartCommand<T>(SnipPagePart part, T oldValue, T newValue, SetPropertyCore<T> setCoreMethod)
        {
            AddExecSetPropertyPartCommand<T>(part, oldValue, newValue, setCoreMethod, PartAction.None);
        }
        public void AddExecSetPropertyPartCommand<T>(SnipPagePart part, T oldValue, T newValue, SetPropertyCore<T> setCoreMethod, PartAction action)
        {
            SetPropertyPartCommand<T> cmd = new SetPropertyPartCommand<T>(part, oldValue, newValue, setCoreMethod, action);
            cmd.Execute();
            AddCommand(cmd);
            Designer.IsModified = true;
        }

        public void AddExecSetPropertyDesignerCommand<T>(SnipPageDesigner designer, T oldValue, T newValue, SetPropertyCore<T> setCoreMethod,PartAction action)
        {
            SetPropertyDesignerCommand<T> cmd = new SetPropertyDesignerCommand<T>(designer, oldValue, newValue, setCoreMethod,action);
            cmd.Execute();
            AddCommand(cmd);
            Designer.IsModified = true;
        }

        public void AddExecAddPartCommand(IPartContainer parent, SnipPagePart part)
        {
            AddPartCommand cmd = new AddPartCommand(parent, part);
            cmd.Execute();
            AddCommand(cmd);
            Designer.IsModified = true;
        }
        public void AddExecInsertPartCommand(SnipPagePart part, IPartContainer parent, int index)
        {
            InsertPartCommand cmd = new InsertPartCommand(part, parent, index);
            cmd.Execute();
            AddCommand(cmd);
            Designer.IsModified = true;
        }
        public void AddExecRemovePartCommand(SnipPagePart part)
        {
            RemovePartCommand cmd = new RemovePartCommand(part);
            cmd.Execute();
            AddCommand(cmd);
            Designer.IsModified = true;
        }
        public void AddExecSelectPartCommand( SnipPagePart part)
        {
            SelectPartCommand cmd = new SelectPartCommand( part);
            cmd.Execute();
            AddCommand(cmd);
        }
        public void AddExecUnSelectPartCommand( SnipPagePart part)
        {
            UnSelectPartCommand cmd = new UnSelectPartCommand( part);
            cmd.Execute();
            AddCommand(cmd);
        }
        public void AddExecClearSelectCommand(SnipPageDesigner designer)
        {
            ClearSelectCommand cmd = new ClearSelectCommand(designer);
            cmd.Execute();
            AddCommand(cmd);
        }
        public void AddExecClearPartCommand(SnipPageDesigner designer)
        {
            ClearPartCommand cmd = new ClearPartCommand(designer);
            cmd.Execute();
            AddCommand(cmd);
            Designer.IsModified = true;
        }
    }

    internal delegate void SetPropertyCore<T>(T value);

    internal enum PartAction
    {
        None        = 0,
        Invalidate  = 1,
        Relayout    = 2,
    }
}
