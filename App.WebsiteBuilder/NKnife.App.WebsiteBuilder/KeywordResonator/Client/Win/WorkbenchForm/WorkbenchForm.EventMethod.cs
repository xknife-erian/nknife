using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using Jeelu.WordSegmentor;
using Jeelu.Billboard;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public partial class WorkbenchForm
    {
        #region 事件的方法

        private void _InitMenuStrip_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Owner = this;

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                this.IsInitForApplication = true;
                this._InitMenuStrip.Enabled = false;
            }
        }

        private void _CatchMenuStrip_Click(object sender, EventArgs e)
        {
            _wordsManager.ManageWords();

            this.IsSnatchAtKewword = true;
            //string file = Path.Combine(Service.Path.Directory.Temp, "demo.txt");
            //StreamReader sr = File.OpenText(file);
            //string content = sr.ReadToEnd();
            //sr.Close();
            //sr.Dispose();

            //WordSeg seg = WordSegmentorService.WordSegmentor;
            //seg.FilterStopWords = true;
            //seg.MatchName = true;

            //WordsManager word = new WordsManager(this);
            //List<string> arr = seg.Segment(content);
            //KeywordListView keywordListView = new KeywordListView(arr, word);
            //keywordListView.ContextMenuStrip = MyContextMenuStrip.CreateForListCheckBox(keywordListView);
            //keywordListView.Text = "KeywordListView";
            //keywordListView.Show(this.MainDockPanel, DockState.Document);
        }

        private void _UpdateMenuStrip_Click(object sender, EventArgs e)
        {
            UpdateWord();
        }

        private void _UpdateLocalWordMenuStrip_Click(object sender, EventArgs e)
        {
         //   UpdateLocalWord();
        }

        private void _BackupMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _SaveMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _SaveAsMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _CloseMenuStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, this.GetType().Name + " Close!", this.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void _CutMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _CopyMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _PasteMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _AllSelectMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _OptionMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void _CleanCatchListMenuStrip_Click(object sender, EventArgs e)
        {

        }

        private void WorkbenchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnInitialize();
        }

        private void _AboutMenuStrip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            WordsManager.ShowExistWordsForm();
        }

        private void 查看信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buildRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebRuleForm form = new WebRuleForm();
            form.ShowDialog();
            
        }

        #endregion
        

        #region 方法
        /// <summary>
        /// 更新词库
        /// </summary>
        public void UpdateWord()
        {
            ClientServer _client = new ClientServer();
            _client.StartConn();

            MessageHead head = new MessageHead((int)KeywordMessageType.Upload, 0, 1);
            byte[] bodyBytes = Service.GetBodyBytes(Service.SessionId, Service.DictVersion, Service.ComputerMac);
            MessageBag bag = new MessageBag(head, bodyBytes);
            
            _client.SendMessage(bag);

            MessageBag recBag = _client.ReceiveMessage();

            string returnVal = _client.AnalyzeResponeMessage(recBag);

            if (string.Compare(returnVal, "versionSameOfSuccess") == 0)
            {
                //验证成功，版本一样，可上传
                MessageHead head2 = new MessageHead((int)KeywordMessageType.Upload, 0, 2);
                string updateCmd = WordsManager.GetUpdateCmd();
                byte[] bytes = Encoding.UTF8.GetBytes(updateCmd);
                MessageBag bag2 = new MessageBag(head2, bytes);

                _client.SendMessage(bag2);

                MessageBag recBag2 = _client.ReceiveMessage();

                string recVal = _client.AnalyzeResponeMessage(recBag2);


                if (string.Compare(recVal, "true") != 0)
                {
                    Debug.Fail("更新失败!");
                }
                else
                {
                    MessageBox.Show("更新成功！");
                    KeywordClear();
                }

            }
            else if (string.Compare(returnVal, "versionDifferentOfSuccess") == 0)
            {
                //验证成功，版本不一样，不可上传  需更新本地词库并显示
                WordsManager man = new WordsManager(this);
                man.ShowNewWordsForm();
            }
            else
            {
                if (!string.IsNullOrEmpty(returnVal))
                {
                    MessageBox.Show(returnVal);
                }
            }

            _client.CloseConn();

        }
        /// <summary>
        /// 上传成功后，清理
        /// </summary>
        private void KeywordClear()
        {
            WordsManager.NewWords.Clear();
            WordsManager.ExistWords.Clear();
            WordsManager.ShowNewWordsForm();
            WordsManager.ShowExistWordsForm();
        }

        /// <summary>
        /// 更新本地词库
        /// </summary>
        private void UpdateLocalWord()
        {
            ClientServer _client = new ClientServer();
            _client.StartConn();

            MessageHead head = new MessageHead((int)KeywordMessageType.Update, 0, 1);
            byte[] bodyBytes = Service.GetBodyBytes(Service.SessionId, Service.DictVersion, Service.ComputerMac);
            MessageBag bag = new MessageBag(head, bodyBytes);

            _client.SendMessage(bag);

            MessageBag recBag = _client.ReceiveMessage();

            string returnVal = _client.AnalyzeResponeMessage(recBag);

            _client.CloseConn();

            WordsManager.UpdateLocalWord();

        }

        #endregion
    }
}
