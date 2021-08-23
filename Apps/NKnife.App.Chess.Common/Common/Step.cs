using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;
using NKnife.Interface;
using NKnife.Util;
using IItem = NKnife.Chesses.Common.Interface.IChessItem;

namespace NKnife.Chesses.Common
{
    /// <summary>
    /// 描述棋局中的单方的一步棋。如："Nc6"代表马走到c6格。
    /// 对于这步棋，绑定了一个注释的集合，一个变招的集合（变招也是每一步棋的集合）。
    /// </summary>
    [Serializable]
    public class Step : MarshalByRefObject, IStepTree, IChessItem, IParse, IGenerator, ICloneable, ISerializable
    {
        #region Property

        /// <summary>
        /// 回合编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 获取或设置该步棋的棋子战方
        /// </summary>
        public Enums.GameSide GameSide { get; set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.PieceType PieceType { get; set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public Position SourcePosition { get; set; }
        public char SourceChar { get; set; }
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public Position TargetPosition { get; set; }
        /// <summary>
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public Enums.ActionCollection Actions { get; set; }
        /// <summary>
        /// 获取或设置该步棋的升变后棋子类型
        /// </summary>
        public Enums.PieceType PromotionPieceType { get; set; }

        #endregion

        #region FEN Property

        public ISituation Fen { get; set; }

        #endregion

        #region ctor

        public Step()
        {
            this.Number = 0;
            this.GameSide = Enums.GameSide.White;
            this.PieceType = Enums.PieceType.None;
            this.SourcePosition = Position.Empty;
            this.SourceChar = '?';
            this.TargetPosition = Position.Empty;
            this.Actions = new Enums.ActionCollection();
            this.PromotionPieceType = Enums.PieceType.None;
        }

        #endregion

        #region IItem

        public string ItemType { get { return "Step"; } }
        public string Value
        {
            get { return this.Generator(); }
        }

        #endregion

        #region ITree

        public IChessItem Parent { get; set; }

        public IList<IItem> Items { get; set; }

        public bool HasChildren
        {
            get
            {
                if (this.Items == null) return false;
                if (this.Items.Count <= 0) return false;
                return true;
            }
        }

        #endregion

        #region IParse

        public void Parse(string value)
        {
            #region Starting

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException();
            value = value.Trim();
            if (value.Length < 2 || value == "\r\n")
                throw new ArgumentOutOfRangeException(value);
            this.Actions.Add(Enums.Action.General);

            #endregion

            #region 针对尾部标记符进行一些操作,并返回裁剪掉尾部标记符的Value值
            foreach (string flagword in Servicer.Flags)
            {
                if (value.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))//Qh5+
                        this.Actions.Add(Enums.Action.Check);
                    value = value.Substring(0, value.LastIndexOf(flagword));//裁剪掉尾部标记符
                    break;
                }
            }
            #endregion

            bool isMatch = false;
            foreach (var item in Servicer.StepRegex)
            {
                if (item.Item2.IsMatch(value))
                {
                    switch (item.Item1)
                    {
                        #region case
                        case Servicer.AsStep.As_e4:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.TargetPosition = Position.Parse(value);
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_Rd7:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_Rxa2:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.Actions.Add(Enums.Action.Capture);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_Rbe1:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.SourceChar = value[1];
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_N1c3:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.SourceChar = value[1];
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_hxg6:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.SourceChar = value[0];
                            this.Actions.Add(Enums.Action.Capture);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_Ngxf6:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.SourceChar = value[1];
                            this.Actions.Add(Enums.Action.Capture);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_R8xf5://N1c3
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.SourceChar = value[1];
                            this.Actions.Add(Enums.Action.Capture);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_e8_Q:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.Actions.Add(Enums.ToPromoteAction(value[value.Length - 1]));
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 4, 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_exf8_Q:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.SourceChar = value[0];
                            this.Actions.Add(Enums.Action.Capture);
                            this.Actions.Add(Enums.ToPromoteAction(value[value.Length - 1]));
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 4, 2));
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_O_O:
                            this.PieceType = Enums.PieceType.None;
                            this.Actions.Add(Enums.Action.KingSideCastling);
                            isMatch = true;
                            break;
                        case Servicer.AsStep.As_O_O_O:
                            this.PieceType = Enums.PieceType.None;
                            this.Actions.Add(Enums.Action.QueenSideCastling);
                            isMatch = true;
                            break;
                        #endregion
                    }
                    if (isMatch == true)
                    {
                        break;
                    }
                }
            }
        }

        #endregion

        #region IGenerator

        public string Generator()
        {
            StringBuilder sb = new StringBuilder(7);

            if (this.Actions.Contains(Enums.Action.KingSideCastling))
                sb.Append("O-O");
            else if (this.Actions.Contains(Enums.Action.QueenSideCastling))
                sb.Append("O-O-O");
            else
            {
                sb.Append(this.TargetPosition.ToString());

                if (this.Actions.Contains(Enums.Action.PromoteToQueen))
                    sb.Append("=Q");
                else if (this.Actions.Contains(Enums.Action.PromoteToKnight))
                    sb.Append("=N");
                else if (this.Actions.Contains(Enums.Action.PromoteToBishop))
                    sb.Append("=B");
                else if (this.Actions.Contains(Enums.Action.PromoteToRook))
                    sb.Append("=R");

                if (this.Actions.Contains(Enums.Action.Capture) || this.Actions.Contains(Enums.Action.EnPassant))
                    sb.Insert(0, 'x');
                if (!this.SourceChar.Equals('?'))
                    sb.Insert(0, this.SourceChar);
                sb.Insert(0, Enums.FromPieceTypeToStep(this.PieceType));
            }
            if (this.Actions.Contains(Enums.Action.Check))
                sb.Append('+');
            if (this.GameSide == Enums.GameSide.White)
            {
                sb.Insert(0, ' ').Insert(0, '.').Insert(0, this.Number.ToString());
            }
            return sb.ToString();
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region override

        public override string ToString()
        {
            return this.Generator();
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this.Number.GetHashCode() +
                this.Actions.GetHashCode() +
                this.PieceType.GetHashCode() +
                this.PromotionPieceType.GetHashCode() +
                this.TargetPosition.GetHashCode() + 
                this.SourcePosition.GetHashCode()
                ));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Step step = (Step)obj;
            if (!this.Number.Equals(step.Number))
                return false;
            if (!this.PieceType.Equals(step.PieceType))
                return false;
            if (!this.GameSide.Equals(step.GameSide))
                return false;
            if (!UtilEquals.CollectionsEquals<Enums.Action>(this.Actions, step.Actions))
                return false;
            if (!UtilEquals.PairEquals(this.TargetPosition, step.TargetPosition))
                return false;
            if (!UtilEquals.PairEquals(this.SourcePosition, step.SourcePosition))
                return false;
            if (!this.PromotionPieceType.Equals(step.PromotionPieceType))
                return false;
            if (!UtilEquals.PairEquals(this.Parent, step.Parent))
                return false;
            if (!UtilEquals.CollectionsEquals<IItem>(this.Items, step.Items))
                return false;
            return true;
        }

        #endregion
    }
}