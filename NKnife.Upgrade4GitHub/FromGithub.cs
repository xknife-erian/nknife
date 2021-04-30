using System;
using System.Threading;
using System.Threading.Tasks;
using NKnife.Upgrade4Github.Base.GithubDomain;
using Octokit;

namespace NKnife.Upgrade4Github
{
    /// <summary>
    ///     本组件提供的从github获取更新方法的包装
    /// </summary>
    public static class FromGithub
    {
        /// <summary>
        ///     Github是否访问完成。
        /// </summary>
        private static bool _visited;

        private static LatestRelease _latestRelease;

        /// <summary>
        ///     从指定的github账户获取指定的项目的最后更新信息
        /// </summary>
        /// <param name="owner">指定的github账户</param>
        /// <param name="projectName">指定的项目</param>
        /// <param name="latestRelease">最后更新信息</param>
        /// <param name="errorMessage">如果更新失败的异常信息</param>
        /// <returns>获取是否成功</returns>
        public static bool TryGetLatestRelease(string owner, string projectName, out LatestRelease latestRelease, out string errorMessage)
        {
            latestRelease = null;
            errorMessage = string.Empty;
            try
            {
                var rand = Guid.NewGuid().ToString("N").Substring(0, 4);
                var userAgent = $"nknife-app-updater-{rand}";
                var msg = string.Empty; //交换信息。
                var isRunsNormally = true; //程序是否正常运行，没有发生异常。
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var client = new GitHubClient(new ProductHeaderValue(userAgent));
                        var releases = await client.Repository.Release.GetAll(owner, projectName);
                        _latestRelease = LatestRelease.Mapped(releases[0]);
                    }
                    catch (Exception e)
                    {
                        isRunsNormally = false;
                        msg = $"{e.Message}\r\n------\r\n{e.InnerException}";
                    }
                    finally
                    {
                        _visited = true;
                    }
                });
                while (!_visited) Thread.Sleep(10);

                latestRelease = _latestRelease;
                errorMessage = msg;
                return isRunsNormally;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
        }
    }
}