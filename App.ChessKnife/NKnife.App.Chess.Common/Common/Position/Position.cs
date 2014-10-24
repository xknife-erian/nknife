using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;

namespace Gean.Module.Chess
{
    /*   FEN Dot
     * 
     *   8 |     1   2   3   4   5   6   7   8
     *   7 |     9  10  11  12  13  14  15  16
     *   6 |    17  18  19  20  21  22  23  24
     *   5 |    25  26  27  28  29  30  31  32
     *   4 |    33  34  35  36  37  38  39  40
     *   3 |    41  42  43  44  45  46  47  48
     *   2 |    49  50  51  52  53  54  55  56
     *   1 |    57  58  59  60  61  62  63  64
     *          ------------------------------
     *           1   2   3   4   5   6   7   8
     *           a   b   c   d   e   f   g   h
     *   
     *   this.Dot = (8 * (7 - _y)) + (_x + 1);
     */

    /// <summary>
    /// 一个描述棋盘位置的类型
    /// </summary>
    [Serializable]
    public class Position
    {

        #region Static

        /// <summary>
        /// 值为空时。该成员为只读状态。
        /// </summary>
        public static readonly Position Empty = null;

        /// <summary>
        /// 根据棋盘点的字符串值(e4,a8,h1...)解析Chess Position
        /// </summary>
        /// <param name="value">棋盘点的字符串值(e4,a8,h1...)</param>
        public static Position Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
            if (value.Length != 2)
                return Empty;

            char horizontal = value[0];
            int vertical = int.Parse(value[1].ToString());

            return new Position(horizontal, vertical);
        }

        /// <summary>
        /// 根据指定的相应的FEN的点获取相应的棋盘位置
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        public static Position GetPositionByDot(int dot)
        {
            if (dot < 1 || dot > 64)
            {
                return Empty;
            }
            int x;
            int y;
            Position.CalculateXY(dot, out x, out y);
            return new Position(x, y);
        }

        /// <summary>
        /// 将指定的相应的FEN的点转换为横坐标(1-8)与纵坐标(1-8)
        /// </summary>
        /// <param name="x">横坐标(1-8)</param>
        /// <param name="y">纵坐标(1-8)</param>
        public static void CalculateXY(int dot, out int x, out int y)
        {
            x = dot % 8;
            y = 8 - ((dot - 1) / 8);
            if (x == 0) x = 8;
        }

        /// <summary>
        /// 将指定的横坐标(1-8)与指定的纵坐标(1-8)转换成相应的FEN的点
        /// </summary>
        /// <param name="x">横坐标(1-8)</param>
        /// <param name="y">纵坐标(1-8)</param>
        public static int CalculateDot(int x, int y)
        {
            return (8 * (7 - (y - 1))) + ((x - 1) + 1);
        }

        /// <summary>
        /// 从当前的位置移动到新位置。
        /// 而后，判断该位置中是否有棋子，并根据判断结果更新两个Position集合(可移动"位置"集合,可杀棋"位置"集合)。
        /// 并返回是否可以继续移动的判断。
        /// </summary>
        /// <param name="situation">当前局面</param>
        /// <param name="tgtPos">目标位置</param>
        /// <param name="moveInPs">可移动"位置"集合</param>
        /// <param name="capturePs">可杀棋"位置"集合</param>
        /// <param name="enableToMoved">该棋格为空时，是否可以移入，一般是指“兵”，“兵”的斜向的两个棋格对于“兵”来讲能吃子不能移入。</param>
        /// <returns>移动到新位置后是否可以继续移动(当目标棋格中无棋子)</returns>
        public static bool Shift(
            Enums.GameSide side, ISituation situation,
            Position tgtPos, Positions moveInPs, Positions capturePs, bool enableToMoved)
        {
            if (tgtPos == Position.Empty) return false;

            char pieceChar;
            if (situation.TryGetPiece(tgtPos.Dot, out pieceChar))
            {
                if (char.IsLower(pieceChar) && side == Enums.GameSide.White)
                    capturePs.Add(tgtPos);
                if (char.IsUpper(pieceChar) && side == Enums.GameSide.Black)
                    capturePs.Add(tgtPos);
                return false;
            }
            else
            {
                if (enableToMoved)
                {
                    moveInPs.Add(tgtPos);
                    return true;
                }
                else
                    return false;
            }
        }

