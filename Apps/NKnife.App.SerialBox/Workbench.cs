using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NKnife.SerialBox.Views;

namespace NKnife.SerialBox
{
    public partial class Workbench : Form
    {
        private ViewModel _viewModel = new ViewModel();
        private string sendFileName = string.Empty;

        public static StringCollection strCkbMultiSendList;
        public static StringCollection strTbxMultiSendList;
        public static StringCollection strReMarkTbxMultiSendList;

        public static bool isMultiSendInfoFrameSureClosed = false;
        private static string[] strMultiSendArr = new string[40];

        private System.Timers.Timer sendMultiTimer = new System.Timers.Timer();

        private ArrayList ckbMultiSendTbxList = new ArrayList();
        private ArrayList ckbMultiSendCkbList = new ArrayList();
        private int[] ckbMultiSendStatus = new int[40];
        private int currentMultiSend;
        private multiSendPage multiSendPageCur;
        private ArrayList multiSendPageList = new ArrayList();

        private long sendFileTickDelt;
        private bool isSendFileSuccess;
        private bool isHexRevDisp;
        private ulong recieveCount;
        private ulong sendCount;
        private bool isExistSerialPort = true;
        private bool isListening;
        private bool isSerailPortClose;
        private bool isSendNewLine = true;
        private bool isMultiSendNewLine = true;
        private bool isHexSend;
        private bool isMultiHexSend;
        private bool isRuningReset;
        private bool isRuningLoadUserInfo;
        private bool isSendFile = true;

        private MultiSendInfoFrame multiSendInfoFrame;
        private System.Timers.Timer sendTimer = new System.Timers.Timer();
        private System.Timers.Timer transportSendTimer = new System.Timers.Timer();
        private StringBuilder sbRecvData = new StringBuilder();
        private System.Timers.Timer transportProtocolRevTimeOutTimer = new System.Timers.Timer();
        private List<byte> transportProtocolRevList = new List<byte>();
        private int serialPortStatus;
        public static int PORT_OUT = 1;
        public static int PORT_IN = 2;
        private int currentSerialPortNumber;
        private OpenFileDialog openFileDlgTransportProtocol;
        private OpenFileDialog openFileDlgSend;

        #region Events

        private delegate void UpdateProgBarSendFileEventHandler(int pro);
        private static UpdateProgBarSendFileEventHandler updateProgBarSendFile;

        private delegate void UpdateTextEventHandler(string revText);
        private UpdateTextEventHandler updateText;

        #endregion

        public Workbench()
        {
            InitializeComponent();
            float[] numArray = new float[(base.Controls.Count * 2) + 2];
            int num2 = 0;
            numArray[num2++] = base.Size.Width;
            numArray[num2++] = base.Size.Height;
            foreach (Control control in base.Controls)
            {
                Point location = control.Location;
                Size size = base.Size;
                numArray[num2++] = ((float)location.X) / ((float)size.Width);
                Point point2 = control.Location;
                Size size4 = base.Size;
                numArray[num2++] = ((float)point2.Y) / ((float)size4.Height);
                control.Tag = control.Size;
            }
            base.Tag = numArray;
        }

        private void OpenSerialPortStripButton_Click(object sender, EventArgs e)
        {
            if (_viewModel.SerialPort != null && _viewModel.SerialPort.IsOpen)
            {
                this.CloseSerialPort(true);
            }
            if (!this.isExistSerialPort)
            {
                MessageBox.Show("没有搜索到串口！", "错误提示");
            }
            else
            {
                var dialog = new SerialPortSelectorDialog();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _viewModel.SerialPort = dialog.SerialPort;
                }
                else
                {
                    return;
                }
                this.SetPortProperty();
                this.OpenSerialPort(true);
                if (_viewModel.SerialPort.IsOpen)
                {
                    this.SetSerialInfoStatusLabel(_viewModel.SerialPort);
                }
            }
        }

        private void SetPortProperty()
        {
            _viewModel.SerialPort.Encoding = Encoding.GetEncoding("GB18030");
            if (!this.ckbTimeSend.Checked)
            {
                this.tbxSendPeriod.Enabled = true;
                this.sendTimer.Stop();
            }
            else
            {
                this.tbxSendPeriod.Enabled = false;
                this.sendTimer.Interval = Convert.ToInt32(this.tbxSendPeriod.Text.Trim());
                this.sendTimer.Start();
            }
            if (!this.ckbMultiAutoSend.Checked)
            {
                this.tbxMutilSendPeriod.Enabled = true;
                this.sendMultiTimer.Stop();
            }
            else
            {
                this.tbxMutilSendPeriod.Enabled = false;
                this.sendMultiTimer.Interval = Convert.ToInt32(this.tbxMutilSendPeriod.Text.Trim());
                this.sendMultiTimer.Start();
            }
            _viewModel.SerialPort.ReadTimeout = -1;
        }


