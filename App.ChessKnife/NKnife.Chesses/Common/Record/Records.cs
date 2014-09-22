using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using NKnife.Interface;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 描述一个记载有多个棋局记录文件中的数据封装类型。
    /// 该文件将被解析成一个IList&lt;Record&gt;集合,即本类型。
    /// 该类型实现了PGN解析接口。
    /// </summary>
    public class Records : ICollection<Record>, IPGNReaderEvents
    {

        private List<Record> _chessRecords = new List<Record>();

        #region IGameReaderEvents 成员

        private Stack _states;
        private string _lastNumber;
        private Step _tmpStep;
        private Annotation _tmpComment;
        private ITree _tmpStepTree = null;

        public void NewGame(IPGNReader iParser)
        {
            _states = new Stack();
            _tmpStepTree = new Record();
            _chessRecords.Add((Record)_tmpStepTree);
        }

        public void ExitHeader(IPGNReader iParser)
        {
        }

        public void EnterVariation(IPGNReader iParser)
        {
            _states.Push(_lastNumber);
            if (_tmpStep != null)
            {
                _tmpStep.Parent = _tmpStepTree;
                _tmpStepTree = _tmpStep;
                if (_tmpStepTree.Items == null)
                {
                    _tmpStepTree.Items = new Variation();
                }
                //_tmpStepTree.Items.Add(_tmpStep);
                _tmpStep = null;
            }
            else
            {
                ITree tmpTree = null;
                int i = 1;
                while (!(_tmpStepTree.Items[_tmpStepTree.Items.Count - i] is ITree))
                {
                    i++;
                }
                tmpTree = (ITree)_tmpStepTree.Items[_tmpStepTree.Items.Count - i];
                tmpTree.Parent = _tmpStepTree;
                _tmpStepTree = tmpTree;
            }
            if (_tmpStepTree.Items == null)
            {
                _tmpStepTree.Items = new Steps();
            }
        }

        public void ExitVariation(IPGNReader iParser)
        {
            if (iParser.State != Enums.PGNReaderState.Number)
                StepParsed(iParser);
            _tmpStepTree = (ITree)_tmpStepTree.Parent;
            _lastNumber = (string)_states.Pop();
        }

        public void Starting(IPGNReader iParser)
        {
        }

        public void Finished(IPGNReader iParser)
        {
            _states = new Stack();
        }

        public void TagParsed(IPGNReader iParser)
        {
            try
            {
                ((Record)_tmpStepTree).Tags.Set<string>(iParser.Tag, iParser.Value);
            }
            catch (RecordException e)
            {
                throw e;
            }
        }

        public void NagParsed(IPGNReader iParser)
        {
            if (_tmpStep != null)
            {
                _tmpStepTree.Items.Add(_tmpStep);
                _tmpStep = null;
            }
            Nag nag = new Nag(iParser.Value);
            _tmpStepTree.Items.Add(nag);
        }

        public void StepParsed(IPGNReader iParser)
        {
            if (iParser.State == Enums.PGNReaderState.Number)
            {
                _lastNumber = iParser.Value;
            }
            else if (iParser.State == Enums.PGNReaderState.White)
            {
                _tmpStep = new Step();
                _tmpStep.Number = int.Parse(_lastNumber);
                _tmpStep.GameSide = Enums.GameSide.White;
                _tmpStep.Parse(iParser.Value);
                _tmpStepTree.Items.Add(_tmpStep);
            }
            else if (iParser.State == Enums.PGNReaderState.Black)
            {
                if (string.IsNullOrEmpty(iParser.Value))
                    return;
                _tmpStep = new Step();
                _tmpStep.Number = int.Parse(_lastNumber);
                _tmpStep.GameSide = Enums.GameSide.Black;
                _tmpStep.Parse(iParser.Value);
                _tmpStepTree.Items.Add(_tmpStep);
            }
        }

        public void CommentParsed(IPGNReader iParser)
        {
            _tmpComment = Annotation.Parse(iParser.Value);
            _tmpStepTree.Items.Add(_tmpComment);
        }

        public void EndMarker(IPGNReader iParser)
        {
            GameResult end = GameResult.Parse(iParser.Value);
            _tmpStepTree.Items.Add(end);
        }

        #endregion

        #region IEnumerable<Record> 成员

        public IEnumerator<Record> GetEnumerator()
        {
            return _chessRecords.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _chessRecords.GetEnumerator();
        }

        #endregion

        public Record this[int index]
        {
            get { return _chessRecords[index]; }
        }

        #region ICollection<Record> 成员

        public int Count
        {
            get { return _chessRecords.Count; }
        }

        public void Add(Record item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Record item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Record[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(Record item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
