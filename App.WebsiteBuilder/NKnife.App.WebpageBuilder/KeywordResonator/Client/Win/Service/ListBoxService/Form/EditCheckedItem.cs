using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.KeywordResonator.Client
{
    public partial class EditCheckedItem : Form
    {

        #region 构造函数

        public EditCheckedItem(WordsManager wordManager,EditFormType type, string strSource)
        {
            InitializeComponent();
            CurrentWordsManager = wordManager;
            SourceString = strSource;
            FormType = type;
        }

        public EditCheckedItem()
        {
            InitializeComponent();
        }

        #endregion

        #region 字段或属性

        private EditFormType _formType;
        private EditFormType FormType
        {
            get { return _formType; }

            set
            {
                _formType = value;
                switch (_formType)
                {
                    case EditFormType.CombinItem:
                        this.Text = "合并项";
                        lb_tip.Text = "";
                        txt_src.Text = SourceString;
                        txt_dec.Text = SourceString.Replace(",", "");
                        break;
                    case EditFormType.SplitItem:
                        this.Text = "拆分项";
                        lb_tip.Text = "拆分关键字 以\",\"分隔";
                        txt_src.Text = SourceString;
                        txt_dec.Text = SourceString;
                        break;
                    case EditFormType.EditItem:
                        this.Text = "编辑项";
                        lb_tip.Text = "";
                        lb_src.Visible = false;
                        txt_src.Visible = false;
                        txt_dec.Text = SourceString;
                        break;
                    case EditFormType.EditFrequency:
                        this.Text = "修改权重";
                        lb_tip.Text = "";
                        lb_src.Visible = false;
                        txt_src.Visible = false;
                        lb_word.Visible = true;
                        txt_word.Visible = true;

                        txt_dec.ReadOnly = true;
                        txt_dec.BorderStyle = BorderStyle.FixedSingle;

                        int index = SourceString.IndexOf(",");
                        if(index < 0) return;

                        txt_dec.Text = SourceString.Substring(0,index);
                        txt_word.Text = SourceString.Substring(++index);

                        break;
                    case EditFormType.AddItem:
                        this.Text = "增加项";
                        lb_tip.Text = "";
                        lb_src.Visible = false;
                        txt_src.Visible = false;
                        break;
                    default:
                        Debug.Fail("未知窗口类型");
                        break;
                }
            }
        }

        private WordsManager CurrentWordsManager { get; set; }

        /// <summary>
        /// 源字符串
        /// </summary>
        private string SourceString { get; set; }

        /// <summary>
        /// 操作最后的字符串
        /// </summary>
        public string ResultString { get; set; }

        #endregion

        #region 事件

        private void btn_ok_Click(object sender, EventArgs e)
        {
         //   ResultString = txt_dec.Text.Trim();
            if (!IsKeywordsRepeat())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region 内部方法

        /// <summary>
        ///  判断关键词是否重复
        /// </summary>
        /// <returns></returns>
        private bool IsKeywordsRepeat()
        {
            if (string.IsNullOrEmpty(txt_dec.Text))
            {
                MessageBox.Show("新的关键词不能为空");
                return true;
            }
            string szDec = txt_dec.Text.Replace("，",",");

            string []szArrayDec = szDec.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (FormType != EditFormType.EditFrequency)
            {
                foreach (string strtmp in szArrayDec)
                {
                    if (CurrentWordsManager.IsWordExist(strtmp))
                    {
                        MessageBox.Show(strtmp + "已存在,请修改");
                        return true;
                    }
                }
                ResultString = txt_dec.Text.Trim();
            }
            else
            {
                if (String.IsNullOrEmpty(txt_word.Value.ToString()))
                {
                    MessageBox.Show("词频不能为空");
                    return true;
                }
                ResultString = txt_word.Value.ToString();
            }





            return false;
        }
        #endregion
    }

}
