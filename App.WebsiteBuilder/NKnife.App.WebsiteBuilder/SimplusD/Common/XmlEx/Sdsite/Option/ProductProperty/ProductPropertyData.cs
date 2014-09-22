using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace Jeelu.SimplusD
{
    public class ProductPropertyData
    {
        /// <summary>
        /// 是否是用户自定义的
        /// </summary>
        public bool IsUserAdd { get; set; }
        /// <summary>
        ///产品属性的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品属性的值
        /// </summary>
        public string Value { get; set; }
    }
    public class ProductPropertyGroupData : AnyXmlElement
    {
        public ProductPropertyGroupData(XmlDocument doc)
            : base("GroupItem", doc)
        {
        }
        public new DesignDataXmlDocument OwnerDocument
        {
            get
            {
                return (DesignDataXmlDocument)base.OwnerDocument;
            }
        }
        /// <summary>
        /// 产品属性的id
        /// </summary>
        public string GroupId 
        {
            get { return GetAttribute("groupId"); }
            set { SetAttribute("groupId", value); }
        }
        public string GroupName 
        {
            get { return GetAttribute("groupName"); }
            set { SetAttribute("groupName", value); }
        }
        public bool IsUsed 
        {
            get { return Convert.ToBoolean(GetAttribute("isUsed")); }
            set { SetAttribute("isUsed", Convert.ToString(value)); }
        }
        /// <summary>
        /// 产品属性的Group项的集合
        /// </summary>
        public ProductPropertyData[] Item
        {
            get 
            {
                List<ProductPropertyData> productList = new List<ProductPropertyData>();
                XmlNodeList nodes = this.SelectNodes("//item");
                foreach (XmlElement item in nodes)
                {
                    ProductPropertyData data = new ProductPropertyData();
                    data.Name = item.GetAttribute("name");
                    data.IsUserAdd = Utility.Convert.StringToBool(item.GetAttribute("isUserAdd"));
                    productList.Add(data);
                }
                return productList.ToArray();
            }
            set 
            {
                foreach (var item in value)
                {
                    XmlElement ele=OwnerDocument.CreateElement("item");
                    ele.SetAttribute("name", item.Name);
                    ele.SetAttribute("isUserAdd",  item.IsUserAdd.ToString());
                    this.AppendChild(ele);
                }
            }
        }
    }
    public class ItemCollection : XmlElement, ICollection<ProductPropertyData>
    {
        public ItemCollection(XmlDocument doc)
            : base("", "productProperty", "", doc)
        {
        }
        public string GroupId
        {
            get { return GetAttribute("groupId"); }
            set { SetAttribute("groupId", value); }
        }
        public string GroupName
        {
            get { return GetAttribute("groupName"); }
            set { SetAttribute("groupName", value); }
        }
        #region ICollection<ProductPropertyData> 成员

        public void Add(ProductPropertyData item)
        {
            XmlElement ele = this.OwnerDocument.CreateElement("item");
            ele.SetAttribute("name", item.Name);
            ele.SetAttribute("value", item.Value);
            this.AppendChild((XmlNode)ele);
        }

        public void Clear()
        {
            this.RemoveAll();
        }

        public bool Contains(ProductPropertyData item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ProductPropertyData[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ProductPropertyData item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<ProductPropertyData> 成员

        public IEnumerator<ProductPropertyData> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
