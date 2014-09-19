using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusD.Client.Win.Internal;

namespace Jeelu.SimplusD.Client.Win
{
    abstract public class BaseEditViewForm : BaseViewForm
    {
        virtual public bool IsModified
        {
            get { return false; }
        }

        virtual public bool Save()
        {
            return true;
        }

        /// <summary>
        /// add by fenggy 2008-06-19
        /// 当用户取消保存时想做的一些操作
        /// </summary>
        /// <returns></returns>
        virtual public bool CannelSave()
        {
            return true;
        }
        virtual public Control PropertyPanel
        {
            get { return null; }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ///关闭时提示保存
            if (IsModified)
            {
                bool isSave = false;

                ///如果是关闭所有窗口，则弹出针对关闭所有窗口的提示
                if (Service.Workbench.CloseAllWindowData.ClosingAllWindow)
                {
                    switch (Service.Workbench.CloseAllWindowData.Option)
                    {
                        case CloseAllWindowOption.None:
                            {
                                CloseAllWindowOptionResult result = MessageService.ShowAdvance(
                                    string.Format(StringParserService.Parse("${res:SimplusD.formClosingTitleFormat.text}"), this.Text));
                                switch (result)
                                {
                                    case CloseAllWindowOptionResult.None:
                                        e.Cancel = true;
                                        return;
                                    case CloseAllWindowOptionResult.Yes:
                                        isSave = true;
                                        break;
                                    case CloseAllWindowOptionResult.AllYes:
                                        isSave = true;
                                        Service.Workbench.CloseAllWindowData.Option = CloseAllWindowOption.AllSave;
                                        break;
                                    case CloseAllWindowOptionResult.No:
                                        isSave = false;
                                        break;
                                    case CloseAllWindowOptionResult.AllNo:
                                        isSave = false;
                                        Service.Workbench.CloseAllWindowData.Option = CloseAllWindowOption.AllNoSave;
                                        break;
                                    default:
                                        throw new Exception("开发期错误。未知的CloseAllWindowOptionResult:" + result);
                                }
                                break;
                            }
                        case CloseAllWindowOption.AllSave:
                            isSave = true;
                            break;
                        case CloseAllWindowOption.AllNoSave:
                            isSave = false;
                            break;
                        default:
                            throw new Exception("开发期错误。未知的CloseAllWindowOption:" + Service.Workbench.CloseAllWindowData.Option);
                    }
                }
                ///如果不是关闭所有窗口，弹出一般性提示
                else
                {
                    DialogResult result = MessageBox.Show(
                        string.Format(StringParserService.Parse("${res:SimplusD.formClosingTitleFormat.text}"), this.Text),
                        "SimplusD", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        isSave = true;
                    }
                    else if (result == DialogResult.No)
                    {
                        isSave = false;
                    }
                }

                if (isSave)
                {
                    bool isSuccess = this.Save();

                    if (isSuccess)
                    {
                        ///两个文件触发这个事件，还有个地方是MenuStripManager.cs
                        InternalService.OnWorkDocumentSaved(new WorkDocumentEventArgs(this.WorkDocumentType, this.Id));
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    bool isSuccess = CannelSave();
                }
            }

            base.OnFormClosing(e);
        }
    }
}
