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
        /// ���û�ȡ������ʱ������һЩ����
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
            ///�ر�ʱ��ʾ����
            if (IsModified)
            {
                bool isSave = false;

                ///����ǹر����д��ڣ��򵯳���Թر����д��ڵ���ʾ
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
                                        throw new Exception("�����ڴ���δ֪��CloseAllWindowOptionResult:" + result);
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
                            throw new Exception("�����ڴ���δ֪��CloseAllWindowOption:" + Service.Workbench.CloseAllWindowData.Option);
                    }
                }
                ///������ǹر����д��ڣ�����һ������ʾ
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
                        ///�����ļ���������¼������и��ط���MenuStripManager.cs
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
