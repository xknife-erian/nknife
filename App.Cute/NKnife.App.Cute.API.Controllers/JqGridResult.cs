using System.Web.Mvc;

namespace NKnife.App.Cute.API.Controllers
{
    /// <summary>����jqGrid��Json��ʽ�������ע����Js��Ҫ��������Ĵ��뼴�ɵõ���ȷ�Ľ�����
    /// <code>
    /// jsonReader: {
    ///    root: "Data",
    ///    repeatitems: false
    /// }</code>
    /// </summary>
    public class JqGridResult : JsonResult
    {
        public JqGridResult()
        {
            page = 1;
        }
        /// <summary>��ҳ��
        /// </summary>
        public int total { get; set; }
        /// <summary>��ǰҳ��
        /// </summary>
        public int page { get; set; }
        /// <summary>��ѯ���ļ�¼��
        /// </summary>
        public int records { get; set; }
    }
}