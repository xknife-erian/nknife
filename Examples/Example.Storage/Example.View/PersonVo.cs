using System;

namespace Example.View
{
    /// <summary>
    ///     例子对象：人
    /// </summary>
    public class PersonVo
    {
        public string Id { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     年龄
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        ///     身份证编号
        /// </summary>
        public string IDCardNumber { get; set; }

        /// <summary>
        ///     考试编号
        /// </summary>
        public string ExaminationNumber { get; set; }

        /// <summary>
        ///     学校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        ///     班级
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        ///     学生编号
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     是否有收藏
        /// </summary>
        public bool HasCollection { get; set; }
    }
}