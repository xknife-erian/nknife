using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Collections;
using WeifenLuo.WinFormsUI.Docking;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    abstract public class MdiBaseViewForm : BaseViewForm
    {
        #region 字段与属性

        StatusStrip _statusStrip = new StatusStrip();
        ToolStripStatusLabel _statusLabel = new ToolStripStatusLabel();
        private bool _showStatusBar;
        /// <summary>
        /// 是否显示基于BaseForm的HelpTip的状态栏
        /// </summary>
        public bool ShowStatusBar
        {
            get { return _showStatusBar; }
            set
            {
                _showStatusBar = value;
                _statusStrip.Visible = _showStatusBar;
            }
        }
        XmlDocument _docForm = new XmlDocument();
        Dictionary<Control, string> _dicControlDataBindId = new Dictionary<Control, string>();
        Dictionary<string, string> _textResources = new Dictionary<string, string>();
        Dictionary<string, object> _objectResources = new Dictionary<string, object>();
        Dictionary<string, DataTable> _controlsDataTable = new Dictionary<string, DataTable>();
        List<ConditionData> _dynamicNodeList = new List<ConditionData>();
        Dictionary<string, List<string>> _dicResourcesKeyBuffer = new Dictionary<string, List<string>>();
        Dictionary<string, Image> _imgList = new Dictionary<string, Image>();
        List<XmlNode> _needProcessNode = new List<XmlNode>();
        /// <summary>
        /// 此容器为elementBind服务。
        /// key是elementBindGroup(没有则为空字符串);
        /// value仍然是个容器，value容器中的key是elementBindName，value是对应的控件节点数据
        /// </summary>
        Dictionary<string, Dictionary<string, ControlNodeData>> _dicElementBindGroup = new Dictionary<string, Dictionary<string, ControlNodeData>>();

        #endregion

        #region 构造函数

        public MdiBaseViewForm()
        {
            ///设置一些Form的基本属性
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyPreview = true;
        }

        #endregion

        #region 重写的方法

        protected override void OnLoad(EventArgs e)
        {
            RegisterForm();

            base.OnLoad(e);

            if (ShowStatusBar)
            {
                ///添加状态栏
                _statusStrip.GripStyle = ToolStripGripStyle.Visible;
                _statusStrip.SizingGrip = false;
                _statusStrip.Items.Add(_statusLabel);
                _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
                _statusLabel.Spring = true;

                this.Controls.Add(_statusStrip);
            }

            ///处理MyTip
            MouseEventHandler handler = delegate
            {
                //MyTip.HideTip();
            };
            ProcessEvent(this, handler);
        }

        const int WM_MBUTTONDOWN = 0x0207;
        const int WM_RBUTTONDOWN = 0x0204;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_XBUTTONDOWN = 0x020B;
        const int WM_KEYDOWN = 0x0100;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN
                || m.Msg == WM_MBUTTONDOWN
                || m.Msg == WM_LBUTTONDOWN
                || m.Msg == WM_XBUTTONDOWN
                || m.Msg == WM_KEYDOWN)
            {
                //MyTip.HideTip();
            }

            base.WndProc(ref m);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //MyTip.HideTip();

            base.OnKeyDown(e);
        }
        protected override void OnDeactivate(EventArgs e)
        {
            //MyTip.HideTip();
            base.OnDeactivate(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            //MyTip.HideTip();
            base.OnSizeChanged(e);
        }
        protected override void OnMove(EventArgs e)
        {
            //MyTip.HideTip();
            base.OnMove(e);
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 注册Form的控件信息
        /// </summary>
        void RegisterForm()
        {
            //在设计期跳过，以便让设计器正常打开
            if (this.DesignMode)
            {
                return;
            }

            ///先检查基类是否存在相应的配置
            string baseType = this.GetType().BaseType.Name;
           // string baseConfigForm = Path.Combine(PathService.CL_Dialog_Folder, baseType + ".xml");
            string configForm = Path.Combine(PathService.CL_Dialog_Folder, this.GetType().Name + ".xml");

            //if (File.Exists(baseConfigForm))
            //{
            //    XmlDocument baseDocForm = new XmlDocument();
            //    baseDocForm.Load(baseConfigForm);
            //    _docForm.Load(configForm);
            //    baseDocForm.DocumentElement.InnerXml+= _docForm.DocumentElement.InnerXml;
            //    ProcessXmlDocument(baseDocForm);
            //    ProcessXmlNodeResourceFinal();
            //}
            if (File.Exists(configForm))
            {
                //开始注册
                XmlDocument baseDocForm = new XmlDocument();
                baseDocForm.Load(configForm);
                ProcessXmlDocument(baseDocForm);
               // _docForm.Load(configForm);
                //ProcessXmlDocument(_docForm);
                ProcessXmlNodeResourceFinal();
            }
        }

        private void ProcessXmlDocument(XmlDocument docForm)
        {

            foreach (XmlNode node in docForm.DocumentElement.ChildNodes)
            {
                ///如果不是Element，则继续遍历下一个
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                XmlElement ele = (XmlElement)node;

                ///如果enabled属性为false，则继续遍历下一个
                XmlAttribute enabledAtt = ele.Attributes["enabled"];
                if (enabledAtt != null && enabledAtt.Value == "false")
                {
                    continue;
                }

                ///先处理其condition值
                ProcessXmlNodeCondition(ele);

                switch (ele.Name)
                {
                    case "databind":    ///数据绑定节点
                        {
                            ProcessXmlNodeDatabind(ele);
                            break;
                        }
                    case "form":
                    case "control":
                        {
                            ProcessXmlNodeControl(ele);
                            break;
                        }
                    case "text":
                        {
                            ///将text节点中的数据写入到TextResources容器中去
                            ProcessXmlNodeText(ele);
                            break;
                        }
                    case "object":
                        {
                            ProcessXmlNodeObject(ele);
                            break;
                        }
                    case "resource":
                        {
                            ProcessXmlNodeResource(ele);
                            break;
                        }
                    default:
                        throw new Exception("出现未知的节点名:" + node.Name);
                }
            }
        }

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

        private void ProcessXmlNodeResource(XmlElement node)
        {
            string file = node.Attributes["file"].Value;
            ///先检查公共的资源中是否已载入
            if (ResourceService.HasProcessFile(file))
            {
                string key = node.Attributes["key"].Value;
                //_imgList.Add(key, (Image)ResourceService.GetResource(key));
            }
            ///未在公共资源中载入的则在这里载入
            else
            {
                List<string> listKey;
                if (!_dicResourcesKeyBuffer.TryGetValue(file, out listKey))
                {
                    listKey = new List<string>();
                    _dicResourcesKeyBuffer.Add(file, listKey);
                }
                listKey.Add(node.Attributes["key"].Value);
            }
        }

        void ProcessXmlNodeResourceFinal()
        {
            foreach (KeyValuePair<string, List<string>> keyvaluepair in _dicResourcesKeyBuffer)
            {
                using (IResourceReader reader = new ResourceReader(
                    Path.Combine(PathService.CL_Resources_Folder, keyvaluepair.Key)))
                {
                    /// 枚举资源文件所有对象
                    IDictionaryEnumerator iden = reader.GetEnumerator();
                    while (iden.MoveNext())
                    {
                        int index = keyvaluepair.Value.IndexOf(iden.Key.ToString());

                        if (keyvaluepair.Value.Contains(iden.Key.ToString()))
                        {
                            _imgList.Add(iden.Key.ToString(), (Image)iden.Value);
                        }
                    }
                }
            }
        }

        void CheckCondition(object sender, EventArgs e)
        {
            _needProcessNode.Clear();
            foreach (ConditionData conditionData in _dynamicNodeList)
            {
                string conditionValue = Convert.ToString(conditionData.ConditionProperty.GetValue(this, null));
                if (conditionValue != conditionData.ConditionValue)
                {
                    conditionData.ConditionValue = conditionValue;

                    if (!_needProcessNode.Contains(conditionData.ConditionNode.ParentNode))
                    {
                        _needProcessNode.Add(conditionData.ConditionNode.ParentNode);

                        //根据实际值，找到if节点，并将其放到根节点下
                        XmlNode newNode = conditionData.ConditionNode.SelectSingleNode(string.Format(@"if[@value='{0}']", conditionValue));
                        if (newNode == null)
                        {
                            newNode = conditionData.ConditionNode["else"];
                        }

                        foreach (XmlAttribute att in newNode.Attributes)
                        {
                            if (att.Name != "value")
                            {
                                conditionData.ConditionNode.ParentNode.Attributes[att.Name].Value = att.Value;
                            }
                        }
                    }
                }
            }

            if (_needProcessNode.Count > 0)
            {
                foreach (XmlNode node in _needProcessNode)
                {
                    switch (node.Name)
                    {
                        case "control":
                        case "form":
                            ProcessXmlNodeControl((XmlElement)node);
                            break;
                        case "object":
                            ProcessXmlNodeObject((XmlElement)node);
                            break;
                        default:
                            throw new Exception("意外的Node:" + node.Name);
                    }
                }
            }
        }

        void ProcessXmlNodeCondition(XmlElement node)
        {
            XmlNodeList conditionNodes = node.SelectNodes(@"condition");

            //如果确实有condition节点，则克隆一个当前节点
            if (conditionNodes.Count > 0)
            {
                Application.Idle += new EventHandler(CheckCondition);
            }
            else
            {
                //无condition节点，直接返回
                return;
            }

            //处理所有condition节点
            foreach (XmlNode condition in conditionNodes)
            {
                //condition的name
                string conditionName = condition.Attributes["name"].Value;

                //通过condition的name，找到当前Form的对应的属性的实际值
                string conditionValue = null;
                PropertyInfo propertyInfo = this.GetType().GetProperty(conditionName);
                try
                {
                    conditionValue = Convert.ToString(propertyInfo.GetValue(this, null));
                }
                catch (Exception ex)
                {
                    throw new Exception("开发期错误。Form数据配置文件中的条件在程序中未配对相应的属性:" + conditionName, ex);
                }

                //根据实际值，找到if节点，并将其放到根节点下
                XmlNode newNode = condition.SelectSingleNode(string.Format(@"if[@value='{0}']", conditionValue));
                if (newNode == null)
                {
                    newNode = condition["else"];
                }

                foreach (XmlAttribute att in newNode.Attributes)
                {
                    if (att.Name != "value")
                    {
                        node.Attributes.Append((XmlAttribute)att.Clone());
                    }
                }

                //node.RemoveChild(node.SelectSingleNode(string.Format(@"condition[@name='{0}']",conditionName)));

                XmlAttribute attDynamic = condition.Attributes["dynamic"];
                if (attDynamic != null && attDynamic.Value == "true")
                {
                    _dynamicNodeList.Add(new ConditionData(propertyInfo, condition));
                }
            }
        }

        private void ProcessXmlNodeDatabind(XmlElement node)
        {
            DataTable dt;
            if (!_controlsDataTable.TryGetValue(node.Attributes["id"].Value, out dt))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(PathService.CL_DataSources_Folder, node.Attributes["file"].Value));
                dt = LoadDataTable(doc.DocumentElement, int.Parse(node.Attributes["level"].Value), null);
                _controlsDataTable.Add(node.Attributes["id"].Value, dt);
            }
        }

        void ProcessXmlNodeObject(XmlElement node)
        {
            string name = node.Attributes["Name"].Value;
            object obj = this.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            foreach (XmlAttribute att in node.Attributes)
            {
                if (att.Name == "Name")
                {
                    continue;
                }

                PropertyInfo propInfo = obj.GetType().GetProperty(att.Name);

                propInfo.SetValue(obj, att.Value, null);
            }
        }
        void ProcessXmlNodeText(XmlElement node)
        {
            _textResources.Add(node.Attributes["name"].Value, node.Attributes["value"].Value.Replace(@"\r\n", "\r\n"));
        }
        void ProcessXmlNodeControl(XmlElement node)
        {
            ///根据Name找到目标控件
            Control targetControl = null;
            if (node.Name == "form")
            {
                targetControl = this;
            }
            else
            {
                targetControl = this.Controls.Find(node.Attributes["name"].Value, true)[0];
            }

            ///设置窗体控件的属性
            XmlAttribute attText = node.Attributes["text"];
            if (attText != null && attText.Value != "")
            {
                targetControl.Text = attText.Value.Replace(@"\r\n", "\r\n");

                ///targetControl为Form的时候，设置TabText
                if (targetControl is IDockContent)
                {
                    ((IDockContent)targetControl).DockHandler.TabText = targetControl.Text;
                }
            }

            ///当离开焦点时，控件的值的验证//deleted by zhucai. reason:不需要实时验证
            //XmlAttribute attValidate = node.Attributes["validate"];
            //if (attValidate != null && attValidate.Value != "")
            //{
            //    targetControl.Leave += delegate
            //    {
            //        ValidateService.Validate(attValidate.Value, targetControl);
            //    };
            //}

            ///设置窗体控件的提示文本
            XmlAttribute attHelptip = node.Attributes["helptip"];
            if (attHelptip != null && attHelptip.Value != "")
            {
                targetControl.Enter += delegate
                {
                    _statusLabel.Text = attHelptip.Value;
                };
            }

            ///文件数据绑定
            XmlAttribute attDataBindId = node.Attributes["databindid"];
            if (attDataBindId != null && attDataBindId.Value != "")
            {
                _dicControlDataBindId.Add(targetControl, attDataBindId.Value);

                XmlAttribute attSelectControl = node.Attributes["selectControl"];

                Control selectControl = attSelectControl == null ? null :
                    this.Controls.Find(attSelectControl.Value, true)[0];

                BindFile(targetControl, selectControl, attDataBindId.Value);
            }

            ///检查elementBind
            string strElementBindName = node.GetAttribute("elementBindName");
            if (!string.IsNullOrEmpty(strElementBindName))
            {
                ///将elementBind数据放入_dicElementBindGroup里
                string strElementBindGroup = node.GetAttribute("elementBindGroup");
                strElementBindGroup = (strElementBindGroup == null ? "" : strElementBindGroup);
                Dictionary<string, ControlNodeData> dicElementBindControl = new Dictionary<string, ControlNodeData>();
                if (!_dicElementBindGroup.TryGetValue(strElementBindGroup, out dicElementBindControl))
                {
                    dicElementBindControl = new Dictionary<string, ControlNodeData>();
                    _dicElementBindGroup.Add(strElementBindGroup, dicElementBindControl);
                }
                BindType bindType = BindType.Default;
                string strBindType = node.GetAttribute("elementBindType");
                if (!string.IsNullOrEmpty(strBindType))
                {
                    bindType = (BindType)Enum.Parse(typeof(BindType), strBindType, true);
                }
                dicElementBindControl.Add(strElementBindName, new ControlNodeData(targetControl, node, node.GetAttribute("elementBindFormat"),bindType));
            }

            ///处理Node的子节点
            if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.NodeType == XmlNodeType.Element)
                    {
                        switch (childNode.Name)
                        {
                            case "columns":
                                {
                                    int columnIndex = 0;
                                    System.Windows.Forms.ListView.ColumnHeaderCollection columns = ((ListView)targetControl).Columns;
                                    foreach (XmlNode columnNode in childNode.ChildNodes)
                                    {
                                        if (columnNode.NodeType != XmlNodeType.Element)
                                        {
                                            continue;
                                        }
                                        if (columnNode.Name == "column")
                                        {
                                            columns[columnIndex++].Text = columnNode.Attributes["name"].Value;
                                        }
                                    }
                                    break;
                                }
                            case "condition":
                                break;
                            default:
                                throw new Exception("Control节点出现未知的子节点");
                        }
                    }
                }
            }
        }

        void BindFile(Control control, Control selectControl, string id)
        {
            ///载入数据
            DataTable dt = _controlsDataTable[id];

            if (selectControl != null)
            {
                ///若有selectControl控件,则在selectControl选择项改变后绑定control
                ListControl listSelect = (ListControl)selectControl;
                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;
                    EventHandler bindingHandler = delegate(object sender, EventArgs e)
                    {
                        DataView dv = new DataView(_controlsDataTable[_dicControlDataBindId[listControl]]);
                        dv.RowFilter = string.Format("selectValue='{0}'", listSelect.SelectedValue);
                        listControl.DataSource = dv;
                        listControl.DisplayMember = "text";
                        listControl.ValueMember = "value";
                    };
                    listSelect.SelectedValueChanged += bindingHandler;

                    //首先执行一次绑定
                    bindingHandler(listSelect, EventArgs.Empty);
                }
                else
                {
                    SelectGroup listControl = (SelectGroup)control;
                    EventHandler bindingHandler = delegate(object sender, EventArgs e)
                    {
                        DataView dv = new DataView(_controlsDataTable[_dicControlDataBindId[listControl]]);
                        dv.RowFilter = string.Format("selectValue='{0}'", listSelect.SelectedValue);
                        listControl.DataSource = dv;
                        listControl.DisplayMember = "text";
                        listControl.ValueMember = "value";

                        listControl.DataBinding();
                    };
                    listSelect.SelectedValueChanged += bindingHandler;

                    //首先执行一次绑定
                    bindingHandler(listSelect, EventArgs.Empty);
                }
            }
            else
            {
                //无selectControl控件,直接绑定数据
                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;

                    listControl.DataSource = dt;
                    listControl.DisplayMember = "text";
                    listControl.ValueMember = "value";
                }
                else
                {
                    SelectGroup listControl = (SelectGroup)control;

                    listControl.DataSource = dt;
                    listControl.DisplayMember = "text";
                    listControl.ValueMember = "value";

                    listControl.DataBinding();
                }
            }
        }

        /// <summary>
        /// 载入XmlNode的数据到DataTable中去,level为数据在XmlNode中的层数
        /// </summary>
        DataTable LoadDataTable(XmlElement node, int level, string selectValue)
        {
            if (level == 1)
            {
                return LoadDataTableEx(node, selectValue);
            }
            else
            {
                DataTable dtAll = new DataTable();
                foreach (XmlNode n in node.ChildNodes)
                {
                    XmlAttribute attParent = n.Attributes["value"];
                    DataTable dt = LoadDataTable((XmlElement)n, level - 1, attParent == null ? null : attParent.Value);
                    dtAll.Merge(dt);
                }
                return dtAll;
            }
        }
        DataTable LoadDataTableEx(XmlElement node, string selectValue)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            if (!string.IsNullOrEmpty(selectValue))
            {
                dt.Columns.Add("selectValue");
            }
            foreach (XmlNode n in node.ChildNodes)
            {
                if (!string.IsNullOrEmpty(selectValue))
                {
                    dt.Rows.Add(n.Attributes["value"].Value, n.Attributes["text"].Value, selectValue);
                }
                else
                {
                    dt.Rows.Add(n.Attributes["value"].Value, n.Attributes["text"].Value);
                }
            }
            return dt;
        }

        string GetValueFromElement(XmlElement ele, string name, BindType type)
        {
            switch (type)
            {
                case BindType.Default:
                    {
                        return ele.GetAttribute(name);
                    }
                case BindType.CDATA:
                    {
                        XmlElement eleValue = (XmlElement)ele.SelectSingleNode(string.Format("cdata[@type='{0}']", name));
                        if (eleValue == null)
                        {
                            return "";
                        }
                        else
                        {
                            XmlCDataSection cdata = (XmlCDataSection)eleValue.FirstChild;
                            if (cdata == null)
                            {
                                return "";
                            }
                            else
                            {
                                return cdata.Value;
                            }
                        }
                    }
                default:
                    throw new ArgumentException("未知的type:" + type, "type");
            }
        }
        string[] GetValuesFromElement(XmlElement ele, string name, BindType type)
        {
            switch (type)
            {
                case BindType.Default:
                    {
                        XmlNode bindNode = ele.SelectSingleNode(string.Format("group[@type='{0}']", name));

                        if (bindNode == null)
                        {
                            return new string[0];
                        }
                        else
                        {
                            List<string> list = new List<string>();
                            foreach (XmlNode n in bindNode.ChildNodes)
                            {
                                if (n.NodeType == XmlNodeType.Element)
                                {
                                    list.Add(n.Attributes["value"].Value);
                                }
                            }

                            return list.ToArray();
                        }
                    }
                case BindType.CDATA:
                    {
                        XmlNode bindNode = ele.SelectSingleNode(string.Format("group[@type='{0}']", name));

                        if (bindNode == null)
                        {
                            return new string[0];
                        }
                        else
                        {
                            List<string> list = new List<string>();
                            foreach (XmlNode n in bindNode.ChildNodes)
                            {
                                if (n.NodeType == XmlNodeType.Element)
                                {
                                    XmlCDataSection cdata = (XmlCDataSection)n.FirstChild;
                                    if (cdata == null)
                                    {
                                        list.Add("");
                                    }
                                    else
                                    {
                                        list.Add(cdata.Value);
                                    }
                                }
                            }

                            return list.ToArray();
                        }
                    }
                default:
                    throw new ArgumentException("未知的type:" + type, "type");
            }
        }

        void SetValueToElement(XmlElement ele, string name, BindType type, string value)
        {
            switch (type)
            {
                case BindType.Default:
                    {
                        ele.SetAttribute(name, value);
                        break;
                    }
                case BindType.CDATA:
                    {
                        XmlElement eleValue = (XmlElement)ele.SelectSingleNode(string.Format("cdata[@type='{0}']", name));
                        if (eleValue == null)
                        {
                            eleValue = ele.OwnerDocument.CreateElement("cdata");
                            ele.AppendChild(eleValue);
                        }
                        eleValue.SetAttribute("type", name);

                        XmlCDataSection cdata = (XmlCDataSection)eleValue.FirstChild;
                        if (cdata == null)
                        {
                            cdata = ele.OwnerDocument.CreateCDataSection(value);
                            eleValue.AppendChild(cdata);
                        }
                        else
                        {
                            cdata.Value = value;
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("未知的type:" + type, "type");
            }
        }
        void SetValuesToElement(XmlElement ele, string name, BindType type, string[] values)
        {
            switch (type)
            {
                case BindType.Default:
                    {
                        XmlNode bindNode = ele.SelectSingleNode(string.Format("group[@type='{0}']", name));
                        if (bindNode == null)
                        {
                            bindNode = ele.OwnerDocument.CreateElement("group");
                            ((XmlElement)bindNode).SetAttribute("type", name);
                            ele.AppendChild(bindNode);
                        }
                        else
                        {
                            bindNode.RemoveAll();
                            ((XmlElement)bindNode).SetAttribute("type", name);
                        }

                        foreach (string str in values)
                        {
                            XmlElement subItem = ele.OwnerDocument.CreateElement("item");
                            subItem.SetAttribute("value", str);
                            bindNode.AppendChild(subItem);
                        }
                        break;
                    }
                case BindType.CDATA:
                    {
                        XmlNode bindNode = ele.SelectSingleNode(string.Format("group[@type='{0}']", name));
                        if (bindNode == null)
                        {
                            bindNode = ele.OwnerDocument.CreateElement("group");
                            ((XmlElement)bindNode).SetAttribute("type", name);
                            ele.AppendChild(bindNode);
                        }
                        else
                        {
                            bindNode.RemoveAll();
                            ((XmlElement)bindNode).SetAttribute("type", name);
                        }

                        foreach (string str in values)
                        {
                            XmlElement subItem = ele.OwnerDocument.CreateElement("item");
                            XmlCDataSection cdata = ele.OwnerDocument.CreateCDataSection(str);
                            subItem.AppendChild(cdata);
                            bindNode.AppendChild(subItem);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("未知的type:" + type, "type");
            }
        }

        #endregion

        #region 供派生类使用的方法

        protected void SetElementToForm(XmlElement ele)
        {
            SetElementToForm(ele, "");
        }

        protected void SetElementToForm(XmlElement ele, string elementGroup)
        {
            elementGroup = (elementGroup == null ? "" : elementGroup);
            Dictionary<string, ControlNodeData> dicElementBindControl = _dicElementBindGroup[elementGroup];
            foreach (KeyValuePair<string, ControlNodeData> keyvalue in dicElementBindControl)
            {
                if (keyvalue.Value.Control is CheckBox)
                {
                    string strBindValue = GetValueFromElement(ele, keyvalue.Key, keyvalue.Value.Type);
                    ((CheckBox)keyvalue.Value.Control).Checked = bool.Parse(strBindValue);
                }
                else if (keyvalue.Value.Control is ListControl)
                {
                    string strBindValue = GetValueFromElement(ele, keyvalue.Key, keyvalue.Value.Type);

                    ((ListControl)keyvalue.Value.Control).SelectedValue = strBindValue;
                }
                else if (keyvalue.Value.Control is SelectGroup)
                {
                    SelectGroup selectGroup = (SelectGroup)keyvalue.Value.Control;

                    ///多选
                    if (selectGroup.MultiSelect)
                    {
                        string[] strBindValues = GetValuesFromElement(ele, keyvalue.Key, keyvalue.Value.Type);

                        selectGroup.SelectedStringValues = strBindValues;
                    }
                    ///单选
                    else
                    {
                        string strBindValue = GetValueFromElement(ele, keyvalue.Key, keyvalue.Value.Type);

                        ((SelectGroup)keyvalue.Value.Control).SelectedValue = strBindValue;
                    }
                }
                else if (keyvalue.Value.Control is TextBox)
                {
                    string strBindValue = GetValueFromElement(ele, keyvalue.Key, keyvalue.Value.Type);

                    keyvalue.Value.Control.Text = strBindValue;
                }
                else if (keyvalue.Value.Control is NumericUpDown)
                {
                    string strBindValue = GetValueFromElement(ele, keyvalue.Key, keyvalue.Value.Type);

                    keyvalue.Value.Control.Text = strBindValue;
                }
                else if (keyvalue.Value.Control is DateTimePicker)
                {
                    string strBindValue = GetValueFromElement(ele, keyvalue.Key, keyvalue.Value.Type);

                    if (string.IsNullOrEmpty(keyvalue.Value.Format))
                    {
                        ((DateTimePicker)keyvalue.Value.Control).Value = DateTime.Parse(strBindValue);
                    }
                    else
                    {
                        ((DateTimePicker)keyvalue.Value.Control).Value = DateTime.ParseExact(strBindValue, keyvalue.Value.Format, null);
                    }
                }
                else
                {
                    MessageBox.Show("未知的控件:" + keyvalue.Value.Control.GetType().FullName);
                }
            }
        }

        protected void GetElementFromForm(XmlElement ele)
        {
            GetElementFromForm(ele, "");
        }

        protected void GetElementFromForm(XmlElement ele, string elementGroup)
        {
            elementGroup = (elementGroup == null ? "" : elementGroup);
            Dictionary<string, ControlNodeData> dicElementBindControl = _dicElementBindGroup[elementGroup];
            foreach (KeyValuePair<string, ControlNodeData> keyvalue in dicElementBindControl)
            {
                if (keyvalue.Value.Control is CheckBox)
                {
                    SetValueToElement(ele,keyvalue.Key,keyvalue.Value.Type,((CheckBox)keyvalue.Value.Control).Checked.ToString());
                }
                else if (keyvalue.Value.Control is ListControl)
                {
                    SetValueToElement(ele, keyvalue.Key, keyvalue.Value.Type, ((ListControl)keyvalue.Value.Control).SelectedValue.ToString());
                }
                else if (keyvalue.Value.Control is SelectGroup)
                {
                    SelectGroup selectGroup = (SelectGroup)keyvalue.Value.Control;

                    ///多选
                    if (selectGroup.MultiSelect)
                    {
                        SetValuesToElement(ele, keyvalue.Key, keyvalue.Value.Type, ((SelectGroup)keyvalue.Value.Control).SelectedStringValues);
                    }
                    ///单选
                    else
                    {
                        SetValueToElement(ele, keyvalue.Key, keyvalue.Value.Type, Convert.ToString(((SelectGroup)keyvalue.Value.Control).SelectedValue));
                    }
                }
                else if (keyvalue.Value.Control is TextBox)
                {
                    SetValueToElement(ele, keyvalue.Key, keyvalue.Value.Type, keyvalue.Value.Control.Text);
                }
                else if (keyvalue.Value.Control is NumericUpDown)
                {
                    SetValueToElement(ele, keyvalue.Key, keyvalue.Value.Type, keyvalue.Value.Control.Text);
                }
                else if (keyvalue.Value.Control is DateTimePicker)
                {
                    SetValueToElement(ele, keyvalue.Key, keyvalue.Value.Type, keyvalue.Value.Control.Text);
                }
                else
                {
                    MessageBox.Show("未知的控件:" + keyvalue.Value.Control.GetType().FullName);
                }
            }
        }

        /// <summary>
        /// 从资源文件中载入图像。需要在此xml配置文件有resource节点(file和key属性)
        /// </summary>
        /// <param name="key">图像文件的ID值</param>
        protected Image GetResourceImage(string key)
        {
            return _imgList[key];
        }

        protected virtual bool ValidateForm()
        {
            return ValidateForm(string.Empty);
        }
        protected virtual bool ValidateForm(string group)
        {
            Control control = null;
            return ValidateForm(group, out control);
        }
        protected virtual bool ValidateForm(out Control errorControl)
        {
            return ValidateForm(string.Empty, out errorControl);
        }
        protected virtual bool ValidateForm(string group, out Control firstErrorControl)
        {
            firstErrorControl = null;
            StringBuilder allErrorMsg = new StringBuilder();
            bool isSuccess = true;
            foreach (XmlNode node in _docForm.DocumentElement.ChildNodes)
            {
                ///如果不是Element，则继续遍历下一个
                if (node.NodeType != XmlNodeType.Element || node.Name != "control")
                {
                    continue;
                }

                ///如果有验证组，则仅验证属于组的数据
                if (!string.IsNullOrEmpty(group))
                {
                    XmlAttribute att = node.Attributes["group"];
                    if (att == null || att.Value != group)
                    {
                        continue;
                    }
                }

                ///根据Name找到目标控件
                Control targetControl = this.Controls.Find(node.Attributes["name"].Value, true)[0];

                ///控件的值的验证
                XmlAttribute attValidate = node.Attributes["validate"];
                if (attValidate != null)
                {
                    string errorText = null;
                    if (!ValidateService.Validate(attValidate.Value, targetControl, out errorText))
                    {
                        if (firstErrorControl == null)
                        {
                            firstErrorControl = targetControl;
                        }
                        allErrorMsg.AppendLine(errorText);
                        isSuccess = false;
                    }
                }
            }

            if (!isSuccess)
            {
                MessageService.Show(allErrorMsg.ToString(0, allErrorMsg.Length - Environment.NewLine.Length),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                firstErrorControl.Focus();
                firstErrorControl.Select();
            }

            return isSuccess;
        }

        protected string ValueToText(string databindId, string value)
        {
            return _controlsDataTable[databindId].Select(string.Format("value='{0}'", value))[0]["text"].ToString();
        }
        /// <summary>
        /// 从资源文件中载入字符串
        /// </summary>
        protected string GetTextResource(string name)
        {
            string textRes = string.Empty;
            if (_textResources.TryGetValue(name, out textRes))
            {
                return textRes;
            }
            return textRes;
        }
        public DialogResult ShowMessage(string resourceName, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string caption)
        {
            return MessageService.Show(GetTextResource(resourceName), buttons, icon, defaultButton, caption);
        }
        public DialogResult ShowMessage(string resourceName, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageService.Show(GetTextResource(resourceName), buttons, icon);
        }
        public DialogResult ShowMessage(string resourceName, MessageBoxButtons buttons)
        {
            return MessageService.Show(GetTextResource(resourceName), buttons);
        }
        public DialogResult ShowMessage(string resourceName)
        {
            return MessageService.Show(GetTextResource(resourceName));
        }

        #endregion

        #region 内部类

        class ConditionData
        {
            readonly public PropertyInfo ConditionProperty;
            readonly public XmlNode ConditionNode;
            public string ConditionValue;
            public ConditionData(PropertyInfo conditionProperty, XmlNode conditionNode)
            {
                this.ConditionProperty = conditionProperty;
                this.ConditionNode = conditionNode;
            }
        }

        class ControlNodeData
        {
            private Control _control;
            public Control Control
            {
                get { return _control; }
            }

            private XmlElement _xmlElement;
            public XmlElement XmlElement
            {
                get { return _xmlElement; }
            }

            private string _format;
            public string Format
            {
                get { return _format; }
            }

            private BindType _type;
            public BindType Type
            {
                get { return _type; }
            }

            public ControlNodeData(Control control, XmlElement ele,string format,BindType cdata)
            {
                this._control = control;
                this._xmlElement = ele;
                this._format = format;
                this._type = cdata;
            }
        }

        enum BindType
        {
            Default         = 0,
            CDATA           = 1,
        }

        #endregion

        #region 实现的基类的抽象函数

        public override WorkDocumentType WorkDocumentType
        {
            get { return WorkDocumentType.None; }
        }

        public override bool CanUndo()
        {
            return false;
        }

        public override bool CanRedo()
        {
            return false;
        }

        public override bool CanCut()
        {
            return false;
        }

        public override bool CanCopy()
        {
            return false;
        }

        public override bool CanPaste()
        {
            return false;
        }

        public override bool CanDelete()
        {
            return false;
        }

        public override bool CanSelectAll()
        {
            return false;
        }

        public override void Undo()
        {
        }

        public override void Redo()
        {
        }

        public override void Cut()
        {
        }

        public override void Copy()
        {
        }

        public override void Paste()
        {
        }

        public override void Delete()
        {
        }

        public override void SelectAll()
        {
        }

        #endregion
    }
}
