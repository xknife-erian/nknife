using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 部门相关信息
    /// </summary>
    public class SiteDepartmentXmlElement : AnyXmlElement
    {
        public SiteDepartmentXmlElement(XmlDocument doc)
            : base("department", doc)
        {
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string DeptID
        {
            get { return this.GetAttribute("id"); }
            set { this.SetAttribute("id", value); }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get { return this.GetAttribute("name"); }
            set { this.SetAttribute("name", value); }
        }
        /// <summary>
        /// 部门联系人
        /// </summary>
        public string LinkMan
        {
            get { return this.GetAttribute("linkMan"); }
            set { this.SetAttribute("linkMan", value); }
        }
        /// <summary>
        /// 部门联系人性别
        /// </summary>
        public string LinkManSex
        {
            get { return this.GetAttribute("linkManSex"); }
            set { this.SetAttribute("linkManSex", value); }
        }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string LinkPhone
        {
            get { return this.GetAttribute("phone"); }
            set { this.SetAttribute("phone", value); }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string LinkMobelPhone
        {
            get { return this.GetAttribute("mobelPhone"); }
            set { this.SetAttribute("mobelPhone", value); }
        }
        /// <summary>
        /// 联系传真
        /// </summary>
        public string LinkFax
        {
            get { return this.GetAttribute("fax"); }
            set { this.SetAttribute("fax", value); }
        }
        /// <summary>
        /// 联系MSN
        /// </summary>
        public string LinkMsn
        {
            get { return this.GetAttribute("msn"); }
            set { this.SetAttribute("msn", value); }
        }
        /// <summary>
        /// 联系Email
        /// </summary>
        public string LinkEmail
        {
            get { return this.GetAttribute("email"); }
            set { this.SetAttribute("email", value); }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string LinkAddress
        {
            get { return this.GetAttribute("address"); }
            set { this.SetAttribute("address", value); }
        }
        /// <summary>
        /// 联系邮编
        /// </summary>
        public string LinkPostCode
        {
            get { return this.GetAttribute("postCode"); }
            set { this.SetAttribute("postCode", value); }
        }
        /// <summary>
        /// 部门职务
        /// </summary>
        public string DeptDuty
        {
            get { return this.GetAttribute("deptDuty"); }
            set { this.SetAttribute("deptDuty", value); }
        }
    }
}
