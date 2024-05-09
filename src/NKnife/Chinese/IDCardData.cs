using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NKnife.Chinese
{
    /// <summary>中华人民共和国第2代身份证信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
// ReSharper disable InconsistentNaming
    public struct IDCardData : IEquatable<IDCardData>, ICloneable
// ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// 姓名 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;
        /// <summary>
        /// 性别
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Sex; 
        /// <summary>
        /// 民族
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        public string Nation;
        /// <summary>
        /// 出生日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string Born;
        /// <summary>
        /// 住址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
        public string Address;
        /// <summary>
        /// 身份证号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        public string IDCardNo;
        /// <summary>
        /// 发证机关
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string GrantDept;
        /// <summary>
        /// 有效开始日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string UserLifeBegin;
        /// <summary>
        /// 有效截止日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string UserLifeEnd;
        /// <summary>
        /// 保留
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        public string Reserved;
        /// <summary>
        /// 照片路径
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string PhotoFileName;

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Name);
            sb.AppendLine(Sex);
            sb.AppendLine(Nation);
            sb.AppendLine(Born);
            sb.AppendLine(Address);
            sb.AppendLine(IDCardNo);
            sb.AppendLine(GrantDept);
            sb.AppendLine(UserLifeBegin);
            sb.AppendLine(UserLifeEnd);
            sb.AppendLine(Reserved);
            sb.AppendLine(PhotoFileName);
            return sb.ToString();
        }

        public object Clone()
        {
            var data = new IDCardData();
            data.Address = Address;
            data.Born = Born;
            data.GrantDept = GrantDept;
            data.IDCardNo = IDCardNo;
            data.Name = Name;
            data.Nation = Nation;
            data.PhotoFileName = PhotoFileName;
            data.Reserved = Reserved;
            data.Sex = Sex;
            data.UserLifeBegin = UserLifeBegin;
            data.UserLifeEnd = UserLifeEnd;
            return data;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IDCardData other)
        {
            return Equals(other.Name, Name) 
                && Equals(other.Sex, Sex)
                && Equals(other.Nation, Nation)
                && Equals(other.Born, Born)
                && Equals(other.Address, Address)
                && Equals(other.IDCardNo, IDCardNo)
                && Equals(other.GrantDept, GrantDept)
                && Equals(other.UserLifeBegin, UserLifeBegin)
                && Equals(other.UserLifeEnd, UserLifeEnd)
                && Equals(other.Reserved, Reserved)
                && Equals(other.PhotoFileName, PhotoFileName);
        }

        /// <summary>
        ///     验证身份证号码
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>验证成功为True，否则为False</returns>
        public static bool Check(string id)
        {
            if (id.Length == 18)
            {
                bool check = CheckIdCard18(id);
                return check;
            }

            if (id.Length == 15)
            {
                bool check = CheckIdCard15(id);
                return check;
            }
            return false;
        }

        #region 身份证号码验证

        /// <summary>
        ///     验证18位身份证号
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIdCard18(string id)
        {
            if (!long.TryParse(id.Remove(17), out var n) || n < Math.Pow(10, 16))
            {
                return false; //数字验证
            }
            else if (!long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n))
            {
                return false; //数字验证
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; //省份验证
            }
            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out var time) == false)
            {
                return false; //生日验证
            }
            string[] arrVerifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVerifyCode[y] != id.Substring(17, 1).ToLower())
            {
                return false; //校验码验证
            }
            return true; //符合GB11643-1999标准
        }

        /// <summary>
        ///     验证15位身份证号
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool CheckIdCard15(string id)
        {
            if (!long.TryParse(id, out var n) || n < Math.Pow(10, 14))
            {
                return false; //数字验证
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; //省份验证
            }
            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            if (!DateTime.TryParse(birth, out var time))
            {
                return false; //生日验证
            }
            return true; //符合15位身份证标准
        }

        #endregion

    }
}
