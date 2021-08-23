using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.App.XPath
{
    public class HelpForm : Form
    {
        private readonly Container components = null;
        private TextBox textBox1;

        public HelpForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(5, 5);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(378, 156);
            textBox1.TabIndex = 0;
            textBox1.Text = "simpkan@gmail.com\r\n2010-08-04 17:27:26";
            // 
            // HelpForm
            // 
            AutoScaleBaseSize = new Size(5, 14);
            ClientSize = new Size(388, 166);
            Controls.Add(textBox1);
            Font = new Font("Tahoma", 8.25F);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HelpForm";
            Padding = new Padding(5);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Help";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}