using System;
using System.Collections.Generic;
using System.Globalization;
using Octokit;

namespace NKnife.Upgrade4Github.Base.GithubDomain
{
    public class LatestRelease
    {
        public LatestRelease()
        {
        }

        public LatestRelease(
            string url,
            string htmlUrl,
            string assetsUrl,
            string uploadUrl,
            int id,
            string nodeId,
            string tagName,
            string targetCommit,
            string name,
            string body,
            bool draft,
            bool preRelease,
            DateTimeOffset createdAt,
            DateTimeOffset? publishedAt,
            Author author,
            string tarballUrl,
            string zipballUrl,
            IReadOnlyList<ReleaseAsset> assets)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            AssetsUrl = assetsUrl;
            UploadUrl = uploadUrl;
            Id = id;
            NodeId = nodeId;
            TagName = tagName;
            TargetCommit = targetCommit;
            Name = name;
            Body = body;
            Draft = draft;
            PreRelease = preRelease;
            CreatedAt = createdAt;
            PublishedAt = publishedAt;
            Author = author;
            TarballUrl = tarballUrl;
            ZipballUrl = zipballUrl;
            Assets = assets;
        }

        public LatestRelease(string uploadUrl)
        {
            UploadUrl = uploadUrl;
        }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string AssetsUrl { get; protected set; }

        public string UploadUrl { get; protected set; }

        public int Id { get; protected set; }

        /// <summary>GraphQL Node Id</summary>
        public string NodeId { get; protected set; }

        public string TagName { get; protected set; }

        public string TargetCommit { get; protected set; }

        public string Name { get; protected set; }

        public string Body { get; protected set; }

        public bool Draft { get; protected set; }

        public bool PreRelease { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset? PublishedAt { get; protected set; }

        public Author Author { get; protected set; }

        public string TarballUrl { get; protected set; }

        public string ZipballUrl { get; protected set; }

        public IReadOnlyList<ReleaseAsset> Assets { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0} PublishedAt: {1}", Name, PublishedAt);

        internal static LatestRelease Mapped(Release r)
        {
            return new LatestRelease
            {
                Url = r.Url, HtmlUrl = r.HtmlUrl, AssetsUrl = r.AssetsUrl, UploadUrl = r.UploadUrl, Id = r.Id, NodeId = r.NodeId,
                TagName = r.TagName, TargetCommit = r.TargetCommitish, Name = r.Name, Body = r.Body, Draft = r.Draft, PreRelease = r.Prerelease,
                CreatedAt = r.CreatedAt, PublishedAt = r.PublishedAt, Author = GithubDomain.Author.Mapped(r.Author), TarballUrl = r.TarballUrl,
                ZipballUrl = r.ZipballUrl, Assets = ReleaseAsset.MappedList(r.Assets)
            };
        }

        public ReleaseUpdate ToUpdate()
        {
            return new ReleaseUpdate
            {
                Body = Body,
                Draft = Draft,
                Name = Name,
                PreRelease = PreRelease,
                TargetCommit = TargetCommit,
                TagName = TagName
            };
        }
    }
}