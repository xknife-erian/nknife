using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// �滭����
    /// </summary>
    public class Rect : INavigateNode
    {
        #region �ֶγ�Ա����

        private int _x;
        private int _y;
        private int _width;
        private int _height;

        private CssSection _cssSection = new CssSection();

        private PageSnipType _snipType;

        #endregion

        #region ���Զ���

        /// <summary>
        /// ��ȡ�����ð���������
        /// </summary>
        public SnipData SnipData { get; set; }

        /// <summary>
        /// ��ȡ������ҳ��Ƭ����
        /// </summary>
        [PropertyPad(0, 0, "snipType", MainControlType = MainControlType.TextBox, IsReadOnly = true)]
        public PageSnipType SnipType
        {
            get
            {
                if (HasSnip)
                    return _snipType;
                else
                    return PageSnipType.None;
            }
            set { _snipType = value; }
        }

        /// <summary>
        /// ��ȡ������ҳ��ƬID
        /// </summary>
        //[PropertyPad(0, 1, "snipID", MainControlType = MainControlType.TextBox)]
        public string SnipID { get; set; }

        /// <summary>
        /// ��ȡ�����ñ���
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ���ϽǺ�����
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
                _cssSection.Properties["left"] = value.ToString() + "px";
                _x = value;
            }
        }

        /// <summary>
        /// ���Ͻ�������
        /// </summary>
        public int Y
        {
            get { return _y; }
            set
            {
                _cssSection.Properties["top"] = value.ToString() + "px";
                _y = value;
            }
        }

        /// <summary>
        /// ���ο��
        /// </summary>
        [PropertyPad(1, 0, "rectWidth", MainControlType = MainControlType.TextBox,IsReadOnly=true)]
        public int Width
        {
            get { return _width; }
            set
            {
                _cssSection.Properties["width"] = value.ToString() + "px";
                _width = value;
            }
        }

        /// <summary>
        /// ���θ߶�
        /// </summary>
        [PropertyPad(1, 1, "rectHeight", MainControlType = MainControlType.TextBox,IsReadOnly=true)]
        public int Height
        {
            get { return _height; }
            set
            {
                _cssSection.Properties["height"] = value.ToString() + "px";
                _height = value;
            }
        }

        /// <summary>
        /// �Ƿ���ɾ��״̬
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// �Ƿ���ѡ��״̬
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        //[PropertyPad(0, 1, "%IsLocked", MainControlType = MainControlType.SimpleCheckBox)]
        public bool IsLocked { get; set; }

        /// <summary>
        /// ��ȡ���Σ�rect�����Ƿ���ҳ��Ƭ
        /// </summary>
        //[PropertyPad(0, 1, "%HasSnip", MainControlType = MainControlType.SimpleCheckBox)]
        public bool HasSnip { get; set; }

        /// <summary>
        /// ��ȡ�����þ��ε�Css�ַ���
        /// </summary>
        [PropertyPad(1, 2, "rectCss", MainControlType = MainControlType.TextBox)]
        public string ToCss
        {
            get { return _cssSection.ToString(); }
            set
            {
                _cssSection = CssSection.Parse(value);
            }
        }

        #endregion

        #region ���캯��

        public Rect(int x, int y, int width, int height)
        {
            Title = "";
            IsDeleted = false;
            IsSelected = false;
            IsLocked = false;
            HasSnip = false;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            _cssSection.Properties["float"] = "none";
            _cssSection.Properties["clear"] = "left";
            _cssSection.Properties["overflow"] = "auto";
        }

        public Rect(Rectangle rect)
            : this(rect.X, rect.Y, rect.Width, rect.Height)
        { }

        #endregion

        #region ������Ա�����ӿ�

        /// <summary>
        /// �ж��Ƿ���Ա�ѡ��
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsSelectable(Point point)
        {
            return point.X > X &&
                point.X < X + Width &&
                point.Y > Y &&
                point.Y < Y + Height;
        }

        /// <summary>
        /// �ж��Ƿ�����ھ��ȷ�Χ�ڱ�ѡ��
        /// </summary>
        /// <param name="point"></param>
        /// <param name="precision">����</param>
        /// <returns></returns>
        public bool IsSelectable(Point point, int precision)
        {
            return point.X > X + precision &&
                 point.X < X + Width - precision &&
                 point.Y > Y + precision &&
                 point.Y < Y + Height - precision;
        }

        /// <summary>
        /// ����ֱ��,�õ�����֮�߽�.
        /// Ĭ��line����this֮ĳ�߽�
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public PartitionLine GetBorderline(PartitionLine line)
        {
            if (line.IsRow)
            {
                if (line.Position == this.Y)
                {
                    return new PartitionLine(X, X + Width, Y, true);
                }
                else if (line.Position == Y + Height)
                {
                    return new PartitionLine(X, X + Width, Y + Height, true);
                }
                else return null;
            }
            else
            {
                if (line.Position == this.X)
                {
                    return new PartitionLine(Y, Y + Height, X, false);
                }
                else if (line.Position == X + Width)
                {
                    return new PartitionLine(Y, Y + Height, X + Width, false);
                }
                else return null;
            }
        }

        /// <summary>
        /// �õ����α߽�
        /// </summary>
        /// <returns></returns>
        public List<PartitionLine> GetBorderlines()
        {
            List<PartitionLine> borderLines = new List<PartitionLine>();
            borderLines.Add(new PartitionLine(X, X + Width, Y, true));
            borderLines.Add(new PartitionLine(X, X + Width, Y + Height, true));
            borderLines.Add(new PartitionLine(Y, Y + Height, X, false));
            borderLines.Add(new PartitionLine(Y, Y + Height, X + Width, false));
            return borderLines;
        }

        /// <summary>
        /// �ϲ���������,��neighbourRect�ϲ���this��.
        /// </summary>
        /// <param name="neighbourRect">���ϲ������ھ���</param>
        /// <returns></returns>
        public void MergeTwoRect(Rect neighbourRect)
        {
            neighbourRect.IsDeleted = true;
            if (this.X == neighbourRect.X)
            {
                ///neighbourRectλ��this���Ϸ�
                if (this.Y == neighbourRect.Y + neighbourRect.Height)
                {
                    this.Y = neighbourRect.Y;
                    this.Height += neighbourRect.Height;
                }
                ///�·�
                else
                {
                    this.Height += neighbourRect.Height;
                }
            }
            else
            {
                ///��
                if (this.X == neighbourRect.X + neighbourRect.Width)
                {
                    this.X = neighbourRect.X;
                    this.Width += neighbourRect.Width;
                }
                ///�ҷ�
                else
                {
                    this.Width += neighbourRect.Width;
                }
            }
        }

        /// <summary>
        /// ��this�ָ����������
        /// </summary>
        /// <param name="childRect">�ں���this��,������֮����</param>
        public void PartTwoRect(Rect childRect)
        {
            ///�ָ���ɾ��֮����
            childRect.IsDeleted = false;
            ///�޸ı��������ϲ����������εľ���
            if (this.Width == childRect.Width)
            {
                ///��ɾ�����������Ϸ�
                if (this.Y == childRect.Y)
                {
                    this.Y += childRect.Height;
                    this.Height -= childRect.Height;
                }
                else///����
                {
                    this.Height -= childRect.Height;
                }
            }
            else
            {
                ///��
                if (this.X == childRect.X)
                {
                    this.X += childRect.Width;
                    this.Width -= childRect.Width;
                }
                ///�ҷ�
                else
                {
                    this.Width -= childRect.Width;
                }
            }
        }

        public void MergeMutiRects(MergeRectCommand command)
        {
            foreach (Rect rect in command.RemovedRects)
            {
                rect.IsDeleted = true;
            }
            this.X = command.BoundaryRect.X;
            this.Y = command.BoundaryRect.Y;
            this.Width = command.BoundaryRect.Width;
            this.Height = command.BoundaryRect.Height;
        }

        public void PartMutiRects(MergeRectCommand command)
        {
            foreach (Rect rect in command.RemovedRects)
            {
                rect.IsDeleted = false;
            }

            this.X = command.LeftRect.X;
            this.Y = command.LeftRect.Y;
            this.Width = command.LeftRect.Width;
            this.Height = command.LeftRect.Height;
        }

        public void SelectRect()
        {
            this.IsSelected = true;
        }

        public void UnSelectRect()
        {
            this.IsSelected = false;
        }


        public void LockRect()
        {
            this.IsLocked = true;
        }

        public void UnLockRect()
        {
            this.IsLocked = false;
        }

        #endregion

        #region ���⺯��

        virtual public void CreateRectData()
        {
        }

        #endregion


        #region INavigateNode ��Ա


        public string Message
        {
            get { return SnipID; }
        }

        #endregion
    }
}