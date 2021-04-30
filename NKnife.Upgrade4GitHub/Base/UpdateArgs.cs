using System;

namespace NKnife.Upgrade4Github.Base
{
    /// <summary>
    /// 更新参数
    /// </summary>
    class UpdateArgs
    {
        /// <summary>
        /// 将要被更新的软件
        /// </summary>
        public ParentClass Parent { get; } = new ParentClass();

        public UpdateArgs()
        {
            
        }

        public UpdateArgs(string username, string project, string title, string runner, string parentVersion, bool isAutoRun)
        {
            Username = username;
            Project = project;
            Parent.Title = title;
            Parent.Runner = runner;
            Parent.Version = Version.Parse(parentVersion);
            Parent.IsAutoRun = isAutoRun;
        }

        /// <summary>
        /// github用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// github项目
        /// </summary>
        public string Project { get; set; }

        public class ParentClass
        {
            /// <summary>
            /// 被更新的软件标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 被更新的软件运行文件的文件名
            /// </summary>
            public string Runner { get; set; }

            /// <summary>
            /// 被更新的软件当前的版本
            /// </summary>
            public Version Version { get; set; } = new Version(1,0,0);

            /// <summary>
            /// 更新完毕后是否直接运行
            /// </summary>
            public bool IsAutoRun { get; set; } = false;
        }
    }
}
