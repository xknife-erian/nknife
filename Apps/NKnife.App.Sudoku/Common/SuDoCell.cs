using System;
using System.Collections;
using System.Collections.Generic;

namespace NKnife.App.Sudoku.Common
{
    /// <summary>
    /// Representation of a cell in a Sudoku board
    /// </summary>
    public class SuDoCell
    {

        #region ��̬��������������������...

        /// <summary>
        /// Character to be used when representing the content of this cell as a string.
        /// </summary>
        public static char _UnknowCellValueChar = '?';

        /// <summary>
        /// Row for this cell into a Sudoku board (zero-based).
        /// </summary>
        public int Row
        {
            get { return this._Row; }
        }
        private int _Row;

        /// <summary>
        /// Column for this cell into a Sudoku board (zero-based).
        /// </summary>
        public int Column
        {
            get { return this._Column; }
        }
        private int _Column;

        /// <summary>
        /// Value for this cell into a Sudoku board (-1 if value is unknown)
        /// </summary>
        public int Value
        {
            get
            {
                if (this.IsValid)
                    return this._Value;
                return -1;
            }
        }
        private int _Value;

        /// <summary>
        /// Has this cell a valid value?
        /// </summary>
        public bool IsValid
        {
            get { return (this._Value >= 1 && this._Value <= 9); }
        }

        /// <summary>
        /// Number of possible values for this cell.
        /// </summary>
        public int NumPossibilities
        {
            get
            {
                if (this.IsValid)
                    return 0;
                return this._NumPossibilities;
            }
        }
        private int _NumPossibilities;

        /// <summary>
        /// Bit mask with the possible values for this cell.
        /// </summary>
        private BitArray _Possibilities;

        #endregion

        #region ���캯��...

        /// <summary>
        /// Constructor for this class.
        /// </summary>
        /// <param name="row">
        /// Row for this cell into a Sudoku board (zero-based).
        /// </param>
        /// <param name="col">
        /// Column for this cell into a Sudoku board (zero-based).
        /// </param>
        public SuDoCell(int row, int col)
        {
            this._Row = row;
            this._Column = col;
            this._Value = 0;
            this._Possibilities = new BitArray(new int[] { 0x01ff });
            this._NumPossibilities = 9;
        }

        #endregion

        #region ����...

        /// <summary>
        /// ����һ����Ԫ��Ŀ���ֵ������
        /// </summary>
        internal int[] GetPossibilities()
        {
            if (this.IsValid)
            {
                return new int[] { };
            }

            List<int> possibilities = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                if (this._Possibilities.Get(i))
                {
                    possibilities.Add(i + 1);
                }
            }

            return possibilities.ToArray();
        }

        /// <summary>
        /// �������һ����Ԫ��Ŀ���ֵ
        /// </summary>
        internal int PickRandomPossibility()
        {
            int[] possibilities = this.GetPossibilities();

            if (possibilities.Length == 0)
            {
                return -1;
            }

            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(possibilities.Length);
            return possibilities[index];
        }

        /// <summary>
        /// �Ƴ�һ����Ԫ���е�һ������ֵ
        /// </summary>
        /// <param name="value">���Ƴ���һ������ֵ</param>
        internal void RemovePossibility(int value)
        {
            if (value ==0)
            {
                return;
            }
            this.CheckValue(value);
            if (this._Possibilities.Get(value - 1))
            {
                this._Possibilities.Set(value - 1, false);
                this._NumPossibilities--;
            }
        }

        /// <summary>
        /// ����һ����Ԫ���ֵ
        /// </summary>
        /// <param name="value"></param>
        internal void SetValue(int value)
        {
            if (value ==0)
            {
                return;
            }
            if (this.IsValid)
            {
                throw new ApplicationException(
                    string.Format("Already has been set a value for cell at [{0}, {1}].",
                    this._Row + 1, this._Column + 1));
            }

            this.CheckValue(value);

            int[] possibilities = this.GetPossibilities();

            if (Array.IndexOf<int>(possibilities, value) == -1)
            {
                throw new ApplicationException(
                    string.Format("Invalid value {2} for cell at [{0}, {1}].",
                    this._Row + 1, this._Column + 1, value));
            }

            this._Value = value;
        }

        /// <summary>
        /// У��һ����Ԫ���ֵ
        /// </summary>
        /// <param name="value"></param>
        private void CheckValue(int value)
        {
            if (value < 1 || value > 9)
            {
                throw new ApplicationException(
                    string.Format("Invalid value {2} for cell at [{0}, {1}].",
                    this._Row + 1, this._Column + 1, value));
            }
        }

        #endregion

        #region Overrides...

        public override string ToString()
        {
            if (this.IsValid)
                return this._Value.ToString();
            return _UnknowCellValueChar.ToString();
        }

        #endregion

    }

}
