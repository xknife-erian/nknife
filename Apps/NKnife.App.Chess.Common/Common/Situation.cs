using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;

namespace NKnife.Chesses.Common
{
    /* 国际象棋的FEN格式串
    国际象棋的FEN格式串是由6段ASCII字符串组成的代码(彼此5个空格隔开)，这6段代码的意义依次是：
    (1) 棋子位置数值区域(Piece placement data)
    表示棋盘上每行的棋子，这是FEN格式串的主要部分；规则是从第 8横线开始顺次数到第 1横线
    (白方在下，从上数到下)，从 a线开始顺次数到h线；白方棋子以大写字母“PNBRQK”表示，黑方棋子
    以小写 “pnbrqk”表示，这是英文表示法，每个字母代表的意义与常规规定相同。数字代表一个横线
    上的连续空格，反斜杠“/” 表示结束一个横线的描述。
    (2) 轮走棋方(Active color): 轮到哪一方走子；
    (3) 易位可行性(Castling availability): 每方及该方的王翼和后翼是否还存在“王车易位”的可能；
    (4) 吃过路兵目标格(En passant target square): 是否存在吃过路兵的可能，过路兵是经过哪个格子的；
    (5) 半回合计数(Halfmove clock): 最近一次吃子或者进兵后棋局进行的步数(半回合数)，用来判断“50回合自然限着”；
    (6) 回合数(Fullmove number): 棋局的回合数。
    |example: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
    |example: r1bq1rk1/pp2ppbp/2np1np1/8/2PNP3/2N1B3/PP2BPPP/R2QK2R w KQ - 0 9
    |example: 5n2/5Q2/3pp1pk/2P1b3/1P2P2P/r3q3/2R3BP/2R4K b - - 0 37
    |example: 8/8/7k/2p3Qp/1P1bq3/8/6RP/7K b - - 0 48
    |example: rnbqkbnr/ppp1ppp1/7p/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 3
    */

    /// <summary>
    /// 一个棋局局面描述类。
    /// [,sitju'eiʃən] n. 位置,形势,局面,处境,状况,职位
    /// </summary>
    [Serializable]
    public class Situation : MarshalByRefObject, ISituation, ICloneable, ISerializable
    {

        #region Static NewGame

        public static Situation NewGame
        {
            get { return new Situation(NewGameFENString); }
        }

        /// <summary>
        /// 新棋局局面，白棋大写，黑棋小写
        /// </summary>
        public static readonly string NewGameFENString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        #endregion

        #region Constructor

        /// <summary>
        /// 棋局局面类型构造函数
        /// </summary>
        public Situation()
        {
            this.Rows = new StringBuilder[8];
            for (int i = 0; i < 8; i++)
            {
                Rows[i] = new StringBuilder("11111111");
            }
            this.Clear();
        }
        /// <summary>
        /// 棋局局面类型构造函数
        /// </summary>
        /// <param name="fenstring">局面的FEN字符串</param>
        public Situation(string fenstring)
            : this()
        {
            this.Parse(fenstring);
        }

        #endregion

        #region ISituation: FEN Properties

        /// <summary>
        /// 1) 棋子位置数值区域(Piece placement data) 
        /// </summary>
        public virtual string PiecePlacementData
        {
            get { return this.ToString(); }
        }

        /// <summary>
        /// 2) 轮走棋方(Active color)
        /// </summary>
        public virtual Enums.GameSide GameSide { get; internal set; }

        /// <summary>
        /// 3) 易位可行性(Castling availability)(1.白方王侧)
        /// </summary>
        public virtual bool WhiteKingCastlingAvailability { get; internal set; }

        /// <summary>
        /// 3) 易位可行性(Castling availability)(2.白方后侧)
        /// </summary>
        public virtual bool WhiteQueenCastlingAvailability { get; internal set; }

        /// <summary>
        /// 3) 易位可行性(Castling availability)(3.黑方王侧)
        /// </summary>
        public virtual bool BlackKingCastlingAvailability { get; internal set; }

        /// <summary>
        /// 3) 易位可行性(Castling availability)(4.黑方后侧)
        /// </summary>
        public virtual bool BlackQueenCastlingAvailability { get; internal set; }

        /// <summary>
        /// 4) 吃过路兵目标格(En passant target square)
        /// </summary>
        public virtual Position EnPassantTargetPosition { get; internal set; }

        /// <summary>
        /// 5) 半回合计数(Halfmove clock)。
        /// 用一个非负数表示自从上一次动兵或吃子之后目前走了的半回合数。这个是为了适应50步和棋规则而定。
        /// </summary>
        public virtual int HalfMoveClock { get; internal set; }

        /// <summary>
        /// 6) 回合数(Fullmove number)。当前要进行到的回合数。不管白还是黑，第一步时总是以1表示，以后黑方每走一步数字就加1。
        /// </summary>
        public virtual int FullMoveNumber { get; internal set; }

        #endregion

        #region ISituation: ContainsPiece

        public virtual bool ContainsPiece(int dot)
        {
            if (this.GetByPosition(dot).Equals('1'))
                return false;
            else
                return true;
        }

