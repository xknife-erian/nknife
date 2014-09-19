using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.IO;

namespace Jeelu.Win
{
    /// <summary>
    /// һ��Group��Ӧ��һ��ֵ(һ��ValueControl)
    /// </summary>
    public partial class GroupBoxEx : Control
    {
        /// <summary>
        /// ������AutoPanel
        /// </summary>
        public AutoLayoutPanel OwnerAutoPanel { get; private set; }

        private GroupAttsData _itemList;
        private bool _PaintBorder;
        private bool _isGroupBox; ///��ǰ�����Ƿ񱻰�����GroupBox

        /// <summary>
        /// ���ؼ������ݵĶ�������
        /// </summary>
        private AutoLayoutPanelAttribute _autoAttribute;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="itemList">Group���Item</param>
        internal protected GroupBoxEx(GroupAttsData data, AutoLayoutPanel ownerAutoPanel)
        {
            this.OwnerAutoPanel = ownerAutoPanel;
            this.TabStop = false;
            this._itemList = data;
            this._autoAttribute = data.AutoAttributeDatas[0].Attribute;
            this._PaintBorder = data.AutoAttributeDatas[0].Attribute.GroupBoxPaintBorder;
            ///�����Ǹ����Ƿ�ʹ��groupBox�����������𣬵�ǰ��Ҫ���� groupBox
            this._isGroupBox = data.AutoAttributeDatas[0].Attribute.GroupBoxUseWinStyle;
            if (_isGroupBox)
            {
                _innerGroupBox = new GroupBox();
                _innerGroupBox.TabStop = false;
            }
            this.LayoutOwnControl();
        }

        private GroupBox _innerGroupBox = null;
        /// <summary>
        /// �ڲ���GroupBox
        /// </summary>
        public GroupBox InnerGroupBox
        {
            get
            {
                if (_innerGroupBox != null)
                {
                    _innerGroupBox.TabStop = false;
                    return _innerGroupBox;
                }
                else
                    return null;

            }
        }

        /// <summary>
        /// OnPaint, ����GroupBox�ı߿���
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_PaintBorder)
            {
                //Graphics g = this.CreateGraphics();
                //Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                //Pen pen = new Pen(AutoLayoutPanelService.ParseColorStringService(_autoAttribute.GroupBoxBorderColor));
                //Brush b = Brushes.Black;
                //g.DrawRectangle(pen, rect);

                ///���߿�
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                ControlPaint.DrawBorder(e.Graphics, rect,
                    _autoAttribute.GroupBoxBorderColor,
                    ButtonBorderStyle.Solid);
            }
        }

        /// <summary>
        /// ����Group�ڵĿؼ�
        /// </summary>
        private void LayoutOwnControl()
        {
            List<KeyValuePair<int, ValueControl>> ctrList = new List<KeyValuePair<int, ValueControl>>();

            ///��PropertyInfo���ԣ����ԵĶ������ԣ�Attribute�����ݸ�ValueControl��
            ///�����ÿؼ��󣬰��ն��������е�SubControlIndex�洢��List
            foreach (AutoAttributeData autoAttData in _itemList.AutoAttributeDatas)
            {
                ValueControl ctr = OwnerAutoPanel.CreateValueControl(autoAttData, this);
                ctrList.Add(new KeyValuePair<int, ValueControl>(autoAttData.Attribute.MainControlIndex, ctr));
            }

            ///ʹ��List.Sort������List�е�Item��������
            ctrList.Sort(AutoLayoutPanelCtrPairComparer.CreateComparer());

            int height = 1;
            int width = 0;
            int beginX = 1;

            ///���ʹ��ϵͳ�Դ���GroupBox��ʽ
            ///1. ��������߶ȣ�2. �����û������IsGroupPaintBorder���������߿�
            if (_autoAttribute.GroupBoxUseWinStyle)
            {
                height = 13;
                width = 2;
                beginX = 2;
                _PaintBorder = false;
            }

            ///���Group�б�־ͼƬ
            #region
            if (_autoAttribute.GroupBoxMainImage != null)
            {
                width = 6 + 36 + 6;
                beginX = 6 + 36 + 6;
                PictureBox picBox = new PictureBox();
                picBox.Size = new Size(36, 36);
                picBox.Location = new Point(6, 6);
                picBox.Image = Image.FromFile(//TODO:·����������Service
                    Path.Combine(Application.StartupPath, _autoAttribute.GroupBoxMainImage));
                this.Controls.Add(picBox);
            }
            #endregion

            foreach (KeyValuePair<int, ValueControl> pair in ctrList)
            {
                ValueControl ctr = pair.Value;
                OwnerAutoPanel.ValueControls.Add(ctr);

                ctr.Location = new Point(beginX, height);
                height = height + ctr.Height;

                ///�����Ƿ���GroupBox��ʽ������:
                ///trueʹ��System.GroupBox�ٷ�װһ��
                if (_isGroupBox)
                {
                    _innerGroupBox.Controls.Add(ctr);
                }
                else
                {
                    this.Controls.Add(ctr);
                }

                ///����ؼ���������List�ĵ�1��ʱ����width��ֵ
                if (ctrList.IndexOf(pair) == 0)
                    width = width + ctr.Width;

                ///�����ǰ�ؼ��Ŀ�ȱ�width��ʱ
                if (ctr.Width > width)
                    width = ctr.Width;

            }//foreach

            ///�ж��Ƿ�ʹ��GroupBox��ʽ��������this�Ĵ�С
            if (_autoAttribute.GroupBoxUseWinStyle && _innerGroupBox != null)
            {
                if (_autoAttribute.ColumnCountOfGroupControl != 1)
                {
                    ReLayOutControl(width);
                    this.Controls.Add(_innerGroupBox);
                    this.Size = new Size(_innerGroupBox.Width + 4, _innerGroupBox.Height );

                }
                else
                {
                    _innerGroupBox.Size = new Size(width + 2, height + 8);
                    this.Controls.Add(_innerGroupBox);
                    this.Size = new Size(width + 4, height + 10);
                }
            }
            else
            {
                this.Size = new Size(width + 2, height + 5);
            }
        }
        /// <summary>
        /// ���²���GroupBox��Ŀؼ�,�ֳɼ�����ʾ
        /// </summary>
        /// <param name="maxWidth">����ؼ������ؼ����</param>
        private void ReLayOutControl(int maxWidth)
        {
            ControlCollection coll = _innerGroupBox.Controls;

            int col = _autoAttribute.ColumnCountOfGroupControl;
            int groupWidth = col * maxWidth;
            int row = coll.Count / col + (((coll.Count % col) == 0) ? 0 : 1);


            int _height = 0;
            int _addIndex = 0;
            foreach (Control control in coll)
            {
                control.Location = new Point(2 + ((groupWidth - 2) / col) * (_addIndex % col),15 + control.Height * (_addIndex / col));
                _height = control.Height;
                _addIndex++;
            }
            _innerGroupBox.Size = new Size(groupWidth + 2, (row * _height) + 22);
        }

    }
}
