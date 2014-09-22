using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class HrSampleXmlElement :GeneralXmlElement
    {
        internal HrSampleXmlElement(XmlDocument doc)
            : base("hrSample", doc)
        {
        }


        #region tmplt元素特别属性

        public string TmpltName
        {
            get
            {
                return this.GetAttribute("tmpltName");
            }
            set
            {
                this.SetAttribute("tmpltName", value);
            }
        }

        public string IsTmplt
        {
            set
            {
                this.SetAttribute("isTmplt", "true");
            }
        }

        public string IsSomeOne
        {
            get
            {
                return this.GetAttribute("isSomeOne");
            }
            set
            {
                this.SetAttribute("isSomeOne", value);
            }
        }

        public string WorkPlace
        {
            get
            {
                return this.GetAttribute("workPlace");
            }
            set
            {
                this.SetAttribute("workPlace", value);
            }
        }

        public string SubWorkPlace
        {
            get
            {
                return this.GetAttribute("subWorkPlace");
            }
            set
            {
                this.SetAttribute("subWorkPlace", value);
            }
        }

        public string Department
        {
            get
            {
                return this.GetAttribute("department");
            }
            set
            {
                this.SetAttribute("department", value);
            }
        }

        public string EndTime
        {
            get
            {
                return this.GetAttribute("endTime");
            }
            set
            {
                this.SetAttribute("endTime", value);
            }
        }

        public string WorkProperty
        {
            get
            {
                return this.GetAttribute("workProperty");
            }
            set
            {
                this.SetAttribute("workProperty", value);
            }
        }

        public string Email
        {
            get
            {
                return this.GetAttribute("email");
            }
            set
            {
                this.SetAttribute("email", value);
            }
        }

        public string Phone
        {
            get
            {
                return this.GetAttribute("phone");
            }
            set
            {
                this.SetAttribute("phone", value);
            }
        }

        public string PostNumber
        {
            get
            {
                return this.GetAttribute("postNumber");
            }
            set
            {
                this.SetAttribute("postNumber", value);
            }
        }

        public string EduLevel
        {
            get
            {
                return this.GetAttribute("eduLevel");
            }
            set
            {
                this.SetAttribute("eduLevel", value);
            }
        }

        public string ManagerExperience
        {
            get
            {
                return this.GetAttribute("managerExperience");
            }
            set
            {
                this.SetAttribute("managerExperience", value);
            }
        }

        public string Sex
        {
            get
            {
                return this.GetAttribute("sex");
            }
            set
            {
                this.SetAttribute("sex", value);
            }
        }

        public string Evection
        {
            get
            {
                return this.GetAttribute("evection");
            }
            set
            {
                this.SetAttribute("evection", value);
            }
        }

        public string LanguageOtherDesire
        {
            get
            {
                return this.GetAttribute("languageOtherDesire");
            }
            set
            {
                this.SetAttribute("languageOtherDesire", value);
            }
        }

        public string DriverLicence
        {
            get
            {
                return this.GetAttribute("driverLicence");
            }
            set
            {
                this.SetAttribute("driverLicence", value);
            }
        }

        public string OtherCertificate
        {
            get
            {
                return this.GetAttribute("otherCertificate");
            }
            set
            {
                this.SetAttribute("otherCertificate", value);
            }
        }

        public string IsHot
        {
            get
            {
                return this.GetAttribute("isHot");
            }
            set
            {
                this.SetAttribute("isHot", value);
            }
        }

        public string languageDesire
        {
            get
            {
                return this.GetAttribute("languageDesire");
            }
            set
            {
                this.SetAttribute("languageDesire", value);
            }
        }

        public string Salary
        {
            get
            {
                return this.GetAttribute("salary");
            }
            set
            {
                this.SetAttribute("salary", value);
            }
        }

        public string IsNeedNewRelation
        {
            get
            {
                return this.GetAttribute("isNeedNewRelation");
            }
            set
            {
                this.SetAttribute("isNeedNewRelation", value);
            }
        }

        public string Contactname
        {
            get
            {
                return this.GetAttribute("contactname");
            }
            set
            {
                this.SetAttribute("contactname", value);
            }
        }

        public string Address
        {
            get
            {
                return this.GetAttribute("address");
            }
            set
            {
                this.SetAttribute("address", value);
            }
        }

        public string WorkExperience
        {
            get
            {
                return this.GetAttribute("workExperience");
            }
            set
            {
                this.SetAttribute("workExperience", value);
            }
        }

        public string Age
        {
            get
            {
                return this.GetAttribute("age");
            }
            set
            {
                this.SetAttribute("age", value);
            }
        }

        public string Localism
        {
            get
            {
                return this.GetAttribute("localism");
            }
            set
            {
                this.SetAttribute("localism", value);
            }
        }

        public string HireNum
        {
            get
            {
                return this.GetAttribute("hireNum");
            }
            set
            {
                this.SetAttribute("hireNum", value);
            }
        }

        public string IsPubbed
        {
            get
            {
                return this.GetAttribute("isPubbed");
            }
            set
            {
                this.SetAttribute("isPubbed", value);
            }
        }

        public string PubTime
        {
            get { return this.GetAttribute("pubTime"); }
            set { this.SetAttribute("pubTime", value); }
        }
        #endregion


    }
}
