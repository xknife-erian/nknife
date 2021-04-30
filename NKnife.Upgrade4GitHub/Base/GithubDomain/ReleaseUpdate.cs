using System.Globalization;

namespace NKnife.Upgrade4Github.Base.GithubDomain
{
    public class ReleaseUpdate
    {
        /// <summary>Gets the name of the tag.</summary>
        /// <value>The name of the tag.</value>
        public string TagName { get; set; }

        /// <summary>
        ///     Specifies the commitish value that determines where the Git tag is created from. Can be any branch or
        ///     commit SHA. Unused if the Git tag already exists. Default: the repository�s default branch
        ///     (usually master).
        /// </summary>
        /// <value>The target commitish.</value>
        public string TargetCommit { get; set; }

        /// <summary>Gets or sets the name of the release.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the text describing the contents of the tag.
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="T:Octokit.NewRelease" /> is a draft (unpublished).
        ///     Default: false
        /// </summary>
        /// <value>
        ///     <c>true</c> if draft; otherwise, <c>false</c>.
        /// </value>
        public bool? Draft { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="T:Octokit.NewRelease" /> is prerelease.
        /// </summary>
        /// <value>
        ///     <c>true</c> if prerelease; otherwise, <c>false</c>.
        /// </value>
        public bool? PreRelease { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0} TagName: {1}", Name, TagName);
    }
}