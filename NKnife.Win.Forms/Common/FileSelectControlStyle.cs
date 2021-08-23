namespace NKnife.Win.Forms.Common
{
    /// <summary>
    /// �ļ�ѡ��ؼ���ʽö��
    /// </summary>
    public enum FileSelectControlStyle
    {
        None = 0,

        /// <summary>
        /// �ļ�����ʾ��TextBox��, ��ť����ʾ����
        /// </summary>
        TextBoxAndTextButton = 1,

        /// <summary>
        /// �ļ�����ʾ��TextBox��, ��ť����ʾͼ��
        /// </summary>
        TextBoxAndImageButton = 2,

        /// <summary>
        /// �ļ�����ʾ��TextBox��, ��ť����ʾ������ͼ��
        /// </summary>
        TextBoxAndTextImageButton = 3,

        /// <summary>
        /// �ļ�����ʾ��ComboBox��, ��ť����ʾ����
        /// </summary>
        ComboBoxAndTextButton = 4,

        /// <summary>
        /// �ļ�����ʾ��ComboBox��, ��ť����ʾͼ��
        /// </summary>
        ComboBoxAndImageButton = 5,

        /// <summary>
        /// �ļ�����ʾ��ComboBox��, ��ť����ʾ������ͼ��
        /// </summary>
        ComboBoxAndTextImageButton = 6,

        /// <summary>
        /// ����ť, ��ť����ʾ����
        /// </summary>
        OnlyTextButton = 7,

        /// <summary>
        /// ����ť, ��ť����ʾͼ��
        /// </summary>
        OnlyImageButton = 8,

        /// <summary>
        /// ����ť, ��ť����ʾ������ͼ��
        /// </summary>
        OnlyTextImageButton = 9
    }
}