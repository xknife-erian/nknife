using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win.Internal
{
    /// <summary>
    /// 内部使用类。外界不应该使用。用来连接外界与Service的一些互动。
    /// 所有Service里面的不希望外界调用到的函数或属性都应该设为internal，然后通过这个类来中转
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
