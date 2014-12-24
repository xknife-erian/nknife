using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Kernel;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife.Views
{
    public class ControlPanelView : DockContent
    {
        private System.Windows.Forms.Button _StartTunnelButton;
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private bool _IsTunnelStarted;
        private readonly Tunnels _Tunnels = DI.Get<Tunnels>();

        #region 初始化
        public ControlPanelView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._StartTunnelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _StartTunnelButton
            // 
            this._StartTunnelButton.Location = new System.Drawing.Point(311, 368);
            this._StartTunnelButton.Name = "_StartTunnelButton";
            this._StartTunnelButton.Size = new System.Drawing.Size(106, 35);
            this._StartTunnelButton.TabIndex = 55;
            this._StartTunnelButton.Text = "启动";
            this._StartTunnelButton.UseVisualStyleBackColor = true;
            this._StartTunnelButton.Click += new System.EventHandler(this._StartTunnelButton_Click);
            // 
            // ControlPanelView
            // 
            this.ClientSize = new System.Drawing.Size(781, 543);
            this.Controls.Add(this._StartTunnelButton);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ControlPanelView";
            this.Text = "操作面板";
            this.ResumeLayout(false);

        }
        #endregion

        private void _StartTunnelButton_Click(object sender, EventArgs e)
        {
            if (_IsTunnelStarted)
            {
                _Tunnels.Stop();
                _StartTunnelButton.Text = "启动";
            }
            else
            {
                _Tunnels.Start();
                _StartTunnelButton.Text = "停止";
            }
            _IsTunnelStarted = !_IsTunnelStarted;
        }
    }
}
