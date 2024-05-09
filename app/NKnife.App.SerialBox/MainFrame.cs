using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.IO.Ports;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Timers;
    using System.Windows.Forms;
using NKnife.SerialBox;

namespace NKnife.SerialBox
{

    public class MainFrame : Form
    {
        public const int WM_DEVICE_CHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVICE_REMOVE_COMPLETE = 0x8004;
        public const uint DBT_DEVTYP_PORT = 3;

        private string sendFileName = string.Empty;

        public static StringCollection strCkbMultiSendList;
        public static StringCollection strTbxMultiSendList;
        public static StringCollection strReMarkTbxMultiSendList;

        public static bool isMultiSendInfoFrameSureClosed = false;
        private static string[] strMultiSendArr = new string[40];

        private System.Timers.Timer sendMultiTimer = new System.Timers.Timer();
        private System.Timers.Timer transportProtocolAutoRetryTimer = new System.Timers.Timer();

        private ArrayList ckbMultiSendTbxList = new ArrayList();
        private ArrayList ckbMultiSendCkbList = new ArrayList();
        private int[] ckbMultiSendStatus = new int[40];
        private int currentMultiSend;
        private multiSendPage multiSendPageCur;
        private ArrayList multiSendPageList = new ArrayList();
        private ManageTransportProtocol manageTransportProtocol = new ManageTransportProtocol();
        private TransportProtocol revTransportProtocol = new TransportProtocol();
        private TransportProtocol outTransportProtocol = new TransportProtocol();

        private transportProtocolAutoSendFailProcessEventHandler transportProtocolAutoSendFailProcessDelegate;
        private transportProtocolUpdateFileProgressEventHandler transportProtocolUpdateFileProgressDelegate;

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

        private SerialPort sp = new SerialPort();
        private MultiSendInfoFrame multiSendInfoFrame;
        private System.Timers.Timer sendTimer = new System.Timers.Timer();
        private System.Timers.Timer transportSendTimer = new System.Timers.Timer();
        private UpdateTextEventHandler updateText;
        private static UpdateProgBarSendFileEventHandler updateProgBarSendFile;
        private SendTransportProtocolEventHandler sendTransportProtocolDelegate;
        private MonitorSerialPortChangeEventHandler MonitorCom;
        private CloseTransportProtocolAutoSendEventHandler transportProtocolAutoSendDelegate;
        private StringBuilder sbRecvData = new StringBuilder();
        private System.Timers.Timer transportProtocolRevTimeOutTimer = new System.Timers.Timer();
        private List<byte> transportProtocolRevList = new List<byte>();
        private int serialPortStatus;
        public static int PORT_OUT = 1;
        public static int PORT_IN = 2;
        private int currentSerialPortNumber;
        private OpenFileDialog openFileDlgTransportProtocol;

        #region Control

        private IContainer components;
        private Button btnSaveWindow;
        private Button btnClearWindow;
        private CheckBox ckbRevHex;
        private CheckBox ckbRTS;
        private CheckBox ckbDTR;
        private TabControl tabControl1;
        private TabPage multiTab;
        private TabPage singleTab;
        private Button btnStopSendFile;
        private Button btnClearSend;
        private Button btnSendFile;
        private TextBox tbxSendFile;
        private Button btnOpenSendFile;
        private Button btnSend;
        private TextBox tbxSendData;
        private CheckBox ckbSendNewLine;
        private CheckBox ckbHexSend;
        private Label label7;
        private Label label6;
        private TextBox tbxSendPeriod;
        private CheckBox ckbTimeSend;
        private Label label8;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripStatusLabel toolStripStatusLblSendCnt;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripStatusLabel toolStripStatusLblRevCnt;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripStatusLabel toolStripStatusLblCom;
        private ToolStripSeparator toolStripSeparator5;
        private StatusStrip statusStripCom;
        private Button btnOpenCom;
        private Label label1;
        private ComboBox cbxParity;
        private Label label2;
        private ComboBox cbxDataBits;
        private Label label3;
        private ComboBox cbxBaudRate;
        private Label label4;
        private ComboBox cbxComPort;
        private Label label5;
        private ComboBox cbxStopBits;
        private TextBox tbxMultiSend3;
        private TextBox tbxMultiSend4;
        private TextBox tbxMultiSend2;
        private Button btnMultiSend2;
        private Button btnMultiSend4;
        private Button btnMultiSend3;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem wewqToolStripMenuItem;
        private Label label14;
        private TextBox tbxMutilSendPeriod;
        private Label label13;
        private CheckBox ckbMultiAutoSend;
        private SaveFileDialog saveFileDlgRev;
        private System.Windows.Forms.Timer revCntTimer;
        private Label label16;
        private System.Windows.Forms.Timer sendCntTimer;
        private TextBox tbxRecvData;
        private OpenFileDialog openFileDlgSend;
        private ToolStripStatusLabel toolStripStatusLblTime;
        private ProgressBar progBarSendFile;
        private Label lblProgSendFile;
        private System.Windows.Forms.Timer showCurTimer;
        private CheckBox ckbMultiSend4;
        private CheckBox ckbMultiSend3;
        private CheckBox ckbMultiSend2;
        private Panel panel1;
        private CheckBox ckbMultiSend1;
        private TextBox tbxMultiSend1;
        private Button btnMultiSend1;
        private CheckBox ckbMultiSend10;
        private CheckBox ckbMultiSend9;
        private TextBox tbxMultiSend9;
        private TextBox tbxMultiSend10;
        private Button btnMultiSend10;
        private Button btnMultiSend9;
        private CheckBox ckbMultiSend8;
        private CheckBox ckbMultiSend7;
        private CheckBox ckbMultiSend6;
        private CheckBox ckbMultiSend5;
        private TextBox tbxMultiSend5;
        private Button btnMultiSend8;
        private Button btnMultiSend7;
        private TextBox tbxMultiSend7;
        private TextBox tbxMultiSend8;
        private TextBox tbxMultiSend6;
        private Button btnMultiSend6;
        private Button btnMultiSend5;
        private ToolStripMenuItem toolStripMenuItemCalculator;
        private CheckBox ckbMultiHexSend;
        private CheckBox ckbRelateKeyBoard;
        private ToolTip toolTipAll;
        private CheckBox ckbMultiSendNewLine;
        private ToolStripMenuItem toolStripMenuItemReset;
        private CheckBox ckbChangWinColor;
        private Button btnMultiEndPage;
        private Button btnMultiNextPage;
        private Button btnMultiLastPage;
        private Button btnMultiFirstPage;
        private TabPage transportProtocolTab;
        private Button btnRemarkMultiSend;
        private CheckBox ckbShowTime;
        private Panel panelMultiSend1;
        private Panel panelMultiSend2;
        private Button btnMultiSend12;
        private Button btnMultiSend11;
        private TextBox tbxMultiSend12;
        private TextBox tbxMultiSend14;
        private TextBox tbxMultiSend13;
        private Button btnMultiSend13;
        private Button btnMultiSend14;
        private Label label18;
        private TextBox tbxMultiSend11;
        private CheckBox ckbMultiSend11;
        private CheckBox ckbMultiSend20;
        private CheckBox ckbMultiSend12;
        private CheckBox ckbMultiSend19;
        private CheckBox ckbMultiSend13;
        private TextBox tbxMultiSend19;
        private CheckBox ckbMultiSend14;
        private TextBox tbxMultiSend20;
        private Button btnMultiSend15;
        private Button btnMultiSend20;
        private Button btnMultiSend16;
        private Button btnMultiSend19;
        private TextBox tbxMultiSend16;
        private CheckBox ckbMultiSend18;
        private TextBox tbxMultiSend18;
        private CheckBox ckbMultiSend17;
        private TextBox tbxMultiSend17;
        private CheckBox ckbMultiSend16;
        private Button btnMultiSend17;
        private CheckBox ckbMultiSend15;
        private Button btnMultiSend18;
        private TextBox tbxMultiSend15;
        private Panel panelMultiSend3;
        private Button btnMultiSend22;
        private Button btnMultiSend21;
        private TextBox tbxMultiSend22;
        private TextBox tbxMultiSend24;
        private TextBox tbxMultiSend23;
        private Button btnMultiSend23;
        private Button btnMultiSend24;
        private Label label19;
        private TextBox tbxMultiSend21;
        private CheckBox ckbMultiSend21;
        private CheckBox ckbMultiSend30;
        private CheckBox ckbMultiSend22;
        private CheckBox ckbMultiSend29;
        private CheckBox ckbMultiSend23;
        private TextBox tbxMultiSend29;
        private CheckBox ckbMultiSend24;
        private TextBox tbxMultiSend30;
        private Button btnMultiSend25;
        private Button btnMultiSend30;
        private Button btnMultiSend26;
        private Button btnMultiSend29;
        private TextBox tbxMultiSend26;
        private CheckBox ckbMultiSend28;
        private TextBox tbxMultiSend28;
        private CheckBox ckbMultiSend27;
        private TextBox tbxMultiSend27;
        private CheckBox ckbMultiSend26;
        private Button btnMultiSend27;
        private CheckBox ckbMultiSend25;
        private Button btnMultiSend28;
        private TextBox tbxMultiSend25;
        private Panel panelMultiSend4;
        private Button btnMultiSend32;
        private Button btnMultiSend31;
        private TextBox tbxMultiSend32;
        private TextBox tbxMultiSend34;
        private TextBox tbxMultiSend33;
        private Button btnMultiSend33;
        private Button btnMultiSend34;
        private Label label20;
        private TextBox tbxMultiSend31;
        private CheckBox ckbMultiSend31;
        private CheckBox ckbMultiSend40;
        private CheckBox ckbMultiSend32;
        private CheckBox ckbMultiSend39;
        private CheckBox ckbMultiSend33;
        private TextBox tbxMultiSend39;
        private CheckBox ckbMultiSend34;
        private TextBox tbxMultiSend40;
        private Button btnMultiSend35;
        private Button btnMultiSend40;
        private Button btnMultiSend36;
        private Button btnMultiSend39;
        private TextBox tbxMultiSend36;
        private CheckBox ckbMultiSend38;
        private TextBox tbxMultiSend38;
        private CheckBox ckbMultiSend37;
        private TextBox tbxMultiSend37;
        private CheckBox ckbMultiSend36;
        private Button btnMultiSend37;
        private CheckBox ckbMultiSend35;
        private Button btnMultiSend38;
        private TextBox tbxMultiSend35;
        private Label label21;
        private ComboBox cbxTransportProtocolChecksum;
        private Label label27;
        private Label label24;
        private TextBox tbxTransportProtocolSendFunctionType;
        private Label label23;
        private Label label22;
        private GroupBox groupBox2;
        private Label label31;
        private Label label28;
        private Label label29;
        private Label label26;
        private Label label25;
        private GroupBox groupBox1;
        private TextBox tbxTransportProtocolSlaveDeviceAddr;
        private Button btnTransportProtocolStopFile;
        private Button btnTransportProtocolSendFile;
        private Button btnTransportProtocolOpenFile;
        private ProgressBar progBarTransportProtocolSendFile;
        private TextBox tbxTransportProtocolSendData;
        private Button btnTransportProtocolSend;
        private TextBox tbxTransportProtocolFileName;
        private Label label33;
        private Label lblTransportProtocolProgSendFile;
        private Button btnEnbaleTransportProtocol;
        private Label label37;
        private TextBox tbxTransportProtocolSendPeriod;
        private Label label34;
        private TextBox tbxTransportProtocolMaxDataLength;
        private Label label35;
        private Label lblTransportProtocolSendDataLength;
        private Label lblTransportProtocolSendSequence;
        private Label lblTransportProtocolResult;
        private Label lblTransportProtocolRevDataLength;
        private Label lblTransportProtocolRevSequence;
        private Label lblTransportProtocolRevFunctionType;
        private Label lblTransportProtocolRevChecksum;
        private Label label32;
        private Label label38;
        private CheckBox ckbTransportProtocolAutoSend;
        private TextBox tbxTransportProtocolRetryCount;
        private CheckBox ckbTransportProtocolDispOrigialData;
        private Label lblTransportProtocolMasterAddr;
        private CheckBox ckbTransportProtocolAutoNewLine;

        #endregion

        public MainFrame()
        {
            this.InitializeComponent();
            float[] numArray = new float[(base.Controls.Count * 2) + 2];
            int num2 = 0;
            numArray[num2++] = base.Size.Width;
            numArray[num2++] = base.Size.Height;
            foreach (Control control in base.Controls)
            {
                Point location = control.Location;
                Size size = base.Size;
                numArray[num2++] = ((float) location.X) / ((float) size.Width);
                Point point2 = control.Location;
                Size size4 = base.Size;
                numArray[num2++] = ((float) point2.Y) / ((float) size4.Height);
                control.Tag = control.Size;
            }
            base.Tag = numArray;
        }

