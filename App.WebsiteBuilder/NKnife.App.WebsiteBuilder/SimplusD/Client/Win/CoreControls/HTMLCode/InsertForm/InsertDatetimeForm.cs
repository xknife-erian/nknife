using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class InsertDatetimeForm : BaseForm
    {
        public InsertDatetimeForm()
        {
            InitializeComponent();
            GetCHSTextforInsertDateTime CHSText = new GetCHSTextforInsertDateTime();
            string[] strDay = CHSText.DayFormat;
            string[] strTime = CHSText.TimeFormat;
            string[] strDate = CHSText.DateFormat;
            foreach (string dayf in strDay)
                dayComboBox.Items.Add(dayf);
            foreach (string timef in strTime)
                TimeComboBox.Items.Add(timef);
            foreach (string datef in strDate)
                DateListBox.Items.Add(datef);
            Daylabel.Text = ResourceService.GetResourceText("date.label.day");
            Datelabel.Text = ResourceService.GetResourceText("date.label.date");
            Timelabel.Text = ResourceService.GetResourceText("date.label.time");
            updateCheckBox.Text = ResourceService.GetResourceText("date.checkbox.text");
            OKBtn.Text = ResourceService.GetResourceText("date.button.ok");
            CancelBtn.Text = ResourceService.GetResourceText("date.button.cancel");
            HelpBtn.Text = ResourceService.GetResourceText("date.button.help");
            this.Text = ResourceService.GetResourceText("date.caption.text");
            dayComboBox.SelectedIndex = TimeComboBox.SelectedIndex = DateListBox.SelectedIndex = 0;
            
            this.ImeMode = ImeMode.On;
        }

        #region 属性
        public int dayFormat
        {
            get
            {
               return dayComboBox.SelectedIndex;
            }
            set
            {
                dayComboBox.SelectedIndex = value;
            }
        }
        public int dateFormat
        {
            get
            {
                return DateListBox.SelectedIndex;
            }
            set
            {
                DateListBox.SelectedIndex = value;
            }
        }
        public int TimeFormat
        {
            get
            {
                return TimeComboBox.SelectedIndex;
            }
            set
            {
                TimeComboBox.SelectedIndex= value;
            }
        }
        public bool updated
        {
            get
            {
                return updateCheckBox.Checked;
            }
            set
            {
                updateCheckBox.Checked = value;
            }
        }
        #endregion


    }
}