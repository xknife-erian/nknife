using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{

    public class SnipRect : Rect
    {
        #region ���Գ�Ա����

        /// <summary>
        /// ��ȡ������ҳ��Ƭ��
        /// </summary>
        [PropertyPad(0,2,"snipName",MainControlType= MainControlType.TextBox,IsReadOnly=true)]
        public string SnipName { get; set; }

        

        #endregion

        #region ���캯��

        public SnipRect(int x, int y, int width, int height, string snipID)
            : base(x, y, width, height)
        {
            SnipID = snipID;
            SnipName = snipID;
            SnipData = null;
        }

        #endregion

        /// <summary>
        /// ����ҳ��Ƭ����
        /// </summary>
        internal void CopyToClipboard()
        {
            SnipData.CopyToClipboard();
        }
    }
}
