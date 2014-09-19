using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Win;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace Jeelu.SimplusD.Client.Win
{
    public class ValueControlEx : ValueControl
    {
        protected internal ValueControlEx(AutoAttributeData data, GroupBoxEx parentGroup)
            : base(data, parentGroup)
        {
        }

        protected override Control BuildMainControl(MainControlType type)
        {
            Control control = null;
            switch (type)
            {
                case MainControlType.BuildNumberControl:
                    {
                        control = new BuildNumberControl();
                        ((BuildNumberControl)control).PageName = AutoAttribute.PageName;
                        break;
                    }
                case MainControlType.ProjectPartControl:
                    {
                        control = new ProjectPartControl();
                        break;
                    }
                case MainControlType.ProductImage:
                    {
                        control = new ProductImage();
                        ((ProductImage)control).IsBigImageTag = AutoAttribute.IsBigImage;
                        break;
                    }
                case MainControlType.ProductProperty:
                    {
                        control = new ProductProperty();
                        break;
                    }
                case MainControlType.DepartmentNameControl:
                    {
                        control = new DepartmentNameControl();
                        break;
                    }
                case MainControlType.Department:
                    {
                        control = new Department();
                        break;
                    }
                case MainControlType.HTMLDesignControl:
                    {
                        HTMLDesignControl designControl = new HTMLDesignControl("");
                        designControl.Controls.Add(designControl.GetMainToolStrip());
                        designControl.Controls.Add(designControl.GetHtmlPanel());
                        designControl.SetHtmlPanel().BringToFront();
                        designControl.Size = new System.Drawing.Size(650, 150);
                        control = designControl;
                        break;
                    }
                case MainControlType.BiddingAgent:
                    {
                        control = new BiddingAgent(AutoAttribute.MainControlBindingFile);
                        break;
                    }
                default:
                    {
                        control = base.BuildMainControl(type);
                        break;
                    }
            }

            return control;
        }
        protected override void EnumBindingService(Control ctr, Type enumType)
        {
            Array texts = Enum.GetValues(enumType);

            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            foreach (var item in texts)
            {
                string text = ResourceService.GetEnumResourceText(enumType, item.ToString());
                dt.Rows.Add(Enum.Parse(enumType, item.ToString()), text);
            }
            DtBindingService(ctr, dt);
        }

        public override object Value
        {
            get
            {
                switch (MainControlType)
                {
                    case MainControlType.BuildNumberControl:
                        return ((BuildNumberControl)_mainControl).value;
                    case MainControlType.ProjectPartControl:
                        return ((ProjectPartControl)_mainControl).value;
                    case MainControlType.ProductProperty:
                        return ((ProductProperty)_mainControl).value;
                    case MainControlType.Department:
                        return ((Department)_mainControl).value;
                    case MainControlType.ProductImage:
                        return ((ProductImage)_mainControl).Value;
                    case MainControlType.HTMLDesignControl:
                        return ((HTMLDesignControl)_mainControl).PageText;
                    case MainControlType.BiddingAgent:
                        return ((BiddingAgent)_mainControl).Value;
                    case MainControlType.DepartmentNameControl:
                        return ((DepartmentNameControl)_mainControl).Value;
                    default:
                        return base.Value;
                }
            }
            set
            {
                switch (MainControlType)
                {
                    case MainControlType.BuildNumberControl:
                        ((BuildNumberControl)_mainControl).value = (string)value;
                        break;
                    case MainControlType.ProjectPartControl:
                        ((ProjectPartControl)_mainControl).value = (ProjectPart[])value;
                        break;
                    case MainControlType.ProductProperty:
                        ((ProductProperty)_mainControl).value = (ItemCollection)value;
                        break;
                    case MainControlType.ProductImage:
                        ((ProductImage)_mainControl).Value = (ProductImageData)value;
                        break;
                    case MainControlType.Department:
                        ((Department)_mainControl).value = (DepartmentData)value;
                        break;
                    case MainControlType.HTMLDesignControl:
                        ((HTMLDesignControl)_mainControl).PageText = (string)value;
                        break;
                    case MainControlType.BiddingAgent:
                        ((BiddingAgent)_mainControl).Value = (AgentInfo)value;
                        break;
                    case MainControlType.DepartmentNameControl:
                        ((DepartmentNameControl)_mainControl).Value = (string)value;
                        break;
                    default:
                        base.Value = value;
                        break;
                }
            }
        }
        public override bool IsModifiedFromValue
        {
            get
            {
                switch (MainControlType)
                {
                    case MainControlType.BuildNumberControl:
                        {
                            return ((BuildNumberControl)_mainControl).value != (string)_historyValue;
                        }
                    case MainControlType.ProjectPartControl:
                        {
                            ProjectPart[] newDatas = ((ProjectPartControl)_mainControl).value;
                            return !Utility.IsAllEquals(newDatas, (ProjectPart[])_historyValue);
                        }                   
                    case MainControlType.HTMLDesignControl:
                        {
                            string pageText = ((HTMLDesignControl)_mainControl).PageText;
                            return !(pageText == _historyValue.ToString());
                        }
                    case MainControlType.ProductImage:
                        break;
                    case MainControlType.ProductProperty:
                        break;
                    case MainControlType.Department:
                        break;
                    case MainControlType.DepartmentNameControl:
                        break;
                    case MainControlType.ColorSelectorButton:
                        break;
                    case MainControlType.FontComboBox:
                        break;
                    case MainControlType.BiddingAgent:
                        break;
                    default:
                        break;
                }

                return base.IsModifiedFromValue;
            }
        }
        protected override void SetValueFromControlSubMethod(object obj)
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            switch (propertyInfo.PropertyType.FullName)
            {
                case "Jeelu.SimplusD.ItemCollection":
                    {
                        propertyInfo.SetValue(obj, (Jeelu.SimplusD.ItemCollection)this.Value, null);
                        break;
                    }
                case "Jeelu.TypeData[]":
                    {
                        propertyInfo.SetValue(obj, (Jeelu.TypeData[])this.Value, null);
                        break;
                    }
                case "Jeelu.ProductImageData":
                    {
                        propertyInfo.SetValue(obj, (Jeelu.ProductImageData)this.Value, null);
                        break;
                    }
                case "Jeelu.AgentInfo":
                    {
                        propertyInfo.SetValue(obj, (Jeelu.AgentInfo)this.Value, null);
                        break;
                    }
                case "Jeelu.DepartmentData":
                    {
                        propertyInfo.SetValue(obj, (DepartmentData)this.Value, null);
                        break;
                    }
                default:
                    {
                        base.SetValueFromControlSubMethod(obj);
                        break;
                    }
            }
        }

        public override void EventDefine(MainControlType type)
        { ///更改值后的事件处理
            EventHandler eventHandler = new EventHandler(
                delegate
                {
                    if (OwnerAutoPanel.RealTimeSave)
                    {
                        SetValueFromControl();
                    }
                    else
                    {
                        IsModified = true;
                    }
                });
            switch (type)
            {
                case MainControlType.BuildNumberControl:
                    {
                        ((BuildNumberControl)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case MainControlType.Department:
                    {
                        ((Department)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case MainControlType.DepartmentNameControl:
                    {
                        ((DepartmentNameControl)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case MainControlType.ProductImage:
                    {
                        ((ProductImage)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case MainControlType.ProductProperty:
                    {
                        ((ProductProperty)this._mainControl).SelectChanged += eventHandler;
                        break;
                    }
                case MainControlType.ProjectPartControl:
                    {
                        ((ProjectPartControl)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case MainControlType.HTMLDesignControl:
                    {
                        ((HTMLDesignControl)this._mainControl).Changed += eventHandler;
                        break;
                    }
                default:
                    base.EventDefine(type);
                    break;

            }
        }
    }
}