        public virtual bool TryGetPiece(int dot, out Enums.PieceType pieceType)
        {
            char c = this.GetByPosition(dot);
            if (c.Equals('1'))
            {
                pieceType = Enums.PieceType.None;
                return false;
            }
            else
            {//有棋子
                pieceType = Enums.ToPieceType(c);
                return true;
            }
        }

        public virtual bool TryGetPiece(int dot, out char mychar)
        {
            char c = this.GetByPosition(dot);
            if (c.Equals('1'))
            {
                mychar = '?';
                return false;
            }
            else
            {//有棋子
                mychar = c;
                return true;
            }
        }

        #endregion

        #region ISituation: IParse, IGenerator

        /// <summary>
        /// 解析一条FEN字符串
        /// </summary>
        /// <param name="str"></param>
        public virtual void Parse(string str)
        {
            string[] note = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            #region 16.1.3.1: Parse piece placement data

            int dot = 1;
            int index = 0;

            string[] row = note[0].Split('/');
            if (row.Length != 8)
                throw new ArgumentException("Invalid board specification, " + row.Length + " ranks are defined, there should be 8.");

            foreach (string line in row)
            {
                index = 0;
                foreach (char achar in line)
                {
                    if (achar >= '0' && achar <= '9')
                        index += (int)(achar - '0');
                    else
                    {
                        if (Enums.ToPieceType(achar) != Enums.PieceType.None)
                        {
                            if (index > 7)  // This check needed here to avoid overrunning index below under some error conditions.
                                throw new ArgumentException("Invalid board specification, rank " + (dot / 8 + 1) + " has more then 8 items specified.");
                            this.SetByPosition(dot + index, achar);
                        }
                        index++;
                    }
                }

                if (index == 0) // Allow null lines = /8/
                    index += 8;

                if (index != 8)
                    throw new ArgumentException("Invalid board specification, rank " + (dot / 8 + 1) + " has " + index + " items specified, there should be 8.");

                dot += 8;
            }

            #endregion

            #region 16.1.3.2: Parse active color

            if (note.Length >= 2)
            {
                // 16.1.3.2: Parse active color
                if (note[1].Length > 0)
                {
                    char colorchar = Char.ToLower(note[1][0]);
                    if (colorchar.Equals('w') || colorchar.Equals('b'))
                    {
                        GameSide = Enums.ToGameSide(colorchar);
                    }
                    else
                        throw new ArgumentException("Invalid color designation, use w or b as 2nd field separated by spaces.");

                    if (note[1].Length != 1)
                        throw new ArgumentException("Invalid color designation, 2nd field is " + note[1].Length + " chars long, only 1 allowed.");
                }
            }

            #endregion

            #region 16.1.3.3: Parse castling availability

            if (note.Length >= 3)
            {
                foreach (char achar in note[2])
                {
                    switch (achar)
                    {
                        case 'K':
                            this.WhiteKingCastlingAvailability = true;
                            break;
                        case 'Q':
                            this.WhiteQueenCastlingAvailability = true;
                            break;
                        case 'k':
                            this.BlackKingCastlingAvailability = true;
                            break;
                        case 'q':
                            this.BlackQueenCastlingAvailability = true;
                            break;
                        case '-':
                            break;
                        default:
                            throw new Exception("Invalid castle privileges designation, use: KQkq or -");
                    }
                }
            }

            #endregion

            #region 16.1.3.4: Parse en passant target square such as "e3"
            try
            {
                if (note.Length >= 4)
                {
                    // 16.1.3.4: Parse en passant target square such as "e3"
                    if (note[3].Length == 2)
                    {
                        if (GameSide == Enums.GameSide.White)
                        {
                            if (note[3][1] != '6')
                                throw new Exception("Invalid target square for white En passant captures: " + this.EnPassantTargetPosition.ToString());
                        }
                        else
                        {
                            if (note[3][1] != '3')
                                throw new Exception("Invalid target square for black En passant captures: " + this.EnPassantTargetPosition.ToString());
                        }
                    }
                    this.EnPassantTargetPosition = Position.Parse(note[3]);
                }
            }
            catch { }
            #endregion

            #region 16.1.3.5: Parse halfmove clock, count of half-move since last pawn advance or unit capture
            try
            {

                if (note.Length >= 5)
                {
                    // 16.1.3.5: Parse halfmove clock, count of half-move since last pawn advance or unit capture
                    this.HalfMoveClock = Int16.Parse(note[4]);
                }
            }
            catch { }
            #endregion

            #region 16.1.3.6: Parse fullmove number, increment after each black move
            try
            {
                if (note.Length >= 6)
                {
                    // 16.1.3.6: Parse fullmove number, increment after each black move
                    this.FullMoveNumber = Int16.Parse(note[5]);
                }
            }
            catch { }
            #endregion
        }