        private void SetSerialInfoStatusLabel(SerialPort port)
        {
            string str = string.Empty;
            str = !port.CtsHolding ? str + " CTS=0 " : (str + " CTS=1 ");
            str = !port.DsrHolding ? (str + "DSR=0 ") : (str + "DSR=1 ");
            str = !port.CDHolding ? (str + "DCD=0 ") : (str + "DCD=1 ");
            SerialInfoStatusLabel.Text = str;
        }

        private void SendSingleButton_Click(object sender, EventArgs e)
        {
            this.SendSingleData(this.tbxSendData.Text);
        }

        private void btnClearSend_Click(object sender, EventArgs e)
        {

        }

        private void SendSingleData(string data)
        {
            if (this.isSendNewLine)
            {
                if (this.isHexSend)
                {
                    this.SendSerailPortDataByHex(data + " 0D 0A");
                }
                else
                {
                    this.SendSerailPortData(data + "\r\n");
                }
            }
            else if (this.isHexSend)
            {
                this.SendSerailPortDataByHex(data);
            }
            else
            {
                this.SendSerailPortData(data);
            }
        }

        private void SendSerailPortData(string data)
        {
            if (_viewModel.SerialPort.IsOpen)
            {
                try
                {
                    byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(data);
                    int length = bytes.Length;
                    this.sendCount += (ulong)length;
                    _viewModel.SerialPort.Write(bytes, 0, length);
                }
                catch (Exception)
                {
                    MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                    this.CloseSerialPort(true);
                }
            }
            else
            {
                if (!this.isSerailPortClose)
                {
                    this.OpenSerialPort(false);
                }
                if (_viewModel.SerialPort.IsOpen)
                {
                    try
                    {
                        byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(data);
                        int length = bytes.Length;
                        this.sendCount += (ulong)length;
                        _viewModel.SerialPort.Write(bytes, 0, length);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                        this.CloseSerialPort(true);
                    }
                }
            }
        }

        private void SendFile()
        {
            long length = 0L;
            int num2 = 0;
            int count = 0;
            byte[] buffer = new byte[0x400];
            FileStream stream = null;
            try
            {
                stream = new FileStream(this.sendFileName, FileMode.Open);
                length = stream.Length;
                while (true)
                {
                    if (this.isSendFile)
                    {
                        try
                        {
                            count = stream.Read(buffer, 0, buffer.Length);
                        }
                        catch (IOException exception)
                        {
                            stream.Close();
                            MessageBox.Show(exception.ToString(), "错误提示");
                            break;
                        }
                        if (count != 0)
                        {
                            num2 += count;
                            this.sendCount = sendCount + (ulong)count;
                            try
                            {
                                _viewModel.SerialPort.Write(buffer, 0, count);
                                object[] args = new object[] { (int)(((num2 * 1.0) / ((double)length)) * 100.0) };
                                base.Invoke(updateProgBarSendFile, args);
                                if (!this.isSendFile)
                                {
                                    try
                                    {
                                        stream.Close();
                                        stream.Dispose();
                                        stream = null;
                                    }
                                    catch (IOException exception7)
                                    {
                                        MessageBox.Show(exception7.ToString(), "错误提示");
                                    }
                                    object[] objArray3 = new object[] { 0 };
                                    base.Invoke(updateProgBarSendFile, objArray3);
                                    Thread.CurrentThread.Abort();
                                    return;
                                }
                                continue;
                            }
                            catch
                            {
                                MessageBox.Show("写文件被意外中断", "错误提示");
                                try
                                {
                                    stream.Close();
                                    stream.Dispose();
                                    stream = null;
                                    this.isSendFile = false;
                                }
                                catch (IOException exception6)
                                {
                                    MessageBox.Show(exception6.ToString(), "错误提示");
                                }
                                object[] args = new object[] { 0 };
                                base.Invoke(updateProgBarSendFile, args);
                                Thread.CurrentThread.Abort();
                            }
                        }
                        else
                        {
                            try
                            {
                                stream.Close();
                                stream = null;
                                this.isSendFile = false;
                                Thread.CurrentThread.Abort();
                                return;
                            }
                            catch (IOException exception5)
                            {
                                MessageBox.Show(exception5.ToString(), "错误提示");
                            }
                        }
                    }
                    break;
                }
            }
            catch
            {
                MessageBox.Show("文件不存在，或被占用！", "提示");
            }
        }

