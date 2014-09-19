using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.Win
{
    [ResReader(false)]
    public class FileSelecterControl : BaseControl
    {
        #region ���õ��Ŀؼ�����
        MyOpenFileButton btnSelecter;
        TextBox fileNamesTextBox;
        public TextBox FileNamesTextBox
        {
            get { return fileNamesTextBox; }
        }
        ComboBox fileNamesComboBox;
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ�����ÿؼ�����ʾ��ʽ
        /// </summary>
        private FileSelectControlStyle _style;

        /// <summary>
        /// ��ȡ�����ÿؼ�����ʾ��ʽ
        /// </summary>
        public FileSelectControlStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// ���صĵ����ļ��ļ���
        /// </summary>
        private string _fileName;

        /// <summary>
        /// ���صĵ����ļ��ļ���
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public override string Text
        {
            get
            {
                return FileNamesTextBox.Text;
            }
            set
            {
                FileNamesTextBox.Text = value;
            }
        }

        /// <summary>
        /// ���ص�һ���ļ��ļ���
        /// </summary>
        private string[] _fileNames;

        /// <summary>
        /// ���ص�һ���ļ��ļ���
        /// </summary>
        public string[] FileNames
        {
            get { return _fileNames; }
            set { _fileNames = value; }
        }

        /// <summary>
        /// ������򿪵��ļ����͹�����
        /// </summary>
        private FileSelectFilter _fileSelectFilter;

        /// <summary>
        /// ������򿪵��ļ����͹�����
        /// </summary>
        public FileSelectFilter FileSelectFilter
        {
            get { return _fileSelectFilter; }
            set { _fileSelectFilter = value; }
        }

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի����Ƿ���Զ�ѡ�ļ�
        /// </summary>
        private bool _multiSelect;

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի����Ƿ���Զ�ѡ�ļ�
        /// </summary>
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի���ĳ�ʼĿ¼
        /// </summary>
        private string _initialDirectory;

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի���ĳ�ʼĿ¼
        /// </summary>
        public string InitialDirectory
        {
            get { return _initialDirectory; }
            set { _initialDirectory = value; }
        }

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի���ı���
        /// </summary>
        private string _dialogTitle = "Open Files";

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի���ı���
        /// </summary>
        public string DialogTitle
        {
            get { return _dialogTitle; }
            set { _dialogTitle = value; }
        }

        /// <summary>
        /// ��ȡ����������Button����ʾ������
        /// </summary>
        private string _word = "���(&B)...";

        /// <summary>
        /// ��ȡ����������Button����ʾ������
        /// </summary>
        public string Word
        {
            get { return _word; }
            set { _word = value; }
        }

        /// <summary>
        /// ��ȡ�������ļ�ѡ�����ʷ��¼
        /// </summary>
        private string[] _filesHistory;

        /// <summary>
        /// ��ȡ�������ļ�ѡ�����ʷ��¼
        /// </summary>
        public string[] FilesHistory
        {
            get { return _filesHistory; }
            set { _filesHistory = value; }
        }

        /// <summary>
        /// �ؼ��Ƿ�ֻ��
        /// </summary>
        private bool _readonly = false;
        /// <summary>
        /// �ؼ��Ƿ�ֻ��
        /// </summary>
        public bool ReadOnly
        {
            get { return _readonly; }
            set
            {
                if (value == false)
                {
                    if (this.btnSelecter != null)
                    {
                        this.btnSelecter.Enabled = true;
                    }
                    this.fileNamesTextBox.ReadOnly = false;
                    this.fileNamesComboBox.Enabled = false;
                }
                else
                {
                    if (this.btnSelecter != null)
                    {
                        this.btnSelecter.Enabled = false;
                    }
                    this.fileNamesTextBox.ReadOnly = true;
                    this.fileNamesComboBox.Enabled = true;
                }
                _readonly = value;
            }
        }

        private bool _selectFolder;
        /// <summary>
        /// ���öԻ�����ѡ��Ŀ¼����
        /// </summary>
        public bool SelectFolder { get { return _selectFolder; } set { _selectFolder = value; } }

        #endregion

        public FileSelecterControl()
        {
            fileNamesTextBox = new TextBox();
            fileNamesComboBox = new ComboBox();
        }

        protected override void OnCreateControl()
        {
            this.SuspendLayout();
            base.OnCreateControl();
            this.Height = 23;

            if (string.IsNullOrEmpty(this.Text))
            {
                this._fileName = this.Text;
            }
            else
            {
                this._fileName = string.Empty;
            }

            Icon icon = Resource.OpenFolder;

            // �ж�Button����ʾ�����ֵĿ�ȣ���ȷ��Button�Ĵ�С
            Graphics g = CreateGraphics();
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(_word, this.Font);

            switch (_style)
            {
                #region ���ݿؼ�����ʽ�趨�ؼ�����ʾ
                case FileSelectControlStyle.TextBoxAndTextButton:
                    btnSelecter = new MyOpenFileButton(_word);
                    this.btnSelecter.Size = new Size((int)stringSize.Width + 20, this.Height);
                    this.fileNamesTextBox.Width = this.Width - this.btnSelecter.Width - 5;
                    this.btnSelecter.Location = new Point(this.fileNamesTextBox.Width + 5, 0);
                    this.fileNamesTextBox.Location = new Point(0, 1);
                    this.fileNamesTextBox.Text = this.Text;
                    this.Controls.Add(fileNamesTextBox);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.None:
                case FileSelectControlStyle.TextBoxAndImageButton:
                    btnSelecter = new MyOpenFileButton(icon);
                    this.btnSelecter.Size = new Size(this.Height, this.Height);
                    this.fileNamesTextBox.Width = this.Width - this.btnSelecter.Width - 5;
                    this.btnSelecter.Location = new Point(this.fileNamesTextBox.Width + 5, 0);
                    this.fileNamesTextBox.Location = new Point(0, 1);
                    this.fileNamesTextBox.Text = this.Text;
                    this.Controls.Add(fileNamesTextBox);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.TextBoxAndTextImageButton:
                    btnSelecter = new MyOpenFileButton(_word, icon);
                    this.btnSelecter.Size = new Size((int)stringSize.Width + this.Height + 4, this.Height);
                    this.fileNamesTextBox.Width = this.Width - this.btnSelecter.Width - 5;
                    this.btnSelecter.Location = new Point(this.fileNamesTextBox.Width + 5, 0);
                    this.fileNamesTextBox.Location = new Point(0, 1);
                    this.fileNamesTextBox.Text = this.Text;
                    this.Controls.Add(fileNamesTextBox);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.ComboBoxAndTextButton:
                    btnSelecter = new MyOpenFileButton(_word);
                    this.btnSelecter.Size = new Size((int)stringSize.Width + 20, this.Height);
                    this.fileNamesComboBox.Width = this.Width - this.btnSelecter.Width - 5;
                    this.btnSelecter.Location = new Point(this.fileNamesComboBox.Width + 5, 0);
                    this.fileNamesComboBox.Location = new Point(0, 1);
                    if (this._filesHistory != null)
                        this.fileNamesComboBox.Items.AddRange(this._filesHistory);
                    this.fileNamesComboBox.Text = this.Text;
                    this.Controls.Add(fileNamesComboBox);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.ComboBoxAndImageButton:
                    btnSelecter = new MyOpenFileButton(icon);
                    this.btnSelecter.Size = new Size(this.Height, this.Height);
                    this.fileNamesComboBox.Width = this.Width - this.btnSelecter.Width - 5;
                    this.btnSelecter.Location = new Point(this.fileNamesComboBox.Width + 5, 0);
                    this.fileNamesComboBox.Location = new Point(0, 1);
                    if (this._filesHistory != null)
                        this.fileNamesComboBox.Items.AddRange(this._filesHistory);
                    this.fileNamesComboBox.Text = this.Text;
                    this.Controls.Add(fileNamesComboBox);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.ComboBoxAndTextImageButton:
                    btnSelecter = new MyOpenFileButton(_word, icon);
                    this.btnSelecter.Size = new Size((int)stringSize.Width + this.Height + 4, this.Height);
                    this.fileNamesComboBox.Width = this.Width - this.btnSelecter.Width - 5;
                    this.btnSelecter.Location = new Point(this.fileNamesComboBox.Width + 5, 0);
                    this.fileNamesComboBox.Location = new Point(0, 1);
                    if (this._filesHistory != null)
                        this.fileNamesComboBox.Items.AddRange(this._filesHistory);
                    this.fileNamesComboBox.Text = this.Text;
                    this.Controls.Add(fileNamesComboBox);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.OnlyTextButton:
                    btnSelecter = new MyOpenFileButton(_word);
                    this.btnSelecter.Size = new Size((int)stringSize.Width + 20, this.Height);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.OnlyImageButton:
                    btnSelecter = new MyOpenFileButton(icon);
                    this.btnSelecter.Size = new Size(this.Height, this.Height);
                    this.Controls.Add(btnSelecter);
                    break;

                case FileSelectControlStyle.OnlyTextImageButton:
                    btnSelecter = new MyOpenFileButton(_word, icon);
                    this.btnSelecter.Size = new Size((int)stringSize.Width + this.Height + 4, this.Height);
                    this.Controls.Add(btnSelecter);
                    break;

                default:
                    break;
                #endregion
            }
            this.ResumeLayout(false);
            
            this.btnSelecter.Enabled = !this.ReadOnly;
            this.btnSelecter.Click += new EventHandler(OpenFileDialogEvent);
        }

        void OpenFileDialogEvent(object sender, EventArgs e)
        {
            if (_selectFolder)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.SelectedPath = this.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._fileName = dlg.SelectedPath;
                    if (this.Controls.Contains(this.fileNamesComboBox))
                        this.fileNamesComboBox.Text = this._fileName;
                    else if (this.Controls.Contains(this.fileNamesTextBox))
                        this.fileNamesTextBox.Text = this._fileName;

                    OnFileSelected(EventArgs.Empty);
                }
            }
            else
            {
                OpenFileDialog openfiledialog = new OpenFileDialog();
                openfiledialog.Multiselect = this._multiSelect;
                openfiledialog.Title = this._dialogTitle;
                openfiledialog.InitialDirectory = this._initialDirectory;
                switch (_fileSelectFilter)
                {
                    case FileSelectFilter.All:
                        openfiledialog.Filter = "All Files|*.*";
                        break;
                    case FileSelectFilter.Image:
                        openfiledialog.Filter = "Image Files|*.bmp;*.jpg;*.ico;*.icon;*.png;*.gif;|All Files|*.*";
                        break;
                    case FileSelectFilter.Media:
                        openfiledialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.*";
                        break;
                    case FileSelectFilter.Txt:
                        openfiledialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                        break;
                    case FileSelectFilter.SdSite:
                        openfiledialog.Filter = "��վ�ļ� (*.sdsite)|*.sdsite|All Files (*.*)|*.*";
                        break;
                    case FileSelectFilter.SdPage:
                        openfiledialog.Filter = "ҳ���ļ� (*.sdpage)|*.sdpage|All Files (*.*)|*.*";
                        break;
                    case FileSelectFilter.SdTmplt:
                        openfiledialog.Filter = "ģ���ļ� (*.sdtmplt)|*.sdtmplt|All Files (*.*)|*.*";
                        break;
                    default:
                        break;
                }

                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    if (_multiSelect)//�ж��Ƿ���Զ�ѡ
                    {
                        this._fileNames = openfiledialog.FileNames;
                        if (this.Controls.Contains(this.fileNamesComboBox))
                            this.fileNamesComboBox.Text = this._fileNames[0] + ", ...";
                        else if (this.Controls.Contains(this.fileNamesTextBox))
                            this.fileNamesTextBox.Text = this._fileNames[0] + ", ...";
                    }
                    else
                    {
                        this._fileName = openfiledialog.FileName;
                        if (this.Controls.Contains(this.fileNamesComboBox))
                            this.fileNamesComboBox.Text = this._fileName;
                        else if (this.Controls.Contains(this.fileNamesTextBox))
                            this.fileNamesTextBox.Text = this._fileName;
                    }
                    OnFileSelected(EventArgs.Empty);
                }
            }
            base.OnClick(e);
        }
        public event EventHandler FileSelected;
        protected void OnFileSelected(EventArgs e)
        {
            if (FileSelected != null)
            {
                FileSelected(this, e);
            }
        }
    }

    /// <summary>
    /// �Զ���Button,ΪButton����ͼ�꼰�ı�
    /// </summary>
    class MyOpenFileButton : Button
    {
        Icon _myIcon;
        string _myText;
        public MyOpenFileButton(Icon icon)
        {
            _myIcon = icon;
        }
        public MyOpenFileButton(string text)
        {
            _myText = text;
        }
        public MyOpenFileButton(string text, Icon icon)
        {
            _myText = text;
            _myIcon = icon;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;

            if (_myIcon != null && _myText == null)
            {
                g.DrawIcon(_myIcon, new Rectangle(3, 3, this.Width - 5, this.Height - 5));
            }
            else if (_myText != null && _myIcon == null)
            {
                g.DrawString(_myText, this.Font, Brushes.Black, 10, 5);
            }
            else if (_myText != null && _myIcon != null)
            {
                g.DrawIcon(_myIcon, new Rectangle(3 + 2, 3, this.Height - 5, this.Height - 5));
                g.DrawString(_myText, this.Font, Brushes.Black, this.Height - 5 + 3 + 2, 5);
            }
        }
    }

}
