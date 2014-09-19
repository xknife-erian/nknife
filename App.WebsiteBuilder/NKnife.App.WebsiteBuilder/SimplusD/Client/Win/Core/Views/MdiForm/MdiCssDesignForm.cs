using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class MdiCssDesignForm : BaseEditViewForm
    {
        public TextBox MainTextBox { get; private set; }
        public MdiCssDesignForm(string tmpltId)
        {
            this.MainTextBox = new TextBox();
            this.MainTextBox.AcceptsReturn = true;
            this.MainTextBox.AcceptsTab = true;
            this.MainTextBox.Dock = DockStyle.Fill;
            this.MainTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainTextBox.Location = new System.Drawing.Point(0, 0);
            this.MainTextBox.Multiline = true;
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MainTextBox.Size = new System.Drawing.Size(96, 100);
            this.MainTextBox.TabIndex = 0;
            this.MainTextBox.WordWrap = false;

            this.Controls.Add(MainTextBox);
        }
    }
}
