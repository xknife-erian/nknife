using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.App.XPath
{
    public class CodeForm : Form
    {
        private Button btnCopy;
        private readonly Container components = null;
        private Label label1;
        private TextBox txtCode;

        public CodeForm(string code)
        {
            InitializeComponent();
            txtCode.Text = code;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                txtCode.SelectAll();
                txtCode.Copy();
                label1.Text = "Code is copied to the clipboard";
            }
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
            txtCode = new TextBox();
            btnCopy = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtCode
            // 
            txtCode.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                              | AnchorStyles.Left)
                             | AnchorStyles.Right;
            txtCode.Font = new Font("Courier New", 8.25F);
            txtCode.Location = new Point(5, 5);
            txtCode.Multiline = true;
            txtCode.Name = "txtCode";
            txtCode.ScrollBars = ScrollBars.Vertical;
            txtCode.Size = new Size(462, 272);
            txtCode.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopy.Location = new Point(377, 283);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(90, 25);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "&Copy";
            btnCopy.Click += btnCopy_Click;
            // 
            // label1
            // 
            label1.Location = new Point(0, 379);
            label1.Name = "label1";
            label1.Size = new Size(566, 25);
            label1.TabIndex = 2;
            // 
            // CodeForm
            // 
            AutoScaleBaseSize = new Size(5, 14);
            ClientSize = new Size(472, 325);
            Controls.Add(label1);
            Controls.Add(btnCopy);
            Controls.Add(txtCode);
            Font = new Font("Tahoma", 8.25F);
            Name = "CodeForm";
            Padding = new Padding(5);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "CodeForm";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}