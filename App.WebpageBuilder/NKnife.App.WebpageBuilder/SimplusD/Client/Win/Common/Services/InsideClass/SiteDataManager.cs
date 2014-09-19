using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class SiteDataManager
        {
            static public void Initialize()
            {
                //_projectManagerId = Guid.NewGuid().ToString("N");
                //_productManagerId = Guid.NewGuid().ToString("N");
                //_hrManagerId = Guid.NewGuid().ToString("N");
                //_inviteBiddingManagerId = Guid.NewGuid().ToString("N");
                //_knowledgeManagerId = Guid.NewGuid().ToString("N");
                _startupPageId = Guid.NewGuid().ToString("N");
            }

            //static private string _projectManagerId;
            //static public string ProjectManagerId
            //{
            //    get { return _projectManagerId; }
            //}

            //static private string _hrManagerId;
            //static public string HRManagerId
            //{
            //    get { return _hrManagerId; }
            //}

            //static private string _inviteBiddingManagerId;
            //static public string InviteBiddingManagerId
            //{
            //    get { return _inviteBiddingManagerId; }
            //}

            //static private string _knowledgeManagerId;
            //static public string KnowledgeManagerId
            //{
            //    get { return _knowledgeManagerId; }
            //}

            //static private string _productManagerId;
            //static public string ProductManagerId
            //{
            //    get { return _productManagerId; }
            //}

            static private string _startupPageId;
            static public string StartupPageId
            {
                get { return _startupPageId; }
            }

        }
    }
}