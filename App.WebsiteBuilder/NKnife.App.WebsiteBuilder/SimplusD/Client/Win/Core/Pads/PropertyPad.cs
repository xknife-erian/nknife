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

                ///ɾ����ǰ��  AutoLayoutPanel by lisuye on 2008��5��27��
                if (_autoLayoutPanel != null)
                {
                    this.Controls.Remove(_autoLayoutPanel);
                }

                _autoLayoutPanel = value;

                ///����µ�  AutoLayoutPanel by lisuye on 2008��5��27��
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

                ///ɾ����ǰ��
                if (_innerPropertyPanel != null)
                {
                    this.Controls.Remove(_innerPropertyPanel);
                }

                _innerPropertyPanel = value;

                ///����µ�
                if (_innerPropertyPanel != null)
                {
                    this.Controls.Add(_innerPropertyPanel);
                }

                this.ResumeLayout();
            }
        }
        //����������ˢ���¼� by lisuye on 2008��5��27��
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
        /// ����AutoLayoutPanel
        /// </summary>
        public void RefreshAutoPanel()
        {
            if (WorkbenchForm.MainForm.MainDockPanel.ActiveDocument == null)
            {
                ///����Ϊ�µ�AutoLayoutPanel
                InnerPropertyPanel = null;
                AutoLayoutPanel = null;
                return;
            }

            ///���⴦��Html�����
            if (Service.Workbench.ActiveWorkDocumentType == WorkDocumentType.HtmlDesigner)
            {
                BaseEditViewForm view = (BaseEditViewForm)WorkbenchForm.MainForm.MainDockPanel.ActiveDocument;

                ///����Ϊ�µ�InnerPropertyPanel by lisuye on 2008��5��27��
                AutoLayoutPanel = null;
                InnerPropertyPanel = view.PropertyPanel;
            }
            ///�����ĸ���IGetPropertiesForPanelable��ʾ by lisuye on 2008��5��27��
            else
            {
                ///���ݵ�ǰ��������ȡAutoLayoutPanel by lisuye on 2008��5��27��
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

                ///����Ϊ�µ�AutoLayoutPanel
                InnerPropertyPanel = null;
                AutoLayoutPanel = newAutoPanel;
                //Show();
            }
        }
        void newAutoPanel_Saved(object sender, ValueSaveEventArgs e)
        {
            ((PageXmlDocument)e.TargetObj).Save();
        }
        //���������������ƣ�ͼ�� by lisuye on 2008��5��27��
        public PropertyPad()
        {
            this.Text = StringParserService.Parse("${res:Pad.Property.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.view.property").GetHicon());
        }
    }
}
