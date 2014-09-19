using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewWebUnionForm : OMBaseForm
    {

        BindingSource unionBDS = new BindingSource();
        DataSet webUnionDS = new DataSet();
        public ViewWebUnionForm()
        {
            InitializeComponent();

            webUnionDGV.AutoGenerateColumns = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (OMWorkBench.ManagerWebUnion)
            {
                mainToolStrip.Items["NewTSButton"].Visible = true;
                mainToolStrip.Items["EditTSButton"].Visible = true;
                mainToolStrip.Items["DeleteTSButton"].Visible = true;
                mainToolStrip.Items["chargeTSButton"].Visible = true;
            }
            webUnionDS = OMWorkBench.DataAgent.GetWebUnion();
            unionBDS.DataSource = webUnionDS.Tables["webunion"];

            webUnionDGV.DataSource = unionBDS;

            webId.DataPropertyName = "id";
            uname.DataPropertyName = "name";
            uaddtime.DataPropertyName = "regtime";
            uCheckWebCount.DataPropertyName = "unchecked";
            uUncheckWebCount.DataPropertyName = "checked";
            uWebCount.DataPropertyName = "webcount";
            uNopassWebCount.DataPropertyName = "nopass";
            viewInfo.DataPropertyName="viewInfo";

            int unionCount = 0,
                siteCount = 0, siteUncheckedCount = 0, siteCheckedCount = 0, siteNoPassCount = 0,
                industryType = 0, blogType = 0, generalType = 0, doorType = 0;

            for (int i = 0; i < webUnionDS.Tables["webunion"].Rows.Count; i++)
            {
                unionCount = i + 1;
                siteCount += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["webcount"]);
                siteUncheckedCount += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["unchecked"]);
                siteCheckedCount += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["checked"]);
                siteNoPassCount += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["nopass"]);
                industryType += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["industryType"]);
                blogType += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["blogType"]);
                generalType += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["generalType"]);
                doorType += Convert.ToInt32(webUnionDS.Tables["webunion"].Rows[i]["doorType"]);
            }
            unionCountLabel.Text = unionCount.ToString();
            siteCountLabel.Text = siteCount.ToString();
            checkedLabel.Text = siteCheckedCount.ToString();
            uncheckedLabel.Text = siteUncheckedCount.ToString();
            nopassLabel.Text = siteNoPassCount.ToString();
            industryLabel.Text = industryType.ToString();
            doorLabel.Text = doorType.ToString();
            blogLabel.Text = blogType.ToString();
            generalLabel.Text = generalType.ToString();
            DataRow[] drs = webUnionDS.Tables["webunion"].Select("code='jeelu'");
            if (drs.Length > 0)
            {
                jeeluSiteCountLabel.Text = drs[0]["webcount"].ToString();
            }
            else
                jeeluSiteCountLabel.Text = "0";
            base.OnLoad(e);
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            unionBDS.Filter = "name like '%" + nameTextBox.Text + "%'";
        }


        public override void NewCmd()
        {
            DataRow unionRow = webUnionDS.Tables["webunion"].NewRow();
            WebUnionForm newWebUnionForm = new WebUnionForm(unionRow, FormUseMode.New);
            if (newWebUnionForm.ShowDialog() == DialogResult.OK)
            {
                webUnionDS.Tables["webunion"].LoadDataRow(unionRow.ItemArray, false);
                OMWorkBench.DataAgent.UpdateWebUnion(webUnionDS.Tables["webunion"]);
                webUnionDS.AcceptChanges();
            }
        }

        public override void EditCmd()
        {
            DataRow unionRow = webUnionDS.Tables["webunion"].Select("id=" + Convert.ToInt32(webUnionDGV.CurrentRow.Cells["webid"].Value))[0];
            WebUnionForm newWebUnionForm = new WebUnionForm(unionRow, FormUseMode.Edit);
            if (newWebUnionForm.ShowDialog() == DialogResult.OK)
            {
                OMWorkBench.DataAgent.UpdateWebUnion(webUnionDS.Tables["webunion"]);
                webUnionDS.AcceptChanges();
            }
        }

        private void webUnionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           int unionId = Convert.ToInt32(webUnionDGV.CurrentRow.Cells["webid"].Value); 
            switch (webUnionDGV.CurrentCell.OwningColumn.Name)
            {
                case "uname":
                    {
                        DataSet _webSiteDS = OMWorkBench.DataAgent.GetWebSite(unionId);
                        string siteTypeCount = "网站数量： "; //+ _webSiteDS.Tables["website"].Rows.Count.ToString();
                        OMWorkBench.CreateForm(new ViewWebSiteForm(_webSiteDS,webUnionDS, siteTypeCount));
                        break;
                    }
                case "viewInfo":
                    {
                        DataSet _webSiteDS = OMWorkBench.DataAgent.GetWebSite(unionId);
                        WebUnionInfoForm unionInfo = new WebUnionInfoForm(unionId, _webSiteDS);
                        unionInfo.ShowDialog(this);
                        break;
                    }
            }
        }

        DataSet _webSiteDS = null;
        private void Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel thisLabel = sender as LinkLabel;
            switch (thisLabel.Name)
            {
                case "siteCountLabel":
                    {
                        string tabText = "全部";
                        string filterStr = "";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;
                    }
                case "jeeluSiteCountLabel":
                    {
                        string tabText = "jeelu网站";
                        string filterStr = "web_union_id=1";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;
                    }
                case "industryLabel":
                    {
                        string tabText = "行业类";
                        string filterStr = "substring(industry_code,1,1)='1'";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;  
                    }
                case "doorLabel":
                    {
                        string tabText = "门户类";
                        string filterStr = "substring(industry_code,1,1)='5'";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;
                    }
                case "blogLabel":
                    {
                        string tabText = "博客、娱乐类";
                        string filterStr = "substring(industry_code,1,1) in ('2','3') ";
                        GiveDataToSiteForm(tabText, filterStr);
                        break; 
                    }
                case "generalLabel":
                    {
                        string tabText = "普通类";
                        string filterStr = "substring(industry_code,1,1)='4'";
                        GiveDataToSiteForm(tabText, filterStr);
                        break; 
                    }
                case "checkedLabel":
                    {
                       string tabText = "已审核";
                        string filterStr = "current_state='1'";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;
                    }
                case "uncheckedLabel":
                    {
                        string tabText = "未审核";
                        string filterStr = "current_state='0'";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;
                    }
                case "nopassLabel":
                    {
                        string tabText="未通过";
                        string filterStr = "current_state='2'";
                        GiveDataToSiteForm(tabText, filterStr);
                        break;
                    }
            }
        }

        private void GiveDataToSiteForm(string tabText,string filterStr)
        {
            if (_webSiteDS == null)
                _webSiteDS = OMWorkBench.DataAgent.GetWebSite(-1);
            string siteTypeCount = tabText + "网站数量： ";
            ViewWebSiteForm viewWebSite = new ViewWebSiteForm(_webSiteDS,webUnionDS, siteTypeCount);
            viewWebSite.FilterStr = filterStr;
            viewWebSite.TabText += "--"+tabText;
            OMWorkBench.CreateForm(viewWebSite);
        }

    }
}
