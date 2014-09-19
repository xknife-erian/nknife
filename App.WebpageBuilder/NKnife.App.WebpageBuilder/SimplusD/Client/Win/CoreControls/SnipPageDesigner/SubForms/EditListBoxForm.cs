using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.Reflection;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class EditListBoxForm : BaseForm
    {
        public EditListBoxForm( ListBoxPart part)
        {
            InitializeComponent();
            _part = part;
            Init();
        }

        #region 内部变量

        ListBoxPart _part;

        SelectGroup selectGroup = new SelectGroup();

        List<string> _attriButeChildPartTexts = new List<string>();

        #endregion

        #region 公共属性

        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        /// <summary>
        /// edit by zhenghao at 2008-06-18 11:13
        /// 初始化
        /// </summary>
        private void Init()
        {
            Type pageType = null;
            switch (_part.PageType)
            {
                case PageType.General:
                    pageType = typeof(GeneralPageXmlDocument);
                    break;
                case PageType.Product:
                    pageType = typeof(ProductXmlDocument);
                    break;
                case PageType.Project:
                    pageType = typeof(ProjectXmlDocument);
                    break;
                case PageType.InviteBidding:
                    pageType = typeof(InviteBiddingXmlDocument);
                    break;
                case PageType.Knowledge:
                    pageType = typeof(KnowledgeXmlDocument);
                    break;
                case PageType.Hr:
                    pageType = typeof(HrXmlDocument);
                    break;
                default:
                    return;
            }
            BuildSnipAttributeCheckBoxes(pageType.GetProperties());
            ResetpanelAttOption();
        }

        /// <summary>
        /// edit by zhenghao at 2008-06-18 11:13
        /// 从listBoxPart里获得所有定制特性块的文本
        /// </summary>
        private void GetAttriButeChildPartTextsFromListBoxPart()
        {
            _attriButeChildPartTexts.Clear();
            foreach (SnipPagePart spPart in _part.ChildParts)
            {
                if (spPart.GetType() == typeof(ListBoxPart))
                {
                    ListBoxPart lbPart = spPart as ListBoxPart;
                    _attriButeChildPartTexts.Add(lbPart.Text);
                }
            }
        }

        /// <summary>
        /// 设置所有子Part选项的状态
        /// </summary>
        /// <param name="list"></param>
        private void SetOptionByStrings(List<string> list)
        {
            selectGroup.SelectedValues = (object[])list.ToArray();
        }

        /// <summary>
        /// 根据定制特性增加ListBoxPart的子Part类型选项
        /// ZhengHao, 2008年3月11日16时29分
        /// </summary>
        private void BuildSnipAttributeCheckBoxes(PropertyInfo[] propertyInfos)
        {
            //遍历属性集合,propertyInfos含有当前模板类型所对应的页面类型的所有属性
            foreach (PropertyInfo info in propertyInfos)
            {
                object[] snipAttrs = info.GetCustomAttributes(typeof(SnipPartAttribute), false);
                if (snipAttrs.Length <= 0)
                {
                    continue;//无定制属性
                }
                //SelectGroupItem sgi = new SelectGroupItem(checkBoxDisplayChannel.Name, checkBoxDisplayChannel.Text);
                //遍历定制特性
                foreach (SnipPartAttribute snipAttr in snipAttrs)
                {
                    SelectGroupItem item = new SelectGroupItem(snipAttr.Name, AutoLayoutPanel.GetLanguageText(snipAttr.Text));
                    item.Tag = snipAttr;
                    selectGroup.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 刷新子Part选项栏
        /// </summary>
        private void ResetpanelAttOption()
        {
            panelAttOption.Controls.Clear();
            panelAttOption.Controls.Add(selectGroup);
        }

        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string[] _texts = selectGroup.SelectedTexts;
            if (_texts.Length < 1)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                for (int i = 0; i < _texts.Length; i++)
                {

                }
            }
        }
        
        #region 事件响应

        #endregion

        #region 自定义事件

        #endregion

    }
}
