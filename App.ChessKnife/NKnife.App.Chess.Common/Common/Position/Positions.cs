using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class Positions : ICollection<Position>
    {
        private List<Position> _positions = new List<Position>();

        public void AddRange(IEnumerable<Position> postions)
        {
            _positions.AddRange(postions);
        }

        public Position[] ToArray()
        {
            return _positions.ToArray();
        }

        /// <summary>
        /// 尝试从Position集合获取指定的Position，并返该Position的X与Y
        /// </summary>
        /// <param name="postion">指定的Position</param>
        /// <param name="x">该Position的X</param>
        /// <param name="y">该Position的Y</param>
        /// <returns></returns>
        public bool TryGetPosition(Position postion, out int x, out int y)
        {
            x = 0; y = 0;
            foreach (Position pos in _positions)
            {
                if (pos.Equals(postion))
                {
                    x = postion.X;
                    y = postion.Y;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 返回Position集合中是否包含指定的X坐标(1-8)与Y坐标(1-8)的Position
        /// </summary>
        /// <param name="x">指定的X坐标(1-8)</param>
        /// <param name="y">指定的Y坐标(1-8)</param>
        /// <returns></returns>
        public bool Contains(int x, int y)
        {
            foreach (Position pos in _positions)
            {
                if (pos.X == (x - 1) && pos.Y == (y - 1))
                    return true;
            }
            return false;
        }

        #region ICollection<Position> 成员

        public void Add(Position item)
        {
            _positions.Add(item);
            _positions.TrimExcess();
        }

        public void Clear()
        {
            _positions.Clear();
            _positions.TrimExcess();
        }

        public bool Contains(Position item)
        {
            return _positions.Contains(item);
        }

        public void CopyTo(Position[] array, int arrayIndex)
        {
            _positions.CopyTo(array, arrayIndex);
            _positions.TrimExcess();
        }

        public int Count
        {
            get { return _positions.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Position item)
        {
            bool bl = false;
            bl = _positions.Remove(item);
            _positions.TrimExcess();
            return bl;
        }

        #endregion

        #region IEnumerable<Position> 成员

        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        #endregion
    }
}