        private void addAllCbkMultiSendToList()
        {
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend1);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend2);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend3);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend4);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend5);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend6);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend7);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend8);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend9);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend10);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend11);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend12);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend13);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend14);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend15);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend16);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend17);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend18);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend19);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend20);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend21);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend22);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend23);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend24);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend25);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend26);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend27);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend28);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend29);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend30);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend31);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend32);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend33);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend34);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend35);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend36);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend37);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend38);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend39);
            this.ckbMultiSendTbxList.Add(this.tbxMultiSend40);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend1);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend2);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend3);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend4);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend5);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend6);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend7);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend8);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend9);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend10);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend11);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend12);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend13);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend14);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend15);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend16);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend17);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend18);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend19);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend20);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend21);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend22);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend23);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend24);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend25);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend26);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend27);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend28);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend29);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend30);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend31);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend32);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend33);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend34);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend35);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend36);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend37);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend38);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend39);
            this.ckbMultiSendCkbList.Add(this.ckbMultiSend40);
            for (int i = 0; i < this.ckbMultiSendStatus.Length; i++)
            {
                this.ckbMultiSendStatus[i] = 0;
            }
        }

        private void btnClearSend_Click(object sender, EventArgs e)
        {
            this.tbxSendData.Text = "";
            this.sendCount = 0UL;
        }

        private void btnClearWindow_Click(object sender, EventArgs e)
        {
            this.sbRecvData.Clear();
            this.tbxRecvData.Text = "";
            this.recieveCount = 0UL;
            this.sendCount = 0UL;
            this.toolStripStatusLblRevCnt.Text = "R:" + this.recieveCount.ToString();
        }

        private void btnEnbaleTransportProtocol_Click(object sender, EventArgs e)
        {
            if (!this.manageTransportProtocol.IsEnableTransportProtocol)
            {
                this.manageTransportProtocol.IsEnableTransportProtocol = true;
                this.enableTransportProtocolWidget();
            }
            else
            {
                if (this.manageTransportProtocol.IsHasSendTask)
                {
                    this.manageTransportProtocol.IsAbortSend = true;
                }
                this.manageTransportProtocol.IsEnableTransportProtocol = false;
                this.disableTransportProtocolWidget();
            }
        }

        private void btnMultiEndPage_Click(object sender, EventArgs e)
        {
            this.MultiSendCurrentPageSetting(multiSendPage.end);
        }

        private void btnMultiFirstPage_Click(object sender, EventArgs e)
        {
            this.MultiSendCurrentPageSetting(multiSendPage.first);
        }

        private void btnMultiLastPage_Click(object sender, EventArgs e)
        {
            this.MultiSendCurrentPageSetting(multiSendPage.last);
        }

        private void btnMultiNextPage_Click(object sender, EventArgs e)
        {
            this.MultiSendCurrentPageSetting(multiSendPage.next);
        }

        private void btnMultiSend1_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend1.Text);
        }

        private void btnMultiSend10_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend10.Text);
        }

        private void btnMultiSend11_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend11.Text);
        }

        private void btnMultiSend12_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend12.Text);
        }

        private void btnMultiSend13_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend13.Text);
        }

        private void btnMultiSend14_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend14.Text);
        }

        private void btnMultiSend15_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend15.Text);
        }

        private void btnMultiSend16_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend16.Text);
        }

        private void btnMultiSend17_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend17.Text);
        }

        private void btnMultiSend18_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend18.Text);
        }

        private void btnMultiSend19_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend19.Text);
        }

        private void btnMultiSend2_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend2.Text);
        }

        private void btnMultiSend20_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend20.Text);
        }

        private void btnMultiSend21_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend21.Text);
        }

        private void btnMultiSend22_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend22.Text);
        }

        private void btnMultiSend23_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend23.Text);
        }

        private void btnMultiSend24_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend24.Text);
        }

        private void btnMultiSend25_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend25.Text);
        }

        private void btnMultiSend26_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend26.Text);
        }

        private void btnMultiSend27_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend27.Text);
        }

        private void btnMultiSend28_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend28.Text);
        }

        private void btnMultiSend29_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend29.Text);
        }

        private void btnMultiSend3_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend3.Text);
        }

        private void btnMultiSend30_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend30.Text);
        }

        private void btnMultiSend31_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend31.Text);
        }

        private void btnMultiSend32_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend32.Text);
        }

        private void btnMultiSend33_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend33.Text);
        }

        private void btnMultiSend34_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend34.Text);
        }

        private void btnMultiSend35_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend35.Text);
        }

        private void btnMultiSend36_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend36.Text);
        }

        private void btnMultiSend37_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend37.Text);
        }

        private void btnMultiSend38_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend38.Text);
        }

        private void btnMultiSend39_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend39.Text);
        }

        private void btnMultiSend4_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend4.Text);
        }

        private void btnMultiSend40_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend40.Text);
        }

        private void btnMultiSend5_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend5.Text);
        }

        private void btnMultiSend6_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend6.Text);
        }

        private void btnMultiSend7_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend7.Text);
        }

        private void btnMultiSend8_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend8.Text);
        }

        private void btnMultiSend9_Click(object sender, EventArgs e)
        {
            this.sendMultiSerialPortData(this.tbxMultiSend9.Text);
        }

        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(true);
            }
            else if (!this.isExistSerialPort)
            {
                MessageBox.Show("没有搜索到串口！", "错误提示");
            }
            else
            {
                this.SetPortProperty();
                this.openSerialPort(true);
                if (this.sp.IsOpen)
                {
                    this.getCurrentInfoLineStatus(this.sp);
                }
            }
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

        private void btnRemarkMultiSend_Click(object sender, EventArgs e)
        {
            this.updateMultiSendInfoToList();
            if (this.multiSendInfoFrame == null)
            {
                this.multiSendInfoFrame = new MultiSendInfoFrame();
            }
            if (this.multiSendInfoFrame.IsDisposed)
            {
                this.multiSendInfoFrame = new MultiSendInfoFrame();
            }
            isMultiSendInfoFrameSureClosed = false;
            this.multiSendInfoFrame.Owner = this;
            this.multiSendInfoFrame.Disposed += new EventHandler(this.MultiSendInfoFrame_FormClosing);
            this.multiSendInfoFrame.Show();
            this.multiSendInfoFrame.Activate();
        }

        private void btnSaveWindow_Click(object sender, EventArgs e)
        {
            this.saveFileDlgRev.Filter = "txt(*.txt)|*.txt|所有文件|*.*";
            this.saveFileDlgRev.RestoreDirectory = false;
            if (this.saveFileDlgRev.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(this.saveFileDlgRev.FileName);
                writer.Write(this.tbxRecvData.Text);
                writer.Close();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.sendSingleData(this.tbxSendData.Text);
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                if (this.tbxSendFile.Text == "")
                {
                    MessageBox.Show("请先选择要发送的文件!", "提示");
                }
                else if (!this.isSendFile)
                {
                    this.sendFileName = this.tbxSendFile.Text;
                    Thread thread = new Thread(new ThreadStart(this.sendFile));
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

        private void btnTransportProtocolOpenFile_Click(object sender, EventArgs e)
        {
            this.openFileDlgTransportProtocol.Filter = "所有文件|*.*";
            this.openFileDlgTransportProtocol.RestoreDirectory = false;
            if (this.openFileDlgTransportProtocol.ShowDialog() == DialogResult.OK)
            {
                this.tbxTransportProtocolFileName.Text = this.openFileDlgTransportProtocol.FileName;
            }
        }

        private void btnTransportProtocolSend_Click(object sender, EventArgs e)
        {
            if (this.manageTransportProtocol.IsHasSendTask && (this.manageTransportProtocol.CurrentSendTaskType == 1))
            {
                this.manageTransportProtocol.IsAbortSend = true;
                while (this.manageTransportProtocol.IsHasSendTask)
                {
                }
            }
            else
            {
                this.manageTransportProtocol.IsHasSendTask = true;
                this.manageTransportProtocol.CurrentSendTaskType = 1;
                new Thread(new ThreadStart(this.excuteSendFileTask)) { Name = "传输协议发送任务线程" }.Start();
            }
        }

        private void btnTransportProtocolSendFile_Click(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                if (this.tbxTransportProtocolFileName.Text != "")
                {
                    if (this.manageTransportProtocol.IsHasSendTask)
                    {
                        if (this.manageTransportProtocol.CurrentSendTaskType == 3)
                        {
                            return;
                        }
                        this.manageTransportProtocol.IsAbortSend = true;
                        while (this.manageTransportProtocol.IsHasSendTask)
                        {
                        }
                    }
                    this.manageTransportProtocol.IsHasSendTask = true;
                    this.manageTransportProtocol.CurrentSendTaskType = 3;
                    this.disableTransportProtocolWidgetExceptckbTransportProtocolBtnStopSendFile();
                    Thread thread = new Thread(new ThreadStart(this.transportProtocolSendFile));
                    this.manageTransportProtocol.SendFileName = this.tbxTransportProtocolFileName.Text;
                    this.manageTransportProtocol.SendFileOneFrameDataLength = Convert.ToInt32(this.tbxTransportProtocolMaxDataLength.Text);
                    thread.Name = "协议传输发送文件线程";
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("请先选择要发送的文件!", "提示");
                }
            }
        }

        private void btnTransportProtocolStopFile_Click(object sender, EventArgs e)
        {
            if (this.manageTransportProtocol.IsHasSendTask && (this.manageTransportProtocol.CurrentSendTaskType == 3))
            {
                this.manageTransportProtocol.IsAbortSend = true;
            }
        }

        private void cbxBaudRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (((((keyChar < '0') || (keyChar > '9')) && (keyChar != '\b')) && (keyChar != '8')) && (keyChar != '\x0003'))
            {
                e.KeyChar = '\0';
            }
        }

        private void cbxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(false);
                this.cbxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
                this.sp.BaudRate = (this.cbxBaudRate.Text.Trim() != "自定义") ? Convert.ToInt32(this.cbxBaudRate.Text.Trim()) : 0x2580;
                this.openSerialPort(false);
            }
            else if (this.cbxBaudRate.Text.Trim() != "自定义")
            {
                this.cbxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                this.cbxBaudRate.DropDownStyle = ComboBoxStyle.DropDown;
                this.sp.BaudRate = 0x2580;
            }
        }

        private void cbxComPort_MouseClick(object sender, MouseEventArgs e)
        {
            string[] strs = this.searchSerialPort();
            if (strs != null)
            {
                ArrayList list = new ArrayList();
                list.AddRange(this.proceStrForSerialPort(strs));
                if (!list.Contains(this.cbxComPort.Text))
                {
                    this.cbxComPort.Items.Remove(this.cbxComPort.Text);
                    if (this.cbxComPort.Items.Count > 0)
                    {
                        this.cbxComPort.SelectedIndex = 0;
                    }
                }
            }
        }

        private void cbxComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(false);
                this.sp.PortName = this.proceGetOpenSerialPortName(this.cbxComPort.Text.Trim());
                this.openSerialPort(false);
            }
        }

        private void cbxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(false);
                this.sp.DataBits = Convert.ToInt16(this.cbxDataBits.Text.Trim());
                this.openSerialPort(false);
            }
        }

        private void cbxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(false);
                string str = this.cbxParity.Text.Trim();
                this.sp.Parity = (str.CompareTo("无") != 0) ? ((str.CompareTo("奇校验") != 0) ? ((str.CompareTo("偶校验") != 0) ? Parity.None : Parity.Even) : Parity.Odd) : Parity.None;
                this.openSerialPort(false);
            }
        }

        private void cbxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(false);
                float num = Convert.ToSingle(this.cbxStopBits.Text.Trim());
                this.sp.StopBits = (num != 0f) ? ((num != 1.5) ? ((num != 1f) ? ((num != 2f) ? StopBits.One : StopBits.Two) : StopBits.One) : StopBits.OnePointFive) : StopBits.None;
                this.openSerialPort(false);
            }
        }

        private void cbxTransportProtocolChecksum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxTransportProtocolChecksum.SelectedIndex == 0)
            {
                this.revTransportProtocol.ChecksumMethod = 3;
                this.outTransportProtocol.ChecksumMethod = 3;
            }
            else if (this.cbxTransportProtocolChecksum.SelectedIndex == 1)
            {
                this.revTransportProtocol.ChecksumMethod = 2;
                this.outTransportProtocol.ChecksumMethod = 2;
            }
            else if (this.cbxTransportProtocolChecksum.SelectedIndex == 2)
            {
                this.revTransportProtocol.ChecksumMethod = 4;
                this.outTransportProtocol.ChecksumMethod = 4;
            }
            else if (this.cbxTransportProtocolChecksum.SelectedIndex == 3)
            {
                this.revTransportProtocol.ChecksumMethod = 5;
                this.outTransportProtocol.ChecksumMethod = 5;
            }
        }

        private void ckbChangWinColor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbChangWinColor.Checked)
            {
                this.tbxRecvData.BackColor = Color.White;
                this.tbxRecvData.ForeColor = Color.Black;
            }
            else
            {
                this.tbxRecvData.BackColor = Color.Black;
                this.tbxRecvData.ForeColor = Color.Chartreuse;
            }
        }

        private void ckbDTR_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbDTR.Checked)
            {
                this.sp.DtrEnable = true;
            }
            else
            {
                this.sp.DtrEnable = false;
            }
        }

        private void ckbHexSend_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.ckbHexSend.Checked)
            {
                if (!this.isRuningReset)
                {
                    this.tbxSendData.Text = Utils.HexToStr(this.tbxSendData.Text);
                    this.isHexSend = false;
                }
            }
            else if (!this.isRuningLoadUserInfo)
            {
                this.tbxSendData.Text = Utils.StrToHex(this.tbxSendData.Text);
                this.isHexSend = true;
            }
        }

        private void ckbMultiAutoSend_CheckedChanged(object sender, EventArgs e)
        {
            this.tbxMutilSendPeriod.Enabled = !this.ckbMultiAutoSend.Checked;
            if (this.sp.IsOpen)
            {
                if (this.ckbMultiAutoSend.Checked)
                {
                    this.currentMultiSend = 0;
                    this.sendMultiTimer.Interval = Convert.ToInt32(this.tbxMutilSendPeriod.Text.Trim());
                    this.sendMultiTimer.Start();
                }
                else
                {
                    this.sendMultiTimer.Stop();
                }
            }
        }

        private void ckbMultiHexSend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiHexSend.Checked)
            {
                this.isMultiHexSend = true;
            }
            else
            {
                this.isMultiHexSend = false;
            }
        }

        private void ckbMultiSend1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend1.Checked)
            {
                this.ckbMultiSendStatus[0] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0] = 0;
            }
        }

        private void ckbMultiSend10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend10.Checked)
            {
                this.ckbMultiSendStatus[9] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[9] = 0;
            }
        }

        private void ckbMultiSend11_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend11.Checked)
            {
                this.ckbMultiSendStatus[10] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[10] = 0;
            }
        }

        private void ckbMultiSend12_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend12.Checked)
            {
                this.ckbMultiSendStatus[11] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[11] = 0;
            }
        }

        private void ckbMultiSend13_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend13.Checked)
            {
                this.ckbMultiSendStatus[12] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[12] = 0;
            }
        }

        private void ckbMultiSend14_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend14.Checked)
            {
                this.ckbMultiSendStatus[13] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[13] = 0;
            }
        }

        private void ckbMultiSend15_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend15.Checked)
            {
                this.ckbMultiSendStatus[14] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[14] = 0;
            }
        }

        private void ckbMultiSend16_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend16.Checked)
            {
                this.ckbMultiSendStatus[15] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[15] = 0;
            }
        }

        private void ckbMultiSend17_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend17.Checked)
            {
                this.ckbMultiSendStatus[0x10] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x10] = 0;
            }
        }

        private void ckbMultiSend18_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend18.Checked)
            {
                this.ckbMultiSendStatus[0x11] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x11] = 0;
            }
        }

        private void ckbMultiSend19_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend19.Checked)
            {
                this.ckbMultiSendStatus[0x12] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x12] = 0;
            }
        }

        private void ckbMultiSend2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend2.Checked)
            {
                this.ckbMultiSendStatus[1] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[1] = 0;
            }
        }

        private void ckbMultiSend20_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend20.Checked)
            {
                this.ckbMultiSendStatus[0x13] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x13] = 0;
            }
        }

        private void ckbMultiSend21_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend21.Checked)
            {
                this.ckbMultiSendStatus[20] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[20] = 0;
            }
        }

        private void ckbMultiSend22_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend22.Checked)
            {
                this.ckbMultiSendStatus[0x15] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x15] = 0;
            }
        }

        private void ckbMultiSend23_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend23.Checked)
            {
                this.ckbMultiSendStatus[0x16] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x16] = 0;
            }
        }

        private void ckbMultiSend24_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend24.Checked)
            {
                this.ckbMultiSendStatus[0x17] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x17] = 0;
            }
        }

        private void ckbMultiSend25_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend25.Checked)
            {
                this.ckbMultiSendStatus[0x18] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x18] = 0;
            }
        }

        private void ckbMultiSend26_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend26.Checked)
            {
                this.ckbMultiSendStatus[0x19] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x19] = 0;
            }
        }

        private void ckbMultiSend27_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend27.Checked)
            {
                this.ckbMultiSendStatus[0x1a] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x1a] = 0;
            }
        }

        private void ckbMultiSend28_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend28.Checked)
            {
                this.ckbMultiSendStatus[0x1b] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x1b] = 0;
            }
        }

        private void ckbMultiSend29_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend29.Checked)
            {
                this.ckbMultiSendStatus[0x1c] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x1c] = 0;
            }
        }

        private void ckbMultiSend3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend3.Checked)
            {
                this.ckbMultiSendStatus[2] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[2] = 0;
            }
        }

        private void ckbMultiSend30_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend30.Checked)
            {
                this.ckbMultiSendStatus[0x1d] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x1d] = 0;
            }
        }

        private void ckbMultiSend31_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend31.Checked)
            {
                this.ckbMultiSendStatus[30] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[30] = 0;
            }
        }

        private void ckbMultiSend32_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend32.Checked)
            {
                this.ckbMultiSendStatus[0x1f] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x1f] = 0;
            }
        }

        private void ckbMultiSend33_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend33.Checked)
            {
                this.ckbMultiSendStatus[0x20] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x20] = 0;
            }
        }

        private void ckbMultiSend34_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend34.Checked)
            {
                this.ckbMultiSendStatus[0x21] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x21] = 0;
            }
        }

        private void ckbMultiSend35_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend35.Checked)
            {
                this.ckbMultiSendStatus[0x22] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x22] = 0;
            }
        }

        private void ckbMultiSend36_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend36.Checked)
            {
                this.ckbMultiSendStatus[0x23] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x23] = 0;
            }
        }

        private void ckbMultiSend37_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend37.Checked)
            {
                this.ckbMultiSendStatus[0x24] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x24] = 0;
            }
        }

        private void ckbMultiSend38_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend38.Checked)
            {
                this.ckbMultiSendStatus[0x25] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x25] = 0;
            }
        }

        private void ckbMultiSend39_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend39.Checked)
            {
                this.ckbMultiSendStatus[0x26] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x26] = 0;
            }
        }

        private void ckbMultiSend4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend4.Checked)
            {
                this.ckbMultiSendStatus[3] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[3] = 0;
            }
        }

        private void ckbMultiSend40_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend40.Checked)
            {
                this.ckbMultiSendStatus[0x27] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[0x27] = 0;
            }
        }

        private void ckbMultiSend5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend5.Checked)
            {
                this.ckbMultiSendStatus[4] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[4] = 0;
            }
        }

        private void ckbMultiSend6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend6.Checked)
            {
                this.ckbMultiSendStatus[5] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[5] = 0;
            }
        }

        private void ckbMultiSend7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend7.Checked)
            {
                this.ckbMultiSendStatus[6] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[6] = 0;
            }
        }

        private void ckbMultiSend8_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend8.Checked)
            {
                this.ckbMultiSendStatus[7] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[7] = 0;
            }
        }

        private void ckbMultiSend9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSend9.Checked)
            {
                this.ckbMultiSendStatus[8] = 1;
            }
            else
            {
                this.ckbMultiSendStatus[8] = 0;
            }
        }

        private void ckbMultiSendNewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbMultiSendNewLine.Checked)
            {
                this.isMultiSendNewLine = true;
            }
            else
            {
                this.isMultiSendNewLine = false;
            }
        }

        private void ckbRelateKeyBoard_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked)
            {
                this.isRalateKeyBoard(true);
            }
            else
            {
                this.isRalateKeyBoard(false);
            }
        }

        private void ckbRevHex_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbRevHex.Checked)
            {
                this.tbxRecvData.Text = Utils.StrToHex(this.sbRecvData.ToString());
                this.sbRecvData.Clear();
                this.sbRecvData.Append(this.tbxRecvData.Text);
                this.isHexRevDisp = true;
            }
            else
            {
                this.tbxRecvData.Text = Utils.HexToStr(this.sbRecvData.ToString());
                this.sbRecvData.Clear();
                this.sbRecvData.Append(this.tbxRecvData.Text);
                this.isHexRevDisp = false;
            }
        }

        private void ckbRTS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbRTS.Checked)
            {
                this.sp.RtsEnable = true;
            }
            else
            {
                this.sp.RtsEnable = false;
            }
        }

        private void ckbSendNewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbSendNewLine.Checked)
            {
                this.isSendNewLine = true;
            }
            else
            {
                this.isSendNewLine = false;
            }
        }

        private void ckbTimeSend_CheckedChanged(object sender, EventArgs e)
        {
            this.tbxSendPeriod.Enabled = !this.ckbTimeSend.Checked;
            if (this.sp.IsOpen)
            {
                if (this.ckbTimeSend.Checked)
                {
                    this.sendTimer.Interval = Convert.ToInt32(this.tbxSendPeriod.Text.Trim());
                    this.sendTimer.Start();
                }
                else
                {
                    this.sendTimer.Stop();
                }
            }
        }

        private void ckbTransportProtocolAutoNewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbTransportProtocolAutoNewLine.Checked)
            {
                this.manageTransportProtocol.IsAutoNewLine = true;
            }
            else
            {
                this.manageTransportProtocol.IsAutoNewLine = false;
            }
        }

        private void ckbTransportProtocolAutoSend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.sp.IsOpen)
            {
                if (this.ckbTransportProtocolAutoSend.Checked)
                {
                    if (this.manageTransportProtocol.IsHasSendTask)
                    {
                        this.manageTransportProtocol.IsAbortSend = true;
                        MessageBox.Show("当前发送任务被终止！", "提示");
                        while (this.manageTransportProtocol.IsHasSendTask)
                        {
                        }
                    }
                    this.manageTransportProtocol.IsHasSendTask = true;
                    this.manageTransportProtocol.CurrentSendTaskType = 2;
                    this.disableTransportProtocolWidgetExceptckbTransportProtocolAutoSend();
                    this.transportSendTimer.Interval = Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text.Trim());
                    this.transportSendTimer.Start();
                }
                else if (this.manageTransportProtocol.IsHasSendTask && (this.manageTransportProtocol.CurrentSendTaskType == 2))
                {
                    this.manageTransportProtocol.IsAbortSend = true;
                }
            }
        }

        private void ckbTransportProtocolDispOrigialData_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbTransportProtocolDispOrigialData.Checked)
            {
                this.manageTransportProtocol.IsDispOriginalData = true;
            }
            else
            {
                this.manageTransportProtocol.IsDispOriginalData = false;
            }
        }

        private void closeSerialPort(bool isSetICon)
        {
            this.isSerailPortClose = true;
            while (this.isListening)
            {
                Application.DoEvents();
            }
            try
            {
                this.sp.Close();
            }
            catch
            {
            }
            if (isSetICon)
            {
                this.btnOpenCom.Text = "打开串口";
                //Stream manifestResourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("MySSCOM.img.关闭.ico");
                //this.btnOpenCom.Image = Image.FromStream(manifestResourceStream);
            }
        }

        private void closeTransportProtocolAutoSend()
        {
            this.ckbTransportProtocolAutoSend.Checked = false;
        }

        private void disableTransportProtocolWidget()
        {
            this.ckbRevHex.Enabled = true;
            this.tbxTransportProtocolSlaveDeviceAddr.Enabled = false;
            this.tbxTransportProtocolSendFunctionType.Enabled = false;
            this.tbxTransportProtocolSendPeriod.Enabled = false;
            this.ckbTransportProtocolAutoSend.Enabled = false;
            this.tbxTransportProtocolRetryCount.Enabled = false;
            this.cbxTransportProtocolChecksum.Enabled = false;
            this.tbxTransportProtocolSendData.Enabled = false;
            this.btnTransportProtocolSend.Enabled = false;
            this.btnTransportProtocolOpenFile.Enabled = false;
            this.btnTransportProtocolSendFile.Enabled = false;
            this.btnTransportProtocolStopFile.Enabled = false;
            this.tbxTransportProtocolFileName.Enabled = false;
            this.tbxTransportProtocolMaxDataLength.Enabled = false;
            this.ckbTransportProtocolDispOrigialData.Enabled = false;
            this.ckbTransportProtocolAutoNewLine.Enabled = false;
            this.btnEnbaleTransportProtocol.Text = "启动协议传输";
        }

        private void disableTransportProtocolWidgetExceptckbTransportProtocolAutoSend()
        {
            this.tbxTransportProtocolSlaveDeviceAddr.Enabled = false;
            this.tbxTransportProtocolSendFunctionType.Enabled = false;
            this.tbxTransportProtocolSendPeriod.Enabled = false;
            this.tbxTransportProtocolRetryCount.Enabled = false;
            this.cbxTransportProtocolChecksum.Enabled = false;
            this.tbxTransportProtocolSendData.Enabled = false;
            this.btnTransportProtocolSend.Enabled = false;
            this.btnTransportProtocolOpenFile.Enabled = false;
            this.btnTransportProtocolSendFile.Enabled = false;
            this.btnTransportProtocolStopFile.Enabled = false;
            this.tbxTransportProtocolFileName.Enabled = false;
            this.tbxTransportProtocolMaxDataLength.Enabled = false;
            this.ckbTransportProtocolDispOrigialData.Enabled = false;
            this.ckbTransportProtocolAutoNewLine.Enabled = false;
        }

        private void disableTransportProtocolWidgetExceptckbTransportProtocolBtnStopSendFile()
        {
            this.tbxTransportProtocolSlaveDeviceAddr.Enabled = false;
            this.tbxTransportProtocolSendFunctionType.Enabled = false;
            this.tbxTransportProtocolSendPeriod.Enabled = false;
            this.ckbTransportProtocolAutoSend.Enabled = false;
            this.tbxTransportProtocolRetryCount.Enabled = false;
            this.cbxTransportProtocolChecksum.Enabled = false;
            this.tbxTransportProtocolSendData.Enabled = false;
            this.btnTransportProtocolSend.Enabled = false;
            this.btnTransportProtocolOpenFile.Enabled = false;
            this.btnTransportProtocolSendFile.Enabled = false;
            this.tbxTransportProtocolFileName.Enabled = false;
            this.tbxTransportProtocolMaxDataLength.Enabled = false;
            this.ckbTransportProtocolDispOrigialData.Enabled = false;
            this.ckbTransportProtocolAutoNewLine.Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void enableTransportProtocolWidget()
        {
            if (!this.ckbRevHex.Checked)
            {
                this.ckbRevHex.Checked = true;
            }
            this.ckbRevHex.Enabled = false;
            this.tbxTransportProtocolSlaveDeviceAddr.Enabled = true;
            this.tbxTransportProtocolSendFunctionType.Enabled = true;
            this.tbxTransportProtocolSendPeriod.Enabled = true;
            this.ckbTransportProtocolAutoSend.Enabled = true;
            this.tbxTransportProtocolRetryCount.Enabled = true;
            this.cbxTransportProtocolChecksum.Enabled = true;
            this.tbxTransportProtocolSendData.Enabled = true;
            this.btnTransportProtocolSend.Enabled = true;
            this.btnTransportProtocolOpenFile.Enabled = true;
            this.btnTransportProtocolSendFile.Enabled = true;
            this.btnTransportProtocolStopFile.Enabled = true;
            this.tbxTransportProtocolFileName.Enabled = true;
            this.tbxTransportProtocolMaxDataLength.Enabled = true;
            this.ckbTransportProtocolDispOrigialData.Enabled = true;
            this.ckbTransportProtocolAutoNewLine.Enabled = true;
            this.btnEnbaleTransportProtocol.Text = "关闭协议传输";
        }

        private void excuteSendFileTask()
        {
            bool flag = false;
            while (true)
            {
                if (!flag)
                {
                    base.Invoke(this.sendTransportProtocolDelegate);
                    if (this.manageTransportProtocol.IsSendTransportProtocolSuccess)
                    {
                        this.manageTransportProtocol.IsCompleteRetry = false;
                        this.transportProtocolAutoRetryTimer.Interval = Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text);
                        this.transportProtocolAutoRetryTimer.Start();
                        this.manageTransportProtocol.OriginalFrameSequence = this.manageTransportProtocol.SendSequence;
                        while (true)
                        {
                            if (this.manageTransportProtocol.IsRevTransportProtocolSuccess)
                            {
                                this.manageTransportProtocol.SendTaskResult = 1;
                            }
                            else if (this.manageTransportProtocol.IsCompleteRetry)
                            {
                                this.manageTransportProtocol.SendTaskResult = 4;
                            }
                            else if (this.manageTransportProtocol.IsAbortSend)
                            {
                                this.manageTransportProtocol.SendTaskResult = 8;
                            }
                            else
                            {
                                if (this.manageTransportProtocol.IsSendTransportProtocolSuccess)
                                {
                                    continue;
                                }
                                this.manageTransportProtocol.SendTaskResult = 2;
                            }
                            this.transportProtocolAutoRetryTimer.Stop();
                            this.manageTransportProtocol.RetryCount = 0;
                            flag = true;
                            break;
                        }
                        continue;
                    }
                    flag = true;
                    this.manageTransportProtocol.SendTaskResult = 2;
                }
                if (this.manageTransportProtocol.SendTaskResult != 1)
                {
                    this.manageTransportProtocol.IsHasSendTask = false;
                    this.manageTransportProtocol.IsAbortSend = false;
                    switch (this.manageTransportProtocol.CurrentSendTaskType)
                    {
                        case 2:
                            base.Invoke(this.transportProtocolAutoSendFailProcessDelegate);
                            break;

                        case 3:
                            this.manageTransportProtocol.IsStopSendFile = true;
                            this.manageTransportProtocol.IsReadSendFileOneFrameData = false;
                            break;

                        default:
                            break;
                    }
                    this.promptSendTaskFailInfo(this.manageTransportProtocol.SendTaskResult);
                    return;
                }
                switch (this.manageTransportProtocol.CurrentSendTaskType)
                {
                    case 1:
                        this.manageTransportProtocol.IsHasSendTask = false;
                        return;

                    case 2:
                        this.transportSendTimer.Interval = Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text.Trim());
                        this.transportSendTimer.Start();
                        return;

                    case 3:
                        this.manageTransportProtocol.IsReadSendFileOneFrameData = false;
                        return;
                }
                return;
            }
        }

        private void getCurrentInfoLineStatus(SerialPort port)
        {
            string str = string.Empty;
            str = !port.CtsHolding ? (str + " CTS=0 ") : (str + " CTS=1 ");
            str = !port.DsrHolding ? (str + "DSR=0 ") : (str + "DSR=1 ");
            str = !port.CDHolding ? (str + "DCD=0 ") : (str + "DCD=1 ");
            this.toolStripStatusLblCom.Text = str;
        }

        private string getHexSystemTime() => 
            DateTime.Now.ToString("0A[yyyy-MM-dd hh:mm:ss.fff]\r\n");

        private string getStrSystemTime() => 
            DateTime.Now.ToString("[yyyy-MM-dd hh:mm:ss.fff]\r");

        private void initAllKeyBoardEvent()
        {
            this.tbxMultiSend1.KeyPress += new KeyPressEventHandler(this.tbxMultiSend1_KeyPress);
            this.tbxMultiSend2.KeyPress += new KeyPressEventHandler(this.tbxMultiSend2_KeyPress);
            this.tbxMultiSend3.KeyPress += new KeyPressEventHandler(this.tbxMultiSend3_KeyPress);
            this.tbxMultiSend4.KeyPress += new KeyPressEventHandler(this.tbxMultiSend4_KeyPress);
            this.tbxMultiSend5.KeyPress += new KeyPressEventHandler(this.tbxMultiSend5_KeyPress);
            this.tbxMultiSend6.KeyPress += new KeyPressEventHandler(this.tbxMultiSend6_KeyPress);
            this.tbxMultiSend7.KeyPress += new KeyPressEventHandler(this.tbxMultiSend7_KeyPress);
            this.tbxMultiSend8.KeyPress += new KeyPressEventHandler(this.tbxMultiSend8_KeyPress);
            this.tbxMultiSend9.KeyPress += new KeyPressEventHandler(this.tbxMultiSend9_KeyPress);
            this.tbxMultiSend10.KeyPress += new KeyPressEventHandler(this.tbxMultiSend10_KeyPress);
        }

        private void initAllSerialPortSettings()
        {
            this.isRuningLoadUserInfo = true;
            if (!this.isExistSerialPort)
            {
                this.cbxComPort.Items.Add(Settings.Default.PortName);
            }
            else if (Settings.Default.PortName == "")
            {
                Settings.Default.PortName = (string) this.cbxComPort.Items[0];
            }
            this.cbxComPort.Text = Settings.Default.PortName;
            this.cbxBaudRate.Text = !this.cbxBaudRate.Items.Contains(Settings.Default.BaudRate) ? "9600" : Settings.Default.BaudRate;
            this.cbxStopBits.Text = Settings.Default.StopBits;
            this.cbxDataBits.Text = Settings.Default.DataBits;
            this.cbxParity.Text = Settings.Default.Parity;
            this.ckbRevHex.Checked = Settings.Default.RevHex;
            this.isHexRevDisp = this.ckbRevHex.Checked;
            this.ckbRTS.Checked = Settings.Default.RTS;
            this.ckbDTR.Checked = Settings.Default.DTR;
            this.ckbChangWinColor.Checked = Settings.Default.ChangWinColor;
            this.ckbShowTime.Checked = Settings.Default.ShowTimeCheckBox;
            this.tbxSendData.Text = Settings.Default.SendData;
            this.ckbTimeSend.Checked = Settings.Default.TimeSend;
            this.tbxSendPeriod.Text = Settings.Default.SendPeriod;
            this.ckbHexSend.Checked = Settings.Default.HexSend;
            this.isHexSend = this.ckbHexSend.Checked;
            this.ckbSendNewLine.Checked = Settings.Default.SendNewLine;
            this.isSendNewLine = this.ckbSendNewLine.Checked;
            this.ckbMultiSendNewLine.Checked = Settings.Default.MultiSendNewLine;
            this.isMultiSendNewLine = this.ckbMultiSendNewLine.Checked;
            this.ckbMultiHexSend.Checked = Settings.Default.MultiHexSend;
            this.isMultiHexSend = this.ckbMultiHexSend.Checked;
            this.ckbRelateKeyBoard.Checked = Settings.Default.RelateKeyBoard;
            this.ckbMultiAutoSend.Checked = Settings.Default.MultiAutoSend;
            this.tbxMutilSendPeriod.Text = Settings.Default.MutilSendPeriod;
            this.ckbTransportProtocolAutoNewLine.Checked = Settings.Default.CkbTransportProtocolAutoNewLine;
            this.ckbTransportProtocolDispOrigialData.Checked = Settings.Default.CkbTransportProtocolDispOrigialData;
            this.tbxTransportProtocolSlaveDeviceAddr.Text = Settings.Default.TbxTransportProtocolSlaveDeviceAddr;
            this.tbxTransportProtocolSendFunctionType.Text = Settings.Default.TbxTransportProtocolSendFunctionType;
            this.tbxTransportProtocolSendPeriod.Text = Settings.Default.TbxTransportProtocolSendPeriod;
            this.ckbTransportProtocolAutoSend.Checked = Settings.Default.CkbTransportProtocolAutoSend;
            this.tbxTransportProtocolRetryCount.Text = Settings.Default.TbxTransportProtocolRetryCount;
            this.cbxTransportProtocolChecksum.Text = Settings.Default.CbxTransportProtocolChecksum;
            if (this.cbxTransportProtocolChecksum.Text == "SUM(累加)")
            {
                this.cbxTransportProtocolChecksum.SelectedIndex = 0;
            }
            else if (this.cbxTransportProtocolChecksum.Text == "XOR(异或)")
            {
                this.cbxTransportProtocolChecksum.SelectedIndex = 1;
            }
            else if (this.cbxTransportProtocolChecksum.Text == "CRC8")
            {
                this.cbxTransportProtocolChecksum.SelectedIndex = 2;
            }
            else if (this.cbxTransportProtocolChecksum.Text == "CRC16")
            {
                this.cbxTransportProtocolChecksum.SelectedIndex = 3;
            }
            this.tbxTransportProtocolSendData.Text = Settings.Default.TransportProtocolSendDataTextBox;
            this.tbxTransportProtocolMaxDataLength.Text = Settings.Default.TransportProtocolMaxDataLengthTextBox;
            this.isSendFile = false;
            this.multiSendEditInit();
            this.transportProtocolInit();
            this.isRuningLoadUserInfo = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSaveWindow = new System.Windows.Forms.Button();
            this.btnClearWindow = new System.Windows.Forms.Button();
            this.ckbRevHex = new System.Windows.Forms.CheckBox();
            this.ckbRTS = new System.Windows.Forms.CheckBox();
            this.ckbDTR = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.singleTab = new System.Windows.Forms.TabPage();
            this.lblProgSendFile = new System.Windows.Forms.Label();
            this.progBarSendFile = new System.Windows.Forms.ProgressBar();
            this.label16 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnStopSendFile = new System.Windows.Forms.Button();
            this.btnClearSend = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.tbxSendFile = new System.Windows.Forms.TextBox();
            this.btnOpenSendFile = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxSendData = new System.Windows.Forms.TextBox();
            this.ckbSendNewLine = new System.Windows.Forms.CheckBox();
            this.ckbHexSend = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxSendPeriod = new System.Windows.Forms.TextBox();
            this.ckbTimeSend = new System.Windows.Forms.CheckBox();
            this.multiTab = new System.Windows.Forms.TabPage();
            this.panelMultiSend1 = new System.Windows.Forms.Panel();
            this.btnMultiSend2 = new System.Windows.Forms.Button();
            this.btnMultiSend1 = new System.Windows.Forms.Button();
            this.tbxMultiSend2 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend4 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend3 = new System.Windows.Forms.TextBox();
            this.btnMultiSend3 = new System.Windows.Forms.Button();
            this.btnMultiSend4 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxMultiSend1 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend1 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend10 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend2 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend9 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend3 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend9 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend4 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend10 = new System.Windows.Forms.TextBox();
            this.btnMultiSend5 = new System.Windows.Forms.Button();
            this.btnMultiSend10 = new System.Windows.Forms.Button();
            this.btnMultiSend6 = new System.Windows.Forms.Button();
            this.btnMultiSend9 = new System.Windows.Forms.Button();
            this.tbxMultiSend6 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend8 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend8 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend7 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend7 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend6 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend7 = new System.Windows.Forms.Button();
            this.ckbMultiSend5 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend8 = new System.Windows.Forms.Button();
            this.tbxMultiSend5 = new System.Windows.Forms.TextBox();
            this.panelMultiSend2 = new System.Windows.Forms.Panel();
            this.btnMultiSend12 = new System.Windows.Forms.Button();
            this.btnMultiSend11 = new System.Windows.Forms.Button();
            this.tbxMultiSend12 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend14 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend13 = new System.Windows.Forms.TextBox();
            this.btnMultiSend13 = new System.Windows.Forms.Button();
            this.btnMultiSend14 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxMultiSend11 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend11 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend20 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend12 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend19 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend13 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend19 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend14 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend20 = new System.Windows.Forms.TextBox();
            this.btnMultiSend15 = new System.Windows.Forms.Button();
            this.btnMultiSend20 = new System.Windows.Forms.Button();
            this.btnMultiSend16 = new System.Windows.Forms.Button();
            this.btnMultiSend19 = new System.Windows.Forms.Button();
            this.tbxMultiSend16 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend18 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend18 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend17 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend17 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend16 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend17 = new System.Windows.Forms.Button();
            this.ckbMultiSend15 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend18 = new System.Windows.Forms.Button();
            this.tbxMultiSend15 = new System.Windows.Forms.TextBox();
            this.panelMultiSend3 = new System.Windows.Forms.Panel();
            this.btnMultiSend22 = new System.Windows.Forms.Button();
            this.btnMultiSend21 = new System.Windows.Forms.Button();
            this.tbxMultiSend22 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend24 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend23 = new System.Windows.Forms.TextBox();
            this.btnMultiSend23 = new System.Windows.Forms.Button();
            this.btnMultiSend24 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxMultiSend21 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend21 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend30 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend22 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend29 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend23 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend29 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend24 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend30 = new System.Windows.Forms.TextBox();
            this.btnMultiSend25 = new System.Windows.Forms.Button();
            this.btnMultiSend30 = new System.Windows.Forms.Button();
            this.btnMultiSend26 = new System.Windows.Forms.Button();
            this.btnMultiSend29 = new System.Windows.Forms.Button();
            this.tbxMultiSend26 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend28 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend28 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend27 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend27 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend26 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend27 = new System.Windows.Forms.Button();
            this.ckbMultiSend25 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend28 = new System.Windows.Forms.Button();
            this.tbxMultiSend25 = new System.Windows.Forms.TextBox();
            this.panelMultiSend4 = new System.Windows.Forms.Panel();
            this.btnMultiSend32 = new System.Windows.Forms.Button();
            this.btnMultiSend31 = new System.Windows.Forms.Button();
            this.tbxMultiSend32 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend34 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend33 = new System.Windows.Forms.TextBox();
            this.btnMultiSend33 = new System.Windows.Forms.Button();
            this.btnMultiSend34 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxMultiSend31 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend31 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend40 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend32 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend39 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend33 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend39 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend34 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend40 = new System.Windows.Forms.TextBox();
            this.btnMultiSend35 = new System.Windows.Forms.Button();
            this.btnMultiSend40 = new System.Windows.Forms.Button();
            this.btnMultiSend36 = new System.Windows.Forms.Button();
            this.btnMultiSend39 = new System.Windows.Forms.Button();
            this.tbxMultiSend36 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend38 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend38 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend37 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend37 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend36 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend37 = new System.Windows.Forms.Button();
            this.ckbMultiSend35 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend38 = new System.Windows.Forms.Button();
            this.tbxMultiSend35 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnRemarkMultiSend = new System.Windows.Forms.Button();
            this.btnMultiEndPage = new System.Windows.Forms.Button();
            this.btnMultiNextPage = new System.Windows.Forms.Button();
            this.btnMultiLastPage = new System.Windows.Forms.Button();
            this.btnMultiFirstPage = new System.Windows.Forms.Button();
            this.ckbMultiSendNewLine = new System.Windows.Forms.CheckBox();
            this.ckbRelateKeyBoard = new System.Windows.Forms.CheckBox();
            this.ckbMultiHexSend = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxMutilSendPeriod = new System.Windows.Forms.TextBox();
            this.ckbMultiAutoSend = new System.Windows.Forms.CheckBox();
            this.transportProtocolTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTransportProtocolMasterAddr = new System.Windows.Forms.Label();
            this.ckbTransportProtocolAutoNewLine = new System.Windows.Forms.CheckBox();
            this.ckbTransportProtocolDispOrigialData = new System.Windows.Forms.CheckBox();
            this.lblTransportProtocolRevChecksum = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblTransportProtocolResult = new System.Windows.Forms.Label();
            this.lblTransportProtocolRevDataLength = new System.Windows.Forms.Label();
            this.lblTransportProtocolRevSequence = new System.Windows.Forms.Label();
            this.lblTransportProtocolRevFunctionType = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxTransportProtocolRetryCount = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.ckbTransportProtocolAutoSend = new System.Windows.Forms.CheckBox();
            this.lblTransportProtocolSendDataLength = new System.Windows.Forms.Label();
            this.lblTransportProtocolSendSequence = new System.Windows.Forms.Label();
            this.tbxTransportProtocolMaxDataLength = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.tbxTransportProtocolSendPeriod = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.btnEnbaleTransportProtocol = new System.Windows.Forms.Button();
            this.lblTransportProtocolProgSendFile = new System.Windows.Forms.Label();
            this.btnTransportProtocolStopFile = new System.Windows.Forms.Button();
            this.btnTransportProtocolSendFile = new System.Windows.Forms.Button();
            this.btnTransportProtocolOpenFile = new System.Windows.Forms.Button();
            this.progBarTransportProtocolSendFile = new System.Windows.Forms.ProgressBar();
            this.tbxTransportProtocolSendData = new System.Windows.Forms.TextBox();
            this.btnTransportProtocolSend = new System.Windows.Forms.Button();
            this.tbxTransportProtocolFileName = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.tbxTransportProtocolSlaveDeviceAddr = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tbxTransportProtocolSendFunctionType = new System.Windows.Forms.TextBox();
            this.cbxTransportProtocolChecksum = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLblSendCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLblRevCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLblCom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStripCom = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCalculator = new System.Windows.Forms.ToolStripMenuItem();
            this.wewqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxComPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.saveFileDlgRev = new System.Windows.Forms.SaveFileDialog();
            this.revCntTimer = new System.Windows.Forms.Timer(this.components);
            this.sendCntTimer = new System.Windows.Forms.Timer(this.components);
            this.tbxRecvData = new System.Windows.Forms.TextBox();
            this.openFileDlgSend = new System.Windows.Forms.OpenFileDialog();
            this.showCurTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckbShowTime = new System.Windows.Forms.CheckBox();
            this.ckbChangWinColor = new System.Windows.Forms.CheckBox();
            this.toolTipAll = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDlgTransportProtocol = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.singleTab.SuspendLayout();
            this.multiTab.SuspendLayout();
            this.panelMultiSend1.SuspendLayout();
            this.panelMultiSend2.SuspendLayout();
            this.panelMultiSend3.SuspendLayout();
            this.panelMultiSend4.SuspendLayout();
            this.transportProtocolTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStripCom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveWindow
            // 
            this.btnSaveWindow.Location = new System.Drawing.Point(6, 242);
            this.btnSaveWindow.Name = "btnSaveWindow";
            this.btnSaveWindow.Size = new System.Drawing.Size(79, 41);
            this.btnSaveWindow.TabIndex = 14;
            this.btnSaveWindow.Text = "保存窗口";
            this.btnSaveWindow.UseVisualStyleBackColor = true;
            this.btnSaveWindow.Click += new System.EventHandler(this.btnSaveWindow_Click);
            // 
            // btnClearWindow
            // 
            this.btnClearWindow.Location = new System.Drawing.Point(92, 242);
            this.btnClearWindow.Name = "btnClearWindow";
            this.btnClearWindow.Size = new System.Drawing.Size(79, 41);
            this.btnClearWindow.TabIndex = 15;
            this.btnClearWindow.Text = "清除接收";
            this.btnClearWindow.UseVisualStyleBackColor = true;
            this.btnClearWindow.Click += new System.EventHandler(this.btnClearWindow_Click);
            // 
            // ckbRevHex
            // 
            this.ckbRevHex.AutoSize = true;
            this.ckbRevHex.Location = new System.Drawing.Point(9, 289);
            this.ckbRevHex.Name = "ckbRevHex";
            this.ckbRevHex.Size = new System.Drawing.Size(89, 21);
            this.ckbRevHex.TabIndex = 17;
            this.ckbRevHex.Text = "16进制显示";
            this.ckbRevHex.UseVisualStyleBackColor = true;
            this.ckbRevHex.CheckedChanged += new System.EventHandler(this.ckbRevHex_CheckedChanged);
            // 
            // ckbRTS
            // 
            this.ckbRTS.AutoSize = true;
            this.ckbRTS.Location = new System.Drawing.Point(9, 311);
            this.ckbRTS.Name = "ckbRTS";
            this.ckbRTS.Size = new System.Drawing.Size(49, 21);
            this.ckbRTS.TabIndex = 34;
            this.ckbRTS.Text = "RTS";
            this.ckbRTS.UseVisualStyleBackColor = true;
            this.ckbRTS.CheckedChanged += new System.EventHandler(this.ckbRTS_CheckedChanged);
            // 
            // ckbDTR
            // 
            this.ckbDTR.AutoSize = true;
            this.ckbDTR.Location = new System.Drawing.Point(96, 311);
            this.ckbDTR.Name = "ckbDTR";
            this.ckbDTR.Size = new System.Drawing.Size(51, 21);
            this.ckbDTR.TabIndex = 35;
            this.ckbDTR.Text = "DTR";
            this.ckbDTR.UseVisualStyleBackColor = true;
            this.ckbDTR.CheckedChanged += new System.EventHandler(this.ckbDTR_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.singleTab);
            this.tabControl1.Controls.Add(this.multiTab);
            this.tabControl1.Controls.Add(this.transportProtocolTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 567);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(998, 172);
            this.tabControl1.TabIndex = 36;
            // 
            // singleTab
            // 
            this.singleTab.BackColor = System.Drawing.Color.Transparent;
            this.singleTab.Controls.Add(this.lblProgSendFile);
            this.singleTab.Controls.Add(this.progBarSendFile);
            this.singleTab.Controls.Add(this.label16);
            this.singleTab.Controls.Add(this.label8);
            this.singleTab.Controls.Add(this.btnStopSendFile);
            this.singleTab.Controls.Add(this.btnClearSend);
            this.singleTab.Controls.Add(this.btnSendFile);
            this.singleTab.Controls.Add(this.tbxSendFile);
            this.singleTab.Controls.Add(this.btnOpenSendFile);
            this.singleTab.Controls.Add(this.btnSend);
            this.singleTab.Controls.Add(this.tbxSendData);
            this.singleTab.Controls.Add(this.ckbSendNewLine);
            this.singleTab.Controls.Add(this.ckbHexSend);
            this.singleTab.Controls.Add(this.label7);
            this.singleTab.Controls.Add(this.label6);
            this.singleTab.Controls.Add(this.tbxSendPeriod);
            this.singleTab.Controls.Add(this.ckbTimeSend);
            this.singleTab.Location = new System.Drawing.Point(4, 26);
            this.singleTab.Name = "singleTab";
            this.singleTab.Padding = new System.Windows.Forms.Padding(3);
            this.singleTab.Size = new System.Drawing.Size(990, 142);
            this.singleTab.TabIndex = 0;
            this.singleTab.Text = "单条发送";
            // 
            // lblProgSendFile
            // 
            this.lblProgSendFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProgSendFile.AutoSize = true;
            this.lblProgSendFile.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblProgSendFile.Location = new System.Drawing.Point(750, 116);
            this.lblProgSendFile.Name = "lblProgSendFile";
            this.lblProgSendFile.Size = new System.Drawing.Size(33, 21);
            this.lblProgSendFile.TabIndex = 49;
            this.lblProgSendFile.Text = "0%";
            // 
            // progBarSendFile
            // 
            this.progBarSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progBarSendFile.Location = new System.Drawing.Point(199, 115);
            this.progBarSendFile.Name = "progBarSendFile";
            this.progBarSendFile.Size = new System.Drawing.Size(545, 25);
            this.progBarSendFile.Step = 1;
            this.progBarSendFile.TabIndex = 47;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(160, 89);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 17);
            this.label16.TabIndex = 46;
            this.label16.Text = "ms";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 45;
            this.label8.Text = "周期:";
            // 
            // btnStopSendFile
            // 
            this.btnStopSendFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnStopSendFile.Location = new System.Drawing.Point(912, 83);
            this.btnStopSendFile.Name = "btnStopSendFile";
            this.btnStopSendFile.Size = new System.Drawing.Size(75, 30);
            this.btnStopSendFile.TabIndex = 44;
            this.btnStopSendFile.Text = "停止发送";
            this.btnStopSendFile.UseVisualStyleBackColor = true;
            this.btnStopSendFile.Click += new System.EventHandler(this.btnStopSendFile_Click);
            // 
            // btnClearSend
            // 
            this.btnClearSend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClearSend.Location = new System.Drawing.Point(902, 45);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(85, 30);
            this.btnClearSend.TabIndex = 43;
            this.btnClearSend.Text = "清除发送";
            this.btnClearSend.UseVisualStyleBackColor = true;
            this.btnClearSend.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSendFile.Location = new System.Drawing.Point(831, 83);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 30);
            this.btnSendFile.TabIndex = 42;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // tbxSendFile
            // 
            this.tbxSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSendFile.BackColor = System.Drawing.SystemColors.Control;
            this.tbxSendFile.Location = new System.Drawing.Point(199, 86);
            this.tbxSendFile.Name = "tbxSendFile";
            this.tbxSendFile.ReadOnly = true;
            this.tbxSendFile.Size = new System.Drawing.Size(545, 23);
            this.tbxSendFile.TabIndex = 41;
            // 
            // btnOpenSendFile
            // 
            this.btnOpenSendFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOpenSendFile.Location = new System.Drawing.Point(750, 83);
            this.btnOpenSendFile.Name = "btnOpenSendFile";
            this.btnOpenSendFile.Size = new System.Drawing.Size(75, 30);
            this.btnOpenSendFile.TabIndex = 40;
            this.btnOpenSendFile.Text = "打开文件";
            this.btnOpenSendFile.UseVisualStyleBackColor = true;
            this.btnOpenSendFile.Click += new System.EventHandler(this.btnOpenSendFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSend.Location = new System.Drawing.Point(902, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(85, 30);
            this.btnSend.TabIndex = 39;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxSendData
            // 
            this.tbxSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSendData.Location = new System.Drawing.Point(4, 1);
            this.tbxSendData.Multiline = true;
            this.tbxSendData.Name = "tbxSendData";
            this.tbxSendData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSendData.Size = new System.Drawing.Size(892, 77);
            this.tbxSendData.TabIndex = 38;
            // 
            // ckbSendNewLine
            // 
            this.ckbSendNewLine.AutoSize = true;
            this.ckbSendNewLine.Checked = true;
            this.ckbSendNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSendNewLine.Location = new System.Drawing.Point(99, 116);
            this.ckbSendNewLine.Name = "ckbSendNewLine";
            this.ckbSendNewLine.Size = new System.Drawing.Size(75, 21);
            this.ckbSendNewLine.TabIndex = 37;
            this.ckbSendNewLine.Text = "发送新行";
            this.ckbSendNewLine.UseVisualStyleBackColor = true;
            this.ckbSendNewLine.CheckedChanged += new System.EventHandler(this.ckbSendNewLine_CheckedChanged);
            // 
            // ckbHexSend
            // 
            this.ckbHexSend.AutoSize = true;
            this.ckbHexSend.Location = new System.Drawing.Point(4, 116);
            this.ckbHexSend.Name = "ckbHexSend";
            this.ckbHexSend.Size = new System.Drawing.Size(89, 21);
            this.ckbHexSend.TabIndex = 36;
            this.ckbHexSend.Text = "16进制发送";
            this.ckbHexSend.UseVisualStyleBackColor = true;
            this.ckbHexSend.CheckedChanged += new System.EventHandler(this.ckbHexSend_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label7.Location = new System.Drawing.Point(97, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 14);
            this.label7.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label6.Location = new System.Drawing.Point(177, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 14);
            this.label6.TabIndex = 34;
            // 
            // tbxSendPeriod
            // 
            this.tbxSendPeriod.Location = new System.Drawing.Point(116, 86);
            this.tbxSendPeriod.Name = "tbxSendPeriod";
            this.tbxSendPeriod.Size = new System.Drawing.Size(38, 23);
            this.tbxSendPeriod.TabIndex = 33;
            this.tbxSendPeriod.Text = "1000";
            // 
            // ckbTimeSend
            // 
            this.ckbTimeSend.AutoSize = true;
            this.ckbTimeSend.Location = new System.Drawing.Point(4, 88);
            this.ckbTimeSend.Name = "ckbTimeSend";
            this.ckbTimeSend.Size = new System.Drawing.Size(75, 21);
            this.ckbTimeSend.TabIndex = 32;
            this.ckbTimeSend.Text = "定时发送";
            this.ckbTimeSend.UseVisualStyleBackColor = true;
            this.ckbTimeSend.CheckedChanged += new System.EventHandler(this.ckbTimeSend_CheckedChanged);
            // 
            // multiTab
            // 
            this.multiTab.BackColor = System.Drawing.Color.Transparent;
            this.multiTab.Controls.Add(this.panelMultiSend1);
            this.multiTab.Controls.Add(this.panelMultiSend2);
            this.multiTab.Controls.Add(this.panelMultiSend3);
            this.multiTab.Controls.Add(this.panelMultiSend4);
            this.multiTab.Controls.Add(this.label21);
            this.multiTab.Controls.Add(this.btnRemarkMultiSend);
            this.multiTab.Controls.Add(this.btnMultiEndPage);
            this.multiTab.Controls.Add(this.btnMultiNextPage);
            this.multiTab.Controls.Add(this.btnMultiLastPage);
            this.multiTab.Controls.Add(this.btnMultiFirstPage);
            this.multiTab.Controls.Add(this.ckbMultiSendNewLine);
            this.multiTab.Controls.Add(this.ckbRelateKeyBoard);
            this.multiTab.Controls.Add(this.ckbMultiHexSend);
            this.multiTab.Controls.Add(this.label14);
            this.multiTab.Controls.Add(this.tbxMutilSendPeriod);
            this.multiTab.Controls.Add(this.ckbMultiAutoSend);
            this.multiTab.Location = new System.Drawing.Point(4, 26);
            this.multiTab.Name = "multiTab";
            this.multiTab.Padding = new System.Windows.Forms.Padding(3);
            this.multiTab.Size = new System.Drawing.Size(990, 142);
            this.multiTab.TabIndex = 1;
            this.multiTab.Text = "多条发送";
            // 
            // panelMultiSend1
            // 
            this.panelMultiSend1.Controls.Add(this.btnMultiSend2);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend1);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend2);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend4);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend3);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend3);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend4);
            this.panelMultiSend1.Controls.Add(this.label13);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend1);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend1);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend10);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend2);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend9);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend3);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend9);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend4);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend10);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend5);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend10);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend6);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend9);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend6);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend8);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend8);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend7);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend7);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend6);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend7);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend5);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend8);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend5);
            this.panelMultiSend1.Location = new System.Drawing.Point(0, 0);
            this.panelMultiSend1.Name = "panelMultiSend1";
            this.panelMultiSend1.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend1.TabIndex = 70;
            // 
            // btnMultiSend2
            // 
            this.btnMultiSend2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend2.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend2.Name = "btnMultiSend2";
            this.btnMultiSend2.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend2.TabIndex = 6;
            this.btnMultiSend2.Text = "1";
            this.btnMultiSend2.UseVisualStyleBackColor = true;
            this.btnMultiSend2.Click += new System.EventHandler(this.btnMultiSend2_Click);
            // 
            // btnMultiSend1
            // 
            this.btnMultiSend1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend1.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend1.Name = "btnMultiSend1";
            this.btnMultiSend1.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend1.TabIndex = 2;
            this.btnMultiSend1.Text = "0";
            this.btnMultiSend1.UseVisualStyleBackColor = true;
            this.btnMultiSend1.Click += new System.EventHandler(this.btnMultiSend1_Click);
            // 
            // tbxMultiSend2
            // 
            this.tbxMultiSend2.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend2.Name = "tbxMultiSend2";
            this.tbxMultiSend2.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend2.TabIndex = 7;
            this.tbxMultiSend2.Text = "1";
            // 
            // tbxMultiSend4
            // 
            this.tbxMultiSend4.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend4.Name = "tbxMultiSend4";
            this.tbxMultiSend4.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend4.TabIndex = 8;
            this.tbxMultiSend4.Text = "3";
            // 
            // tbxMultiSend3
            // 
            this.tbxMultiSend3.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend3.Name = "tbxMultiSend3";
            this.tbxMultiSend3.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend3.TabIndex = 9;
            this.tbxMultiSend3.Text = "2";
            // 
            // btnMultiSend3
            // 
            this.btnMultiSend3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend3.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend3.Name = "btnMultiSend3";
            this.btnMultiSend3.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend3.TabIndex = 10;
            this.btnMultiSend3.Text = "2";
            this.btnMultiSend3.UseVisualStyleBackColor = true;
            this.btnMultiSend3.Click += new System.EventHandler(this.btnMultiSend3_Click);
            // 
            // btnMultiSend4
            // 
            this.btnMultiSend4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend4.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend4.Name = "btnMultiSend4";
            this.btnMultiSend4.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend4.TabIndex = 11;
            this.btnMultiSend4.Text = "3";
            this.btnMultiSend4.UseVisualStyleBackColor = true;
            this.btnMultiSend4.Click += new System.EventHandler(this.btnMultiSend4_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(588, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 17);
            this.label13.TabIndex = 27;
            this.label13.Text = "间隔：";
            // 
            // tbxMultiSend1
            // 
            this.tbxMultiSend1.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend1.Name = "tbxMultiSend1";
            this.tbxMultiSend1.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend1.TabIndex = 30;
            this.tbxMultiSend1.Text = "0";
            this.tbxMultiSend1.WordWrap = false;
            // 
            // ckbMultiSend1
            // 
            this.ckbMultiSend1.AutoSize = true;
            this.ckbMultiSend1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend1.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend1.Name = "ckbMultiSend1";
            this.ckbMultiSend1.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend1.TabIndex = 31;
            this.ckbMultiSend1.UseVisualStyleBackColor = true;
            this.ckbMultiSend1.CheckedChanged += new System.EventHandler(this.ckbMultiSend1_CheckedChanged);
            // 
            // ckbMultiSend10
            // 
            this.ckbMultiSend10.AutoSize = true;
            this.ckbMultiSend10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend10.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend10.Name = "ckbMultiSend10";
            this.ckbMultiSend10.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend10.TabIndex = 56;
            this.ckbMultiSend10.UseVisualStyleBackColor = true;
            this.ckbMultiSend10.CheckedChanged += new System.EventHandler(this.ckbMultiSend10_CheckedChanged);
            // 
            // ckbMultiSend2
            // 
            this.ckbMultiSend2.AutoSize = true;
            this.ckbMultiSend2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend2.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend2.Name = "ckbMultiSend2";
            this.ckbMultiSend2.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend2.TabIndex = 32;
            this.ckbMultiSend2.UseVisualStyleBackColor = true;
            this.ckbMultiSend2.CheckedChanged += new System.EventHandler(this.ckbMultiSend2_CheckedChanged);
            // 
            // ckbMultiSend9
            // 
            this.ckbMultiSend9.AutoSize = true;
            this.ckbMultiSend9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend9.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend9.Name = "ckbMultiSend9";
            this.ckbMultiSend9.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend9.TabIndex = 55;
            this.ckbMultiSend9.UseVisualStyleBackColor = true;
            this.ckbMultiSend9.CheckedChanged += new System.EventHandler(this.ckbMultiSend9_CheckedChanged);
            // 
            // ckbMultiSend3
            // 
            this.ckbMultiSend3.AutoSize = true;
            this.ckbMultiSend3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend3.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend3.Name = "ckbMultiSend3";
            this.ckbMultiSend3.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend3.TabIndex = 33;
            this.ckbMultiSend3.UseVisualStyleBackColor = true;
            this.ckbMultiSend3.CheckedChanged += new System.EventHandler(this.ckbMultiSend3_CheckedChanged);
            // 
            // tbxMultiSend9
            // 
            this.tbxMultiSend9.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend9.Name = "tbxMultiSend9";
            this.tbxMultiSend9.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend9.TabIndex = 54;
            this.tbxMultiSend9.Text = "8";
            // 
            // ckbMultiSend4
            // 
            this.ckbMultiSend4.AutoSize = true;
            this.ckbMultiSend4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend4.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend4.Name = "ckbMultiSend4";
            this.ckbMultiSend4.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend4.TabIndex = 34;
            this.ckbMultiSend4.UseVisualStyleBackColor = true;
            this.ckbMultiSend4.CheckedChanged += new System.EventHandler(this.ckbMultiSend4_CheckedChanged);
            // 
            // tbxMultiSend10
            // 
            this.tbxMultiSend10.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend10.Name = "tbxMultiSend10";
            this.tbxMultiSend10.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend10.TabIndex = 49;
            this.tbxMultiSend10.Text = "9";
            // 
            // btnMultiSend5
            // 
            this.btnMultiSend5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend5.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend5.Name = "btnMultiSend5";
            this.btnMultiSend5.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend5.TabIndex = 35;
            this.btnMultiSend5.Text = "4";
            this.btnMultiSend5.UseVisualStyleBackColor = true;
            this.btnMultiSend5.Click += new System.EventHandler(this.btnMultiSend5_Click);
            // 
            // btnMultiSend10
            // 
            this.btnMultiSend10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend10.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend10.Name = "btnMultiSend10";
            this.btnMultiSend10.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend10.TabIndex = 48;
            this.btnMultiSend10.Text = "9";
            this.btnMultiSend10.UseVisualStyleBackColor = true;
            this.btnMultiSend10.Click += new System.EventHandler(this.btnMultiSend10_Click);
            // 
            // btnMultiSend6
            // 
            this.btnMultiSend6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend6.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend6.Name = "btnMultiSend6";
            this.btnMultiSend6.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend6.TabIndex = 36;
            this.btnMultiSend6.Text = "5";
            this.btnMultiSend6.UseVisualStyleBackColor = true;
            this.btnMultiSend6.Click += new System.EventHandler(this.btnMultiSend6_Click);
            // 
            // btnMultiSend9
            // 
            this.btnMultiSend9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend9.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend9.Name = "btnMultiSend9";
            this.btnMultiSend9.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend9.TabIndex = 47;
            this.btnMultiSend9.Text = "8";
            this.btnMultiSend9.UseVisualStyleBackColor = true;
            this.btnMultiSend9.Click += new System.EventHandler(this.btnMultiSend9_Click);
            // 
            // tbxMultiSend6
            // 
            this.tbxMultiSend6.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend6.Name = "tbxMultiSend6";
            this.tbxMultiSend6.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend6.TabIndex = 37;
            this.tbxMultiSend6.Text = "5";
            // 
            // ckbMultiSend8
            // 
            this.ckbMultiSend8.AutoSize = true;
            this.ckbMultiSend8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend8.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend8.Name = "ckbMultiSend8";
            this.ckbMultiSend8.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend8.TabIndex = 46;
            this.ckbMultiSend8.UseVisualStyleBackColor = true;
            this.ckbMultiSend8.CheckedChanged += new System.EventHandler(this.ckbMultiSend8_CheckedChanged);
            // 
            // tbxMultiSend8
            // 
            this.tbxMultiSend8.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend8.Name = "tbxMultiSend8";
            this.tbxMultiSend8.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend8.TabIndex = 38;
            this.tbxMultiSend8.Text = "7";
            // 
            // ckbMultiSend7
            // 
            this.ckbMultiSend7.AutoSize = true;
            this.ckbMultiSend7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend7.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend7.Name = "ckbMultiSend7";
            this.ckbMultiSend7.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend7.TabIndex = 45;
            this.ckbMultiSend7.UseVisualStyleBackColor = true;
            this.ckbMultiSend7.CheckedChanged += new System.EventHandler(this.ckbMultiSend7_CheckedChanged);
            // 
            // tbxMultiSend7
            // 
            this.tbxMultiSend7.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend7.Name = "tbxMultiSend7";
            this.tbxMultiSend7.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend7.TabIndex = 39;
            this.tbxMultiSend7.Text = "6";
            // 
            // ckbMultiSend6
            // 
            this.ckbMultiSend6.AutoSize = true;
            this.ckbMultiSend6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend6.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend6.Name = "ckbMultiSend6";
            this.ckbMultiSend6.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend6.TabIndex = 44;
            this.ckbMultiSend6.UseVisualStyleBackColor = true;
            this.ckbMultiSend6.CheckedChanged += new System.EventHandler(this.ckbMultiSend6_CheckedChanged);
            // 
            // btnMultiSend7
            // 
            this.btnMultiSend7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend7.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend7.Name = "btnMultiSend7";
            this.btnMultiSend7.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend7.TabIndex = 40;
            this.btnMultiSend7.Text = "6";
            this.btnMultiSend7.UseVisualStyleBackColor = true;
            this.btnMultiSend7.Click += new System.EventHandler(this.btnMultiSend7_Click);
            // 
            // ckbMultiSend5
            // 
            this.ckbMultiSend5.AutoSize = true;
            this.ckbMultiSend5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend5.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend5.Name = "ckbMultiSend5";
            this.ckbMultiSend5.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend5.TabIndex = 43;
            this.ckbMultiSend5.UseVisualStyleBackColor = true;
            this.ckbMultiSend5.CheckedChanged += new System.EventHandler(this.ckbMultiSend5_CheckedChanged);
            // 
            // btnMultiSend8
            // 
            this.btnMultiSend8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend8.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend8.Name = "btnMultiSend8";
            this.btnMultiSend8.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend8.TabIndex = 41;
            this.btnMultiSend8.Text = "7";
            this.btnMultiSend8.UseVisualStyleBackColor = true;
            this.btnMultiSend8.Click += new System.EventHandler(this.btnMultiSend8_Click);
            // 
            // tbxMultiSend5
            // 
            this.tbxMultiSend5.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend5.Name = "tbxMultiSend5";
            this.tbxMultiSend5.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend5.TabIndex = 42;
            this.tbxMultiSend5.Text = "4";
            // 
            // panelMultiSend2
            // 
            this.panelMultiSend2.Controls.Add(this.btnMultiSend12);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend11);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend12);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend14);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend13);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend13);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend14);
            this.panelMultiSend2.Controls.Add(this.label18);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend11);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend11);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend20);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend12);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend19);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend13);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend19);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend14);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend20);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend15);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend20);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend16);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend19);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend16);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend18);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend18);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend17);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend17);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend16);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend17);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend15);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend18);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend15);
            this.panelMultiSend2.Location = new System.Drawing.Point(0, 0);
            this.panelMultiSend2.Name = "panelMultiSend2";
            this.panelMultiSend2.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend2.TabIndex = 71;
            this.panelMultiSend2.Visible = false;
            // 
            // btnMultiSend12
            // 
            this.btnMultiSend12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend12.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend12.Name = "btnMultiSend12";
            this.btnMultiSend12.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend12.TabIndex = 6;
            this.btnMultiSend12.Text = "11";
            this.btnMultiSend12.UseVisualStyleBackColor = true;
            this.btnMultiSend12.Click += new System.EventHandler(this.btnMultiSend12_Click);
            // 
            // btnMultiSend11
            // 
            this.btnMultiSend11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend11.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend11.Name = "btnMultiSend11";
            this.btnMultiSend11.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend11.TabIndex = 2;
            this.btnMultiSend11.Text = "10";
            this.btnMultiSend11.UseVisualStyleBackColor = true;
            this.btnMultiSend11.Click += new System.EventHandler(this.btnMultiSend11_Click);
            // 
            // tbxMultiSend12
            // 
            this.tbxMultiSend12.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend12.Name = "tbxMultiSend12";
            this.tbxMultiSend12.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend12.TabIndex = 7;
            this.tbxMultiSend12.Text = "1";
            // 
            // tbxMultiSend14
            // 
            this.tbxMultiSend14.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend14.Name = "tbxMultiSend14";
            this.tbxMultiSend14.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend14.TabIndex = 8;
            this.tbxMultiSend14.Text = "3";
            // 
            // tbxMultiSend13
            // 
            this.tbxMultiSend13.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend13.Name = "tbxMultiSend13";
            this.tbxMultiSend13.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend13.TabIndex = 9;
            this.tbxMultiSend13.Text = "2";
            // 
            // btnMultiSend13
            // 
            this.btnMultiSend13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend13.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend13.Name = "btnMultiSend13";
            this.btnMultiSend13.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend13.TabIndex = 10;
            this.btnMultiSend13.Text = "12";
            this.btnMultiSend13.UseVisualStyleBackColor = true;
            this.btnMultiSend13.Click += new System.EventHandler(this.btnMultiSend13_Click);
            // 
            // btnMultiSend14
            // 
            this.btnMultiSend14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend14.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend14.Name = "btnMultiSend14";
            this.btnMultiSend14.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend14.TabIndex = 11;
            this.btnMultiSend14.Text = "13";
            this.btnMultiSend14.UseVisualStyleBackColor = true;
            this.btnMultiSend14.Click += new System.EventHandler(this.btnMultiSend14_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(588, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 17);
            this.label18.TabIndex = 27;
            this.label18.Text = "间隔：";
            // 
            // tbxMultiSend11
            // 
            this.tbxMultiSend11.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend11.Name = "tbxMultiSend11";
            this.tbxMultiSend11.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend11.TabIndex = 30;
            this.tbxMultiSend11.Text = "0";
            this.tbxMultiSend11.WordWrap = false;
            // 
            // ckbMultiSend11
            // 
            this.ckbMultiSend11.AutoSize = true;
            this.ckbMultiSend11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend11.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend11.Name = "ckbMultiSend11";
            this.ckbMultiSend11.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend11.TabIndex = 31;
            this.ckbMultiSend11.UseVisualStyleBackColor = true;
            this.ckbMultiSend11.CheckedChanged += new System.EventHandler(this.ckbMultiSend11_CheckedChanged);
            // 
            // ckbMultiSend20
            // 
            this.ckbMultiSend20.AutoSize = true;
            this.ckbMultiSend20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend20.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend20.Name = "ckbMultiSend20";
            this.ckbMultiSend20.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend20.TabIndex = 56;
            this.ckbMultiSend20.UseVisualStyleBackColor = true;
            this.ckbMultiSend20.CheckedChanged += new System.EventHandler(this.ckbMultiSend20_CheckedChanged);
            // 
            // ckbMultiSend12
            // 
            this.ckbMultiSend12.AutoSize = true;
            this.ckbMultiSend12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend12.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend12.Name = "ckbMultiSend12";
            this.ckbMultiSend12.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend12.TabIndex = 32;
            this.ckbMultiSend12.UseVisualStyleBackColor = true;
            this.ckbMultiSend12.CheckedChanged += new System.EventHandler(this.ckbMultiSend12_CheckedChanged);
            // 
            // ckbMultiSend19
            // 
            this.ckbMultiSend19.AutoSize = true;
            this.ckbMultiSend19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend19.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend19.Name = "ckbMultiSend19";
            this.ckbMultiSend19.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend19.TabIndex = 55;
            this.ckbMultiSend19.UseVisualStyleBackColor = true;
            this.ckbMultiSend19.CheckedChanged += new System.EventHandler(this.ckbMultiSend19_CheckedChanged);
            // 
            // ckbMultiSend13
            // 
            this.ckbMultiSend13.AutoSize = true;
            this.ckbMultiSend13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend13.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend13.Name = "ckbMultiSend13";
            this.ckbMultiSend13.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend13.TabIndex = 33;
            this.ckbMultiSend13.UseVisualStyleBackColor = true;
            this.ckbMultiSend13.CheckedChanged += new System.EventHandler(this.ckbMultiSend13_CheckedChanged);
            // 
            // tbxMultiSend19
            // 
            this.tbxMultiSend19.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend19.Name = "tbxMultiSend19";
            this.tbxMultiSend19.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend19.TabIndex = 54;
            this.tbxMultiSend19.Text = "8";
            // 
            // ckbMultiSend14
            // 
            this.ckbMultiSend14.AutoSize = true;
            this.ckbMultiSend14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend14.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend14.Name = "ckbMultiSend14";
            this.ckbMultiSend14.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend14.TabIndex = 34;
            this.ckbMultiSend14.UseVisualStyleBackColor = true;
            this.ckbMultiSend14.CheckedChanged += new System.EventHandler(this.ckbMultiSend14_CheckedChanged);
            // 
            // tbxMultiSend20
            // 
            this.tbxMultiSend20.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend20.Name = "tbxMultiSend20";
            this.tbxMultiSend20.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend20.TabIndex = 49;
            this.tbxMultiSend20.Text = "9";
            // 
            // btnMultiSend15
            // 
            this.btnMultiSend15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend15.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend15.Name = "btnMultiSend15";
            this.btnMultiSend15.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend15.TabIndex = 35;
            this.btnMultiSend15.Text = "14";
            this.btnMultiSend15.UseVisualStyleBackColor = true;
            this.btnMultiSend15.Click += new System.EventHandler(this.btnMultiSend15_Click);
            // 
            // btnMultiSend20
            // 
            this.btnMultiSend20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend20.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend20.Name = "btnMultiSend20";
            this.btnMultiSend20.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend20.TabIndex = 48;
            this.btnMultiSend20.Text = "19";
            this.btnMultiSend20.UseVisualStyleBackColor = true;
            this.btnMultiSend20.Click += new System.EventHandler(this.btnMultiSend20_Click);
            // 
            // btnMultiSend16
            // 
            this.btnMultiSend16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend16.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend16.Name = "btnMultiSend16";
            this.btnMultiSend16.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend16.TabIndex = 36;
            this.btnMultiSend16.Text = "15";
            this.btnMultiSend16.UseVisualStyleBackColor = true;
            this.btnMultiSend16.Click += new System.EventHandler(this.btnMultiSend16_Click);
            // 
            // btnMultiSend19
            // 
            this.btnMultiSend19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend19.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend19.Name = "btnMultiSend19";
            this.btnMultiSend19.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend19.TabIndex = 47;
            this.btnMultiSend19.Text = "18";
            this.btnMultiSend19.UseVisualStyleBackColor = true;
            this.btnMultiSend19.Click += new System.EventHandler(this.btnMultiSend19_Click);
            // 
            // tbxMultiSend16
            // 
            this.tbxMultiSend16.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend16.Name = "tbxMultiSend16";
            this.tbxMultiSend16.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend16.TabIndex = 37;
            this.tbxMultiSend16.Text = "5";
            // 
            // ckbMultiSend18
            // 
            this.ckbMultiSend18.AutoSize = true;
            this.ckbMultiSend18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend18.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend18.Name = "ckbMultiSend18";
            this.ckbMultiSend18.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend18.TabIndex = 46;
            this.ckbMultiSend18.UseVisualStyleBackColor = true;
            this.ckbMultiSend18.CheckedChanged += new System.EventHandler(this.ckbMultiSend18_CheckedChanged);
            // 
            // tbxMultiSend18
            // 
            this.tbxMultiSend18.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend18.Name = "tbxMultiSend18";
            this.tbxMultiSend18.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend18.TabIndex = 38;
            this.tbxMultiSend18.Text = "7";
            // 
            // ckbMultiSend17
            // 
            this.ckbMultiSend17.AutoSize = true;
            this.ckbMultiSend17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend17.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend17.Name = "ckbMultiSend17";
            this.ckbMultiSend17.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend17.TabIndex = 45;
            this.ckbMultiSend17.UseVisualStyleBackColor = true;
            this.ckbMultiSend17.CheckedChanged += new System.EventHandler(this.ckbMultiSend17_CheckedChanged);
            // 
            // tbxMultiSend17
            // 
            this.tbxMultiSend17.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend17.Name = "tbxMultiSend17";
            this.tbxMultiSend17.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend17.TabIndex = 39;
            this.tbxMultiSend17.Text = "6";
            // 
            // ckbMultiSend16
            // 
            this.ckbMultiSend16.AutoSize = true;
            this.ckbMultiSend16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend16.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend16.Name = "ckbMultiSend16";
            this.ckbMultiSend16.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend16.TabIndex = 44;
            this.ckbMultiSend16.UseVisualStyleBackColor = true;
            this.ckbMultiSend16.CheckedChanged += new System.EventHandler(this.ckbMultiSend16_CheckedChanged);
            // 
            // btnMultiSend17
            // 
            this.btnMultiSend17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend17.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend17.Name = "btnMultiSend17";
            this.btnMultiSend17.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend17.TabIndex = 40;
            this.btnMultiSend17.Text = "16";
            this.btnMultiSend17.UseVisualStyleBackColor = true;
            this.btnMultiSend17.Click += new System.EventHandler(this.btnMultiSend17_Click);
            // 
            // ckbMultiSend15
            // 
            this.ckbMultiSend15.AutoSize = true;
            this.ckbMultiSend15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend15.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend15.Name = "ckbMultiSend15";
            this.ckbMultiSend15.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend15.TabIndex = 43;
            this.ckbMultiSend15.UseVisualStyleBackColor = true;
            this.ckbMultiSend15.CheckedChanged += new System.EventHandler(this.ckbMultiSend15_CheckedChanged);
            // 
            // btnMultiSend18
            // 
            this.btnMultiSend18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend18.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend18.Name = "btnMultiSend18";
            this.btnMultiSend18.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend18.TabIndex = 41;
            this.btnMultiSend18.Text = "17";
            this.btnMultiSend18.UseVisualStyleBackColor = true;
            this.btnMultiSend18.Click += new System.EventHandler(this.btnMultiSend18_Click);
            // 
            // tbxMultiSend15
            // 
            this.tbxMultiSend15.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend15.Name = "tbxMultiSend15";
            this.tbxMultiSend15.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend15.TabIndex = 42;
            this.tbxMultiSend15.Text = "4";
            // 
            // panelMultiSend3
            // 
            this.panelMultiSend3.Controls.Add(this.btnMultiSend22);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend21);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend22);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend24);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend23);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend23);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend24);
            this.panelMultiSend3.Controls.Add(this.label19);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend21);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend21);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend30);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend22);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend29);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend23);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend29);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend24);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend30);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend25);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend30);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend26);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend29);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend26);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend28);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend28);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend27);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend27);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend26);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend27);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend25);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend28);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend25);
            this.panelMultiSend3.Location = new System.Drawing.Point(0, 0);
            this.panelMultiSend3.Name = "panelMultiSend3";
            this.panelMultiSend3.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend3.TabIndex = 71;
            this.panelMultiSend3.Visible = false;
            // 
            // btnMultiSend22
            // 
            this.btnMultiSend22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend22.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend22.Name = "btnMultiSend22";
            this.btnMultiSend22.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend22.TabIndex = 6;
            this.btnMultiSend22.Text = "21";
            this.btnMultiSend22.UseVisualStyleBackColor = true;
            this.btnMultiSend22.Click += new System.EventHandler(this.btnMultiSend22_Click);
            // 
            // btnMultiSend21
            // 
            this.btnMultiSend21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend21.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend21.Name = "btnMultiSend21";
            this.btnMultiSend21.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend21.TabIndex = 2;
            this.btnMultiSend21.Text = "20";
            this.btnMultiSend21.UseVisualStyleBackColor = true;
            this.btnMultiSend21.Click += new System.EventHandler(this.btnMultiSend21_Click);
            // 
            // tbxMultiSend22
            // 
            this.tbxMultiSend22.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend22.Name = "tbxMultiSend22";
            this.tbxMultiSend22.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend22.TabIndex = 7;
            this.tbxMultiSend22.Text = "1";
            // 
            // tbxMultiSend24
            // 
            this.tbxMultiSend24.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend24.Name = "tbxMultiSend24";
            this.tbxMultiSend24.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend24.TabIndex = 8;
            this.tbxMultiSend24.Text = "3";
            // 
            // tbxMultiSend23
            // 
            this.tbxMultiSend23.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend23.Name = "tbxMultiSend23";
            this.tbxMultiSend23.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend23.TabIndex = 9;
            this.tbxMultiSend23.Text = "2";
            // 
            // btnMultiSend23
            // 
            this.btnMultiSend23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend23.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend23.Name = "btnMultiSend23";
            this.btnMultiSend23.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend23.TabIndex = 10;
            this.btnMultiSend23.Text = "22";
            this.btnMultiSend23.UseVisualStyleBackColor = true;
            this.btnMultiSend23.Click += new System.EventHandler(this.btnMultiSend23_Click);
            // 
            // btnMultiSend24
            // 
            this.btnMultiSend24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend24.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend24.Name = "btnMultiSend24";
            this.btnMultiSend24.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend24.TabIndex = 11;
            this.btnMultiSend24.Text = "23";
            this.btnMultiSend24.UseVisualStyleBackColor = true;
            this.btnMultiSend24.Click += new System.EventHandler(this.btnMultiSend24_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(588, 97);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 17);
            this.label19.TabIndex = 27;
            this.label19.Text = "间隔：";
            // 
            // tbxMultiSend21
            // 
            this.tbxMultiSend21.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend21.Name = "tbxMultiSend21";
            this.tbxMultiSend21.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend21.TabIndex = 30;
            this.tbxMultiSend21.Text = "0";
            this.tbxMultiSend21.WordWrap = false;
            // 
            // ckbMultiSend21
            // 
            this.ckbMultiSend21.AutoSize = true;
            this.ckbMultiSend21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend21.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend21.Name = "ckbMultiSend21";
            this.ckbMultiSend21.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend21.TabIndex = 31;
            this.ckbMultiSend21.UseVisualStyleBackColor = true;
            this.ckbMultiSend21.CheckedChanged += new System.EventHandler(this.ckbMultiSend21_CheckedChanged);
            // 
            // ckbMultiSend30
            // 
            this.ckbMultiSend30.AutoSize = true;
            this.ckbMultiSend30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend30.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend30.Name = "ckbMultiSend30";
            this.ckbMultiSend30.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend30.TabIndex = 56;
            this.ckbMultiSend30.UseVisualStyleBackColor = true;
            this.ckbMultiSend30.CheckedChanged += new System.EventHandler(this.ckbMultiSend30_CheckedChanged);
            // 
            // ckbMultiSend22
            // 
            this.ckbMultiSend22.AutoSize = true;
            this.ckbMultiSend22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend22.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend22.Name = "ckbMultiSend22";
            this.ckbMultiSend22.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend22.TabIndex = 32;
            this.ckbMultiSend22.UseVisualStyleBackColor = true;
            this.ckbMultiSend22.CheckedChanged += new System.EventHandler(this.ckbMultiSend22_CheckedChanged);
            // 
            // ckbMultiSend29
            // 
            this.ckbMultiSend29.AutoSize = true;
            this.ckbMultiSend29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend29.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend29.Name = "ckbMultiSend29";
            this.ckbMultiSend29.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend29.TabIndex = 55;
            this.ckbMultiSend29.UseVisualStyleBackColor = true;
            this.ckbMultiSend29.CheckedChanged += new System.EventHandler(this.ckbMultiSend29_CheckedChanged);
            // 
            // ckbMultiSend23
            // 
            this.ckbMultiSend23.AutoSize = true;
            this.ckbMultiSend23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend23.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend23.Name = "ckbMultiSend23";
            this.ckbMultiSend23.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend23.TabIndex = 33;
            this.ckbMultiSend23.UseVisualStyleBackColor = true;
            this.ckbMultiSend23.CheckedChanged += new System.EventHandler(this.ckbMultiSend23_CheckedChanged);
            // 
            // tbxMultiSend29
            // 
            this.tbxMultiSend29.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend29.Name = "tbxMultiSend29";
            this.tbxMultiSend29.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend29.TabIndex = 54;
            this.tbxMultiSend29.Text = "8";
            // 
            // ckbMultiSend24
            // 
            this.ckbMultiSend24.AutoSize = true;
            this.ckbMultiSend24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend24.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend24.Name = "ckbMultiSend24";
            this.ckbMultiSend24.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend24.TabIndex = 34;
            this.ckbMultiSend24.UseVisualStyleBackColor = true;
            this.ckbMultiSend24.CheckedChanged += new System.EventHandler(this.ckbMultiSend24_CheckedChanged);
            // 
            // tbxMultiSend30
            // 
            this.tbxMultiSend30.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend30.Name = "tbxMultiSend30";
            this.tbxMultiSend30.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend30.TabIndex = 49;
            this.tbxMultiSend30.Text = "9";
            // 
            // btnMultiSend25
            // 
            this.btnMultiSend25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend25.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend25.Name = "btnMultiSend25";
            this.btnMultiSend25.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend25.TabIndex = 35;
            this.btnMultiSend25.Text = "24";
            this.btnMultiSend25.UseVisualStyleBackColor = true;
            this.btnMultiSend25.Click += new System.EventHandler(this.btnMultiSend25_Click);
            // 
            // btnMultiSend30
            // 
            this.btnMultiSend30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend30.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend30.Name = "btnMultiSend30";
            this.btnMultiSend30.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend30.TabIndex = 48;
            this.btnMultiSend30.Text = "29";
            this.btnMultiSend30.UseVisualStyleBackColor = true;
            this.btnMultiSend30.Click += new System.EventHandler(this.btnMultiSend30_Click);
            // 
            // btnMultiSend26
            // 
            this.btnMultiSend26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend26.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend26.Name = "btnMultiSend26";
            this.btnMultiSend26.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend26.TabIndex = 36;
            this.btnMultiSend26.Text = "25";
            this.btnMultiSend26.UseVisualStyleBackColor = true;
            this.btnMultiSend26.Click += new System.EventHandler(this.btnMultiSend26_Click);
            // 
            // btnMultiSend29
            // 
            this.btnMultiSend29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend29.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend29.Name = "btnMultiSend29";
            this.btnMultiSend29.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend29.TabIndex = 47;
            this.btnMultiSend29.Text = "28";
            this.btnMultiSend29.UseVisualStyleBackColor = true;
            this.btnMultiSend29.Click += new System.EventHandler(this.btnMultiSend29_Click);
            // 
            // tbxMultiSend26
            // 
            this.tbxMultiSend26.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend26.Name = "tbxMultiSend26";
            this.tbxMultiSend26.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend26.TabIndex = 37;
            this.tbxMultiSend26.Text = "5";
            // 
            // ckbMultiSend28
            // 
            this.ckbMultiSend28.AutoSize = true;
            this.ckbMultiSend28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend28.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend28.Name = "ckbMultiSend28";
            this.ckbMultiSend28.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend28.TabIndex = 46;
            this.ckbMultiSend28.UseVisualStyleBackColor = true;
            this.ckbMultiSend28.CheckedChanged += new System.EventHandler(this.ckbMultiSend28_CheckedChanged);
            // 
            // tbxMultiSend28
            // 
            this.tbxMultiSend28.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend28.Name = "tbxMultiSend28";
            this.tbxMultiSend28.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend28.TabIndex = 38;
            this.tbxMultiSend28.Text = "7";
            // 
            // ckbMultiSend27
            // 
            this.ckbMultiSend27.AutoSize = true;
            this.ckbMultiSend27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend27.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend27.Name = "ckbMultiSend27";
            this.ckbMultiSend27.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend27.TabIndex = 45;
            this.ckbMultiSend27.UseVisualStyleBackColor = true;
            this.ckbMultiSend27.CheckedChanged += new System.EventHandler(this.ckbMultiSend27_CheckedChanged);
            // 
            // tbxMultiSend27
            // 
            this.tbxMultiSend27.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend27.Name = "tbxMultiSend27";
            this.tbxMultiSend27.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend27.TabIndex = 39;
            this.tbxMultiSend27.Text = "6";
            // 
            // ckbMultiSend26
            // 
            this.ckbMultiSend26.AutoSize = true;
            this.ckbMultiSend26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend26.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend26.Name = "ckbMultiSend26";
            this.ckbMultiSend26.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend26.TabIndex = 44;
            this.ckbMultiSend26.UseVisualStyleBackColor = true;
            this.ckbMultiSend26.CheckedChanged += new System.EventHandler(this.ckbMultiSend26_CheckedChanged);
            // 
            // btnMultiSend27
            // 
            this.btnMultiSend27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend27.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend27.Name = "btnMultiSend27";
            this.btnMultiSend27.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend27.TabIndex = 40;
            this.btnMultiSend27.Text = "26";
            this.btnMultiSend27.UseVisualStyleBackColor = true;
            this.btnMultiSend27.Click += new System.EventHandler(this.btnMultiSend27_Click);
            // 
            // ckbMultiSend25
            // 
            this.ckbMultiSend25.AutoSize = true;
            this.ckbMultiSend25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend25.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend25.Name = "ckbMultiSend25";
            this.ckbMultiSend25.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend25.TabIndex = 43;
            this.ckbMultiSend25.UseVisualStyleBackColor = true;
            this.ckbMultiSend25.CheckedChanged += new System.EventHandler(this.ckbMultiSend25_CheckedChanged);
            // 
            // btnMultiSend28
            // 
            this.btnMultiSend28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend28.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend28.Name = "btnMultiSend28";
            this.btnMultiSend28.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend28.TabIndex = 41;
            this.btnMultiSend28.Text = "27";
            this.btnMultiSend28.UseVisualStyleBackColor = true;
            this.btnMultiSend28.Click += new System.EventHandler(this.btnMultiSend28_Click);
            // 
            // tbxMultiSend25
            // 
            this.tbxMultiSend25.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend25.Name = "tbxMultiSend25";
            this.tbxMultiSend25.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend25.TabIndex = 42;
            this.tbxMultiSend25.Text = "4";
            // 
            // panelMultiSend4
            // 
            this.panelMultiSend4.Controls.Add(this.btnMultiSend32);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend31);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend32);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend34);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend33);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend33);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend34);
            this.panelMultiSend4.Controls.Add(this.label20);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend31);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend31);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend40);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend32);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend39);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend33);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend39);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend34);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend40);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend35);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend40);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend36);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend39);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend36);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend38);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend38);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend37);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend37);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend36);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend37);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend35);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend38);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend35);
            this.panelMultiSend4.Location = new System.Drawing.Point(0, 0);
            this.panelMultiSend4.Name = "panelMultiSend4";
            this.panelMultiSend4.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend4.TabIndex = 71;
            this.panelMultiSend4.Visible = false;
            // 
            // btnMultiSend32
            // 
            this.btnMultiSend32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend32.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend32.Name = "btnMultiSend32";
            this.btnMultiSend32.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend32.TabIndex = 6;
            this.btnMultiSend32.Text = "31";
            this.btnMultiSend32.UseVisualStyleBackColor = true;
            this.btnMultiSend32.Click += new System.EventHandler(this.btnMultiSend32_Click);
            // 
            // btnMultiSend31
            // 
            this.btnMultiSend31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend31.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend31.Name = "btnMultiSend31";
            this.btnMultiSend31.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend31.TabIndex = 2;
            this.btnMultiSend31.Text = "30";
            this.btnMultiSend31.UseVisualStyleBackColor = true;
            this.btnMultiSend31.Click += new System.EventHandler(this.btnMultiSend31_Click);
            // 
            // tbxMultiSend32
            // 
            this.tbxMultiSend32.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend32.Name = "tbxMultiSend32";
            this.tbxMultiSend32.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend32.TabIndex = 7;
            this.tbxMultiSend32.Text = "1";
            // 
            // tbxMultiSend34
            // 
            this.tbxMultiSend34.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend34.Name = "tbxMultiSend34";
            this.tbxMultiSend34.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend34.TabIndex = 8;
            this.tbxMultiSend34.Text = "3";
            // 
            // tbxMultiSend33
            // 
            this.tbxMultiSend33.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend33.Name = "tbxMultiSend33";
            this.tbxMultiSend33.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend33.TabIndex = 9;
            this.tbxMultiSend33.Text = "2";
            // 
            // btnMultiSend33
            // 
            this.btnMultiSend33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend33.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend33.Name = "btnMultiSend33";
            this.btnMultiSend33.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend33.TabIndex = 10;
            this.btnMultiSend33.Text = "32";
            this.btnMultiSend33.UseVisualStyleBackColor = true;
            this.btnMultiSend33.Click += new System.EventHandler(this.btnMultiSend33_Click);
            // 
            // btnMultiSend34
            // 
            this.btnMultiSend34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend34.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend34.Name = "btnMultiSend34";
            this.btnMultiSend34.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend34.TabIndex = 11;
            this.btnMultiSend34.Text = "33";
            this.btnMultiSend34.UseVisualStyleBackColor = true;
            this.btnMultiSend34.Click += new System.EventHandler(this.btnMultiSend34_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(588, 97);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(44, 17);
            this.label20.TabIndex = 27;
            this.label20.Text = "间隔：";
            // 
            // tbxMultiSend31
            // 
            this.tbxMultiSend31.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend31.Name = "tbxMultiSend31";
            this.tbxMultiSend31.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend31.TabIndex = 30;
            this.tbxMultiSend31.Text = "0";
            this.tbxMultiSend31.WordWrap = false;
            // 
            // ckbMultiSend31
            // 
            this.ckbMultiSend31.AutoSize = true;
            this.ckbMultiSend31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend31.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend31.Name = "ckbMultiSend31";
            this.ckbMultiSend31.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend31.TabIndex = 31;
            this.ckbMultiSend31.UseVisualStyleBackColor = true;
            this.ckbMultiSend31.CheckedChanged += new System.EventHandler(this.ckbMultiSend31_CheckedChanged);
            // 
            // ckbMultiSend40
            // 
            this.ckbMultiSend40.AutoSize = true;
            this.ckbMultiSend40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend40.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend40.Name = "ckbMultiSend40";
            this.ckbMultiSend40.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend40.TabIndex = 56;
            this.ckbMultiSend40.UseVisualStyleBackColor = true;
            this.ckbMultiSend40.CheckedChanged += new System.EventHandler(this.ckbMultiSend40_CheckedChanged);
            // 
            // ckbMultiSend32
            // 
            this.ckbMultiSend32.AutoSize = true;
            this.ckbMultiSend32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend32.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend32.Name = "ckbMultiSend32";
            this.ckbMultiSend32.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend32.TabIndex = 32;
            this.ckbMultiSend32.UseVisualStyleBackColor = true;
            this.ckbMultiSend32.CheckedChanged += new System.EventHandler(this.ckbMultiSend32_CheckedChanged);
            // 
            // ckbMultiSend39
            // 
            this.ckbMultiSend39.AutoSize = true;
            this.ckbMultiSend39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend39.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend39.Name = "ckbMultiSend39";
            this.ckbMultiSend39.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend39.TabIndex = 55;
            this.ckbMultiSend39.UseVisualStyleBackColor = true;
            this.ckbMultiSend39.CheckedChanged += new System.EventHandler(this.ckbMultiSend39_CheckedChanged);
            // 
            // ckbMultiSend33
            // 
            this.ckbMultiSend33.AutoSize = true;
            this.ckbMultiSend33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend33.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend33.Name = "ckbMultiSend33";
            this.ckbMultiSend33.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend33.TabIndex = 33;
            this.ckbMultiSend33.UseVisualStyleBackColor = true;
            this.ckbMultiSend33.CheckedChanged += new System.EventHandler(this.ckbMultiSend33_CheckedChanged);
            // 
            // tbxMultiSend39
            // 
            this.tbxMultiSend39.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend39.Name = "tbxMultiSend39";
            this.tbxMultiSend39.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend39.TabIndex = 54;
            this.tbxMultiSend39.Text = "8";
            // 
            // ckbMultiSend34
            // 
            this.ckbMultiSend34.AutoSize = true;
            this.ckbMultiSend34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend34.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend34.Name = "ckbMultiSend34";
            this.ckbMultiSend34.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend34.TabIndex = 34;
            this.ckbMultiSend34.UseVisualStyleBackColor = true;
            this.ckbMultiSend34.CheckedChanged += new System.EventHandler(this.ckbMultiSend34_CheckedChanged);
            // 
            // tbxMultiSend40
            // 
            this.tbxMultiSend40.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend40.Name = "tbxMultiSend40";
            this.tbxMultiSend40.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend40.TabIndex = 49;
            this.tbxMultiSend40.Text = "9";
            // 
            // btnMultiSend35
            // 
            this.btnMultiSend35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend35.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend35.Name = "btnMultiSend35";
            this.btnMultiSend35.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend35.TabIndex = 35;
            this.btnMultiSend35.Text = "34";
            this.btnMultiSend35.UseVisualStyleBackColor = true;
            this.btnMultiSend35.Click += new System.EventHandler(this.btnMultiSend35_Click);
            // 
            // btnMultiSend40
            // 
            this.btnMultiSend40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend40.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend40.Name = "btnMultiSend40";
            this.btnMultiSend40.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend40.TabIndex = 48;
            this.btnMultiSend40.Text = "39";
            this.btnMultiSend40.UseVisualStyleBackColor = true;
            this.btnMultiSend40.Click += new System.EventHandler(this.btnMultiSend40_Click);
            // 
            // btnMultiSend36
            // 
            this.btnMultiSend36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend36.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend36.Name = "btnMultiSend36";
            this.btnMultiSend36.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend36.TabIndex = 36;
            this.btnMultiSend36.Text = "35";
            this.btnMultiSend36.UseVisualStyleBackColor = true;
            this.btnMultiSend36.Click += new System.EventHandler(this.btnMultiSend36_Click);
            // 
            // btnMultiSend39
            // 
            this.btnMultiSend39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend39.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend39.Name = "btnMultiSend39";
            this.btnMultiSend39.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend39.TabIndex = 47;
            this.btnMultiSend39.Text = "38";
            this.btnMultiSend39.UseVisualStyleBackColor = true;
            this.btnMultiSend39.Click += new System.EventHandler(this.btnMultiSend39_Click);
            // 
            // tbxMultiSend36
            // 
            this.tbxMultiSend36.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend36.Name = "tbxMultiSend36";
            this.tbxMultiSend36.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend36.TabIndex = 37;
            this.tbxMultiSend36.Text = "5";
            // 
            // ckbMultiSend38
            // 
            this.ckbMultiSend38.AutoSize = true;
            this.ckbMultiSend38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend38.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend38.Name = "ckbMultiSend38";
            this.ckbMultiSend38.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend38.TabIndex = 46;
            this.ckbMultiSend38.UseVisualStyleBackColor = true;
            this.ckbMultiSend38.CheckedChanged += new System.EventHandler(this.ckbMultiSend38_CheckedChanged);
            // 
            // tbxMultiSend38
            // 
            this.tbxMultiSend38.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend38.Name = "tbxMultiSend38";
            this.tbxMultiSend38.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend38.TabIndex = 38;
            this.tbxMultiSend38.Text = "7";
            // 
            // ckbMultiSend37
            // 
            this.ckbMultiSend37.AutoSize = true;
            this.ckbMultiSend37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend37.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend37.Name = "ckbMultiSend37";
            this.ckbMultiSend37.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend37.TabIndex = 45;
            this.ckbMultiSend37.UseVisualStyleBackColor = true;
            this.ckbMultiSend37.CheckedChanged += new System.EventHandler(this.ckbMultiSend37_CheckedChanged);
            // 
            // tbxMultiSend37
            // 
            this.tbxMultiSend37.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend37.Name = "tbxMultiSend37";
            this.tbxMultiSend37.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend37.TabIndex = 39;
            this.tbxMultiSend37.Text = "6";
            // 
            // ckbMultiSend36
            // 
            this.ckbMultiSend36.AutoSize = true;
            this.ckbMultiSend36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend36.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend36.Name = "ckbMultiSend36";
            this.ckbMultiSend36.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend36.TabIndex = 44;
            this.ckbMultiSend36.UseVisualStyleBackColor = true;
            this.ckbMultiSend36.CheckedChanged += new System.EventHandler(this.ckbMultiSend36_CheckedChanged);
            // 
            // btnMultiSend37
            // 
            this.btnMultiSend37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend37.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend37.Name = "btnMultiSend37";
            this.btnMultiSend37.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend37.TabIndex = 40;
            this.btnMultiSend37.Text = "36";
            this.btnMultiSend37.UseVisualStyleBackColor = true;
            this.btnMultiSend37.Click += new System.EventHandler(this.btnMultiSend37_Click);
            // 
            // ckbMultiSend35
            // 
            this.ckbMultiSend35.AutoSize = true;
            this.ckbMultiSend35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend35.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend35.Name = "ckbMultiSend35";
            this.ckbMultiSend35.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend35.TabIndex = 43;
            this.ckbMultiSend35.UseVisualStyleBackColor = true;
            this.ckbMultiSend35.CheckedChanged += new System.EventHandler(this.ckbMultiSend35_CheckedChanged);
            // 
            // btnMultiSend38
            // 
            this.btnMultiSend38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend38.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend38.Name = "btnMultiSend38";
            this.btnMultiSend38.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend38.TabIndex = 41;
            this.btnMultiSend38.Text = "37";
            this.btnMultiSend38.UseVisualStyleBackColor = true;
            this.btnMultiSend38.Click += new System.EventHandler(this.btnMultiSend38_Click);
            // 
            // tbxMultiSend35
            // 
            this.tbxMultiSend35.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend35.Name = "tbxMultiSend35";
            this.tbxMultiSend35.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend35.TabIndex = 42;
            this.tbxMultiSend35.Text = "4";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(594, 97);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 17);
            this.label21.TabIndex = 71;
            this.label21.Text = "周期:";
            // 
            // btnRemarkMultiSend
            // 
            this.btnRemarkMultiSend.Location = new System.Drawing.Point(596, 119);
            this.btnRemarkMultiSend.Name = "btnRemarkMultiSend";
            this.btnRemarkMultiSend.Size = new System.Drawing.Size(95, 23);
            this.btnRemarkMultiSend.TabIndex = 69;
            this.btnRemarkMultiSend.Text = "导入导出条目";
            this.btnRemarkMultiSend.UseVisualStyleBackColor = true;
            this.btnRemarkMultiSend.Click += new System.EventHandler(this.btnRemarkMultiSend_Click);
            // 
            // btnMultiEndPage
            // 
            this.btnMultiEndPage.Location = new System.Drawing.Point(356, 119);
            this.btnMultiEndPage.Name = "btnMultiEndPage";
            this.btnMultiEndPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiEndPage.TabIndex = 66;
            this.btnMultiEndPage.Text = "尾页";
            this.btnMultiEndPage.UseVisualStyleBackColor = true;
            this.btnMultiEndPage.Click += new System.EventHandler(this.btnMultiEndPage_Click);
            // 
            // btnMultiNextPage
            // 
            this.btnMultiNextPage.Location = new System.Drawing.Point(290, 119);
            this.btnMultiNextPage.Name = "btnMultiNextPage";
            this.btnMultiNextPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiNextPage.TabIndex = 65;
            this.btnMultiNextPage.Text = "下一页";
            this.btnMultiNextPage.UseVisualStyleBackColor = true;
            this.btnMultiNextPage.Click += new System.EventHandler(this.btnMultiNextPage_Click);
            // 
            // btnMultiLastPage
            // 
            this.btnMultiLastPage.Location = new System.Drawing.Point(224, 119);
            this.btnMultiLastPage.Name = "btnMultiLastPage";
            this.btnMultiLastPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiLastPage.TabIndex = 64;
            this.btnMultiLastPage.Text = "上一页";
            this.btnMultiLastPage.UseVisualStyleBackColor = true;
            this.btnMultiLastPage.Click += new System.EventHandler(this.btnMultiLastPage_Click);
            // 
            // btnMultiFirstPage
            // 
            this.btnMultiFirstPage.Location = new System.Drawing.Point(158, 119);
            this.btnMultiFirstPage.Name = "btnMultiFirstPage";
            this.btnMultiFirstPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiFirstPage.TabIndex = 63;
            this.btnMultiFirstPage.Text = "首页";
            this.btnMultiFirstPage.UseVisualStyleBackColor = true;
            this.btnMultiFirstPage.Click += new System.EventHandler(this.btnMultiFirstPage_Click);
            // 
            // ckbMultiSendNewLine
            // 
            this.ckbMultiSendNewLine.AutoSize = true;
            this.ckbMultiSendNewLine.Checked = true;
            this.ckbMultiSendNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMultiSendNewLine.Location = new System.Drawing.Point(596, 5);
            this.ckbMultiSendNewLine.Name = "ckbMultiSendNewLine";
            this.ckbMultiSendNewLine.Size = new System.Drawing.Size(75, 21);
            this.ckbMultiSendNewLine.TabIndex = 62;
            this.ckbMultiSendNewLine.Text = "发送新行";
            this.ckbMultiSendNewLine.UseVisualStyleBackColor = true;
            this.ckbMultiSendNewLine.CheckedChanged += new System.EventHandler(this.ckbMultiSendNewLine_CheckedChanged);
            // 
            // ckbRelateKeyBoard
            // 
            this.ckbRelateKeyBoard.AutoSize = true;
            this.ckbRelateKeyBoard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbRelateKeyBoard.Location = new System.Drawing.Point(596, 51);
            this.ckbRelateKeyBoard.Name = "ckbRelateKeyBoard";
            this.ckbRelateKeyBoard.Size = new System.Drawing.Size(99, 21);
            this.ckbRelateKeyBoard.TabIndex = 61;
            this.ckbRelateKeyBoard.Text = "关联数字键盘";
            this.ckbRelateKeyBoard.UseVisualStyleBackColor = true;
            this.ckbRelateKeyBoard.CheckedChanged += new System.EventHandler(this.ckbRelateKeyBoard_CheckedChanged);
            // 
            // ckbMultiHexSend
            // 
            this.ckbMultiHexSend.AutoSize = true;
            this.ckbMultiHexSend.Location = new System.Drawing.Point(596, 27);
            this.ckbMultiHexSend.Name = "ckbMultiHexSend";
            this.ckbMultiHexSend.Size = new System.Drawing.Size(89, 21);
            this.ckbMultiHexSend.TabIndex = 60;
            this.ckbMultiHexSend.Text = "16进制发送";
            this.ckbMultiHexSend.UseVisualStyleBackColor = true;
            this.ckbMultiHexSend.CheckedChanged += new System.EventHandler(this.ckbMultiHexSend_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(674, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 17);
            this.label14.TabIndex = 29;
            this.label14.Text = "ms";
            // 
            // tbxMutilSendPeriod
            // 
            this.tbxMutilSendPeriod.Location = new System.Drawing.Point(635, 94);
            this.tbxMutilSendPeriod.Name = "tbxMutilSendPeriod";
            this.tbxMutilSendPeriod.Size = new System.Drawing.Size(35, 23);
            this.tbxMutilSendPeriod.TabIndex = 28;
            this.tbxMutilSendPeriod.Text = "1000";
            this.tbxMutilSendPeriod.TextChanged += new System.EventHandler(this.tbxMutilSendPeriod_TextChanged);
            // 
            // ckbMultiAutoSend
            // 
            this.ckbMultiAutoSend.AutoSize = true;
            this.ckbMultiAutoSend.Location = new System.Drawing.Point(596, 75);
            this.ckbMultiAutoSend.Name = "ckbMultiAutoSend";
            this.ckbMultiAutoSend.Size = new System.Drawing.Size(99, 21);
            this.ckbMultiAutoSend.TabIndex = 26;
            this.ckbMultiAutoSend.Text = "自动循环发送";
            this.ckbMultiAutoSend.UseVisualStyleBackColor = true;
            this.ckbMultiAutoSend.CheckedChanged += new System.EventHandler(this.ckbMultiAutoSend_CheckedChanged);
            // 
            // transportProtocolTab
            // 
            this.transportProtocolTab.BackColor = System.Drawing.Color.Transparent;
            this.transportProtocolTab.Controls.Add(this.groupBox2);
            this.transportProtocolTab.Controls.Add(this.groupBox1);
            this.transportProtocolTab.Location = new System.Drawing.Point(4, 26);
            this.transportProtocolTab.Name = "transportProtocolTab";
            this.transportProtocolTab.Size = new System.Drawing.Size(990, 142);
            this.transportProtocolTab.TabIndex = 4;
            this.transportProtocolTab.Text = "协议传输";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.lblTransportProtocolMasterAddr);
            this.groupBox2.Controls.Add(this.ckbTransportProtocolAutoNewLine);
            this.groupBox2.Controls.Add(this.ckbTransportProtocolDispOrigialData);
            this.groupBox2.Controls.Add(this.lblTransportProtocolRevChecksum);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.lblTransportProtocolResult);
            this.groupBox2.Controls.Add(this.lblTransportProtocolRevDataLength);
            this.groupBox2.Controls.Add(this.lblTransportProtocolRevSequence);
            this.groupBox2.Controls.Add(this.lblTransportProtocolRevFunctionType);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 142);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "从机响应";
            // 
            // lblTransportProtocolMasterAddr
            // 
            this.lblTransportProtocolMasterAddr.AutoSize = true;
            this.lblTransportProtocolMasterAddr.Location = new System.Drawing.Point(96, 16);
            this.lblTransportProtocolMasterAddr.Name = "lblTransportProtocolMasterAddr";
            this.lblTransportProtocolMasterAddr.Size = new System.Drawing.Size(22, 17);
            this.lblTransportProtocolMasterAddr.TabIndex = 16;
            this.lblTransportProtocolMasterAddr.Text = "01";
            // 
            // ckbTransportProtocolAutoNewLine
            // 
            this.ckbTransportProtocolAutoNewLine.AutoSize = true;
            this.ckbTransportProtocolAutoNewLine.Checked = true;
            this.ckbTransportProtocolAutoNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTransportProtocolAutoNewLine.Enabled = false;
            this.ckbTransportProtocolAutoNewLine.Location = new System.Drawing.Point(8, 119);
            this.ckbTransportProtocolAutoNewLine.Name = "ckbTransportProtocolAutoNewLine";
            this.ckbTransportProtocolAutoNewLine.Size = new System.Drawing.Size(75, 21);
            this.ckbTransportProtocolAutoNewLine.TabIndex = 15;
            this.ckbTransportProtocolAutoNewLine.Text = "自动换行";
            this.ckbTransportProtocolAutoNewLine.UseVisualStyleBackColor = true;
            this.ckbTransportProtocolAutoNewLine.CheckedChanged += new System.EventHandler(this.ckbTransportProtocolAutoNewLine_CheckedChanged);
            // 
            // ckbTransportProtocolDispOrigialData
            // 
            this.ckbTransportProtocolDispOrigialData.AutoSize = true;
            this.ckbTransportProtocolDispOrigialData.Checked = true;
            this.ckbTransportProtocolDispOrigialData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTransportProtocolDispOrigialData.Enabled = false;
            this.ckbTransportProtocolDispOrigialData.Location = new System.Drawing.Point(131, 120);
            this.ckbTransportProtocolDispOrigialData.Name = "ckbTransportProtocolDispOrigialData";
            this.ckbTransportProtocolDispOrigialData.Size = new System.Drawing.Size(87, 21);
            this.ckbTransportProtocolDispOrigialData.TabIndex = 14;
            this.ckbTransportProtocolDispOrigialData.Text = "显示原始帧";
            this.ckbTransportProtocolDispOrigialData.UseVisualStyleBackColor = true;
            this.ckbTransportProtocolDispOrigialData.CheckedChanged += new System.EventHandler(this.ckbTransportProtocolDispOrigialData_CheckedChanged);
            // 
            // lblTransportProtocolRevChecksum
            // 
            this.lblTransportProtocolRevChecksum.AutoSize = true;
            this.lblTransportProtocolRevChecksum.Location = new System.Drawing.Point(92, 67);
            this.lblTransportProtocolRevChecksum.Name = "lblTransportProtocolRevChecksum";
            this.lblTransportProtocolRevChecksum.Size = new System.Drawing.Size(0, 17);
            this.lblTransportProtocolRevChecksum.TabIndex = 13;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(3, 67);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(84, 17);
            this.label32.TabIndex = 12;
            this.label32.Text = "  校验值(dec):";
            // 
            // lblTransportProtocolResult
            // 
            this.lblTransportProtocolResult.AutoSize = true;
            this.lblTransportProtocolResult.Location = new System.Drawing.Point(80, 95);
            this.lblTransportProtocolResult.Name = "lblTransportProtocolResult";
            this.lblTransportProtocolResult.Size = new System.Drawing.Size(0, 17);
            this.lblTransportProtocolResult.TabIndex = 11;
            // 
            // lblTransportProtocolRevDataLength
            // 
            this.lblTransportProtocolRevDataLength.AutoSize = true;
            this.lblTransportProtocolRevDataLength.Location = new System.Drawing.Point(96, 42);
            this.lblTransportProtocolRevDataLength.Name = "lblTransportProtocolRevDataLength";
            this.lblTransportProtocolRevDataLength.Size = new System.Drawing.Size(15, 17);
            this.lblTransportProtocolRevDataLength.TabIndex = 9;
            this.lblTransportProtocolRevDataLength.Text = "0";
            // 
            // lblTransportProtocolRevSequence
            // 
            this.lblTransportProtocolRevSequence.AutoSize = true;
            this.lblTransportProtocolRevSequence.Location = new System.Drawing.Point(212, 42);
            this.lblTransportProtocolRevSequence.Name = "lblTransportProtocolRevSequence";
            this.lblTransportProtocolRevSequence.Size = new System.Drawing.Size(15, 17);
            this.lblTransportProtocolRevSequence.TabIndex = 8;
            this.lblTransportProtocolRevSequence.Text = "0";
            // 
            // lblTransportProtocolRevFunctionType
            // 
            this.lblTransportProtocolRevFunctionType.AutoSize = true;
            this.lblTransportProtocolRevFunctionType.Location = new System.Drawing.Point(212, 16);
            this.lblTransportProtocolRevFunctionType.Name = "lblTransportProtocolRevFunctionType";
            this.lblTransportProtocolRevFunctionType.Size = new System.Drawing.Size(22, 17);
            this.lblTransportProtocolRevFunctionType.TabIndex = 7;
            this.lblTransportProtocolRevFunctionType.Text = "01";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(21, 94);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(59, 17);
            this.label31.TabIndex = 5;
            this.label31.Text = "解析结果:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 42);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(88, 17);
            this.label28.TabIndex = 3;
            this.label28.Text = "数据长度(dec):";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(129, 42);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(76, 17);
            this.label29.TabIndex = 2;
            this.label29.Text = "帧序列(dec):";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(129, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(75, 17);
            this.label26.TabIndex = 1;
            this.label26.Text = "帧功能(hex):";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(87, 17);
            this.label25.TabIndex = 0;
            this.label25.Text = "主机地址(hex):";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxTransportProtocolRetryCount);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.ckbTransportProtocolAutoSend);
            this.groupBox1.Controls.Add(this.lblTransportProtocolSendDataLength);
            this.groupBox1.Controls.Add(this.lblTransportProtocolSendSequence);
            this.groupBox1.Controls.Add(this.tbxTransportProtocolMaxDataLength);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.tbxTransportProtocolSendPeriod);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.btnEnbaleTransportProtocol);
            this.groupBox1.Controls.Add(this.lblTransportProtocolProgSendFile);
            this.groupBox1.Controls.Add(this.btnTransportProtocolStopFile);
            this.groupBox1.Controls.Add(this.btnTransportProtocolSendFile);
            this.groupBox1.Controls.Add(this.btnTransportProtocolOpenFile);
            this.groupBox1.Controls.Add(this.progBarTransportProtocolSendFile);
            this.groupBox1.Controls.Add(this.tbxTransportProtocolSendData);
            this.groupBox1.Controls.Add(this.btnTransportProtocolSend);
            this.groupBox1.Controls.Add(this.tbxTransportProtocolFileName);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.tbxTransportProtocolSlaveDeviceAddr);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.tbxTransportProtocolSendFunctionType);
            this.groupBox1.Controls.Add(this.cbxTransportProtocolChecksum);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Location = new System.Drawing.Point(245, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 142);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主机发送";
            // 
            // tbxTransportProtocolRetryCount
            // 
            this.tbxTransportProtocolRetryCount.Enabled = false;
            this.tbxTransportProtocolRetryCount.Location = new System.Drawing.Point(77, 39);
            this.tbxTransportProtocolRetryCount.Name = "tbxTransportProtocolRetryCount";
            this.tbxTransportProtocolRetryCount.Size = new System.Drawing.Size(30, 23);
            this.tbxTransportProtocolRetryCount.TabIndex = 44;
            this.tbxTransportProtocolRetryCount.Text = "10";
            this.tbxTransportProtocolRetryCount.TextChanged += new System.EventHandler(this.tbxTransportProtocolRetryCount_TextChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(12, 41);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(59, 17);
            this.label38.TabIndex = 43;
            this.label38.Text = "重发次数:";
            // 
            // ckbTransportProtocolAutoSend
            // 
            this.ckbTransportProtocolAutoSend.AutoSize = true;
            this.ckbTransportProtocolAutoSend.Enabled = false;
            this.ckbTransportProtocolAutoSend.Location = new System.Drawing.Point(367, 15);
            this.ckbTransportProtocolAutoSend.Name = "ckbTransportProtocolAutoSend";
            this.ckbTransportProtocolAutoSend.Size = new System.Drawing.Size(75, 21);
            this.ckbTransportProtocolAutoSend.TabIndex = 42;
            this.ckbTransportProtocolAutoSend.Text = "自动发送";
            this.ckbTransportProtocolAutoSend.UseVisualStyleBackColor = true;
            this.ckbTransportProtocolAutoSend.CheckedChanged += new System.EventHandler(this.ckbTransportProtocolAutoSend_CheckedChanged);
            // 
            // lblTransportProtocolSendDataLength
            // 
            this.lblTransportProtocolSendDataLength.AutoSize = true;
            this.lblTransportProtocolSendDataLength.Location = new System.Drawing.Point(186, 42);
            this.lblTransportProtocolSendDataLength.Name = "lblTransportProtocolSendDataLength";
            this.lblTransportProtocolSendDataLength.Size = new System.Drawing.Size(15, 17);
            this.lblTransportProtocolSendDataLength.TabIndex = 41;
            this.lblTransportProtocolSendDataLength.Text = "0";
            // 
            // lblTransportProtocolSendSequence
            // 
            this.lblTransportProtocolSendSequence.AutoSize = true;
            this.lblTransportProtocolSendSequence.Location = new System.Drawing.Point(284, 43);
            this.lblTransportProtocolSendSequence.Name = "lblTransportProtocolSendSequence";
            this.lblTransportProtocolSendSequence.Size = new System.Drawing.Size(15, 17);
            this.lblTransportProtocolSendSequence.TabIndex = 40;
            this.lblTransportProtocolSendSequence.Text = "0";
            // 
            // tbxTransportProtocolMaxDataLength
            // 
            this.tbxTransportProtocolMaxDataLength.Enabled = false;
            this.tbxTransportProtocolMaxDataLength.Location = new System.Drawing.Point(94, 115);
            this.tbxTransportProtocolMaxDataLength.Name = "tbxTransportProtocolMaxDataLength";
            this.tbxTransportProtocolMaxDataLength.Size = new System.Drawing.Size(32, 23);
            this.tbxTransportProtocolMaxDataLength.TabIndex = 39;
            this.tbxTransportProtocolMaxDataLength.Text = "255";
            this.tbxTransportProtocolMaxDataLength.TextChanged += new System.EventHandler(this.tbxTransportProtocolMaxDataLength_TextChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(338, 16);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(25, 17);
            this.label37.TabIndex = 37;
            this.label37.Text = "ms";
            // 
            // tbxTransportProtocolSendPeriod
            // 
            this.tbxTransportProtocolSendPeriod.Enabled = false;
            this.tbxTransportProtocolSendPeriod.Location = new System.Drawing.Point(286, 11);
            this.tbxTransportProtocolSendPeriod.Name = "tbxTransportProtocolSendPeriod";
            this.tbxTransportProtocolSendPeriod.Size = new System.Drawing.Size(46, 23);
            this.tbxTransportProtocolSendPeriod.TabIndex = 36;
            this.tbxTransportProtocolSendPeriod.Text = "1000";
            this.tbxTransportProtocolSendPeriod.TextChanged += new System.EventHandler(this.tbxTransportProtocolSendResponseTime_TextChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(4, 121);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(92, 17);
            this.label35.TabIndex = 38;
            this.label35.Text = "最大数据长度：";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(232, 15);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(47, 17);
            this.label34.TabIndex = 35;
            this.label34.Text = "帧周期:";
            // 
            // btnEnbaleTransportProtocol
            // 
            this.btnEnbaleTransportProtocol.Location = new System.Drawing.Point(281, 115);
            this.btnEnbaleTransportProtocol.Name = "btnEnbaleTransportProtocol";
            this.btnEnbaleTransportProtocol.Size = new System.Drawing.Size(167, 23);
            this.btnEnbaleTransportProtocol.TabIndex = 34;
            this.btnEnbaleTransportProtocol.Text = "启动协议传输";
            this.btnEnbaleTransportProtocol.UseVisualStyleBackColor = true;
            this.btnEnbaleTransportProtocol.Click += new System.EventHandler(this.btnEnbaleTransportProtocol_Click);
            // 
            // lblTransportProtocolProgSendFile
            // 
            this.lblTransportProtocolProgSendFile.AutoSize = true;
            this.lblTransportProtocolProgSendFile.Location = new System.Drawing.Point(262, 120);
            this.lblTransportProtocolProgSendFile.Name = "lblTransportProtocolProgSendFile";
            this.lblTransportProtocolProgSendFile.Size = new System.Drawing.Size(26, 17);
            this.lblTransportProtocolProgSendFile.TabIndex = 33;
            this.lblTransportProtocolProgSendFile.Text = "%0";
            // 
            // btnTransportProtocolStopFile
            // 
            this.btnTransportProtocolStopFile.Enabled = false;
            this.btnTransportProtocolStopFile.Location = new System.Drawing.Point(367, 90);
            this.btnTransportProtocolStopFile.Name = "btnTransportProtocolStopFile";
            this.btnTransportProtocolStopFile.Size = new System.Drawing.Size(80, 23);
            this.btnTransportProtocolStopFile.TabIndex = 32;
            this.btnTransportProtocolStopFile.Text = "停止发送";
            this.btnTransportProtocolStopFile.UseVisualStyleBackColor = true;
            this.btnTransportProtocolStopFile.Click += new System.EventHandler(this.btnTransportProtocolStopFile_Click);
            // 
            // btnTransportProtocolSendFile
            // 
            this.btnTransportProtocolSendFile.Enabled = false;
            this.btnTransportProtocolSendFile.Location = new System.Drawing.Point(281, 89);
            this.btnTransportProtocolSendFile.Name = "btnTransportProtocolSendFile";
            this.btnTransportProtocolSendFile.Size = new System.Drawing.Size(80, 23);
            this.btnTransportProtocolSendFile.TabIndex = 31;
            this.btnTransportProtocolSendFile.Text = "发送文件";
            this.btnTransportProtocolSendFile.UseVisualStyleBackColor = true;
            this.btnTransportProtocolSendFile.Click += new System.EventHandler(this.btnTransportProtocolSendFile_Click);
            // 
            // btnTransportProtocolOpenFile
            // 
            this.btnTransportProtocolOpenFile.Enabled = false;
            this.btnTransportProtocolOpenFile.Location = new System.Drawing.Point(6, 89);
            this.btnTransportProtocolOpenFile.Name = "btnTransportProtocolOpenFile";
            this.btnTransportProtocolOpenFile.Size = new System.Drawing.Size(80, 23);
            this.btnTransportProtocolOpenFile.TabIndex = 30;
            this.btnTransportProtocolOpenFile.Text = "打开文件";
            this.btnTransportProtocolOpenFile.UseVisualStyleBackColor = true;
            this.btnTransportProtocolOpenFile.Click += new System.EventHandler(this.btnTransportProtocolOpenFile_Click);
            // 
            // progBarTransportProtocolSendFile
            // 
            this.progBarTransportProtocolSendFile.Location = new System.Drawing.Point(132, 118);
            this.progBarTransportProtocolSendFile.Name = "progBarTransportProtocolSendFile";
            this.progBarTransportProtocolSendFile.Size = new System.Drawing.Size(124, 14);
            this.progBarTransportProtocolSendFile.TabIndex = 29;
            // 
            // tbxTransportProtocolSendData
            // 
            this.tbxTransportProtocolSendData.Enabled = false;
            this.tbxTransportProtocolSendData.Location = new System.Drawing.Point(8, 64);
            this.tbxTransportProtocolSendData.Multiline = true;
            this.tbxTransportProtocolSendData.Name = "tbxTransportProtocolSendData";
            this.tbxTransportProtocolSendData.Size = new System.Drawing.Size(353, 21);
            this.tbxTransportProtocolSendData.TabIndex = 28;
            this.tbxTransportProtocolSendData.Text = "01 02 03 04 05";
            // 
            // btnTransportProtocolSend
            // 
            this.btnTransportProtocolSend.Enabled = false;
            this.btnTransportProtocolSend.Location = new System.Drawing.Point(367, 64);
            this.btnTransportProtocolSend.Name = "btnTransportProtocolSend";
            this.btnTransportProtocolSend.Size = new System.Drawing.Size(80, 23);
            this.btnTransportProtocolSend.TabIndex = 26;
            this.btnTransportProtocolSend.Text = "发送";
            this.btnTransportProtocolSend.UseVisualStyleBackColor = true;
            this.btnTransportProtocolSend.Click += new System.EventHandler(this.btnTransportProtocolSend_Click);
            // 
            // tbxTransportProtocolFileName
            // 
            this.tbxTransportProtocolFileName.BackColor = System.Drawing.SystemColors.Control;
            this.tbxTransportProtocolFileName.Enabled = false;
            this.tbxTransportProtocolFileName.Location = new System.Drawing.Point(94, 91);
            this.tbxTransportProtocolFileName.Multiline = true;
            this.tbxTransportProtocolFileName.Name = "tbxTransportProtocolFileName";
            this.tbxTransportProtocolFileName.ReadOnly = true;
            this.tbxTransportProtocolFileName.Size = new System.Drawing.Size(185, 21);
            this.tbxTransportProtocolFileName.TabIndex = 25;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(121, 42);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 17);
            this.label33.TabIndex = 23;
            this.label33.Text = "数据长度:";
            // 
            // tbxTransportProtocolSlaveDeviceAddr
            // 
            this.tbxTransportProtocolSlaveDeviceAddr.Enabled = false;
            this.tbxTransportProtocolSlaveDeviceAddr.Location = new System.Drawing.Point(77, 11);
            this.tbxTransportProtocolSlaveDeviceAddr.Name = "tbxTransportProtocolSlaveDeviceAddr";
            this.tbxTransportProtocolSlaveDeviceAddr.Size = new System.Drawing.Size(30, 23);
            this.tbxTransportProtocolSlaveDeviceAddr.TabIndex = 22;
            this.tbxTransportProtocolSlaveDeviceAddr.Text = "01";
            this.tbxTransportProtocolSlaveDeviceAddr.TextChanged += new System.EventHandler(this.tbxTransportProtocolSlaveDeviceAddr_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 17);
            this.label22.TabIndex = 7;
            this.label22.Text = "从机地址:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(133, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(47, 17);
            this.label23.TabIndex = 9;
            this.label23.Text = "帧功能:";
            // 
            // tbxTransportProtocolSendFunctionType
            // 
            this.tbxTransportProtocolSendFunctionType.Enabled = false;
            this.tbxTransportProtocolSendFunctionType.Location = new System.Drawing.Point(181, 12);
            this.tbxTransportProtocolSendFunctionType.Name = "tbxTransportProtocolSendFunctionType";
            this.tbxTransportProtocolSendFunctionType.Size = new System.Drawing.Size(30, 23);
            this.tbxTransportProtocolSendFunctionType.TabIndex = 10;
            this.tbxTransportProtocolSendFunctionType.Text = "01";
            this.tbxTransportProtocolSendFunctionType.TextChanged += new System.EventHandler(this.tbxTransportProtocolSendFunctionType_TextChanged);
            // 
            // cbxTransportProtocolChecksum
            // 
            this.cbxTransportProtocolChecksum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTransportProtocolChecksum.Enabled = false;
            this.cbxTransportProtocolChecksum.FormattingEnabled = true;
            this.cbxTransportProtocolChecksum.Items.AddRange(new object[] {
            "SUM(累加)",
            "XOR(异或)",
            "CRC8",
            "CRC16"});
            this.cbxTransportProtocolChecksum.Location = new System.Drawing.Point(367, 39);
            this.cbxTransportProtocolChecksum.Name = "cbxTransportProtocolChecksum";
            this.cbxTransportProtocolChecksum.Size = new System.Drawing.Size(79, 25);
            this.cbxTransportProtocolChecksum.TabIndex = 17;
            this.cbxTransportProtocolChecksum.SelectedIndexChanged += new System.EventHandler(this.cbxTransportProtocolChecksum_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(232, 43);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(47, 17);
            this.label24.TabIndex = 11;
            this.label24.Text = "帧序列:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(306, 42);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(59, 17);
            this.label27.TabIndex = 16;
            this.label27.Text = "校验方式:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(120, 21);
            this.toolStripStatusLabel1.Text = "www.openedv.com";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripStatusLblSendCnt
            // 
            this.toolStripStatusLblSendCnt.AutoSize = false;
            this.toolStripStatusLblSendCnt.Name = "toolStripStatusLblSendCnt";
            this.toolStripStatusLblSendCnt.Size = new System.Drawing.Size(100, 21);
            this.toolStripStatusLblSendCnt.Text = "S:0";
            this.toolStripStatusLblSendCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripStatusLblRevCnt
            // 
            this.toolStripStatusLblRevCnt.AutoSize = false;
            this.toolStripStatusLblRevCnt.Name = "toolStripStatusLblRevCnt";
            this.toolStripStatusLblRevCnt.Size = new System.Drawing.Size(100, 21);
            this.toolStripStatusLblRevCnt.Text = "R:0";
            this.toolStripStatusLblRevCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripStatusLblCom
            // 
            this.toolStripStatusLblCom.AutoSize = false;
            this.toolStripStatusLblCom.Name = "toolStripStatusLblCom";
            this.toolStripStatusLblCom.Size = new System.Drawing.Size(130, 21);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
            // 
            // statusStripCom
            // 
            this.statusStripCom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStripCom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolStripStatusLabel1,
            this.toolStripSeparator2,
            this.toolStripStatusLblSendCnt,
            this.toolStripSeparator3,
            this.toolStripStatusLblRevCnt,
            this.toolStripSeparator4,
            this.toolStripStatusLblCom,
            this.toolStripSeparator5});
            this.statusStripCom.Location = new System.Drawing.Point(0, 739);
            this.statusStripCom.Name = "statusStripCom";
            this.statusStripCom.Size = new System.Drawing.Size(998, 26);
            this.statusStripCom.SizingGrip = false;
            this.statusStripCom.TabIndex = 33;
            this.statusStripCom.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.AutoSize = false;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownButtonWidth = 20;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemReset,
            this.toolStripMenuItemCalculator,
            this.wewqToolStripMenuItem});
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(60, 24);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // toolStripMenuItemReset
            // 
            this.toolStripMenuItemReset.Name = "toolStripMenuItemReset";
            this.toolStripMenuItemReset.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemReset.Text = "恢复默认";
            this.toolStripMenuItemReset.Click += new System.EventHandler(this.toolStripMenuItemReset_Click);
            // 
            // toolStripMenuItemCalculator
            // 
            this.toolStripMenuItemCalculator.Name = "toolStripMenuItemCalculator";
            this.toolStripMenuItemCalculator.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemCalculator.Text = "计算器";
            this.toolStripMenuItemCalculator.Click += new System.EventHandler(this.toolStripMenuItemCalculator_Click);
            // 
            // wewqToolStripMenuItem
            // 
            this.wewqToolStripMenuItem.Name = "wewqToolStripMenuItem";
            this.wewqToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wewqToolStripMenuItem.Text = "退出";
            this.wewqToolStripMenuItem.Click += new System.EventHandler(this.wewqToolStripMenuItem_Click);
            // 
            // toolStripStatusLblTime
            // 
            this.toolStripStatusLblTime.Name = "toolStripStatusLblTime";
            this.toolStripStatusLblTime.Size = new System.Drawing.Size(0, 21);
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Location = new System.Drawing.Point(6, 187);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(165, 49);
            this.btnOpenCom.TabIndex = 13;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.UseVisualStyleBackColor = true;
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "串口选择";
            // 
            // cbxParity
            // 
            this.cbxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(71, 156);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(100, 25);
            this.cbxParity.TabIndex = 6;
            this.cbxParity.SelectedIndexChanged += new System.EventHandler(this.cbxParity_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "波特率";
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(71, 125);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(100, 25);
            this.cbxDataBits.TabIndex = 5;
            this.cbxDataBits.SelectedIndexChanged += new System.EventHandler(this.cbxDataBits_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "停止位";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(71, 63);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(100, 25);
            this.cbxBaudRate.TabIndex = 3;
            this.cbxBaudRate.SelectedIndexChanged += new System.EventHandler(this.cbxBaudRate_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "数据位";
            // 
            // cbxComPort
            // 
            this.cbxComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxComPort.FormattingEnabled = true;
            this.cbxComPort.Location = new System.Drawing.Point(9, 32);
            this.cbxComPort.Name = "cbxComPort";
            this.cbxComPort.Size = new System.Drawing.Size(162, 25);
            this.cbxComPort.TabIndex = 2;
            this.cbxComPort.SelectedIndexChanged += new System.EventHandler(this.cbxComPort_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "奇偶校验";
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(71, 94);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(100, 25);
            this.cbxStopBits.TabIndex = 4;
            this.cbxStopBits.SelectedIndexChanged += new System.EventHandler(this.cbxStopBits_SelectedIndexChanged);
            // 
            // revCntTimer
            // 
            this.revCntTimer.Tick += new System.EventHandler(this.revCntTimer_Tick);
            // 
            // sendCntTimer
            // 
            this.sendCntTimer.Tick += new System.EventHandler(this.sendCntTimer_Tick);
            // 
            // tbxRecvData
            // 
            this.tbxRecvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxRecvData.BackColor = System.Drawing.Color.Black;
            this.tbxRecvData.ForeColor = System.Drawing.Color.Chartreuse;
            this.tbxRecvData.Location = new System.Drawing.Point(0, 0);
            this.tbxRecvData.Multiline = true;
            this.tbxRecvData.Name = "tbxRecvData";
            this.tbxRecvData.ReadOnly = true;
            this.tbxRecvData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxRecvData.Size = new System.Drawing.Size(816, 567);
            this.tbxRecvData.TabIndex = 37;
            // 
            // showCurTimer
            // 
            this.showCurTimer.Tick += new System.EventHandler(this.showCurTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckbShowTime);
            this.panel1.Controls.Add(this.ckbChangWinColor);
            this.panel1.Controls.Add(this.cbxComPort);
            this.panel1.Controls.Add(this.btnOpenCom);
            this.panel1.Controls.Add(this.btnSaveWindow);
            this.panel1.Controls.Add(this.ckbDTR);
            this.panel1.Controls.Add(this.btnClearWindow);
            this.panel1.Controls.Add(this.ckbRTS);
            this.panel1.Controls.Add(this.ckbRevHex);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbxParity);
            this.panel1.Controls.Add(this.cbxStopBits);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbxDataBits);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbxBaudRate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(815, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 567);
            this.panel1.TabIndex = 38;
            // 
            // ckbShowTime
            // 
            this.ckbShowTime.AutoSize = true;
            this.ckbShowTime.Location = new System.Drawing.Point(9, 333);
            this.ckbShowTime.Name = "ckbShowTime";
            this.ckbShowTime.Size = new System.Drawing.Size(155, 21);
            this.ckbShowTime.TabIndex = 38;
            this.ckbShowTime.Text = "时间戳(以换行回车断帧)";
            this.ckbShowTime.UseVisualStyleBackColor = true;
            // 
            // ckbChangWinColor
            // 
            this.ckbChangWinColor.AutoSize = true;
            this.ckbChangWinColor.Location = new System.Drawing.Point(96, 289);
            this.ckbChangWinColor.Name = "ckbChangWinColor";
            this.ckbChangWinColor.Size = new System.Drawing.Size(75, 21);
            this.ckbChangWinColor.TabIndex = 37;
            this.ckbChangWinColor.Text = "白底黑字";
            this.ckbChangWinColor.UseVisualStyleBackColor = true;
            this.ckbChangWinColor.CheckedChanged += new System.EventHandler(this.ckbChangWinColor_CheckedChanged);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(998, 765);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbxRecvData);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStripCom);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XCOM V2.0";
            this.Load += new System.EventHandler(this.MainFrame_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.singleTab.ResumeLayout(false);
            this.singleTab.PerformLayout();
            this.multiTab.ResumeLayout(false);
            this.multiTab.PerformLayout();
            this.panelMultiSend1.ResumeLayout(false);
            this.panelMultiSend1.PerformLayout();
            this.panelMultiSend2.ResumeLayout(false);
            this.panelMultiSend2.PerformLayout();
            this.panelMultiSend3.ResumeLayout(false);
            this.panelMultiSend3.PerformLayout();
            this.panelMultiSend4.ResumeLayout(false);
            this.panelMultiSend4.PerformLayout();
            this.transportProtocolTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStripCom.ResumeLayout(false);
            this.statusStripCom.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void initOpenSerialPort()
        {
            if (this.sp.IsOpen)
            {
                this.closeSerialPort(true);
            }
            else if (!this.isExistSerialPort)
            {
                MessageBox.Show("没有搜索到串口！", "错误提示");
            }
            else
            {
                this.SetPortProperty();
                this.openSerialPort(true);
                if (this.sp.IsOpen)
                {
                    this.getCurrentInfoLineStatus(this.sp);
                }
            }
        }

        private void isRalateKeyBoard(bool cmd)
        {
            if (cmd)
            {
                base.KeyPreview = true;
            }
            else
            {
                base.KeyPreview = false;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Process.Start("http://openedv.com/posts/list/22994.htm");
        }

        private void lblLinkImg_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.openedv.com/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.openedv.com/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.alientek.com/");
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveAllSerialPortSettings();
        }

        private void MainFrame_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            switch (keyCode)
            {
                case Keys.D0:
                    goto TR_0001;

                case Keys.D1:
                    goto TR_0002;

                case Keys.D2:
                    goto TR_0003;

                case Keys.D3:
                    goto TR_0004;

                case Keys.D4:
                    goto TR_0005;

                case Keys.D5:
                    goto TR_0006;

                case Keys.D6:
                    goto TR_0007;

                case Keys.D7:
                    goto TR_0008;

                case Keys.D8:
                    goto TR_0009;

                case Keys.D9:
                    break;

                default:
                    switch (keyCode)
                    {
                        case Keys.NumPad0:
                            goto TR_0001;

                        case Keys.NumPad1:
                            goto TR_0002;

                        case Keys.NumPad2:
                            goto TR_0003;

                        case Keys.NumPad3:
                            goto TR_0004;

                        case Keys.NumPad4:
                            goto TR_0005;

                        case Keys.NumPad5:
                            goto TR_0006;

                        case Keys.NumPad6:
                            goto TR_0007;

                        case Keys.NumPad7:
                            goto TR_0008;

                        case Keys.NumPad8:
                            goto TR_0009;

                        case Keys.NumPad9:
                            break;

                        default:
                            return;
                    }
                    break;
            }
            this.btnMultiSend10.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend10.Text);
            return;
        TR_0001:
            this.btnMultiSend1.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend1.Text);
            return;
        TR_0002:
            this.btnMultiSend2.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend2.Text);
            return;
        TR_0003:
            this.btnMultiSend3.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend3.Text);
            return;
        TR_0004:
            this.btnMultiSend4.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend4.Text);
            return;
        TR_0005:
            this.btnMultiSend5.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend5.Text);
            return;
        TR_0006:
            this.btnMultiSend6.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend6.Text);
            return;
        TR_0007:
            this.btnMultiSend7.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend7.Text);
            return;
        TR_0008:
            this.btnMultiSend8.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend8.Text);
            return;
        TR_0009:
            this.btnMultiSend9.Focus();
            this.sendMultiSerialPortData(this.tbxMultiSend9.Text);
        }

        private void MainFrame_Load_1(object sender, EventArgs e)
        {
            Stream manifestResourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("MySSCOM.img.window.ico");
            //base.Icon = new Icon(manifestResourceStream);
            base.FormClosing += new FormClosingEventHandler(this.MainFrame_FormClosing);
            this.initAllKeyBoardEvent();
            base.KeyDown += new KeyEventHandler(this.MainFrame_KeyDown);
            base.KeyPreview = false;
            this.tbxSendData.KeyDown += new KeyEventHandler(this.tbxSendData_KeyDown);
            this.sp.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            this.updateText = new UpdateTextEventHandler(this.UpdateTextBox);
            updateProgBarSendFile = new UpdateProgBarSendFileEventHandler(this.updateProgBar);
            this.MonitorCom = new MonitorSerialPortChangeEventHandler(this.monitorSerialPortChange);
            this.sendTransportProtocolDelegate = new SendTransportProtocolEventHandler(this.sendTransportProtocol);
            this.transportProtocolAutoSendDelegate = new CloseTransportProtocolAutoSendEventHandler(this.closeTransportProtocolAutoSend);
            this.tbxSendPeriod.TextChanged += new EventHandler(this.tbxSendPeriod_TextChanged);
            this.tbxMutilSendPeriod.TextChanged += new EventHandler(this.tbxMutilSendPeriod_TextChanged);
            this.tbxSendData.KeyPress += new KeyPressEventHandler(this.tbxSendData_KeyPress);
            this.tbxTransportProtocolSendData.KeyPress += new KeyPressEventHandler(this.tbxTransportProtocolSendData_KeyPress);
            this.cbxBaudRate.KeyPress += new KeyPressEventHandler(this.cbxBaudRate_KeyPress);
            this.cbxComPort.MouseClick += new MouseEventHandler(this.cbxComPort_MouseClick);
            this.sendTimer.Elapsed += new ElapsedEventHandler(this.timerSend);
            this.sendTimer.Interval = Convert.ToInt32(this.tbxSendPeriod.Text.Trim());
            this.sendTimer.AutoReset = true;
            this.sendMultiTimer.Elapsed += new ElapsedEventHandler(this.sendMultiTimer_Elapsed);
            this.sendTimer.Interval = Convert.ToInt32(this.tbxMutilSendPeriod.Text);
            this.sendTimer.AutoReset = true;
            this.transportSendTimer.Elapsed += new ElapsedEventHandler(this.transportSendTimer_Elapsed);
            this.transportSendTimer.Interval = Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text);
            this.transportSendTimer.AutoReset = true;
            this.transportProtocolRevTimeOutTimer.Elapsed += new ElapsedEventHandler(this.transportProtocolRevTimeOutTimer_Elapsed);
            this.revCntTimer.Interval = 200;
            this.revCntTimer.Start();
            this.sendCntTimer.Interval = 200;
            this.sendCntTimer.Start();
            this.showCurTimer.Interval = 0x3e8;
            this.showCurTimer.Start();
            this.toolTipAll.AutoPopDelay = 0x1388;
            this.toolTipAll.InitialDelay = 500;
            this.toolTipAll.ReshowDelay = 500;
            this.toolTipAll.ShowAlways = true;
            this.toolTipAll.IsBalloon = true;
            this.toolTipAll.SetToolTip(this.btnSend, "按Ctrl+Enter发送");
            this.toolTipAll.SetToolTip(this.ckbMultiAutoSend, "请先选择需要循环发送的条目，再勾选此项！");
            this.toolTipAll.SetToolTip(this.ckbRelateKeyBoard, "发送按钮 0-9->数字键盘0-9");
            this.toolTipAll.SetToolTip(this.ckbSendNewLine, "勾选此项，发送的时候将自动加入回车换行！");
            this.toolTipAll.SetToolTip(this.ckbMultiSendNewLine, "勾选此项，发送的时候将自动加入回车换行！");
            this.addAllCbkMultiSendToList();
            this.searchAvailableSerialPort();
            this.cbxBaudRate.Items.Add("自定义");
            this.cbxBaudRate.Items.Add("1382400");
            this.cbxBaudRate.Items.Add("921600");
            this.cbxBaudRate.Items.Add("460800");
            this.cbxBaudRate.Items.Add("256000");
            this.cbxBaudRate.Items.Add("230400");
            this.cbxBaudRate.Items.Add("128000");
            this.cbxBaudRate.Items.Add("115200");
            this.cbxBaudRate.Items.Add("76800");
            this.cbxBaudRate.Items.Add("57600");
            this.cbxBaudRate.Items.Add("43000");
            this.cbxBaudRate.Items.Add("38400");
            this.cbxBaudRate.Items.Add("19200");
            this.cbxBaudRate.Items.Add("14400");
            this.cbxBaudRate.Items.Add("9600");
            this.cbxBaudRate.Items.Add("4800");
            this.cbxBaudRate.Items.Add("2400");
            this.cbxBaudRate.Items.Add("1200");
            this.cbxStopBits.Items.Add("1");
            this.cbxStopBits.Items.Add("1.5");
            this.cbxStopBits.Items.Add("2");
            this.cbxDataBits.Items.Add("8");
            this.cbxDataBits.Items.Add("7");
            this.cbxDataBits.Items.Add("6");
            this.cbxDataBits.Items.Add("5");
            this.cbxParity.Items.Add("无");
            this.cbxParity.Items.Add("奇校验");
            this.cbxParity.Items.Add("偶校验");
            this.initAllSerialPortSettings();
            this.initOpenSerialPort();
        }

        private void monitorSerialPortChange()
        {
            string[] strs = this.searchSerialPort();
            if (strs != null)
            {
                strs = this.proceStrForSerialPort(strs);
                ArrayList list = new ArrayList();
                list.AddRange(this.cbxComPort.Items);
                if (strs.Length < this.cbxComPort.Items.Count)
                {
                    this.currentSerialPortNumber = strs.Length;
                    for (int i = 0; i < strs.Length; i++)
                    {
                        list.Remove(strs[i]);
                    }
                    if (strs.Length == 0)
                    {
                        this.isExistSerialPort = false;
                    }
                    if (this.cbxComPort.Text == ((string) list[0]))
                    {
                        this.closeSerialPort(true);
                    }
                    else
                    {
                        this.cbxComPort.Items.Remove((string) list[0]);
                    }
                }
                else if (strs.Length > this.currentSerialPortNumber)
                {
                    this.currentSerialPortNumber = strs.Length;
                    int index = 0;
                    while (true)
                    {
                        if (index >= strs.Length)
                        {
                            if (strs.Length > 0)
                            {
                                this.isExistSerialPort = true;
                            }
                            break;
                        }
                        if (!this.cbxComPort.Items.Contains(strs[index]))
                        {
                            this.cbxComPort.Items.Add(strs[index]);
                        }
                        index++;
                    }
                }
            }
        }

        private void MultiSendCurrentPageSetting(multiSendPage page)
        {
            if (this.multiSendPageList.Capacity > 0)
            {
                ((Panel) this.multiSendPageList[(int) this.multiSendPageCur]).Visible = false;
                if ((page == multiSendPage.first) || (page == multiSendPage.end))
                {
                    ((Panel) this.multiSendPageList[(int) page]).Visible = true;
                    this.multiSendPageCur = page;
                }
                else if (page == multiSendPage.next)
                {
                    this.multiSendPageCur = (multiSendPage) ((byte) ((((int) this.multiSendPageCur) + 1) % ((int) ((multiSendPage) 4))));
                    ((Panel) this.multiSendPageList[(int) this.multiSendPageCur]).Visible = true;
                }
                else if (page == multiSendPage.last)
                {
                    this.multiSendPageCur = (multiSendPage) ((byte) ((((int) this.multiSendPageCur) + 3) % ((int) ((multiSendPage) 4))));
                    ((Panel) this.multiSendPageList[(int) this.multiSendPageCur]).Visible = true;
                }
            }
        }

        public void multiSendEditInit()
        {
            this.MultiSendPageInit();
            strCkbMultiSendList = Settings.Default.StrCkbMultiSendList;
            strTbxMultiSendList = Settings.Default.StrTbxMultiSendList;
            strReMarkTbxMultiSendList = Settings.Default.StrReMarkTbxMultiSendList;
            for (int i = 0; i < strMultiSendArr.Length; i++)
            {
                strMultiSendArr[i] = "";
            }
            if (strCkbMultiSendList == null)
            {
                strCkbMultiSendList = new StringCollection();
                strCkbMultiSendList.AddRange(strMultiSendArr);
            }
            if (strTbxMultiSendList == null)
            {
                strTbxMultiSendList = new StringCollection();
                strTbxMultiSendList.AddRange(strMultiSendArr);
            }
            if (strReMarkTbxMultiSendList == null)
            {
                strReMarkTbxMultiSendList = new StringCollection();
                strReMarkTbxMultiSendList.AddRange(strMultiSendArr);
            }
            this.updateMultiSendInfoListToMainFrame();
        }

        private void MultiSendInfoFrame_FormClosing(object sender, EventArgs e)
        {
            this.updateMultiSendInfoFrameToMainFrame();
        }

        private void MultiSendPageInit()
        {
            this.multiSendPageList.Add(this.panelMultiSend1);
            this.multiSendPageList.Add(this.panelMultiSend2);
            this.multiSendPageList.Add(this.panelMultiSend3);
            this.multiSendPageList.Add(this.panelMultiSend4);
            this.multiSendPageCur = multiSendPage.first;
        }

        public void openSerialPort(bool isSetICon)
        {
            try
            {
                this.sp.Open();
                this.isSerailPortClose = false;
                if (isSetICon)
                {
                    this.btnOpenCom.Text = "关闭串口";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("打开串口失败!!!,或其它错误。\r\n请选择正确的串口或该串口被占用", "错误提示");
                this.closeSerialPort(true);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://eboard.taobao.com/index.htm?spm=2013.1.w5002-959568018.2.QA2Jus");
        }

        private string proceGetOpenSerialPortName(string spName)
        {
            string str = spName.Substring(0, spName.IndexOf(':'));
            if (str.IndexOf('-') > 0)
            {
                str = str.Substring(0, str.IndexOf('-'));
            }
            return str;
        }

        private string[] proceStrForSerialPort(string[] strs)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].IndexOf('(') > 0)
                {
                    list.Add(strs[i].Substring(strs[i].IndexOf('(') + 1, (strs[i].IndexOf(')') - strs[i].IndexOf('(')) - 1) + ":" + strs[i].Substring(0, strs[i].IndexOf('(')));
                }
            }
            string[] strArray = new string[list.Count];
            for (int j = 0; j < strArray.Length; j++)
            {
                strArray[j] = (string) list[j];
            }
            return strArray;
        }

        private void promptSendTaskFailInfo(int info)
        {
            int num = info;
            switch (num)
            {
                case 2:
                    MessageBox.Show("发送数据失败，数据未能成功发出！", "提示");
                    return;

                case 3:
                    break;

                case 4:
                    if (Convert.ToInt32(this.tbxTransportProtocolRetryCount.Text) == 0)
                    {
                        break;
                    }
                    MessageBox.Show("重发完成，设备无响应！", "提示");
                    return;

                default:
                    if (num != 8)
                    {
                        return;
                    }
                    if (Convert.ToInt32(this.tbxTransportProtocolRetryCount.Text) != 0)
                    {
                        MessageBox.Show("当前发送被终止!", "提示");
                    }
                    break;
            }
        }

        private void ReSize(object sender, EventArgs e)
        {
            float[] tag = (float[]) base.Tag;
            int num = 2;
            foreach (Control control in base.Controls)
            {
                Size size = base.Size;
                control.Left = (int) (size.Width * tag[num++]);
                Size size2 = base.Size;
                control.Top = (int) (size2.Height * tag[num++]);
                Size size3 = base.Size;
                Size size4 = (Size) control.Tag;
                control.Width = (int) ((((float) size3.Width) / tag[0]) * size4.Width);
                Size size5 = base.Size;
                Size size6 = (Size) control.Tag;
                control.Height = (int) ((((float) size5.Height) / tag[1]) * size6.Height);
            }
        }

        private void revCntTimer_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLblRevCnt.Text = "R:" + this.recieveCount.ToString();
            if (!this.isSerailPortClose && this.sp.IsOpen)
            {
                try
                {
                    this.getCurrentInfoLineStatus(this.sp);
                }
                catch (Exception)
                {
                    this.closeSerialPort(true);
                }
            }
        }

        private void saveAllSerialPortSettings()
        {
            Settings.Default.PortName = this.cbxComPort.Text;
            Settings.Default.BaudRate = this.cbxBaudRate.Text;
            Settings.Default.StopBits = this.cbxStopBits.Text;
            Settings.Default.DataBits = this.cbxDataBits.Text;
            Settings.Default.Parity = this.cbxParity.Text;
            Settings.Default.RevHex = this.ckbRevHex.Checked;
            Settings.Default.RTS = this.ckbRTS.Checked;
            Settings.Default.DTR = this.ckbDTR.Checked;
            Settings.Default.ChangWinColor = this.ckbChangWinColor.Checked;
            Settings.Default.ShowTimeCheckBox = this.ckbShowTime.Checked;
            Settings.Default.SendData = this.tbxSendData.Text;
            Settings.Default.TimeSend = this.ckbTimeSend.Checked;
            Settings.Default.SendPeriod = this.tbxSendPeriod.Text;
            Settings.Default.HexSend = this.ckbHexSend.Checked;
            Settings.Default.SendNewLine = this.ckbSendNewLine.Checked;
            Settings.Default.MultiSendNewLine = this.ckbMultiSendNewLine.Checked;
            Settings.Default.MultiHexSend = this.ckbMultiHexSend.Checked;
            Settings.Default.RelateKeyBoard = this.ckbRelateKeyBoard.Checked;
            Settings.Default.MultiAutoSend = this.ckbMultiAutoSend.Checked;
            Settings.Default.MutilSendPeriod = this.tbxMutilSendPeriod.Text;
            this.updateMultiSendInfoToList();
            Settings.Default.StrCkbMultiSendList = strCkbMultiSendList;
            Settings.Default.StrTbxMultiSendList = strTbxMultiSendList;
            Settings.Default.StrReMarkTbxMultiSendList = strReMarkTbxMultiSendList;
            Settings.Default.CkbTransportProtocolAutoNewLine = this.ckbTransportProtocolAutoNewLine.Checked;
            Settings.Default.CkbTransportProtocolDispOrigialData = this.ckbTransportProtocolDispOrigialData.Checked;
            Settings.Default.TbxTransportProtocolSlaveDeviceAddr = this.tbxTransportProtocolSlaveDeviceAddr.Text;
            Settings.Default.TbxTransportProtocolSendFunctionType = this.tbxTransportProtocolSendFunctionType.Text;
            Settings.Default.TbxTransportProtocolSendPeriod = this.tbxTransportProtocolSendPeriod.Text;
            Settings.Default.CkbTransportProtocolAutoSend = this.ckbTransportProtocolAutoSend.Checked;
            Settings.Default.TbxTransportProtocolRetryCount = this.tbxTransportProtocolRetryCount.Text;
            Settings.Default.CbxTransportProtocolChecksum = this.cbxTransportProtocolChecksum.Text;
            Settings.Default.TransportProtocolSendDataTextBox = this.tbxTransportProtocolSendData.Text;
            Settings.Default.TransportProtocolMaxDataLengthTextBox = this.tbxTransportProtocolMaxDataLength.Text;
            Settings.Default.Save();
        }

        private void searchAvailableSerialPort()
        {
            string[] strs = this.searchSerialPort();
            if (strs != null)
            {
                if (strs.Length > 0)
                {
                    strs = this.proceStrForSerialPort(strs);
                }
                for (int i = 0; i < strs.Length; i++)
                {
                    this.cbxComPort.Items.Add(strs[i]);
                }
            }
            if (this.cbxComPort.Items.Count == 0)
            {
                this.isExistSerialPort = false;
            }
            else
            {
                this.isExistSerialPort = true;
            }
        }

        private string[] searchSerialPort()
        {
            string[] portNames = new string[0];//SerialPortManager.MulGetHardwareInfo(SerialPortManager.HardwareEnum.Win32_PnPEntity, "Name");
            if (portNames == null)
            {
                portNames = SerialPort.GetPortNames();
                if (portNames == null)
                {
                    return null;
                }
                for (int i = 0; i < portNames.Length; i++)
                {
                    portNames[i] = "USB-SERIAL(" + portNames[i] + ")";
                }
            }
            return portNames;
        }

        private void sendCntTimer_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLblSendCnt.Text = "S:" + this.sendCount.ToString();
        }

        private void sendFile()
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
                            this.sendCount = sendCount+ (ulong)count;
                            try
                            {
                                this.sp.Write(buffer, 0, count);
                                object[] args = new object[] { (int) (((num2 * 1.0) / ((double) length)) * 100.0) };
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

        private void sendMultiSerialPortData(string data)
        {
            if (this.isMultiSendNewLine)
            {
                if (this.isMultiHexSend)
                {
                    this.sendSerailPortDataByHex(data + " 0D 0A");
                }
                else
                {
                    this.sendSerailPortData(data + "\r\n");
                }
            }
            else if (this.isMultiHexSend)
            {
                this.sendSerailPortDataByHex(data);
            }
            else
            {
                this.sendSerailPortData(data);
            }
        }

        private void sendMultiTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool flag = false;
            if (!this.sp.IsOpen)
            {
                this.sendTimer.Stop();
            }
            else
            {
                int currentMultiSend = this.currentMultiSend;
                while (true)
                {
                    if (currentMultiSend < this.ckbMultiSendStatus.Length)
                    {
                        if (this.ckbMultiSendStatus[currentMultiSend] <= 0)
                        {
                            currentMultiSend++;
                            continue;
                        }
                        this.sendMultiSerialPortData(((TextBox) this.ckbMultiSendTbxList[currentMultiSend]).Text);
                        flag = true;
                        this.currentMultiSend = currentMultiSend + 1;
                    }
                    if (flag)
                    {
                        break;
                    }
                    this.currentMultiSend = 0;
                    return;
                }
            }
        }

        private void sendSerailPortData(string data)
        {
            if (this.sp.IsOpen)
            {
                try
                {
                    byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(data);
                    int length = bytes.Length;
                    this.sendCount += (ulong)length;
                    this.sp.Write(bytes, 0, length);
                }
                catch (Exception)
                {
                    MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                    this.closeSerialPort(true);
                }
            }
            else
            {
                if (!this.isSerailPortClose)
                {
                    this.openSerialPort(false);
                }
                if (this.sp.IsOpen)
                {
                    try
                    {
                        byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(data);
                        int length = bytes.Length;
                        this.sendCount += (ulong)length;
                        this.sp.Write(bytes, 0, length);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                        this.closeSerialPort(true);
                    }
                }
            }
        }

        private void sendSerailPortDataByHex(string str)
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
                        if (this.sp.IsOpen)
                        {
                            try
                            {
                                this.sp.Write(buffer, 0, buffer.Length);
                                this.sendCount += (ulong)buffer.Length;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                                this.closeSerialPort(true);
                            }
                        }
                        else
                        {
                            if (!this.isSerailPortClose)
                            {
                                this.openSerialPort(false);
                            }
                            if (!this.sp.IsOpen)
                            {
                                MessageBox.Show("串口未打开，请先打开串口！", "错误提示");
                            }
                            else
                            {
                                try
                                {
                                    this.sp.Write(buffer, 0, buffer.Length);
                                    this.sendCount += (ulong)buffer.Length;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                                    this.closeSerialPort(true);
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

        private void sendSingleData(string data)
        {
            if (this.isSendNewLine)
            {
                if (this.isHexSend)
                {
                    this.sendSerailPortDataByHex(data + " 0D 0A");
                }
                else
                {
                    this.sendSerailPortData(data + "\r\n");
                }
            }
            else if (this.isHexSend)
            {
                this.sendSerailPortDataByHex(data);
            }
            else
            {
                this.sendSerailPortData(data);
            }
        }

        private void sendTransportProtocol()
        {
            this.updateTransportProtocolUItoObj();
            byte[] buffer = this.manageTransportProtocol.ConvertTransportProtocolToByte();
            this.sendTransportProtocolByteData(buffer);
        }

        private void sendTransportProtocolByteData(byte[] buffer)
        {
            if (this.sp.IsOpen)
            {
                try
                {
                    this.sp.Write(buffer, 0, buffer.Length);
                    this.sendCount += (ulong)buffer.Length;
                    this.manageTransportProtocol.IsSendTransportProtocolSuccess = true;
                    this.manageTransportProtocol.IsRevTransportProtocolSuccess = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("发送数据时发生错误, 串口将被关闭！", "错误提示");
                    this.closeSerialPort(true);
                    this.manageTransportProtocol.IsSendTransportProtocolSuccess = false;
                }
            }
            else
            {
                if (!this.isSerailPortClose)
                {
                    this.openSerialPort(false);
                }
                if (!this.sp.IsOpen)
                {
                    MessageBox.Show("串口未打开，请先打开串口！", "错误提示");
                    this.manageTransportProtocol.IsSendTransportProtocolSuccess = false;
                }
                else
                {
                    try
                    {
                        this.sp.Write(buffer, 0, buffer.Length);
                        this.sendCount += (ulong)buffer.Length;
                        this.manageTransportProtocol.IsSendTransportProtocolSuccess = true;
                        this.manageTransportProtocol.IsRevTransportProtocolSuccess = false;
                    }
                    catch (Exception)
                    {
                        this.closeSerialPort(true);
                        this.manageTransportProtocol.IsSendTransportProtocolSuccess = false;
                    }
                }
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!this.isListening && !this.isSerailPortClose)
            {
                try
                {
                    this.transportProtocolRevTimeOutTimer.Stop();
                    this.isListening = true;
                    string s = string.Empty;
                    int count = 0;
                    if (!this.isHexRevDisp)
                    {
                        s = this.sp.ReadExisting();
                        count = this.sp.Encoding.GetBytes(s).Length;
                    }
                    else
                    {
                        byte[] buffer = new byte[this.sp.BytesToRead];
                        count = this.sp.Read(buffer, 0, buffer.Length);
                        if (this.manageTransportProtocol.IsEnableTransportProtocol)
                        {
                            byte[] dst = new byte[count];
                            Buffer.BlockCopy(buffer, 0, dst, 0, count);
                            this.transportProtocolRevList.AddRange(dst);
                            this.transportProtocolRevTimeOutTimer.Interval = (0x4e200 / this.sp.BaudRate) + 2;
                            this.transportProtocolRevTimeOutTimer.Start();
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            int index = 0;
                            while (true)
                            {
                                if (index >= count)
                                {
                                    s = builder.ToString();
                                    break;
                                }
                                builder.Append(Convert.ToString(buffer[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                                index++;
                            }
                        }
                    }
                    this.recieveCount += (ulong)count;
                    if (!this.isHexRevDisp)
                    {
                        s = Utils.StrToHex(s);
                    }
                    base.Invoke(this.updateText, new string[] { s });
                }
                finally
                {
                    this.isListening = false;
                }
            }
        }

        private void SetPortProperty()
        {
            this.sp.Encoding = Encoding.GetEncoding("GB18030");
            this.sp.PortName = (this.cbxComPort.Text == "") ? "XXXX" : this.proceGetOpenSerialPortName(this.cbxComPort.Text.Trim());
            if (this.cbxBaudRate.Text.Trim() != "自定义")
            {
                try
                {
                    this.sp.BaudRate = Convert.ToInt32(this.cbxBaudRate.Text.Trim());
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.ToString(), "异常");
                }
            }
            float num = Convert.ToSingle(this.cbxStopBits.Text.Trim());
            this.sp.StopBits = (num != 0f) ? ((num != 1.5) ? ((num != 1f) ? ((num != 2f) ? StopBits.One : StopBits.Two) : StopBits.One) : StopBits.OnePointFive) : StopBits.None;
            this.sp.DataBits = Convert.ToInt16(this.cbxDataBits.Text.Trim());
            string str = this.cbxParity.Text.Trim();
            this.sp.Parity = (str.CompareTo("无") != 0) ? ((str.CompareTo("奇校验") != 0) ? ((str.CompareTo("偶校验") != 0) ? Parity.None : Parity.Even) : Parity.Odd) : Parity.None;
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
            if (!this.ckbTransportProtocolAutoSend.Checked)
            {
                this.transportSendTimer.Stop();
            }
            else
            {
                this.tbxTransportProtocolSendPeriod.Enabled = false;
                this.transportSendTimer.Interval = Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text);
                this.transportSendTimer.Start();
                this.sendTransportProtocol();
            }
            this.sp.ReadTimeout = -1;
        }

        private void showCurTimer_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLblTime.Text = DateTime.Now.ToString("当前时间 HH:mm:ss");
        }

        private void tbxMultiSend1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '0'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '9'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '1'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '2'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '3'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '4'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '5'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '6'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '7'))
            {
                e.Handled = true;
            }
        }

        private void tbxMultiSend9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ckbRelateKeyBoard.Checked && (e.KeyChar == '8'))
            {
                e.Handled = true;
            }
        }

        private void tbxMutilSendPeriod_TextChanged(object sender, EventArgs e)
        {
            if (this.tbxMutilSendPeriod.Text != "1000")
            {
                decimal num;
                decimal.TryParse(this.tbxMutilSendPeriod.Text, out num);
                if (!(num != 0M))
                {
                    MessageBox.Show("请输入数字(1-1000000)!", "提示");
                    this.tbxMutilSendPeriod.Text = "1000";
                }
                else if (Convert.ToInt32(this.tbxMutilSendPeriod.Text.Trim()) > 0xf4240)
                {
                    MessageBox.Show("输入不能超过1000000!", "提示");
                    this.tbxMutilSendPeriod.Text = "1000";
                }
            }
        }

        private void tbxSendData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Enter))
            {
                this.tbxSendData.ReadOnly = true;
                this.sendSingleData(this.tbxSendData.Text);
            }
        }

        private void tbxSendData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.tbxSendData.ReadOnly)
            {
                this.tbxSendData.ReadOnly = false;
                e.KeyChar = '\0';
            }
            if (this.isHexSend)
            {
                char keyChar = e.KeyChar;
                if (((((keyChar < '0') || (keyChar > '9')) && (((keyChar < 'A') || (keyChar > 'F')) && (((keyChar < 'a') || (keyChar > 'f')) && (keyChar != ' ')))) && (keyChar != '\b')) && (keyChar != '\x0003'))
                {
                    MessageBox.Show("请输入正确的格式 0-9 a-f A-F 例如01 0d 0a", "提示");
                    e.KeyChar = '\0';
                }
            }
        }

        private void tbxSendPeriod_TextChanged(object sender, EventArgs e)
        {
            if (this.tbxSendPeriod.Text != "1000")
            {
                decimal num;
                decimal.TryParse(this.tbxSendPeriod.Text, out num);
                if (!(num != 0M))
                {
                    MessageBox.Show("请输入数字(1-1000000)!", "提示");
                    this.tbxSendPeriod.Text = "1000";
                }
                else if (Convert.ToInt32(this.tbxSendPeriod.Text.Trim()) > 0xf4240)
                {
                    MessageBox.Show("输入不能超过1000000!", "提示");
                    this.tbxSendPeriod.Text = "1000";
                }
            }
        }

        private void tbxTransportProtocolMaxDataLength_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(this.tbxTransportProtocolMaxDataLength.Text.Trim());
            }
            catch
            {
                num = -1;
            }
            if ((num < 0) || (num > 0xff))
            {
                MessageBox.Show("请输入数字(0-255)!", "提示");
                this.tbxTransportProtocolMaxDataLength.Text = "255";
            }
        }

        private void tbxTransportProtocolRetryCount_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(this.tbxTransportProtocolRetryCount.Text.Trim());
            }
            catch
            {
                num = -1;
            }
            if ((num < 0) || (num > 0xff))
            {
                MessageBox.Show("请输入数字(0-255)!", "提示");
                this.tbxTransportProtocolRetryCount.Text = "10";
            }
        }

        private void tbxTransportProtocolSendData_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (((((keyChar < '0') || (keyChar > '9')) && (((keyChar < 'A') || (keyChar > 'F')) && (((keyChar < 'a') || (keyChar > 'f')) && (keyChar != ' ')))) && (keyChar != '\b')) && (keyChar != '\x0003'))
            {
                MessageBox.Show("请输入正确的格式 0-9 a-f A-F 例如01 0d 0a", "提示");
                e.KeyChar = '\0';
            }
        }

        private void tbxTransportProtocolSendFunctionType_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(this.tbxTransportProtocolSendFunctionType.Text, "^[0-9A-Fa-f]+$") || (this.tbxTransportProtocolSendFunctionType.Text.Length > 2))
            {
                MessageBox.Show("请输入16进制(00-FF)！", "提示");
                this.tbxTransportProtocolSendFunctionType.Text = "01";
            }
        }

        private void tbxTransportProtocolSendResponseTime_TextChanged(object sender, EventArgs e)
        {
            if (this.tbxTransportProtocolSendPeriod.Text != "1000")
            {
                decimal num;
                decimal.TryParse(this.tbxTransportProtocolSendPeriod.Text, out num);
                if (!(num != 0M))
                {
                    MessageBox.Show("请输入数字(1-1000000)!", "提示");
                    this.tbxTransportProtocolSendPeriod.Text = "1000";
                }
                else if (Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text.Trim()) > 0xf4240)
                {
                    MessageBox.Show("输入不能超过1000000!", "提示");
                    this.tbxTransportProtocolSendPeriod.Text = "1000";
                }
            }
        }

        private void tbxTransportProtocolSlaveDeviceAddr_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(this.tbxTransportProtocolSlaveDeviceAddr.Text, "^[0-9A-Fa-f]+$") || (this.tbxTransportProtocolSlaveDeviceAddr.Text.Length > 2))
            {
                MessageBox.Show("请输入16进制(00-FF)！", "提示");
                this.tbxTransportProtocolSlaveDeviceAddr.Text = "01";
            }
        }

        private void timerSend(object source, ElapsedEventArgs e)
        {
            if (this.sp.IsOpen)
            {
                this.sendSingleData(this.tbxSendData.Text);
            }
            else
            {
                this.sendTimer.Stop();
            }
        }

        private void toolStripMenuItemCalculator_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                FileName = @"C:\WINDOWS\system32\calc.exe"
            };
            Process.Start(startInfo);
        }

        private void toolStripMenuItemReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要恢复默认设置吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
                if (this.sp.IsOpen)
                {
                    this.closeSerialPort(true);
                }
                this.isRuningReset = true;
                Settings.Default.Reset();
                this.initAllSerialPortSettings();
                this.SetPortProperty();
                this.isRuningReset = false;
            }
        }

        private void transportProtocolAutoRetryTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!this.sp.IsOpen)
            {
                this.transportProtocolAutoRetryTimer.Stop();
                this.manageTransportProtocol.RetryCount = 0;
                this.manageTransportProtocol.IsAbortSend = true;
            }
            else if (Convert.ToInt32(this.tbxTransportProtocolRetryCount.Text) == 0)
            {
                this.transportProtocolAutoRetryTimer.Stop();
                this.manageTransportProtocol.RetryCount = 0;
                this.manageTransportProtocol.IsCompleteRetry = true;
            }
            else
            {
                this.manageTransportProtocol.SendSequence = this.manageTransportProtocol.OriginalFrameSequence;
                this.manageTransportProtocol.RetryCount++;
                base.Invoke(this.sendTransportProtocolDelegate);
                if (this.manageTransportProtocol.RetryCount >= Convert.ToInt32(this.tbxTransportProtocolRetryCount.Text))
                {
                    this.transportProtocolAutoRetryTimer.Stop();
                    this.manageTransportProtocol.RetryCount = 0;
                    this.manageTransportProtocol.IsCompleteRetry = true;
                }
            }
        }

        private void transportProtocolAutoSendFailProcess()
        {
            this.enableTransportProtocolWidget();
            this.ckbTransportProtocolAutoSend.Checked = false;
        }

        public void transportProtocolInit()
        {
            this.manageTransportProtocol.RevTransportProtocol = this.revTransportProtocol;
            this.manageTransportProtocol.SendTransportProtocol = this.outTransportProtocol;
            this.manageTransportProtocol.IsAutoNewLine = this.ckbTransportProtocolAutoNewLine.Checked;
            this.manageTransportProtocol.IsDispOriginalData = this.ckbTransportProtocolDispOrigialData.Checked;
            this.transportProtocolAutoRetryTimer.Elapsed += new ElapsedEventHandler(this.transportProtocolAutoRetryTimer_Elapsed);
            this.transportProtocolAutoRetryTimer.Interval = Convert.ToInt32(this.tbxTransportProtocolSendPeriod.Text);
            this.transportProtocolAutoRetryTimer.AutoReset = true;
            this.transportProtocolAutoSendFailProcessDelegate = new transportProtocolAutoSendFailProcessEventHandler(this.transportProtocolAutoSendFailProcess);
            this.transportProtocolUpdateFileProgressDelegate = new transportProtocolUpdateFileProgressEventHandler(this.transportProtocolUpdateFileProgress);
        }

        private void transportProtocolRevTimeOutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.transportProtocolRevTimeOutTimer.Stop();
            this.manageTransportProtocol.RevTransportProtocolBuf = this.transportProtocolRevList.ToArray();
            this.transportProtocolRevList.Clear();
            base.Invoke(this.updateText, new string[] { this.manageTransportProtocol.ConvertByteToTransportProtocol().SbData });
        }

        private void transportProtocolSendFile()
        {
            long length = 0L;
            long num2 = 0L;
            int count = 0;
            byte[] buffer = new byte[0x400];
            FileStream stream = null;
            this.manageTransportProtocol.IsStopSendFile = false;
            try
            {
                stream = new FileStream(this.manageTransportProtocol.SendFileName, FileMode.Open);
                length = stream.Length;
            }
            catch
            {
                MessageBox.Show("文件不存在，或被占用！", "提示");
                stream = null;
                this.manageTransportProtocol.IsStopSendFile = true;
            }
            this.sendFileTickDelt = Utils.GetMmSystemTime();
            while (!this.manageTransportProtocol.IsStopSendFile)
            {
                try
                {
                    count = stream.Read(buffer, 0, this.manageTransportProtocol.SendFileOneFrameDataLength);
                }
                catch (IOException exception)
                {
                    stream.Close();
                    this.manageTransportProtocol.IsAbortSend = true;
                    MessageBox.Show(exception.ToString(), "错误提示");
                }
                if (count == 0)
                {
                    this.manageTransportProtocol.IsStopSendFile = true;
                    num2 = 0L;
                }
                byte[] dst = new byte[count];
                Buffer.BlockCopy(buffer, 0, dst, 0, count);
                this.manageTransportProtocol.SendTransportProtocolBuf = dst;
                if (count == 0)
                {
                    this.manageTransportProtocol.SendTransportProtocolBuf = new byte[1];
                }
                this.outTransportProtocol.DataLength = count.ToString();
                this.manageTransportProtocol.IsReadSendFileOneFrameData = true;
                new Thread(new ThreadStart(this.excuteSendFileTask)) { Name = "传输协议发送任务线程" }.Start();
                object[] objArray = new object[] { (int) ((((num2 + count) * 1.0) / ((double) length)) * 100.0) };
                base.Invoke(this.transportProtocolUpdateFileProgressDelegate, objArray);
                while (this.manageTransportProtocol.IsReadSendFileOneFrameData)
                {
                }
            }
            stream.Close();
            stream = null;
            this.manageTransportProtocol.IsHasSendTask = false;
            object[] args = new object[] { 0 };
            base.Invoke(this.transportProtocolUpdateFileProgressDelegate, args);
            base.Invoke(this.transportProtocolAutoSendFailProcessDelegate);
        }

        private void transportProtocolUpdateFileProgress(int pro)
        {
            if (this.isSendFileSuccess && (pro == 0))
            {
                this.isSendFileSuccess = false;
                MessageBox.Show("文件发送完毕,总共耗时:" + ((Utils.GetMmSystemTime() - this.sendFileTickDelt) / 0x2710L).ToString() + "ms", "提示");
            }
            this.progBarTransportProtocolSendFile.Value = pro;
            this.lblTransportProtocolProgSendFile.Text = pro.ToString() + "%";
            if (this.progBarTransportProtocolSendFile.Maximum == pro)
            {
                this.progBarTransportProtocolSendFile.Value = 0;
                this.progBarTransportProtocolSendFile.Text = "0%";
                this.enableTransportProtocolWidget();
                this.isSendFileSuccess = true;
            }
        }

        private void transportSendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.transportSendTimer.Stop();
            new Thread(new ThreadStart(this.excuteSendFileTask)) { Name = "传输协议发送任务线程" }.Start();
        }

        public void updateMultiSendInfoFrameToMainFrame()
        {
            if (isMultiSendInfoFrameSureClosed)
            {
                for (int i = 0; i < this.ckbMultiSendTbxList.Count; i++)
                {
                    ((CheckBox) this.ckbMultiSendCkbList[i]).Checked = strCkbMultiSendList[i] == "1";
                    ((TextBox) this.ckbMultiSendTbxList[i]).Text = strTbxMultiSendList[i];
                }
            }
        }

        public void updateMultiSendInfoListToMainFrame()
        {
            for (int i = 0; i < this.ckbMultiSendTbxList.Count; i++)
            {
                ((CheckBox) this.ckbMultiSendCkbList[i]).Checked = strCkbMultiSendList[i] == "1";
                ((TextBox) this.ckbMultiSendTbxList[i]).Text = strTbxMultiSendList[i];
            }
        }

        private void updateMultiSendInfoToList()
        {
            for (int i = 0; i < this.ckbMultiSendTbxList.Count; i++)
            {
                strCkbMultiSendList[i] = !((CheckBox) this.ckbMultiSendCkbList[i]).Checked ? "0" : "1";
                strTbxMultiSendList[i] = ((TextBox) this.ckbMultiSendTbxList[i]).Text;
            }
        }

        private void updateObjToTransportProtocolUI()
        {
            if (!this.revTransportProtocol.IsValid)
            {
                this.lblTransportProtocolResult.ForeColor = Color.Red;
                this.lblTransportProtocolResult.Text = this.revTransportProtocol.Result;
            }
            else
            {
                this.lblTransportProtocolMasterAddr.Text = this.revTransportProtocol.DeviceAddr;
                this.lblTransportProtocolRevFunctionType.Text = this.revTransportProtocol.FunctionType;
                this.lblTransportProtocolRevSequence.Text = this.revTransportProtocol.Sequence;
                this.lblTransportProtocolRevDataLength.Text = this.revTransportProtocol.DataLength;
                this.lblTransportProtocolRevChecksum.Text = this.revTransportProtocol.Checksum;
                this.lblTransportProtocolResult.ForeColor = Color.Black;
                this.lblTransportProtocolResult.Text = this.revTransportProtocol.Result;
            }
        }

        private void updateProgBar(int pro)
        {
            this.progBarSendFile.Value = pro;
            this.lblProgSendFile.Text = pro.ToString() + "%";
            if (this.progBarSendFile.Maximum == pro)
            {
                this.progBarSendFile.Value = 0;
                this.lblProgSendFile.Text = "0%";
                MessageBox.Show("文件发送完毕", "提示");
            }
        }

        private void UpdateTextBox(string revText)
        {
            if (this.manageTransportProtocol.IsEnableTransportProtocol)
            {
                this.updateObjToTransportProtocolUI();
            }
            if (!this.isHexRevDisp)
            {
                revText = Utils.HexToStr(revText);
            }
            this.sbRecvData.Append(revText);
            if (this.ckbShowTime.Checked)
            {
                revText = this.isHexRevDisp ? revText.Replace("0A", this.getHexSystemTime()) : revText.Replace("\r", this.getStrSystemTime());
            }
            this.tbxRecvData.AppendText(revText);
            this.tbxRecvData.SelectionStart = this.tbxRecvData.TextLength;
            this.tbxRecvData.ScrollToCaret();
        }

        private void updateTransportProtocolUItoObj()
        {
            this.outTransportProtocol.DeviceAddr = this.tbxTransportProtocolSlaveDeviceAddr.Text;
            this.outTransportProtocol.FunctionType = this.tbxTransportProtocolSendFunctionType.Text;
            if (this.manageTransportProtocol.RetryCount == 0)
            {
                this.manageTransportProtocol.SendSequence = (byte) (this.manageTransportProtocol.SendSequence + 1);
            }
            this.outTransportProtocol.Sequence = this.manageTransportProtocol.SendSequence.ToString();
            this.lblTransportProtocolSendSequence.Text = this.outTransportProtocol.Sequence;
            if (this.manageTransportProtocol.CurrentSendTaskType != 3)
            {
                StringBuilder builder = new StringBuilder();
                string[] strArray = this.tbxTransportProtocolSendData.Text.Split(new char[] { ' ' });
                int index = 0;
                while (true)
                {
                    if (index >= strArray.Length)
                    {
                        if (builder.Length > 510)
                        {
                            builder.Clear();
                            builder.Append(builder.ToString().Substring(0, 510));
                        }
                        if ((builder.Length % 2) != 0)
                        {
                            builder = builder.Insert(builder.Length - 1, "0");
                        }
                        string[] strArray2 = new string[builder.Length / 2];
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= strArray2.Length)
                            {
                                builder.Clear();
                                int num3 = 0;
                                while (true)
                                {
                                    if (num3 >= strArray2.Length)
                                    {
                                        this.outTransportProtocol.SbData = builder.ToString();
                                        this.outTransportProtocol.DataLength = strArray2.Length.ToString();
                                        builder.Clear();
                                        break;
                                    }
                                    builder.Append(strArray2[num3]);
                                    builder.Append(' ');
                                    num3++;
                                }
                                break;
                            }
                            strArray2[num2] = builder.ToString().Substring(num2 * 2, 2);
                            num2++;
                        }
                        break;
                    }
                    builder.Append(strArray[index]);
                    index++;
                }
            }
            this.lblTransportProtocolSendDataLength.Text = this.outTransportProtocol.DataLength;
            if (this.cbxTransportProtocolChecksum.SelectedIndex == 0)
            {
                this.outTransportProtocol.ChecksumMethod = 3;
            }
            else if (this.cbxTransportProtocolChecksum.SelectedIndex == 1)
            {
                this.outTransportProtocol.ChecksumMethod = 2;
            }
            else if (this.cbxTransportProtocolChecksum.SelectedIndex == 2)
            {
                this.outTransportProtocol.ChecksumMethod = 4;
            }
            else if (this.cbxTransportProtocolChecksum.SelectedIndex == 3)
            {
                this.outTransportProtocol.ChecksumMethod = 5;
            }
            if (this.manageTransportProtocol.RetryCount > 0)
            {
                string text = "第" + this.manageTransportProtocol.RetryCount.ToString() + "次重发完成...\r\n";
                this.tbxRecvData.AppendText(text);
                this.tbxRecvData.SelectionStart = this.tbxRecvData.TextLength;
                this.tbxRecvData.ScrollToCaret();
            }
        }

        private void wewqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x219)
            {
                int num = m.WParam.ToInt32();
                if (num != 7)
                {
                    if (num == 0x8000)
                    {
                        this.serialPortStatus = PORT_IN;
                    }
                }
                else if (this.serialPortStatus != PORT_IN)
                {
                    this.serialPortStatus = PORT_OUT;
                    base.Invoke(this.MonitorCom);
                }
                else
                {
                    this.serialPortStatus = PORT_OUT;
                    base.Invoke(this.MonitorCom);
                }
            }
            base.WndProc(ref m);
        }

        private delegate void CloseTransportProtocolAutoSendEventHandler();

        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_HDR
        {
            public uint dbch_size;
            public uint dbch_devicetype;
            public uint dbch_reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_PORT_Fixed
        {
            public uint dbcp_size;
            public uint dbcp_devicetype;
            public uint dbcp_reserved;
        }

        private delegate void MonitorSerialPortChangeEventHandler();

        private enum multiSendPage : byte
        {
            first = 0,
            last = 1,
            next = 2,
            end = 3
        }

        private delegate void SendTransportProtocolEventHandler();

        private delegate void transportProtocolAutoSendFailProcessEventHandler();

        private delegate void transportProtocolUpdateFileProgressEventHandler(int pro);

        private delegate void UpdateProgBarSendFileEventHandler(int pro);

        private delegate void UpdateTextEventHandler(string revText);
    }
}

