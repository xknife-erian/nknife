using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Win.Forms.Common;

namespace NKnife.Win.Forms
{
    /// <summary>
    ///     �ļ�ѡ�����ؼ�
    /// </summary>
    public class FileSelector : UserControl
    {
        private Control _FileNamesControl;
        private Button _FileSelectorButton;

        /// <summary>
        ///     ���ļ���ѡ��������¼�
        /// </summary>
        public event EventHandler FileSelected;

        protected void OnFileSelected(EventArgs e)
        {
            if (FileSelected != null)
                FileSelected(this, e);
        }

        /// <summary>
        ///     ��ʼ���ؼ�
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            SuspendLayout();

            _Style = FileSelectControlStyle.TextBoxAndTextButton;
            SetViewBoxStyle();

            InitialDirectory = string.Empty;
            DialogTitle = string.Empty;
            FileName = InitialDirectory;
            FileNames = null;
            ButtonImage = null;

            _FileNamesControl.Location = new Point(0, 1);
            _FileNamesControl.Size = new Size(150, 23);
            Controls.Add(_FileNamesControl);

            _FileSelectorButton = new Button();
            _FileSelectorButton.Location = new Point(_FileNamesControl.Width + 2, 0);
            _FileSelectorButton.Size = new Size(23, 23);
            _FileSelectorButton.Click += _FileSelectorButton_Click;
            Controls.Add(_FileSelectorButton);

            SetSize();

            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        ///     ���ÿؼ�����Ŀ��
        /// </summary>
        private void SetSize()
        {
            var width = _FileNamesControl.Width + 2 + _FileSelectorButton.Width;
            Size = new Size(width, 23);
        }

        /// <summary>
        ///     ���ÿؼ��е�ViewBoxStyle
        /// </summary>
        private void SetViewBoxStyle()
        {
            switch (_Style) //������ʽ����Box�ؼ���TextBox����CommoBox
            {
                case FileSelectControlStyle.None:
                case FileSelectControlStyle.TextBoxAndTextButton:
                case FileSelectControlStyle.TextBoxAndImageButton:
                case FileSelectControlStyle.TextBoxAndTextImageButton:
                {
                    _FileNamesControl = new TextBox();
                    _FileNamesControl.Name = "View_TextBox";
                    break;
                }
                case FileSelectControlStyle.ComboBoxAndTextButton:
                case FileSelectControlStyle.ComboBoxAndImageButton:
                case FileSelectControlStyle.ComboBoxAndTextImageButton:
                {
                    _FileNamesControl = new ComboBox();
                    _FileNamesControl.Name = "View_ComboBox";
                    break;
                }
                case FileSelectControlStyle.OnlyTextButton:
                case FileSelectControlStyle.OnlyImageButton:
                case FileSelectControlStyle.OnlyTextImageButton:
                default:
                {
                    _FileNamesControl = null;
                    break;
                }
            }
        }

        private void _FileSelectorButton_Click(object sender, EventArgs e)
        {
            if (IsSelectFolder) //ѡ��Ŀ¼�ķ�ʽ
            {
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = dialog.SelectedPath;
                    if (Controls.Contains(_FileNamesControl))
                        _FileNamesControl.Text = FileName;
                    OnFileSelected(EventArgs.Empty);
                }
            }
            else //ѡ���ļ��ķ�ʽ
            {
                var dialog = new OpenFileDialog();
                dialog.Multiselect = MultiSelect;
                dialog.Title = DialogTitle;
                dialog.InitialDirectory = InitialDirectory;
                dialog.Filter = Filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (MultiSelect) //�ж��Ƿ���Զ�ѡ
                    {
                        FileNames = dialog.FileNames;
                        if (_FileNamesControl != null)
                            if (FileNames.Length > 1)
                                _FileNamesControl.Text = FileNames[0] + ", ...";
                            else
                                _FileNamesControl.Text = FileNames[0];
                    }
                    else
                    {
                        FileName = dialog.FileName;
                        if (_FileNamesControl != null)
                            if (_FileNamesControl is TextBox)
                                _FileNamesControl.Text = FileName;
                    }
                    OnFileSelected(EventArgs.Empty);
                }
            }
            OnClick(e);
        }

        #region ���캯��

        /// <summary>
        ///     ����������������
        /// </summary>
        private IContainer components;

        /// <summary>
        ///     ���캯��
        /// </summary>
        public FileSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        #region ����

        private bool _ReadOnly;
        private FileSelectControlStyle _Style = FileSelectControlStyle.TextBoxAndTextButton;

        /// <summary>
        ///     ��ȡ�����ÿؼ�����ʾ��ʽ
        /// </summary>
        public FileSelectControlStyle Style
        {
            get => _Style;
            set
            {
                _Style = value;
                SetViewBoxStyle();
            }
        }

        /// <summary>
        ///     ������򿪵��ļ����͹�����
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        ///     ���صĵ����ļ��ļ���
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        ///     ���ص�һ���ļ��ļ���
        /// </summary>
        public string[] FileNames { get; private set; }

        /// <summary>
        ///     ��ȡ�����ô��ļ��Ի����Ƿ���Զ�ѡ�ļ�
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        ///     ��ȡ�����ô��ļ��Ի���ĳ�ʼĿ¼
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        ///     ��ȡ�����ô��ļ��Ի���ı���
        /// </summary>
        public string DialogTitle { get; set; }

        /// <summary>
        ///     ��ȡ������ViewBox�Ŀ��
        /// </summary>
        public int ViewBoxWidth
        {
            get => _FileNamesControl.Width;
            set
            {
                if (_FileNamesControl != null)
                {
                    _FileNamesControl.Width = value;
                    _FileSelectorButton.Location = new Point(_FileNamesControl.Width + 2, 0);
                    SetSize();
                }
            }
        }

        /// <summary>
        ///     ��ȡ����������Button����ʾ������
        /// </summary>
        public string ButtonText
        {
            get => _FileSelectorButton.Text;
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "&Browser";
                _FileSelectorButton.Text = value;
                _FileSelectorButton.Width =
                    (int) CreateGraphics().MeasureString(value, Font).Width + 20;
                SetSize();
            }
        }

        /// <summary>
        ///     ��ȡ������Button����ʾ��ͼ��
        /// </summary>
        public Image ButtonImage
        {
            get => _FileSelectorButton.Image;
            set
            {
                if (value != null)
                {
                    _FileSelectorButton.Text = string.Empty;
                    _FileSelectorButton.Image = value;
                    _FileSelectorButton.Width = value.Width + 20;
                    SetSize();
                }
            }
        }

        /// <summary>
        ///     ��ȡ�������ļ�ѡ�����ʷ��¼
        /// </summary>
        public string[] SelectHistory { get; set; }

        /// <summary>
        ///     �Ƿ񱣴���ʷ��¼
        /// </summary>
        public bool IsSaveHistory { get; set; }

        /// <summary>
        ///     �Ƿ���ѡ���ļ���
        /// </summary>
        public bool IsSelectFolder { get; set; }

        /// <summary>
        ///     �ؼ��Ƿ�ֻ��
        /// </summary>
        public bool ReadOnly
        {
            get => _ReadOnly;
            set
            {
                _ReadOnly = value;
                if (_FileNamesControl is TextBox)
                    ((TextBox) _FileNamesControl).ReadOnly = value;
            }
        }

        #endregion
    }
}