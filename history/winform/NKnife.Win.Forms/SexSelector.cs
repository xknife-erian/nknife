using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Win.Forms
{
    public class SexSelector : UserControl
    {
        private RadioButton _femaleRadio;
        private RadioButton _maleRadio;

        public SexSelector()
        {
            InitializeComponent();
        }

        public PersonSex Selected
        {
            get
            {
                if (_femaleRadio.Checked)
                    return PersonSex.Female;
                if (_maleRadio.Checked)
                    return PersonSex.Male;
                return PersonSex.None;
            }
            set
            {
                switch (value)
                {
                    case PersonSex.None:
                        _maleRadio.Checked = false;
                        _femaleRadio.Checked = false;
                        break;
                    case PersonSex.Male:
                        _maleRadio.Checked = true;
                        break;
                    case PersonSex.Female:
                        _femaleRadio.Checked = true;
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            _femaleRadio = new RadioButton();
            _maleRadio = new RadioButton();
            SuspendLayout();
            // 
            // radioButton2
            // 
            _femaleRadio.AutoSize = true;
            _femaleRadio.Location = new Point(42, 4);
            _femaleRadio.Name = "_femaleRadio";
            _femaleRadio.Size = new Size(35, 16);
            _femaleRadio.TabIndex = 10;
            _femaleRadio.TabStop = true;
            _femaleRadio.Text = "女";
            _femaleRadio.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            _maleRadio.AutoSize = true;
            _maleRadio.Location = new Point(3, 4);
            _maleRadio.Name = "_maleRadio";
            _maleRadio.Size = new Size(35, 16);
            _maleRadio.TabIndex = 9;
            _maleRadio.TabStop = true;
            _maleRadio.Text = "男";
            _maleRadio.UseVisualStyleBackColor = true;
            // 
            // SexSelector
            // 
            Controls.Add(_femaleRadio);
            Controls.Add(_maleRadio);
            Name = "SexSelector";
            Size = new Size(86, 22);
            ResumeLayout(false);
            PerformLayout();
        }
    }

    public enum PersonSex
    {
        Female,
        Male,
        None
    }
}