using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.Win
{
    public partial class InsertDatetimeForm : BaseForm
    {
        string insertDateTimeHTML = "";

        public string InsertDateTimeHTML
        {
            get { return insertDateTimeHTML; }
            set { insertDateTimeHTML = value; }
        }
        public InsertDatetimeForm()
        {
            InitializeComponent();
            //GetCHSTextforInsertDateTime CHSText = new GetCHSTextforInsertDateTime();
            //string[] strDay = CHSText.DayFormat;
            //string[] strTime = CHSText.TimeFormat;
            //string[] strDate = CHSText.DateFormat;
            //foreach (string dayf in strDay)
            //    dayComboBox.Items.Add(dayf);
            //foreach (string timef in strTime)
            //    TimeComboBox.Items.Add(timef);
            //foreach (string datef in strDate)
            //    DateListBox.Items.Add(datef);
            //Daylabel.Text = ResourceService.GetResourceText("date.label.day");
            //Datelabel.Text = ResourceService.GetResourceText("date.label.date");
            //Timelabel.Text = ResourceService.GetResourceText("date.label.time");
            //updateCheckBox.Text = ResourceService.GetResourceText("date.checkbox.text");
            //OKBtn.Text = ResourceService.GetResourceText("date.button.ok");
            //CancelBtn.Text = ResourceService.GetResourceText("date.button.cancel");
            //HelpBtn.Text = ResourceService.GetResourceText("date.button.help");
            //this.Text = ResourceService.GetResourceText("date.caption.text");
            //dayComboBox.SelectedIndex = TimeComboBox.SelectedIndex = DateListBox.SelectedIndex = 0;

            this.ImeMode = ImeMode.On;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            DayOfWeek dy = DateTime.Today.DayOfWeek;
            string engday = "";
            string chsday = "";
            switch (dy)
            {
                case DayOfWeek.Monday: { chsday = "星期一"; engday = "Mon"; break; }
                case DayOfWeek.Tuesday: { chsday = "星期二"; engday = "Tue"; break; }
                case DayOfWeek.Wednesday: { chsday = "星期三"; engday = "Wed"; break; }
                case DayOfWeek.Thursday: { chsday = "星期四"; engday = "Thu"; break; }
                case DayOfWeek.Friday: { chsday = "星期五"; engday = "Fri"; break; }
                case DayOfWeek.Saturday: { chsday = "星期六"; engday = "Sat"; break; }
                case DayOfWeek.Sunday: { chsday = "星期日"; engday = "Sun"; break; }
            }
            string dateformat = "";
            string timeformat = "";
            int datef = DateListBox.SelectedIndex;
            int dayf =dayComboBox.SelectedIndex;
            int timef = TimeComboBox.SelectedIndex;
            bool update = updateCheckBox.Checked;
            switch (datef)
            {
                case 0: dateformat += @"yy\/MM\/dd"; break;
                case 1: dateformat += "yyyy年MM月dd日"; break;
                case 2: dateformat += @"MM\/dd\/yyyy"; break;
                case 3: dateformat += @"M\/d\/yy"; break;
                case 4: dateformat += "yyyy-MM-dd"; break;
                case 5: dateformat += @"d\/M\/yy"; break;
                case 6: dateformat += @"d\/MM\/yy"; break;
                case 7: dateformat += "dd.MM.yyyy"; break;
                case 8: dateformat += "dd.MM.yy"; break;
                case 9: dateformat += "d-MM-yyyy"; break;
            }
            switch (timef)
            {
                case 0: break;
                case 2: timeformat += " HH:mm"; ; break;
                case 1: timeformat += " hh:mm tt"; break;

            }

            if (update)
            { }
            else
            { }
            string strDate = DateTime.Now.ToString(dateformat) + " ";
            string strDay = "";
            string strTime = "";
            if (timef > 0)
                strTime = DateTime.Now.ToString(timeformat);
            switch (dayf)
            {
                case 1: strDay = chsday; break;
                case 2: strDay = chsday + " "; break;
                case 3: strDay = dy.ToString(); break;
                case 4: strDay = engday; break;
            }
            insertDateTimeHTML = strDate + " " + strDay + " " + strTime;

            this.DialogResult = DialogResult.OK;
        }


    }
}