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
        #region 需用到的控件对象
        MyOpenFileButton btnSelecter;
        TextBox fileNamesTextBox;
        public TextBox FileNamesTextBox
        {
            get { return fileNamesTextBox; }
        }
        ComboBox fileNamesComboBox;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置控件的显示样式
        /// </summary>
        private FileSelectControlStyle _style;

        /// <summary>
        /// 获取或设置控件的显示样式
        /// </summary>
        public FileSelectControlStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// 返回的单个文件文件名
        /// </summary>
        private string _fileName;

        /// <summary>
        /// 返回的单个文件文件名
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
        /// 返回的一组文件文件名
        /// </summary>
        private string[] _fileNames;

        /// <summary>
        /// 返回的一组文件文件名
        /// </summary>
        public string[] FileNames
        {
            get { return _fileNames; }
            set { _fileNames = value; }
        }

        /// <summary>
        /// 设置需打开的文件类型过滤器
        /// </summary>
        private FileSelectFilter _fileSelectFilter;

        /// <summary>
        /// 设置需打开的文件类型过滤器
        /// </summary>
        public FileSelectFilter FileSelectFilter
        {
            get { return _fileSelectFilter; }
            set { _fileSelectFilter = value; }
        }

        /// <summary>
        /// 获取或设置打开文件对话框是否可以多选文件
        /// </summary>
        private bool _multiSelect;

        /// <summary>
        /// 获取或设置打开文件对话框是否可以多选文件
        /// </summary>
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }

        /// <summary>
        /// 获取或设置打开文件对话框的初始目录
        /// </summary>
        private string _initialDirectory;

        /// <summary>
        /// 获取或设置打开文件对话框的初始目录
        /// </summary>
        public string InitialDirectory
        {
            get { return _initialDirectory; }
            set { _initialDirectory = value; }
        }

        /// <summary>
        /// 获取或设置打开文件对话框的标题
        /// </summary>
        private string _dialogTitle = "Open Files";

        /// <summary>
        /// 获取或设置打开文件对话框的标题
        /// </summary>
        public string DialogTitle
        {
            get { return _dialogTitle; }
            set { _dialogTitle = value; }
        }

        /// <summary>
        /// 获取或设置文字Button上显示的文字
        /// </summary>
        private string _word = "浏览(&B)...";

        /// <summary>
        /// 获取或设置文字Button上显示的文字
        /// </summary>
        public string Word
        {
            get { return _word; }
            set { _word = value; }
        }

        /// <summary>
        /// 获取或设置文件选择的历史记录
        /// </summary>
        private string[] _filesHistory;

        /// <summary>
        /// 获取或设置文件选择的历史记录
        /// </summary>
        public string[] FilesHistory
        {
            get { return _filesHistory; }
            set { _filesHistory = value; }
        }

        /// <summary>
        /// 控件是否只读
        /// </summary>
        private bool _readonly = false;
        /// <summary>
        /// 控件是否只读
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
        /// 设置对话框是选择目录来着
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

            // 判断Button上显示的文字的宽度，以确定Button的大小
            Graphics g = CreateGraphics();
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(_word, this.Font);

            switch (_style)
            {
                #region 根据控件的样式设定控件的显示
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
                        openfiledialog.Filter = "网站文件 (*.sdsite)|*.sdsite|All Files (*.*)|*.*";
                        break;
                    case FileSelectFilter.SdPage:
                        openfiledialog.Filter = "页面文件 (*.sdpage)|*.sdpage|All Files (*.*)|*.*";
                        break;
                    case FileSelectFilter.SdTmplt:
                        openfiledialog.Filter = "模板文件 (*.sdtmplt)|*.sdtmplt|All Files (*.*)|*.*";
                        break;
                    default:
                        break;
                }

                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    if (_multiSelect)//判断是否可以多选
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
    /// 自定义Button,为Button绘制图标及文本
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