        public void OpenSerialPort(bool isSetICon)
        {
            try
            {
                _viewModel.SerialPort.Open();
                this.isSerailPortClose = false;
                if (isSetICon)
                {
                    //this.btnOpenCom.Text = "关闭串口";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("打开串口失败!!!,或其它错误。\r\n请选择正确的串口或该串口被占用", "错误提示");
                this.CloseSerialPort(true);
            }
        }

        private void CloseSerialPort(bool isSetICon)
        {
            this.isSerailPortClose = true;
            while (this.isListening)
            {
                Application.DoEvents();
            }
            try
            {
                _viewModel.SerialPort.Close();
            }
            catch
            {
            }
            if (isSetICon)
            {
                //this.btnOpenCom.Text = "打开串口";
                //Stream manifestResourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("MySSCOM.img.关闭.ico");
                //this.btnOpenCom.Image = Image.FromStream(manifestResourceStream);
            }
        }

        private void SendSerailPortDataByHex(string str)
        {
            byte[] buffer;
            int num2;
            string[] strArray = str.Split(new char[] { ' ' });
            StringBuilder builder = new StringBuilder();
            int index = 0;
            while (true)
            {
                if (index >= strArray.Length)
                {
                    str = builder.ToString();
                    if ((str.Length % 2) != 0)
                    {
                        str = !this.isSendNewLine ? str.Insert(str.Length - 1, "0") : str.Insert(str.Length - 5, "0");
                    }
                    buffer = new byte[str.Length / 2];
                    num2 = 0;
                    break;
                }
                builder.Append(strArray[index]);
                index++;
            }
            while (true)
            {
                while (true)
                {
                    if (num2 < (str.Length / 2))
                    {
                        try
                        {
                            buffer[num2] = Convert.ToByte(str.Substring(num2 * 2, 2), 0x10);
                            break;
                        }
                        catch
                        {
                            MessageBox.Show("包含非16进制字符，发送失败！", "提示");
                        }
                        return;
                    }
                    else
                    {
                        if (_viewModel.SerialPort.IsOpen)
                        {
                            try
                            {
                                _viewModel.SerialPort.Write(buffer, 0, buffer.Length);
                                this.sendCount += (ulong)buffer.Length;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                                this.CloseSerialPort(true);
                            }
                        }
                        else
                        {
                            if (!this.isSerailPortClose)
                            {
                                this.OpenSerialPort(false);
                            }
                            if (!_viewModel.SerialPort.IsOpen)
                            {
                                MessageBox.Show("串口未打开，请先打开串口！", "错误提示");
                            }
                            else
                            {
                                try
                                {
                                    _viewModel.SerialPort.Write(buffer, 0, buffer.Length);
                                    this.sendCount += (ulong)buffer.Length;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                                    this.CloseSerialPort(true);
                                }
                            }
                        }
                        return;
                    }
                    break;
                }
                num2++;
            }
        }


        private enum multiSendPage : byte
        {
            first = 0,
            last = 1,
            next = 2,
            end = 3
        }

        private void btnOpenSendFile_Click(object sender, EventArgs e)
        {
            this.openFileDlgSend.Filter = "txt(*.txt)|*.txt|所有文件|*.*";
            this.openFileDlgSend.RestoreDirectory = false;
            if (this.openFileDlgSend.ShowDialog() == DialogResult.OK)
            {
                this.tbxSendFile.Text = this.openFileDlgSend.FileName;
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (_viewModel.SerialPort.IsOpen)
            {
                if (this.tbxSendFile.Text == "")
                {
                    MessageBox.Show("请先选择要发送的文件!", "提示");
                }
                else if (!this.isSendFile)
                {
                    this.sendFileName = this.tbxSendFile.Text;
                    Thread thread = new Thread(new ThreadStart(this.SendFile));
                    this.isSendFile = true;
                    thread.Start();
                }
            }
        }

        private void btnStopSendFile_Click(object sender, EventArgs e)
        {
            this.isSendFile = false;
            MessageBox.Show("发送文件被终止！", "提示");
        }

    }
}
