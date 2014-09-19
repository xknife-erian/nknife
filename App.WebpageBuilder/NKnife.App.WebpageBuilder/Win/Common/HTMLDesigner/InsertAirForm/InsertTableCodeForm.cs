using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using Jeelu.Win;

namespace Jeelu.Win
{
    public partial class InsertTableCodeForm : BaseForm
    {
        string insertTableHTML = "";

        public string InsertTableHTML
        {
            get { return insertTableHTML; }
            set { insertTableHTML = value; }
        }
        public InsertTableCodeForm()
        {
            InitializeComponent();
            this.ImeMode = ImeMode.On;

            widthUintComBox.SelectedIndex = 0;
        }

        private void initializeTableUI()
        {
            //GetCHSTextforInsertTable getCHSforIn = new GetCHSTextforInsertTable();
            //string[] unit = getCHSforIn.Unit;
            //string[] calign = getCHSforIn.CaptionAlign;

            //foreach (string u in unit)
            //    widthUintComBox.Items.Add(u);
            //foreach (string a in calign)
            //    alignComBox.Items.Add(a);
            //widthUintComBox.SelectedIndex = 0;

            ////DesignDataXmlDocument.InsertTableData data =Service.Sdsite.DesignDataDocument.HTMLDesignerInsertTableData;
            //if (data != null)
            //{
            //    rowTextBox.Text = data.RowNum.ToString();
            //    colTextBox.Text = data.ColNum.ToString();
            //    string width = data.TableWidth;
            //    if (width.IndexOf("%") > 0)
            //    {
            //        widthTextBox.Text = width.Substring(0, width.IndexOf("%"));
            //        widthUintComBox.SelectedIndex = 1;
            //    }
            //    else
            //    {
            //        widthTextBox.Text = width.Substring(0, width.IndexOf("px"));
            //        widthUintComBox.SelectedIndex = 0;
            //    }
            //    borderWidthTextBox.Text = data.BorderWidth.ToString();
            //    cellspacingTextBox.Text = data.CellSpacing.ToString();
            //    cellpaddingTextBox.Text = data.Cellpadding.ToString();
            //    Align = data.Align;
            //    data.Align = Align.ToString();
            //}
        }

        #region  Ù–‘
        public string Align
        {
            get
            {
                if (alignComBox.SelectedIndex == 0) return "top";
                else if (alignComBox.SelectedIndex == 1) return "bottom";
                else if (alignComBox.SelectedIndex == 2) return "left";
                else return "right";
            }
             set 
             {
                 switch (value)
                 {
                     case "left": alignComBox.SelectedIndex = 2; break;
                     case "bottom": alignComBox.SelectedIndex = 1; break;
                     case "right": alignComBox.SelectedIndex = 3; break;
                     case "top": alignComBox.SelectedIndex = 0; break;
                 }
             }
        }
        public HeadScope Headscope
        {
            get
            {
                if (headerListView.Items[3].ForeColor == Color.Red) return HeadScope.both;
                else if (headerListView.Items[1].ForeColor == Color.Red) return HeadScope.left;
                else if (headerListView.Items[2].ForeColor == Color.Red) return HeadScope.top;
                else return HeadScope.none;
            }
            //  set { headscope = value; }
        }

        #endregion

        private void headerListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int selectedindex = e.ItemIndex;
            switch (selectedindex)
            {
                case 0:
                    {
                        headerListView.Items[0].ForeColor = Color.Red;
                        headerListView.Items[1].ForeColor =
                        headerListView.Items[2].ForeColor =
                        headerListView.Items[3].ForeColor = Color.Black;
                    } break;
                case 1:
                    {
                        headerListView.Items[1].ForeColor = Color.Red;
                        headerListView.Items[0].ForeColor =
                        headerListView.Items[2].ForeColor =
                        headerListView.Items[3].ForeColor = Color.Black;
                    } break;
                case 2:
                    {
                        headerListView.Items[2].ForeColor = Color.Red;
                        headerListView.Items[1].ForeColor =
                        headerListView.Items[0].ForeColor =
                        headerListView.Items[3].ForeColor = Color.Black;
                    } break;
                case 3:
                    {
                        headerListView.Items[3].ForeColor = Color.Red;
                        headerListView.Items[1].ForeColor =
                        headerListView.Items[2].ForeColor =
                        headerListView.Items[0].ForeColor = Color.Black;
                    } break;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            string rowc = rowTextBox.Text;
            string colc = colTextBox.Text;
            string twid = widthTextBox.Text;
            string bordwid = borderWidthTextBox.Text;
            Unit twidunit = (widthUintComBox.SelectedIndex == 0) ? Unit.pix: Unit.percent;
            string cellspacing = cellspacingTextBox.Text;
            string cellpadding = cellpaddingTextBox.Text;
            string summary = summaryTextBox.Text;
            string title = titleTextBox.Text;
            string align = Align;
            HeadScope headscope = Headscope;
            Table th = new Table();
            insertTableHTML = th.TableHtml(rowc, colc, twid, twidunit, bordwid, cellspacing, cellpadding, headscope, title, align, summary);
            this.DialogResult = DialogResult.OK;
        }

        private void frmInsertTableCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DesignDataXmlDocument.InsertTableData data = new DesignDataXmlDocument.InsertTableData();

            //data.RowNum = int.Parse(rowTextBox.Text);
            //data.ColNum = int.Parse(colTextBox.Text);
            //data.TableWidth = widthTextBox.Text + ((widthUintComBox.SelectedIndex == 1) ? "%" : "px");
            //data.BorderWidth = int.Parse(borderWidthTextBox.Text);
            //data.CellSpacing = int.Parse(cellspacingTextBox.Text);
            //data.Cellpadding = int.Parse(cellpaddingTextBox.Text);
            //data.Align = Align.ToString();

            //Service.Sdsite.DesignDataDocument.HTMLDesignerInsertTableData = data;
        }
    }
}
