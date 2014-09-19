using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    [ResReader(false)]
    public partial class ExpandCssControl : CssMainControl
    {
        const string CssCursor = "cursor";
        const string Filter = "filter";
        const string PageBreakAfter = "page-break-after";
        const string PageBreakBefore = "page-break-before";

        public ExpandCssControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ///添加值到combobox中
            InsertValue("page-break-after",conForestall);
            InsertValue("page-break-after",conSith);
            InsertValue("cursor",conCursor);
            InsertValue("filter",conColander);
        }
        private void InsertValue(string comboxName, ComboBox control)
        {
            CssResourceList list = CssResources.Resources[comboxName];
            foreach (string text in list.Texts)
            {
                control.Items.Add(text);
            }
        }
        public override void EnterLoad()
        {
            base.EnterLoad();
            InsertContent();
        }
        public override bool LeaveValidate()
        {
            string validateStr=this.conColander.Text;
            if (validateStr.Contains("?"))
            {
              CssUtility.ShowNotStandardSingleBtn(validateStr);
              this.conColander.Select();
              return false;
            }
            SaveContent();
            return true;
        }
        protected void InsertContent()
        {
            //界面赋值
            CssResourceList listForestall = CssResources.Resources["page-break-after"];
            CssResourceList listSith = CssResources.Resources["page-break-after"];
            CssResourceList listCursor = CssResources.Resources["cursor"];
            CssResourceList listColander = CssResources.Resources["filter"];

            this.conForestall.Text = GetValue(listForestall, Value.Properties[PageBreakAfter]);
            this.conSith.Text = GetValue(listSith, Value.Properties[PageBreakBefore]);
            this.conCursor.Text = GetValue(listCursor, Value.Properties[CssCursor]);
            this.conColander.Text = GetValue(listColander, Value.Properties[Filter]);             
        }
        private string GetValue(CssResourceList list, string saveValue)
        {
            string value = saveValue;
            if (list.HasValue(value))
            {
                value = list.ValueToText(value);
            }
            return value;
        }
        protected void SaveContent()
        {

           //取值
            CssResourceList listForestall = CssResources.Resources["page-break-after"];
            CssResourceList listSith = CssResources.Resources["page-break-after"];
            CssResourceList listCursor = CssResources.Resources["cursor"];
            CssResourceList listColander = CssResources.Resources["filter"];


            string forestall = SetValue(listForestall, this.conForestall);
            string sith = SetValue(listSith, this.conSith);
            string cursor = SetValue(listCursor, this.conCursor);
            string colander = SetValue(listCursor, this.conColander);
            Value.Properties[PageBreakAfter] = forestall;
            Value.Properties[PageBreakBefore] = sith;
            Value.Properties[CssCursor] = cursor;
            Value.Properties[Filter] = colander;
        }
        private string SetValue(CssResourceList list,ComboBox combox)
        {
            string value=combox.Text;
            if (list.HasValue(value))
            {
                value = list.GetValue(value);
            }
            return value;
        }

    }

}
