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
    public partial class lineUserControl : UserControl
    {
        public lineUserControl()
        {
            GetCHSTextforLinePropertyPanel getchsforline = new GetCHSTextforLinePropertyPanel();
            InitializeComponent();
            widthLabel.Text =ResourceService.GetResourceText("line.lable.width");
            heightLabel.Text =ResourceService.GetResourceText("line.lable.height");
            alignLabel.Text =ResourceService.GetResourceText("line.lable.align");
            shadcheckBox.Text = ResourceService.GetResourceText("line.checkboxtext.text");
            heightUnitLabel.Text =ResourceService.GetResourceText("line.lable.heightunit");
            useclassLabel.Text =ResourceService.GetResourceText("line.lable.useclass");

            string[] unit = getchsforline.Unit;
            string[] calign = getchsforline.CaptionAlign;

            foreach (string u in unit)
            {
                widthUnitcomboBox.Items.Add(u);
            }
            foreach (string a in calign)
                AligncomboBox.Items.Add(a);
            widthUnitcomboBox.SelectedIndex = AligncomboBox.SelectedIndex = 0;
        }

        private void lineUserControl_Paint(object sender, PaintEventArgs e)
        {
            GeneralMethods.drawLineForProPanel(this, Color.Blue, 1, DashStyle.Solid,Width);
        }

        #region 属性
        public string LineWidth
        {
            get { return widthtextBox.Text; }
            set { widthtextBox.Text = value; }
        }
        public string LineWidthUnit
        {
            get { return widthUnitcomboBox.Text; }
            set
            {
                widthUnitcomboBox.SelectedIndex = Convert.ToInt32(value);
            }
        }
        public string LineHeight
        {
            get { return heighttextBox.Text; }
            set { heighttextBox.Text = value; }
        }
        public bool LineShading
        {
            get { return shadcheckBox.Checked; }
            set { shadcheckBox.Checked = value; }
        }
        public string LineAlign
        {
            get { return AligncomboBox.Text; }
            set
            {
                switch (value)
                {
                    case null: AligncomboBox.SelectedIndex = 0; break;
                    case "left": AligncomboBox.SelectedIndex = 1; break;
                    case "center": AligncomboBox.SelectedIndex = 2; break;
                    case "right": AligncomboBox.SelectedIndex = 3; break;
                }
            }
        }
        #endregion
    }
}
