using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class imgUserControl : UserControl
    {
        GetCHSTextforImgPropertyPanel GetCHSText = new GetCHSTextforImgPropertyPanel();
       
        public imgUserControl()
        {
            InitializeComponent();
            hightUnitComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            resURLtextBox.Enabled = linkFiletextBox.Enabled = false;
            this.label4.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.width");
            this.label3.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.height");
            resourceFilebutton.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            this.label11.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.resourcefile");
            this.linkFilebutton.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            this.label1.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.linkfile");
            this.label10.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.useclass");
            this.label8.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.replace");
            vspacelabel.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.vspace");
            hspacelabel.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.hsapce");
            borderlabel.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.border");
            alignlabel.Text = ResourceService.GetResourceText("ImagepropertyPanel.label.align");

            leftbutton.Image = ResourceService.GetResourceImage("left_align");
            rightbutton.Image = ResourceService.GetResourceImage("right_align");
            middlebutton.Image = ResourceService.GetResourceImage("middle_align");

            string[] unit = GetCHSText.Unit;
            string[] calign = GetCHSText.Align;

            foreach (string u in unit)
            {
                widthunitcomboBox.Items.Add(u);
                hightUnitComboBox.Items.Add(u);
            }
            foreach (string a in calign)
                aligncomboBox.Items.Add(a);
            widthunitcomboBox.SelectedIndex = hightUnitComboBox.SelectedIndex = aligncomboBox.SelectedIndex = 0;
        }
       
        private void imgUserControl_Paint(object sender, PaintEventArgs e)
        {
            GeneralMethods.drawLineForProPanel(this, Color.Blue, 1, DashStyle.Solid, Width);
        }

        #region  Ù–‘
        public string ImgHeight
        {
            get { return highttextBox.Text; }
            set { highttextBox.Text = value; }
        }
        public string ImgWidth
        {
            get { return widthtextBox.Text; }
            set { widthtextBox.Text = value; }
        }
        public string ImgHeightUnit
        {
            get { return hightUnitComboBox.Text; }
            set { hightUnitComboBox.SelectedIndex = Convert.ToInt32(value); }
        }
        public string ImgWidthUnit
        {
            get { return widthunitcomboBox.Text; }
            set { widthunitcomboBox.SelectedIndex = Convert.ToInt32(value); }
        }

        public string ImgVspace
        {
            get
            {
                return vspacetextBox.Text;
            }
            set
            {
                vspacetextBox.Text = value;
            }
        }
        public string ImgHspace
        {
            get
            {
                return hspacetextBox.Text;
            }
            set
            {
                hspacetextBox.Text = value;
            }
        }
        public string ImgBorderWidth
        {
            get
            {
                return bordertextBox.Text;
            }
            set
            {
                bordertextBox.Text = value;
            }
        }

        public string ImgURL
        {
            get { return resURLtextBox.Text; }
            set { resURLtextBox.Text = value; }
        }
        public string ImgLinkURL
        {
            get { return linkFiletextBox.Text; }
            set { linkFiletextBox.Text = value; }
        }
        public string ImgAlign
        {
            get
            {
                return aligncomboBox.Text;
            }
            set
            {
                switch (value)
                {
                    case "baseline": aligncomboBox.SelectedIndex = 1; break;
                    case "top": aligncomboBox.SelectedIndex = 2; break;
                    case "middle": aligncomboBox.SelectedIndex = 3; break;
                    case "bottom": aligncomboBox.SelectedIndex = 4; break;
                    case "textTop": aligncomboBox.SelectedIndex = 5; break;
                    case "absMiddle": aligncomboBox.SelectedIndex = 6; break;
                    case "absBottom": aligncomboBox.SelectedIndex = 7; break;
                    case "left": aligncomboBox.SelectedIndex = 8; break;
                    case "right": aligncomboBox.SelectedIndex = 9; break;

                }
            }
        }
        #endregion
    }
}
