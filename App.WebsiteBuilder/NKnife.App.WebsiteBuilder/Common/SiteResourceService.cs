using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Jeelu
{
    static public class SiteResourceService
    {
        static SelectResourceDelegate _selectResource;
        static GetResourcePath _getPath;
        static GetResourcePath _getAbsPath;
        static GetResourcePath _getUrl;
        static ImportResourceFileDelegate _importResourceFileDelegate;
        static public void Initialize(SelectResourceDelegate selectResource, GetResourcePath getPath,
            GetResourcePath getAbsPath, GetResourcePath getUrl, ImportResourceFileDelegate importResourceFileDelegate)
        {
            _selectResource = selectResource;
            _getPath = getPath;
            _getAbsPath = getAbsPath;
            _getUrl = getUrl;
            _importResourceFileDelegate = importResourceFileDelegate;
        }

        /// <summary>
        /// ѡ����Դ�ļ�,����Id
        /// </summary>
        static public string SelectResource(MediaFileType type,Form owmerForm)
        {
            return _selectResource(type, owmerForm);
        }

        static public string SelectResourceFormat(MediaFileType type, Form owmerForm)
        {
            string id = SelectResource(type,owmerForm);
            if (!string.IsNullOrEmpty(id))
            {
                return Utility.Regex.FormatResourceId(id);
            }
            return null;
        }

        static private Regex _regSrs = new Regex(@"\$\{srs_([a-zA-Z0-9]+)\}");
        /// <summary>
        /// �����ں�${srs_*}��ʽ���ַ����������${srs_*}��GetUrl(*)�滻
        /// </summary>
        static public string ParseFormatId(string str)
        {
            return ParseFormatId(str, false);
        }

        /// <summary>
        /// �����ں�${srs_*}��ʽ���ַ����������${srs_*}��GetUrl(*)�滻
        /// </summary>
        /// <param name="str">����ֵ</param>
        /// <param name="isAbs">�Ƿ��ȡ����·��(Ĭ��Ϊfalse)</param>
        static public string ParseFormatId(string str, bool isAbs)
        {
            if (isAbs)
            {
                return _regSrs.Replace(str, new MatchEvaluator(ReplaceCallbackAbs));
            }
            else
            {
                return _regSrs.Replace(str, new MatchEvaluator(RepalceCallback));
            }
        }

        static private string RepalceCallback(Match m)
        {
            return GetUrl(m.Groups[1].Value);
        }

        static private string ReplaceCallbackAbs(Match m)
        {
            return _getAbsPath(m.Groups[1].Value);
        }

        /// <summary>
        /// ������Դ�ļ���ID�����ļ��ľ���·��
        /// </summary>
        static public string GetFullPath(string resourceId)
        {
            return _getPath(ParseFormatId(resourceId));
        }

        static public string GetUrl(string resourceId)
        {
            return _getUrl(ParseFormatId(resourceId));
        }

        /// <summary>
        /// ��srcFile������resourcePathָ������Դ�ļ�����,������Դ�ļ�ID
        /// </summary>
        static public string ImportResourceFile(string resourcePath, string srcFile)
        {
            return _importResourceFileDelegate(resourcePath, srcFile);
        }
    }

    public enum MediaFileType
    {
        None,
        Pic,
        Flash,
        Audio,
        Video,
    }
    /// <summary>
    /// ��Դ���ӹ���������Դ����,ADD BY fenggy 2008-06-05
    /// </summary>
    public enum ResourceFileType
    {
        ResourceFiles,//��Դ�ļ� ��ͼƬ,��Ƶ,FLASH �ȣ�
        LocalPagesl,//����ҳ��
    }
    public delegate string SelectResourceDelegate(MediaFileType type,Form ownerForm);
    public delegate string GetResourcePath(string id);
    public delegate string ImportResourceFileDelegate(string resourcePath, string srcFile);
}