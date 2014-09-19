using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data;
using Jeelu.SimplusOM.Client.Win;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace Jeelu.SimplusOM.Client
{
    public static class OMWorkBench
    {

        private static MainForm _mainForm;
        public static MainForm MainForm
        {
            get
            {
                if (_mainForm == null)
                {
                    _mainForm = new MainForm();
                }
                return _mainForm;
            }
        }

        public static int AgentId = -1;
        public static string AgentName = "";
        public static int MangerId = -1;
        public static string MangerName = "";
        public static int AreaId = -1;
        public static int Grade = -1;
        public static string Rights = "";
        public static decimal Balance = 0;
        public static DataSet BaseInfoDS;
        public static DataSet CurrentInfo;
        public static DataAgent DataAgent
        {
            get
            {
                return DataAgentFactory.GetDataAgent();
            }
        }


        /// <summary>
        /// 将打开的操作窗口提出来
        /// </summary>
        /// <param name="frmName"></param>
        /// <returns></returns>
        public static void CreateForm(Form form)
        {
            Form[] ChildrenForm = _mainForm.MdiChildren;
            foreach (Form f in ChildrenForm)
            {
                if (((OMBaseForm)f).TabText == ((OMBaseForm)form).TabText)
                {
                    f.BringToFront();
                    return;
                }
            }

            form.Owner = _mainForm;
            OMBaseForm omForm = form as OMBaseForm;
            omForm.Show(((MainForm)_mainForm)._MainDockPanel, DockState.Document);
        }

        public static string StrToBitStr(string str)
        {
            string _bitArrayStr = "";
            for (int i = 0; i < str.Length; i++)
            {
                char rightArr = str[i];
                _bitArrayStr += Convert.ToString(rightArr, 2).PadLeft(8, '0');
            }
            return _bitArrayStr;
        }

        public static string BitStrToStr(string bitStr)
        {
            string BitStr = "";
            List<string> bitArrRight = new List<string>();
            while (bitStr.Length > 0)
            {
                if (bitStr.Length > 8)
                {
                    bitArrRight.Add(bitStr.Substring(0, 8));
                    bitStr = bitStr.Substring(8);
                }
                else
                {
                    bitArrRight.Add(bitStr);
                    bitStr = "";
                }
            }

            foreach (string r in bitArrRight)
            {
                int b = Convert.ToInt32(r, 2);
                char s = Convert.ToChar(b);
                BitStr += s;
            }

            return BitStr;
        }

        #region md5加密解密，暂时没用
        /// <summary>
        /// 创建getMd5方法以获得userPwd的Md5值
        /// </summary>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public static string GetMd5(string userPwd)
        {

            //获取userPwd的byte类型数组
            byte[] byteUserPwd = Encoding.UTF8.GetBytes(userPwd);

            //实例化MD5CryptoServiceProvider
            MD5CryptoServiceProvider myMd5 = new MD5CryptoServiceProvider();

            // byte类型数组的值转换为 byte类型的Md5值
            byte[] byteMd5UserPwd = myMd5.ComputeHash(byteUserPwd);

            //将byte类型的Md5值转换为字符串
            string strMd5Pwd = Encoding.Default.GetString(byteMd5UserPwd).Trim();

            //返回Md5字符串
            return strMd5Pwd;
        }
        ///MD5加密
        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();


        }

        ///MD5解密
        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }
        #endregion

        //public static DataSet DeserializeDataSet(byte[] buffer)
        //{
        //    BinaryFormatter ser = new BinaryFormatter();
        //    DataSet dataSet = ser.Deserialize(new MemoryStream(buffer)) as DataSet;
        //    return dataSet;
        //}


        public static DataSet DeserializeDataSet(byte[] data)
        {
            try
            {
                MemoryStream unCompressMS = new MemoryStream();
                MemoryStream compressMS = new MemoryStream(data);
                Stream decompressedStream = new GZipStream(compressMS, CompressionMode.Decompress, true);

                BinaryFormatter ser = new BinaryFormatter();
                return (DataSet)ser.Deserialize(decompressedStream);
            }
            catch (ApplicationException ex)
            {
                return null;
            }
        }

        #region 权限
        #region
        public static bool AgentViewAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(0, 1)));
            }
        }
        public static bool AgentViewUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(1, 1)));
            }
        }
        public static bool AgentViewUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(2, 1)));
            }
        }

        public static bool AgentAddAllLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(3, 1)));
            }
        }
        public static bool AgentAddUnderSubLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(4, 1)));
            }
        }
        public static bool AgentAddUnderLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(5, 1)));
            }
        }

        public static bool AgentAddAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(6, 1)));
            }
        }
        public static bool AgentAddUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(7, 1)));
            }
        }
        public static bool AgentAddUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(8, 1)));
            }
        }

        public static bool AgentEidtAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(9, 1)));
            }
        }
        public static bool AgentEditUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(10, 1)));
            }
        }
        public static bool AgentEditUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(11, 1)));
            }
        }

        public static bool AgentTranStandardAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(12, 1)));
            }
        }
        public static bool AgentTranStandardUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(13, 1)));
            }
        }
        public static bool AgentTranStandardUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(14, 1)));
            }
        }

        public static bool AgentFrozedAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(15, 1)));
            }
        }
        public static bool AgentFrozedUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(16, 1)));
            }
        }
        public static bool AgentFrozedUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(17, 1)));
            }
        }

        public static bool AgentDeleteAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(18, 1)));
            }
        }
        public static bool AgentDeleteUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(19, 1)));
            }
        }
        public static bool AgentDeleteUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(20, 1)));
            }
        }

        public static bool AgentReturnSetAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(21, 1)));
            }
        }
        public static bool AgentReturnSetUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(22, 1)));
            }
        }
        public static bool AgentReturnSetUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(23, 1)));
            }
        }

        public static bool AgentChargeAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(24, 1)));
            }
        }
        public static bool AgentChargeUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(25, 1)));
            }
        }
        public static bool AgentChargeUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(26, 1)));
            }
        }

        public static bool AgentViewLogAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(27, 1)));
            }
        }
        public static bool AgentViewLogUnderSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(28, 1)));
            }
        }
        public static bool AgentViewLogUnder
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(29, 1)));
            }
        }
        #endregion

        public static bool UserViewAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(30, 1)));
            }
        }
        public static bool UserViewUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(31, 1)));
            }
        }
        public static bool UserViewUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(32, 1)));
            }
        }
        public static bool UserViewUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(33, 1)));
            }
        }

        public static bool UserAddAllLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(34, 1)));
            }
        }
        public static bool UserAddUnderCorpLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(35, 1)));
            }
        }
        public static bool UserAddUnderManagerSubLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(36, 1)));
            }
        }
        public static bool UserAddUnderManagerLatent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(37, 1)));
            }
        }

        public static bool UserAddAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(38, 1)));
            }
        }
        public static bool UserAddUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(39, 1)));
            }
        }
        public static bool UserAddUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(40, 1)));
            }
        }
        public static bool UserAddUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(41, 1)));
            }
        }

        public static bool UserAddJHAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(42, 1)));
            }
        }
        public static bool UserAddJHUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(43, 1)));
            }
        }
        public static bool UserAddJHUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(44, 1)));
            }
        }
        public static bool UserAddJHUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(45, 1)));
            }
        }

        public static bool UserEditAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(46, 1)));
            }
        }
        public static bool UserEditUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(47, 1)));
            }
        }
        public static bool UserEditUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(48, 1)));
            }
        }
        public static bool UserEditUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(49, 1)));
            }
        }

        public static bool UserTranStandardAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(50, 1)));
            }
        }
        public static bool UserTranStandardUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(51, 1)));
            }
        }
        public static bool UserTranStandardUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(52, 1)));
            }
        }
        public static bool UserTranStandardUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(53, 1)));
            }
        }

        public static bool UserFrozedAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(54, 1)));
            }
        }
        public static bool UserFrozedUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(55, 1)));
            }
        }
        public static bool UserFrozedUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(56, 1)));
            }
        }
        public static bool UserFrozedUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(57, 1)));
            }
        }

        public static bool UserDeleteAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(58, 1)));
            }
        }
        public static bool UserDeleteUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(59, 1)));
            }
        }
        public static bool UserDeleteUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(60, 1)));
            }
        }
        public static bool UserDeleteUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(61, 1)));
            }
        }

        public static bool UserSendMsgAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(62, 1)));
            }
        }
        public static bool UserSendMsgUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(63, 1)));
            }
        }
        public static bool UserSendMsgUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(64, 1)));
            }
        }
        public static bool UserSendMsgUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(65, 1)));
            }
        }

        public static bool UserSendMMsgAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(66, 1)));
            }
        }
        public static bool UserSendMMsgUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(67, 1)));
            }
        }
        public static bool UserSendMMsgUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(68, 1)));
            }
        }
        public static bool UserSendMMsgUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(69, 1)));
            }
        }

        public static bool UserChargeAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(70, 1)));
            }
        }
        public static bool UserChargeUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(71, 1)));
            }
        }
        public static bool UserChargeUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(72, 1)));
            }
        }
        public static bool UserChargeUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(73, 1)));
            }
        }

        public static bool UserCheckAdAll
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(74, 1)));
            }
        }
        public static bool UserCheckAdUnderCorp
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(75, 1)));
            }
        }
        public static bool UserCheckAdUnderManagerSub
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(76, 1)));
            }
        }
        public static bool UserCheckAdUnderManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(77, 1)));
            }
        }

        public static bool TranAgent
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(78, 1)));
            }
        }
        public static bool TranUser
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(79, 1)));
            }
        }
        public static bool ViewManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(80, 1)));
            }
        }

        public static bool AddManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(81, 1)));
            }
        }
        public static bool ModifyManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(82, 1)));
            }
        }
        public static bool ViewWebUnion
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(83, 1)));
            }
        }
        public static bool ManagerWebUnion
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(84, 1)));
            }
        }
        public static bool SetRightRights
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(85, 1)));
            }
        }
        public static bool ViewAllPlan
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(86, 1)));
            }
        }
        public static bool SetPlan
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(87, 1)));
            }
        }
        public static bool EditMsgTmpltRight
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(88, 1)));
            }
        }
        public static bool ChargeCheck
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(89, 1)));
            }
        }
        public static bool CustomTask
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(90, 1)));
            }
        }
        public static bool ReturnSetCheck
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(91, 1)));
            }
        }
        public static bool NewsManager
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(92, 1)));
            }
        }
        public static bool ViewAllLog
        {
            get
            {
                return Convert.ToBoolean(int.Parse(Rights.Substring(93, 1)));
            }
        }


        #endregion
    }
}
