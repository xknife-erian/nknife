using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Jeelu.Win
{
    public partial class AutoLayoutPanel : FlowLayoutPanel
    {
        #region ���Զ���

        /// <summary>
        /// �Ƿ�ʵʱ���棨δ��ɣ�by lisuye on 2008��5��28��
        /// </summary>
        public bool RealTimeSave { get; set; }
        /// <summary>
        /// �Ƿ���Ե�������
        /// </summary>
        public bool SingleObject { get; set; }
        /// <summary>
        /// �Ƿ���Ծ�̬����
        /// </summary>
        private bool IsStatic { get; set; }
        /// <summary>
        /// ���ؼ�������:�������������ʵ�ֵĽӿڼ���
        /// </summary>
        private TypeAndInterfaceArr OwnTypeAndInterfaceArr { get; set; }
        internal static string ResourcesPath { get; private set; }
        private Type _autoAttributeType;

        //��һ����ʶ����ǵ�һ�μ������ݣ�������Changed�¼�����
        public bool IsNewOpenForAddEvent{get;set;}
        /// <summary>
        /// ����ValueControl�ļ���
        /// </summary>
        internal List<ValueControl> ValueControls = new List<ValueControl>();

        const int WM_CTLCOLOREDIT = 0x0133;
        const int WM_NCPAINT = 0x085;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCPAINT)
            {
                this.AutoScroll = true;
            }
            base.WndProc(ref m);

        }
        /// <summary>
        ///�ж� ValueControls�����Ƿ����޸� by lisuye on 2008��5��28��
        /// </summary>
        public bool IsModified
        {
            get
            {
                foreach (var item in ValueControls)
                {
                    if (item.IsModified)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        static public void Initialize(string resourcesPath)
        {
            ResourcesPath = resourcesPath;
        }

        #region ���캯�� �򵥵�һЩ�¼� �ؼ���ʼ��

        /// <summary>
        /// ���캯��: 
        /// ����Type�����ؼ������ӿؼ�
        /// </summary>
        protected AutoLayoutPanel(Type attributeType, TypeAndInterfaceArr typeAndInterface, bool isStatic, bool singleObject)
        {
            this.IsNewOpenForAddEvent = true;
            OwnTypeAndInterfaceArr = typeAndInterface;
            IsStatic = isStatic;
            this._autoAttributeType = attributeType;
            this.SingleObject = singleObject;

            //this.Padding = new Padding(10);//�ɽ�������FlowLayoutPanel�ı߾����ı����пؼ�����ʼλ�õĲ�ͬ,�������ؼ�̫������Ե������
           
            CreateControlForPanel(OwnTypeAndInterfaceArr);
        }

        /// <summary>
        /// ��OnCreateControl�¼�����ɳ�ʼ��
        /// ���ڹ��캯�����޷���ÿ����߶ȣ�
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

        }

        /// <summary>
        /// ����FlowLayoutPanel�������ԣ��޷�������ӿؼ���DockЧ����
        /// ����Resize�¼�������ӿؼ��Ŀ������
        /// </summary>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            //��Resize��ʱ��DockTop��GroupBoxEx�Ŀ�Ƚ��п���
            foreach (Control ctr in this.Controls)
            {
                if (ctr.Dock == DockStyle.Top)
                {
                    ctr.Width = this.Width - 6;
                    ResizeControlSubMethod(ctr, ctr.Width - 2);
                }
            }
        }
        /// <summary>
        /// Resize�¼��ĵ��ӷ���
        /// </summary>
        private void ResizeControlSubMethod(Control ctr, int width)
        {
            foreach (Control subCtr in ctr.Controls)
            {
                if (subCtr is GroupBox)
                {
                    subCtr.Width = ctr.Width;
                }
            }
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void CreateControlForPanel(TypeAndInterfaceArr typeAndInterface)
        {
            #region ȡ��һ�����ָ���This
            StringBuilder nameSb;
            {
                nameSb = new StringBuilder();
                nameSb.Append("Control_").Append(typeAndInterface.ClassType).Append("_");
                if (typeAndInterface.Interfaces != null)
                {
                    foreach (Type ty in typeAndInterface.Interfaces)
                    {
                        nameSb.Append(ty.Name).Append("_");
                    }
                }
            }
            this.Name = nameSb.ToString();
            #endregion

            //����Type�����ɿؼ�
            this.BuildControlForType(typeAndInterface);

            #region ���ݽӿ������ɿؼ�//todo:δ��

            Type[] listInterface = typeAndInterface.Interfaces;

            #endregion

            this.AutoScroll = true;
            this.Dock = DockStyle.Fill;
        }

        #endregion

        #region һЩ�鷽��

        protected virtual GroupBoxEx CreateGroup(GroupAttsData data)
        {
            return new GroupBoxEx(data,this);
        }

        protected internal virtual ValueControl CreateValueControl(AutoAttributeData data,GroupBoxEx group)
        {
            return new ValueControl(data, group);
        }

        /// <summary>
        /// ����Դ�ļ����ȡ��ǰ�����ַ���
        /// </summary>
        static public string GetLanguageText(AutoLayoutPanelXmlDocument doc, string str)
        {
            string currLangText;
            if (!doc.TextDic.TryGetValue(str, out currLangText))
            {
                ///��Դ�ļ�Ĭ����:Debug\CHS\AutoLayoutPanelResource.xml��
                Debug.Fail("���ı�û�ж�Ӧ����Դ�ı�:" + str);
                return str;
            }
            return currLangText;
        }
        /// <summary>
        /// ����Դ�ļ����ȡ��ǰ�����ַ���
        /// </summary>
        static public string GetLanguageText(string strKey)
        {
            return GetLanguageText(AutoLayoutPanelXmlDocument.Singler, strKey);
        }

        /// <summary>
        /// ��AutoAttributeData�ļ��������GroupAttsData�ļ��� by lisuye on 2008��5��28��
        /// </summary>
        protected virtual SortedDictionary<int, GroupAttsData> ToGroupDatas(List<AutoAttributeData> objectKeyList)
        {
            SortedDictionary<int, GroupAttsData> dicGroupAttsData = new SortedDictionary<int, GroupAttsData>();

            foreach (var item in objectKeyList)
            {
                int groupBoxIndex = item.Attribute.GroupBoxIndex;
                GroupAttsData groupData;

                ///�Ҵ�groupBoxIndex��Ӧ��Data�Ƿ���dic���Ѵ���
                if (!dicGroupAttsData.TryGetValue(groupBoxIndex, out groupData))
                {
                    ///û���ҵ������첢���
                    groupData = new GroupAttsData(groupBoxIndex);
                    dicGroupAttsData.Add(groupBoxIndex, groupData);
                }

                groupData.AutoAttributeDatas.Add(item);
            }

            return dicGroupAttsData;
        }

        #endregion


        #region �ص㣺����Type�����ɿؼ�
        /// <summary>
        /// ����Type�����ɿؼ�
        /// </summary>
        private void BuildControlForType(TypeAndInterfaceArr typeAndInterface)
        {
            Type type = typeAndInterface.ClassType;

            //PropertyInfo[] properties = null;

            MemberInfo[] memberInfos = null;

            #region GetProperties
            if (IsStatic)
            {
                memberInfos = type.GetMembers(
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.GetProperty |
                    BindingFlags.SetProperty |
                    BindingFlags.InvokeMethod);
                //properties = type.GetProperties(
                //     BindingFlags.Static |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty);
            }
            else
            {
                memberInfos = type.GetMembers(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.GetProperty |
                    BindingFlags.SetProperty |
                    BindingFlags.InvokeMethod);
                /////ȡ��ǰ�������
                //properties = type.GetProperties(
                //     BindingFlags.Instance |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty);
                /////ȡ��ǰ�������
                //properties = type.GetProperties(
                //     BindingFlags.Instance |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty |
                //     BindingFlags.DeclaredOnly);

                /////ȡ�����������
                //PropertyInfo[] baseProperties = type.BaseType.GetProperties(
                //     BindingFlags.Instance |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty |
                //     BindingFlags.DeclaredOnly);

                /////��dedaoshu�õ������Ժϳ�һ������
                //PropertyInfo[] tempNew = new PropertyInfo[properties.Length + baseProperties.Length];
                //Array.Copy(properties, tempNew, properties.Length);
                //Array.Copy(baseProperties, 0, tempNew, properties.Length, baseProperties.Length);
                //properties = tempNew;
            }
            #endregion

            List<AutoAttributeData> attList = new List<AutoAttributeData>();
            //���÷���õ������еĶ������� by lisuye on 2008��5��29��

            //foreach����������������Ƿ��ж�������
            foreach (MemberInfo info in memberInfos)
            {
                object[] propertyPadValues = info.GetCustomAttributes(_autoAttributeType, true);
                if (propertyPadValues.Length <= 0)
                {
                    continue;//�޶�������
                }

                //foreach����������Ķ�������,Add��attList��
                foreach (AutoLayoutPanelAttribute att in propertyPadValues)
                {
                    attList.Add(new AutoAttributeData(info,att));
                }
            }
            //���ݶ������Ի���groupbox�ؼ� by lisuye on 2008��5��29��
            //���attList.CountС�ڻ����0�Ļ�֤��������û�ж�������
            if (attList.Count > 0)
            {
                ///����AutoAttributeData���ϻ��GroupAttsData����
                SortedDictionary<int, GroupAttsData> groupBoxList = ToGroupDatas(attList);

                if (groupBoxList != null)
                {
                    foreach (var pair in groupBoxList)
                    {
                        //����GroupBox��ʱ������Ҫ��groupBox��������Է��ڵڸ���ĵ�һ��(�涨)
                        AutoAttributeData lastAutoAtt = pair.Value.AutoAttributeDatas[0];
                        bool isGroupDockTop = lastAutoAtt.Attribute.GroupBoxDockTop; //��ǰ����Ͽ��Ƿ�Ҫ�ö�
                        string groupText = lastAutoAtt.Attribute.GroupBoxUseWinStyleText; //��ǰ����Ͽ����ʾ�ı�
                        bool useGroup = lastAutoAtt.Attribute.GroupBoxUseWinStyle;
                        //����groupbox 
                        GroupBoxEx box = CreateGroup(pair.Value);

                        //������Ҫ��Group��ΪDock.Topʱ�İ취������ÿؼ���Dock��������Ч�ģ�
                        if (isGroupDockTop)
                        {
                            box.Dock = DockStyle.Top;
                            
                        }
                        //����ؼ��������ı�
                        if (!string.IsNullOrEmpty(groupText))
                        {
                            if (box.InnerGroupBox != null)
                            {
                                //��������ı�
                                box.InnerGroupBox.Text = GetLanguageText(
                                        AutoLayoutPanelXmlDocument.Singler, groupText);
                            }
                        }
                        this.Controls.Add(box);
                    }
                }
            }//if
        }

        #endregion


        #region ȡֵ��ֵ��� Save FillValue
        /// <summary>
        /// Ϊ�ؼ�ȡֵ�����
        /// ���ݴ�������object��ȡ����������������ؼ���ȥ
        /// </summary>
        public void FillValue(object[] objs)
        {
            Debug.Assert(!IsStatic);

            if (objs == null || objs.Length <= 0)
            {
                return;
            }

            FillValueCore(this, objs);
            
        }

        /// <summary>
        /// Ϊ�ؼ�ȡֵ�����(һ��IsStaticʱʹ�ô˷���)
        /// ���ݴ�������object��ȡ����������������ؼ���ȥ
        /// </summary>
        public void FillValue()
        {
            Debug.Assert(IsStatic);

            FillValueCore(this);
        }
        //��ȡ�ؼ�����Ӧ��ֵ��AutoPanel�е����пؼ���ֵ by lisuye on 2008��5��29��
        private void FillValueCore(Control control,object[] objs)
        {
            foreach (var item in ValueControls)
            {
                item.FillValue(objs);
            }
        }
        private void FillValueCore(Control control)
        {
            foreach (var item in ValueControls)
            {
                item.FillValue();
            }
        }

        /// <summary>
        /// ���ؼ���ֵ���浽����(ע��:���ڿؼ���֪�������Ӧ���ļ���
        /// ��������ı���ֻ�ǽ������ֵ���浽�����У����ļ��޹�)
        /// </summary>
        public virtual void Save()
        {
            foreach (var item in ValueControls)
            {
                item.Save();
            }
        }

        public event EventHandler<ValueSaveEventArgs> Saved;
        protected internal virtual void OnSaved(ValueSaveEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }


        #endregion


        #region �ڲ��ࣺTypeAndInterfaceArr �������������ʵ�ֵĽӿڼ��ϣ�ʵ���˱Ƚ�

        /// <summary>
        /// �������������ʵ���ڵĽӿڼ���
        /// </summary>
        /// 

        protected internal class TypeAndInterfaceArr
        {
            public TypeAndInterfaceArr(Type type, Type[] interfaces,string panel)
            {
                _classType = type;
                _interfaces = interfaces;
                this.Panel = panel;
            }
            public TypeAndInterfaceArr(Type type, string panel)
                : this(type, null, panel)
            {
            }

            public string Panel { get; set; }

            private Type _classType;
            public Type ClassType
            {
                get { return _classType; }
                set { _classType = value; }
            }

            private Type[] _interfaces;
            public Type[] Interfaces
            {
                get { return _interfaces; }
                set { _interfaces = value; }
            }

            // override object.Equals
            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                TypeAndInterfaceArr targetObj = (TypeAndInterfaceArr)obj;

                if (this.ClassType != targetObj.ClassType)
                {
                    return false;
                }

                if (!Utility.IsAllEquals<Type>(this.Interfaces, targetObj.Interfaces))
                {
                    return false;
                }

                if (this.Panel != targetObj.Panel)
                {
                    return false;
                }

                return true;
            }

            // override object.GetHashCode
            public override int GetHashCode()
            {
                return this.ClassType.GetHashCode();
            }
        }

        #endregion
    }

    /// <summary>
    /// Auto�Ķ������Ժ�����(������)
    /// </summary>
    /// 
    //�������������Type�ı�������һ��ȡ����������Ӧ��ֵ  by lisuye on 2008��5��29��
    public class AutoAttributeData
    {
        public AutoLayoutPanelAttribute Attribute { get; set; }
        public MemberInfo Property { get; set; }

        public AutoAttributeData(MemberInfo propertyInfo, AutoLayoutPanelAttribute attribute)
        {
            this.Attribute = attribute;
            this.Property = propertyInfo;
        }
        public AutoAttributeData()
        {
        }
    }

    /// <summary>
    /// һ��Group��Ӧ������(GroupBoxIndex��һ��AutoAttributeData�ļ���)
    /// </summary>
    /// 
    //d��ÿ��groupbox��Ӧ�����ݴ洢������һ��Index��һ�����ݼ��ϣ�by lisuye on 2008��5��29��
    public class GroupAttsData
    {
        public int GroupBoxIndex { get; private set; }
        public List<AutoAttributeData> AutoAttributeDatas { get; private set; }
        public GroupAttsData(int groupBoxIndex)
        {
            this.GroupBoxIndex = groupBoxIndex;
            AutoAttributeDatas = new List<AutoAttributeData>();
        }
    }

    public class ValueSaveEventArgs : EventArgs
    {
        public object Value { get; private set; }
        public object TargetObj { get; private set; }
        public ValueSaveEventArgs(object value,object targetObj)
        {
            this.Value = value;
            this.TargetObj = targetObj;
        }
    }
}