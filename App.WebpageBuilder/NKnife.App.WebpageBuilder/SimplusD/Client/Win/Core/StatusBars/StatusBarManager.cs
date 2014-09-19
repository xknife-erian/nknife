using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public static class StatusBarManager
    {
        static WorkbenchForm _mainForm;
        static Dictionary<WorkspaceType, StatusStrip> _allStatusStrip = new Dictionary<WorkspaceType, StatusStrip>();
        static StatusStrip _currentStatusStrip;
        public static StatusStrip CurrentStatusStrip
        {
            get { return _currentStatusStrip; }
        }
        static ToolStripStatusLabel _loginInfo;

        public static void Initialize(WorkbenchForm mainForm)
        {
            _mainForm = mainForm;
            _currentStatusStrip = new StatusStrip();
            _currentStatusStrip.LayoutStyle = ToolStripLayoutStyle.Table;
            ToolStripStatusLabel label = new ToolStripStatusLabel("����");
            label.Spring = true;
            label.TextAlign = ContentAlignment.MiddleLeft;
            _currentStatusStrip.Items.Add(label);
            _mainForm.Controls.Add(_currentStatusStrip);

            _loginInfo = new ToolStripStatusLabel("δ��½");
            _loginInfo.AutoSize = false;
            _loginInfo.Name = "loginInfo";
            _loginInfo.Width = 200;
            _loginInfo.TextAlign = ContentAlignment.MiddleLeft;
            _loginInfo.DoubleClickEnabled = true;
            _loginInfo.DoubleClick += new EventHandler(_loginInfo_DoubleClick);
            _currentStatusStrip.Items.Add(_loginInfo);

            Service.User.UserIDChanged += new EventHandler<ChangedEventArgs<string>>(User_UserIDChanged);

            //ע���������ActiveWorkspaceTypeChanged�¼�
            //_mainForm.ActiveWorkspaceTypeChanged += delegate(object sender, WorkspaceTypeEventArgs e)
            //{
            //    ChangeStatusBar(e.ActiveWorkspaceType);
            //};
        }

        static void _loginInfo_DoubleClick(object sender, EventArgs e)
        {
            Service.User.ShowLoginForm();
        }

        static void User_UserIDChanged(object sender, ChangedEventArgs<string> e)
        {
            if (Service.User.IsLogined)
            {
                _loginInfo.Text = string.Format("��ǰ�û�:{0}", e.NewItem);
            }
            else
            {
                _loginInfo.Text = "δ��½";
            }
        }

        /// <summary>
        /// �ı䵱ǰ״̬��MenuStrip
        /// </summary>
        private static void ChangeStatusBar(WorkspaceType workspaceType)
        {
            StatusStrip newStatusStrip;
            if (!_allStatusStrip.TryGetValue(workspaceType, out newStatusStrip))
            {
                newStatusStrip = CreateStatusStrip(workspaceType);
                _mainForm.Controls.Add(newStatusStrip);
                _allStatusStrip.Add(workspaceType,newStatusStrip);
            }

            HideAll();
            newStatusStrip.Show();
            _currentStatusStrip = newStatusStrip;
        }

        /// <summary>
        /// ��������״̬��StatusStrip
        /// </summary>
        private static void HideAll()
        {
            foreach (StatusStrip ss in _allStatusStrip.Values)
            {
                ss.Hide();
            }
        }

        /// <summary>
        /// ����StatusStrip
        /// </summary>
        private static StatusStrip CreateStatusStrip(WorkspaceType workspaceType)
        {
            StatusStrip outStatusStrip = new StatusStrip();
            switch (workspaceType)
            {
                case WorkspaceType.Default:
                    ToolStripStatusLabel label = new ToolStripStatusLabel("����");
                    outStatusStrip.Items.Add(label);
                    break;
                case WorkspaceType.Content:
                    break;
                case WorkspaceType.Page:
                    break;
                case WorkspaceType.HelpPage:
                    break;
                default:
                    throw new Exception("δ֪����:"+workspaceType);
            }

            return outStatusStrip;
        }
    }
}
