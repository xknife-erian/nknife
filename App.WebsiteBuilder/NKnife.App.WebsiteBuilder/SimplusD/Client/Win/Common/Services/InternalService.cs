using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win.Internal
{
    /// <summary>
    /// �ڲ�ʹ���ࡣ��粻Ӧ��ʹ�á��������������Service��һЩ������
    /// ����Service����Ĳ�ϣ�������õ��ĺ��������Զ�Ӧ����Ϊinternal��Ȼ��ͨ�����������ת
    /// </summary>
    static public class InternalService
    {
        static public void OnActiveWorkDocumentChanged(EventArgs<Form> e)
        {
            Service.Workbench.OnActiveWorkDocumentChanged(e);
        }

        static public void OnWorkDocumentSaved(WorkDocumentEventArgs e)
        {
            Service.Workbench.OnWorkDocumentSaved(e);
        }

        static public void OnWorkDocumentAdded(WorkDocumentEventArgs e)
        {
            Service.Workbench.OnWorkDocumentAdded(e);
        }

        static public void OnWorkDocumentDeleted(WorkDocumentEventArgs e)
        {
            Service.Workbench.OnWorkDocumentDeleted(e);
        }

        static public void SetActiveWorkDocument(WorkDocumentType type, string id,Form activeForm)
        {
            Service.Workbench.SetActiveWorkDocument(type,id,activeForm);
        }

        static public void OnTmpltDocumentHealthChanged(EventArgs<string> e)
        {
            Service.Workbench.OnTmpltDocumentHealthChanged(e);
        }
    }
}
