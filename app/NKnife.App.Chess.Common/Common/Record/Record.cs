using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NKnife.Chesses.Common.Interface;
using NKnife.Util;
using IItem = NKnife.Chesses.Common.Interface.IChessItem;

namespace NKnife.Chesses.Common.Record
{
    /// <summary>
    /// 描述一局棋的记录，该记录可能与更多的棋局记录保存在一个PGN文件中
    /// </summary>
    public class Record : IStepTree, IEnumerable<Step>
    {
        public Record()
        {
            this.Tags = new Tags();
            this.Items = new List<IItem>();
        }

        public Tags Tags { get; set; }

        #region ITree 成员

        public IChessItem Parent { get; set; }

        public bool HasChildren
        {
            get
            {
                if (this.Items == null) return false;
                if (this.Items.Count <= 0) return false;
                return true;
            }
        }

        public IList<IItem> Items { get; set; }

        #endregion

        #region override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            
            Record record = obj as Record;
            //if (!UtilityEquals.EnumerableEquals(this.Tags, record.Tags))
                //return false;
            if (!UtilEquals.CollectionsEquals<IItem>(this.Items, record.Items))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (
                3 * (Tags.GetHashCode() ^ Items.GetHashCode())
                );
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Tags.ToString());
            sb.AppendLine(this.Items.ToString());
            sb.AppendLine();
            return sb.ToString();
        }

        #endregion

        /*
        //#region IEnumerable

        //#region IEnumerable<Step> 成员

        //public IEnumerator<Step> GetEnumerator()
        //{
        //    return new StepEnumerator(this.Items);
        //}

        //#endregion

        //#region IEnumerable 成员

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return new StepEnumerator(this.Items);
        //}

        //#endregion

        //public class StepEnumerator : IEnumerator<Step>
        //{
        //    private List<Step> _chessSteps;
        //    private int _position = -1;

        //    public StepEnumerator(IList<IItem> list)
        //    {
        //        _chessSteps = new List<Step>();
        //        foreach (IItem item in list)
        //        {
        //            if (!(item is Step))
        //                continue;
        //            _chessSteps.Add(item as Step);
        //        }
        //    }

        //    public bool MoveNext()
        //    {
        //        _position++;
        //        return (_position < _chessSteps.Count);
        //    }

        //    public void Reset()
        //    {
        //        _position = -1;
        //    }

        //    public object Current
        //    {
        //        get { return _chessSteps[_position]; }
        //    }

        //    #region IEnumerator<Step> 成员

        //    Step IEnumerator<Step>.Current
        //    {
        //        get { return (Step)this.Current; }
        //    }

        //    #endregion

        //    #region IDisposable 成员

        //    public void Dispose()
        //    {
        //        _chessSteps = null;
        //    }

        //    #endregion
        //}

        #endregion
        */

        #region IEnumerable<Step> 成员

        public IEnumerator<Step> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}