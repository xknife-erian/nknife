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
    public partial class tableUserControl : UserControl
    {
        GetCHSTextforTablePropertyPanel GetCHSText = new GetCHSTextforTablePropertyPanel();

        public tableUserControl()
        {
            InitializeComponent();
            browserbutton.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            widthLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.width.text");
            colLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.col.text"); 
            rowLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.row.text");
            spaceLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.space.text");
            fillLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.fill.text");
            boderLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.border.text");
            alignLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.align.text");
            useclassLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.useclass.text");
            bgcolorLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.bgcolor.text");
            bgpicLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.bgpic.text");
            bordercolorLabel.Text = ResourceService.GetResourceText("TablepropertyPanel.bordercolor.text");

            string[] unit = GetCHSText.Unit;
            string[] calign = GetCHSText.CaptionAlign;

            foreach (string u in unit)
            {
                widthunitcomboBox.Items.Add(u);
            }
            foreach (string a in calign)
                aligncomboBox.Items.Add(a);
            widthunitcomboBox.SelectedIndex = aligncomboBox.SelectedIndex = 0;
        }

        private void tableUserControl_Paint(object sender, PaintEventArgs e)
        {
            GeneralMethods.drawLineForProPanel(this, Color.Blue, 1, DashStyle.Solid, Width);
        }

        #region  Ù–‘
        public string TableRowNum
        {
            get { return rownumtextBox.Text; }
            set
            {
                if (rownumtextBox.Text != value)
                    rownumtextBox.Text = value;
            }
        }
        public string TableColNum
        {
            get { return colnumtextBox.Text; }
            set
            {
                if (colnumtextBox.Text != value)
                    colnumtextBox.Text = value;
            }
        }

        public string TableWidth
        {
            get { return widthtextBox.Text; }
            set
            {
                if (widthtextBox.Text != value)
                    widthtextBox.Text = value;
            }
        }
        public string TableWidthUnit
        {
            get { return widthunitcomboBox.Text; }
            set
            {
                if (widthunitcomboBox.Text != value)
                    widthunitcomboBox.Text = value;
            }
        }

        public string TableFill
        {
            get { return filltextBox.Text; }
            set
            {
                if (filltextBox.Text != value)
                    filltextBox.Text = value;
            }
        }
        public string TableSpace
        {
            get { return spacetextBox.Text; }
            set
            {
                if (spacetextBox.Text != value)
                    spacetextBox.Text = value;
            }
        }
        public string TableAlign
        {
            get { return aligncomboBox.Text; }
            set
            {
                switch (value)
                {
                    case "default": aligncomboBox.SelectedIndex = 0; break;
                    case "left": aligncomboBox.SelectedIndex = 1; break;
                    case "center": aligncomboBox.SelectedIndex = 2; break;
                    case "right": aligncomboBox.SelectedIndex = 3; break;
                }
            }
        }
        public string TableBorder
        {
            get { return bordertextBox.Text; }
            set { bordertextBox.Text = value; }
        }
        public string TableUseClass
        {
            get { return classcomboBox.Text; }
            set { classcomboBox.Text = value; }
        }
        public Color TableBgColor
        {
            get { return bgColorButton.MyColor; }
            set
            {
                if (bgColorButton.MyColor != value)
                {
                    bgColorButton.MyColor = value;
                    bgcolortextBox.Text = GeneralMethods.ColorToRGB(value);
                }
            }
        }
        public Color TableBorderColor
        {
            get { return borderColorButton.MyColor; }
            set
            {
                if (borderColorButton.MyColor != value)
                {
                    bordercolortextBox.Text = GeneralMethods.ColorToRGB(value);
                    borderColorButton.MyColor = value;
                }
            }
        }
        public string TableBgPic
        {
            get { return bgpicsrctextBox.Text; }
            set
            {
                if (bgpicsrctextBox.Text != value)
                    bgpicsrctextBox.Text = value;
            }
        }
        #endregion

    }
}