        /// <summary>
        /// 生成指定的FEN格式字符串
        /// </summary>
        /// <returns></returns>
        public virtual string Generator()
        {
            string[] parms = new string[6];
            StringBuilder note = new StringBuilder();

            for (int j = 7; j >= 0; j--)
            {
                int i = 0;
                StringBuilder rowNote = new StringBuilder();


                string str = Rows[j].ToString();
                foreach (char achar in str)// 11b11k1B = 2b2k1b
                {
                    int tmp = 0;
                    if (!int.TryParse(achar.ToString(), out tmp))
                    {
                        if (i != 0)
                        {
                            rowNote.Append(i).Append(achar);
                            i = 0;
                        }
                        else
                        {
                            rowNote.Append(achar);
                        }
                    }
                    else
                        i++;
                }
                if (i != 0)
                    rowNote.Append(i);
                note.Append(rowNote).Append('/');
            }
            parms[0] = note.ToString().TrimEnd('/');
            note.Length = 0;

            parms[1] = Enums.FormGameSide(GameSide).ToString();

            if (WhiteKingCastlingAvailability | WhiteQueenCastlingAvailability | BlackKingCastlingAvailability | BlackQueenCastlingAvailability)
            {
                if (WhiteKingCastlingAvailability)
                    note.Append('K');
                if (WhiteQueenCastlingAvailability)
                    note.Append('Q');
                if (BlackKingCastlingAvailability)
                    note.Append('k');
                if (BlackQueenCastlingAvailability)
                    note.Append('q');
            }
            else
                note.Append('-');

            parms[2] = note.ToString();
            note.Length = 0;
            if (EnPassantTargetPosition == Position.Empty)
                parms[3] = "-";
            else
                parms[3] = EnPassantTargetPosition.ToString();
            parms[4] = HalfMoveClock.ToString();
            parms[5] = FullMoveNumber.ToString();

            StringBuilder sb = new StringBuilder();
            foreach (string parm in parms)
            {
                sb.Append(parm).Append(' ');
            }
            return sb.ToString();
        }

        #endregion

        #region Protected Method

        #region By Position

        protected virtual char GetByPosition(int dot)
        {
            int x;
            int y;
            Position.CalculateXY(dot, out x, out y);
            return this.Rows[y - 1][x - 1];
        }

        protected virtual void SetByPosition(int dot, char value)
        {
            int x;
            int y;
            Position.CalculateXY(dot, out x, out y);
            this.Rows[y - 1][x - 1] = value;
        }

        #endregion

        /// <summary>
        /// 当前FEN的Chessman(棋子)的Placement(位置)的以行为单位的字符串
        /// </summary>
        protected virtual StringBuilder[] Rows { get; set; }

        /// <summary>
        /// Clear all current settings.
        /// </summary>
        protected virtual void Clear()
        {
            this.GameSide = Enums.GameSide.White;
            this.WhiteKingCastlingAvailability = false;
            this.WhiteQueenCastlingAvailability = false;
            this.BlackKingCastlingAvailability = false;
            this.BlackQueenCastlingAvailability = false;
            this.EnPassantTargetPosition = Position.Empty;
            this.HalfMoveClock = 0;
            this.FullMoveNumber = 1;
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Situation fen = (Situation)obj;
            if (!this.GameSide.Equals(fen.GameSide)) return false;
            if (!this.WhiteKingCastlingAvailability.Equals(fen.WhiteKingCastlingAvailability)) return false;
            if (!this.WhiteQueenCastlingAvailability.Equals(fen.WhiteQueenCastlingAvailability)) return false;
            if (!this.BlackKingCastlingAvailability.Equals(fen.BlackKingCastlingAvailability)) return false;
            if (!this.BlackQueenCastlingAvailability.Equals(fen.BlackQueenCastlingAvailability)) return false;
            if (!this.EnPassantTargetPosition.Equals(fen.EnPassantTargetPosition)) return false;
            if (!this.FullMoveNumber.Equals(fen.FullMoveNumber)) return false;
            if (!this.HalfMoveClock.Equals(fen.HalfMoveClock)) return false;
            if (!this.PiecePlacementData.Equals(fen.PiecePlacementData)) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (
                this.PiecePlacementData.GetHashCode() ^
                this.GameSide.GetHashCode() ^
                this.WhiteKingCastlingAvailability.GetHashCode() ^
                this.WhiteQueenCastlingAvailability.GetHashCode())) ^
                this.BlackKingCastlingAvailability.GetHashCode() ^
                this.BlackQueenCastlingAvailability.GetHashCode() ^
                this.EnPassantTargetPosition.GetHashCode() ^
                this.FullMoveNumber.GetHashCode() ^
                this.HalfMoveClock.GetHashCode();
        }
        /// <summary>
        /// 返回一个未压缩的棋子位置数值区域字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = this.Rows.Length - 1; i >= 0; i--)
            {
                sb.Append(this.Rows[i]).Append('/');
            }
            return sb.ToString().TrimEnd('/');
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return new Situation(this.Generator());
        }

        #endregion

        #region ISerializable

        protected Situation(SerializationInfo info, StreamingContext context)
        {
            this.Parse(info.GetString("fen"));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fen", this.Generator());
        }

        #endregion

    }
}
