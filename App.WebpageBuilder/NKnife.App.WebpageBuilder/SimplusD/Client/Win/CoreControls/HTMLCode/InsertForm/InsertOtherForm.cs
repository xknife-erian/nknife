using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class InsertOtherForm : BaseForm
    {
        public string InsertSign
        {
            get
            {
              return  insertSignTextBox.Text.Trim();
            }
        }
        public InsertOtherForm()
        {
            InitializeComponent();

            string[,] name = initName();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(21, 21);
                    if (name[i, j] != null)
                        btn.Name = name[i, j];
                    btn.Text = HttpUtility.HtmlDecode("&"+name[i,j]+";");
                    btn.Click += new EventHandler(btn_Click);
                    btn.Location = new Point(10 + j * 23, 10 + i * 21);
                    panel1.Controls.Add(btn);
                }
            }

        }

        void btn_Click(object sender, EventArgs e)
        {
            insertSignTextBox.Text = "&" + ((Button)sender).Name + ";";
        }

        string[,] initName()
        {
            string[,] name = new string[9, 11];
            name[0, 0] = "nbsp";
            name[0, 1] = "iexcl";
            name[0, 2] = "cent";
            name[0, 3] = "pound";
            name[0, 4] = "yen";
            name[0, 5] = "sect";
            name[0, 6] = "uml";
            name[0, 7] = "copy";
            name[0, 8] = "laquo";
            name[0, 9] = "not";
            name[0, 10] = "reg";

            name[1, 0] = "deg";
            name[1, 1] = "plusmn";
            name[1, 2] = "acute";
            name[1, 3] = "micro";
            name[1, 4] = "para";
            name[1, 5] = "middot";
            name[1, 6] = "cedil";
            name[1, 7] = "raquo";
            name[1, 8] = "iquest";
            name[1, 9] = "Agrave";
            name[1, 10] = "Aacute";

            name[2, 0] = "Acirc";
            name[2, 1] = "Atilde";
            name[2, 2] = "Auml";
            name[2, 3] = "Aring";
            name[2, 4] = "AElig";
            name[2, 5] = "Ccedil";
            name[2, 6] = "Egrave";
            name[2, 7] = "Eacute";
            name[2, 8] = "Ecirc";
            name[2, 9] = "Euml";
            name[2, 10] = "Igrave";

            name[3, 0] = "Iacute";
            name[3, 1] = "Icirc";
            name[3, 2] = "Iuml";
            name[3, 3] = "Ntilde";
            name[3, 4] = "Ograve";
            name[3, 5] = "Oacute";
            name[3, 6] = "Ocirc";
            name[3, 7] = "Otilde";
            name[3, 8] = "Ouml";
            name[3, 9] = "Oslash";
            name[3, 10] = "Ugrave";

            name[4, 0] = "Uacute";
            name[4, 1] = "Ucirc";
            name[4, 2] = "Uuml";
            name[4, 3] = "szlig";
            name[4, 4] = "agrave";
            name[4, 5] = "aacute";
            name[4, 6] = "acirc";
            name[4, 7] = "atilde";
            name[4, 8] = "auml";
            name[4, 9] = "aring";
            name[4, 10] = "aelig";

            name[5, 0] = "ccedil";
            name[5, 1] = "egrave";
            name[5, 2] = "eacute";
            name[5, 3] = "ecirc";
            name[5, 4] = "euml";
            name[5, 5] = "igrave";
            name[5, 6] = "iacute";
            name[5, 7] = "icirc";
            name[5, 8] = "iuml";
            name[5, 9] = "ntilde";
            name[5, 10] = "ograve";

            name[6, 0] = "oacute";
            name[6, 1] = "ocirc";
            name[6, 2] = "otilde";
            name[6, 3] = "ouml";
            name[6, 4] = "divide";
            name[6, 5] = "oslash";
            name[6, 6] = "ugrave";
            name[6, 7] = "uacute";
            name[6, 8] = "ucirc";
            name[6, 9] = "uuml";
            name[6, 10] = "yuml";

            name[7, 0] = "#8218";
            name[7, 1] = "#402";
            name[7, 2] = "#8222";
            name[7, 3] = "#8230";
            name[7, 4] = "#8224";
            name[7, 5] = "#8225";
            name[7, 6] = "#710";
            name[7, 7] = "#8240";
            name[7, 8] = "#8249";
            name[7, 9] = "#338";
            name[7, 10] = "#8216";


            name[8, 0] = "#8217";
            name[8, 1] = "#8220";
            name[8, 2] = "#8221";
            name[8, 3] = "#8226";
            name[8, 4] = "#8211";
            name[8, 5] = "#8212";
            name[8, 6] = "#732";
            name[8, 7] = "#8482";
            name[8, 8] = "#8250";
            name[8, 9] = "#339";
            name[8, 10] = "#376";

            return name;
        }
    }
}
