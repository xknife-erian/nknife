using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Octokit;

namespace NKnife.Upgrade4Github.Base.GithubDomain
{
    public class ReleaseAsset
    {
        public ReleaseAsset()
        {
        }

        public ReleaseAsset(
            string url,
            int id,
            string nodeId,
            string name,
            string label,
            string state,
            string contentType,
            int size,
            int downloadCount,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt,
            string browserDownloadUrl,
            Author uploader)
        {
            Url = url;
            Id = id;
            NodeId = nodeId;
            Name = name;
            Label = label;
            State = state;
            ContentType = contentType;
            Size = size;
            DownloadCount = downloadCount;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            BrowserDownloadUrl = browserDownloadUrl;
            Uploader = uploader;
        }

        public string Url { get; protected set; }

        public int Id { get; protected set; }

        /// <summary>GraphQL Node Id</summary>
        public string NodeId { get; protected set; }

        public string Name { get; protected set; }

        public string Label { get; protected set; }

        public string State { get; protected set; }

        public string ContentType { get; protected set; }

        public int Size { get; protected set; }

        public int DownloadCount { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public string BrowserDownloadUrl { get; protected set; }

        public Author Uploader { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0} CreatedAt: {1}", Name, CreatedAt);

        public ReleaseAssetUpdate ToUpdate()
        {
            return new ReleaseAssetUpdate(Name)
            {
                Label = Label
            };
        }

        public static ReleaseAsset Mapped(Octokit.ReleaseAsset asset)
        {
            return new ReleaseAsset
            {
                Url = asset.Url,Id = asset.Id,NodeId = asset.NodeId,Name = asset.Name,Label = asset.Label,State = asset.State,
                ContentType = asset.ContentType, Size = asset.Size, DownloadCount = asset.DownloadCount, CreatedAt = asset.CreatedAt,
                UpdatedAt = asset.UpdatedAt, BrowserDownloadUrl = asset.BrowserDownloadUrl, Uploader = Author.Mapped(asset.Uploader),
            };
        }

        public static IReadOnlyList<ReleaseAsset> MappedList(IReadOnlyList<Octokit.ReleaseAsset> assets)
        {
            var list = new List<ReleaseAsset>(assets.Count);
            list.AddRange(assets.Select(ReleaseAsset.Mapped));
            return list.AsReadOnly();
        }
    }
}