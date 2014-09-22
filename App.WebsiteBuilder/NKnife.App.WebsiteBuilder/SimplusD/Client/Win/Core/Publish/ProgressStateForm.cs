using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ProgressStateForm : Form
    {
        SocketServer _socketServer;
        int _prevDownloadBytesCount = -1;
        DateTime _prevDateTime;
        DateTime _startDateTime;
        int _speed;

        public ProgressStateForm(SocketServer socketServer)
        {
            _socketServer = socketServer;
            _startDateTime = DateTime.Now;

            InitializeComponent();

            progressRate.Maximum = _socketServer.TotleFileBytes;
            progressRate.Value = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (_socketServer.State)
            {
                case PublishState.Init:
                    break;

                ///发送中
                case PublishState.Sending:
                    {
                        /////通过上次发送的字节总数与现在的发送总数计算速度
                        //if (_prevDownloadBytesCount != _socketServer.SendedFileBytes)
                        //{
                        //    if (_prevDownloadBytesCount >= 0)
                        //    {
                        //        long now = DateTime.Now.ToFileTimeUtc() / 10000;
                        //        long prevTime = _prevDateTime.ToFileTimeUtc() / 10000;
                        //        int tick = (int)(now - prevTime);

                        //        _speed = (_socketServer.SendedFileBytes - _prevDownloadBytesCount) / tick * 1000;
                        //    }

                        //    _prevDownloadBytesCount = _socketServer.SendedFileBytes;
                        //    _prevDateTime = DateTime.Now;
                        //}

                        /////估算剩余时间
                        //int needTime = (_socketServer.TotleFileBytes - _socketServer.SendedFileBytes) / (_speed == 0 ? 1 : _speed);

                        /////显示的信息
                        //string showMsg = "总大小:{0} 已上传:{1} 速度:{2} 已用时间:{3} 估计剩余时间:{4}";
                        //showMsg = string.Format(showMsg, Utility.Format.FormatBytes(_socketServer.TotleFileBytes, 2),
                        //    Utility.Format.FormatBytes(_socketServer.SendedFileBytes, 0),
                        //    Utility.Format.FormatBytes(_speed, 2) + "/秒",
                        //    Utility.Format.FormatTimes(DateTime.Now - _startDateTime),
                        //    Utility.Format.FormatTimes(new TimeSpan(0, 0, needTime)));
                        //lblShowMsg.Text = showMsg;

                       
                        string showInfo = "总大小:{0} 已上传:{1}";
                        showInfo = string.Format(showInfo,
                            Utility.Format.FormatBytes(_socketServer.TotleFileBytes, 2),
                            Utility.Format.FormatBytes(_socketServer.SendedFileBytes, 0)
                            );
                        lblShowMsg.Text = showInfo;

                        //更新滚动条
                        progressRate.Value = _socketServer.SendedFileBytes;
                        break;
                    }
                case PublishState.SendFlush:
                    {
                        lblShowMsg.Text = "已发送完成，等待确认....";
                    }
                    break;
                case PublishState.SendSuccess:
                    {
                        //modified by zhangling on 2008年6月5日
                        this.progressRate.Value = this.progressRate.Maximum - 1000;
                        lblShowMsg.Text = "已发送成功，等待生成....";
                    }
                    break;
                case PublishState.AllSuccess:
                    {
                        this.progressRate.Value = this.progressRate.Maximum;
                        lblShowMsg.Text = "已生成成功！";
                    }
                    break;
                default:
                    Debug.Fail("未知的状态:" + _socketServer.State);
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void InvokeClose()
        {
            this.BeginInvoke(new Action(this.Close));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_socketServer.IsWorking)
            {
                if (MessageService.Show("确定要取消本次发布吗？", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
                    _socketServer.Abort();
                    return;
                }
                else
                {
                    e.Cancel = true;
                }
            }

            base.OnFormClosing(e);
        }
    }
}
