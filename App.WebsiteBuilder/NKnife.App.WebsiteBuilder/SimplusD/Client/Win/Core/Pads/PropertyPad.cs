using System;
using System.Drawing;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class PropertyPad : PadContent
    {
        private AutoLayoutPanel _autoLayoutPanel;

        public AutoLayoutPanel AutoLayoutPanel
        {
            get { return _autoLayoutPanel; }
            set
            {
                if (_autoLayoutPanel == value)
                {
                    return;
                }

                this.SuspendLayout();

                ///删除以前的  AutoLayoutPanel by lisuye on 2008年5月27日
                if (_autoLayoutPanel != null)
                {
                    this.Controls.Remove(_autoLayoutPanel);
                }

                _autoLayoutPanel = value;

                ///添加新的  AutoLayoutPanel by lisuye on 2008年5月27日
                if (_autoLayoutPanel != null)
                {
                    this.Controls.Add(_autoLayoutPanel);
                }
                this.ResumeLayout();
            }
        }

        private Control _innerPropertyPanel;
        public Control InnerPropertyPanel
        {
            get { return _innerPropertyPanel; }
            set
            {
                if (_innerPropertyPanel == value)
                {
                    return;
                }

                this.SuspendLayout();

                ///删除以前的
                if (_innerPropertyPanel != null)
                {
                    this.Controls.Remove(_innerPropertyPanel);
                }

                _innerPropertyPanel = value;

                ///添加新的
                if (_innerPropertyPanel != null)
                {
                    this.Controls.Add(_innerPropertyPanel);
                }

                this.ResumeLayout();
            }
        }
        //绑定属性面板的刷新事件 by lisuye on 2008年5月27日
        private IGetPropertiesForPanelable _igetprops;
        public IGetPropertiesForPanelable IGetProps
        {
            get { return _igetprops; }
            set
            {
                if (_igetprops != null)
                {
                    _igetprops.PropertiesChanged -= new EventHandler(IGetProperties_PropertiesChanged);
                }
                _igetprops = value;
                if (_igetprops != null)
                {
                    _igetprops.PropertiesChanged += new EventHandler(IGetProperties_PropertiesChanged);
                }
            }
        }

        void IGetProperties_PropertiesChanged(object sender, EventArgs e)
        {
            RefreshAutoPanel();
        }

        /// <summary>
        /// 更新AutoLayoutPanel
        /// </summary>
        public void RefreshAutoPanel()
        {
            if (WorkbenchForm.MainForm.MainDockPanel.ActiveDocument == null)
            {
                ///设置为新的AutoLayoutPanel
                InnerPropertyPanel = null;
                AutoLayoutPanel = null;
                return;
            }

            ///特殊处理Html设计器
            if (Service.Workbench.ActiveWorkDocumentType == WorkDocumentType.HtmlDesigner)
            {
                BaseEditViewForm view = (BaseEditViewForm)WorkbenchForm.MainForm.MainDockPanel.ActiveDocument;

                ///设置为新的InnerPropertyPanel by lisuye on 2008年5月27日
                AutoLayoutPanel = null;
                InnerPropertyPanel = view.PropertyPanel;
            }
            ///其他的根据IGetPropertiesForPanelable显示 by lisuye on 2008年5月27日
            else
            {
                ///根据当前工作区获取AutoLayoutPanel by lisuye on 2008年5月27日
                IGetProps = WorkbenchForm.MainForm.MainDockPanel.ActiveDocument as IGetPropertiesForPanelable;
                AutoLayoutPanel newAutoPanel = null;
                if (_igetprops != null)
                {
                    object[] props = _igetprops.GetPropertiesForPanel();
                    if (props != null && props.Length > 0)
                    {
                        newAutoPanel = AutoLayoutPanelEx.CreatePanel(typeof(PropertyPadAttribute), props, true, false);
                        newAutoPanel.RealTimeSave = true;
                        newAutoPanel.FillValue(props);

                        if (Service.Workbench.ActiveWorkDocumentType == WorkDocumentType.None)
                        {
                            newAutoPanel.Saved += new EventHandler<ValueSaveEventArgs>(newAutoPanel_Saved);
                        }
                    }
                }

                ///设置为新的AutoLayoutPanel
                InnerPropertyPanel = null;
                AutoLayoutPanel = newAutoPanel;
                //Show();
            }
        }
        void newAutoPanel_Saved(object sender, ValueSaveEventArgs e)
        {
            ((PageXmlDocument)e.TargetObj).Save();
        }
        //设置属性面板的名称，图标 by lisuye on 2008年5月27日
        public PropertyPad()
        {
            this.Text = StringParserService.Parse("${res:Pad.Property.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.view.property").GetHicon());
        }
    }
}