        public static bool Shift(
            Enums.GameSide side, ISituation situation,
            Position tgtPos, Positions moveInPs, Positions capturePs)
        {
            return Shift(side, situation, tgtPos, moveInPs, capturePs, true);
        }

        #endregion

        #region ctor

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="horizontal">横坐标(a-h)</param>
        /// <param name="vertical">纵坐标(1-8)</param>
        public Position(char horizontal, int vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
            _x = Utility.CharToInt(horizontal) - 1;
            _y = vertical - 1;
            this.Dot = Position.CalculateDot(_x + 1, _y + 1);
        }

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="x">横坐标(1-8)</param>
        /// <param name="y">纵坐标(1-8)</param>
        public Position(int x, int y)
        {
            _x = x - 1;
            _y = y - 1;
            _horizontal = Utility.IntToChar(x);
            _vertical = y;
            this.Dot = Position.CalculateDot(_x + 1, _y + 1);
        }

        #endregion

        #region Property

        /// <summary>
        /// 获取或设置当前位置的棋盘横坐标(a-h)
        /// </summary>
        public char Horizontal { get { return _horizontal; } }
        /// <summary>
        /// 当前位置的棋盘横坐标(a-h)
        /// </summary>
        [NonSerialized]
        private char _horizontal;

        /// <summary>
        /// 获取或设置当前位置的棋盘纵坐标(1-8)
        /// </summary>
        public int Vertical { get { return _vertical; } }
        /// <summary>
        /// 当前位置的棋盘纵坐标(1-8)
        /// </summary>
        [NonSerialized]
        private int _vertical;

        /// <summary>
        /// 获取或设置当前位置的计算机X坐标(0-7)
        /// </summary>
        public int X { get { return _x; } }
        /// <summary>
        /// 当前位置的计算机X坐标(0-7)
        /// </summary>
        [NonSerialized]
        private int _x;

        /// <summary>
        /// 获取或设置当前位置的计算机Y坐标(0-7)
        /// </summary>
        public int Y { get { return _y; } }
        /// <summary>
        /// 当前位置的计算机Y坐标(0-7)
        /// </summary>
        [NonSerialized]
        private int _y;

        /// <summary>
        /// 获取该位置对应FEN的点(1-64)
        /// </summary>
        public int Dot { get; private set; }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Position point = (Position)obj;
            if (this.X != point.X) return false;
            if (this.Y != point.Y) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this._x.GetHashCode() +
                this._y.GetHashCode()
                ));
        }
        public override string ToString()
        {
            return string.Format("{0}{1}", this.Horizontal, this.Vertical);
        }

        #endregion

        #region Shift

        /// <summary>
        /// 向北移一格
        /// </summary>
        internal Position ShiftNorth()
        {
            if (_y == 7)
                return Position.Empty;
            return new Position(_horizontal, _y + 2);
        }
        /// <summary>
        /// 向南移一格
        /// </summary>
        internal Position ShiftSouth()
        {
            if (_y == 0)
                return Position.Empty;
            return new Position(_horizontal, _y);
        }
        /// <summary>
        /// 向西移一格
        /// </summary>
        internal Position ShiftWest()
        {
            if (_x == 0)
                return Position.Empty;
            return new Position(_x, _vertical);
        }
        /// <summary>
        /// 向东移一格
        /// </summary>
        internal Position ShiftEast()
        {
            if (_x == 7)
                return Position.Empty;
            return new Position(_x + 2, _vertical);
        }
        /// <summary>
        /// 向西北移一格
        /// </summary>
        internal Position ShiftWestNorth()
        {
            if (_x == 0 || _y == 7)
                return Position.Empty;
            return new Position(_x, _y + 2);
        }
        /// <summary>
        /// 向东北移一格
        /// </summary>
        internal Position ShiftEastNorth()
        {
            if (_x == 7 || _y == 7)
                return Position.Empty;
            return new Position(_x + 2, _y + 2);
        }
        /// <summary>
        /// 向西南移一格
        /// </summary>
        internal Position ShiftWestSouth()
        {
            if (_x == 0 || _y == 0)
                return Position.Empty;
            return new Position(_x, _y);
        }
        /// <summary>
        /// 向东南移一格
        /// </summary>
        internal Position ShiftEastSouth()
        {
            if (_x == 7 || _y == 0)
                return Position.Empty;
            return new Position(_x + 2, _y);
        }

        #endregion

    }
}