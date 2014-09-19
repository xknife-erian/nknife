using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.Reflection;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SitePropertyForm : BaseEditViewForm
    {
        private string _id;
        public override string Id { get { return _id; } }

        #region 构造函数与控件一般设置

        public SitePropertyForm(string formId)
        {
            InitializeComponent();
            _stripTab.SelectPage(0);
            _stripTab.CurrentPageChanged += new EventHandler(_stripTab_CurrentPageChanged);
            this._id = formId;
        }

        void _stripTab_CurrentPageChanged(object sender, EventArgs e)
        {
            _panelList[_stripTab.CurrentPage.Text].PerformLayout();
        }

        private void InitializeComponent()
        {
            _stripTab = new StripTabControl();
            this.BuildStripTabPages();

            #region 控件一般性的添加与设置
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            this.Name = this.GetType().FullName;
            this.Text = StringParserService.Parse("${res:siteProperty.Name}");

            _saveButton = new Button();
            _cacelButton = new Button();
            Panel panel1;
            {//底部的Panel
                panel = new Panel();
                panel.Name = "bottomPanel";
                panel.Dock = DockStyle.Bottom;
                this.Controls.Add(panel);
                panel.Height = 45;
                panel.Paint += new PaintEventHandler(panel_Paint);
                #region button
                {
                    int y = 10;
                    Size buttonSize = new Size(90, 28);
                    _saveButton.Text = StringParserService.Parse("${res:siteProperty.btnSave}");
                    _saveButton.Size = buttonSize;
                    _saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                    int x1 = panel.Width - _saveButton.Width * 2 - 15 - 15;
                    _saveButton.Location = new Point(x1, y);
                    _saveButton.UseVisualStyleBackColor = true;



                    _cacelButton.Text = StringParserService.Parse("${res:siteProperty.btnReSet}");
                    _cacelButton.Size = buttonSize;
                    _cacelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                    int x2 = panel.Width - _cacelButton.Width - 15;
                    _cacelButton.Location = new Point(x2, y);
                    _cacelButton.UseVisualStyleBackColor = true;

                    panel.Controls.Add(_saveButton);
                    panel.Controls.Add(_cacelButton);
                }
                #endregion
            }
            {//主Panel 
                panel1 = new Panel();
                panel1.Name = "mainPanel";
                panel1.Dock = DockStyle.Fill;
                this.Controls.Add(panel1);
                panel1.BringToFront();
                this._stripTab.Dock = DockStyle.Fill;
                panel1.Controls.Add(_stripTab);
            }

            this.ResumeLayout(false);
            this.CancelButton = this._cacelButton;
            #endregion

            this._saveButton.Click += new EventHandler(SaveSiteProperty);
            this._cacelButton.Click += new EventHandler(this.CloseForm);
        }

        private IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Button _saveButton;
        private Button _cacelButton;
        private StripTabControl _stripTab;
        private Panel panel;
        #endregion

        //保存AutoPanel
        Dictionary<string, AutoLayoutPanel> _panelList = new Dictionary<string, AutoLayoutPanel>();

        SitePropertyXmlElement _sitePropertyEle;
        private List<Type> sitePropertyList = new List<Type>();
        private void BuildStripTabPages()
        {
            _sitePropertyEle = Service.Sdsite.CurrentDocument.SiteProperty;

            PropertyInfo[] propertys = _sitePropertyEle.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (PropertyInfo item in propertys)
            {
                if (item.GetGetMethod().ReturnType.BaseType == typeof(AnyXmlElement))
                {
                    StripTabPage page = new StripTabPage();
                    object obj = item.GetValue(_sitePropertyEle, null);

                    AutoLayoutPanel panel = AutoLayoutPanelEx.CreatePanel(typeof(PropertyPadAttribute), obj, false, true);
                    page.Controls.Add(panel);
                    panel.FillValue(new[] { obj });

                    panel.Dock = DockStyle.Fill;
                    page.Text = ResourceService.GetResourceText(string.Concat ("siteProperty.",item.Name));

                    this._stripTab.TabPages.Add(page);

                    //将新添加的项置入_panelList列表中，缓存起来
                    _panelList.Add(page.Text, panel);
                }
            }
        }

        void panel_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Black);
            e.Graphics.DrawLine(myPen, 0, 0, panel.Width, 0);
            myPen.Dispose();
            e.Graphics.Dispose();
        }

        void SaveSiteProperty(object sender, EventArgs e)
        {
            this.Save();
            DialogResult = DialogResult.OK;
        }

        private void CloseForm(object sender, EventArgs e)
        {
            //关系时释放AutoPanel
            this.Close();
        }

        public override bool IsModified
        {
            get
            {
                foreach (AutoLayoutPanel item in _panelList.Values)
                {
                    if (item.IsNewOpenForAddEvent)
                    {
                        return false;
                    }
                    else
                    {
                        if (item.IsModified)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public override bool Save()
        {
            foreach (AutoLayoutPanel item in _panelList.Values)
            {
                item.Save();
            }
            Service.Sdsite.CurrentDocument.Save(true);
            return true;
        }
        //为网站属性中的所有控件绑定MouseDown事件 by lisuye on 2008年6月4日
        void ProcessEvent(Control parent, MouseEventHandler handler)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.HasChildren)
                {
                    ProcessEvent(control, handler);
                }
                control.MouseDown += handler;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            MouseEventHandler handler = delegate
            {
                // 将是否是第一次加载的标识设为false主要是为了保存按钮的显示应用by lisuye on 2008年6月4日
                foreach (AutoLayoutPanel item in _panelList.Values)
                {
                    item.IsNewOpenForAddEvent = false;
                }
            };
            ProcessEvent(this, handler);
            base.OnLoad(e);
        }

    }
}
