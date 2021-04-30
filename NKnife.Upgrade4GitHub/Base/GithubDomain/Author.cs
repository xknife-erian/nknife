using System.Globalization;

namespace NKnife.Upgrade4Github.Base.GithubDomain
{
    public class Author
    {
        public Author()
        {
        }

        public Author(
            string login,
            int id,
            string nodeId,
            string avatarUrl,
            string url,
            string htmlUrl,
            string followersUrl,
            string followingUrl,
            string gistsUrl,
            string type,
            string starredUrl,
            string subscriptionsUrl,
            string organizationsUrl,
            string reposUrl,
            string eventsUrl,
            string receivedEventsUrl,
            bool siteAdmin)
        {
            Login = login;
            Id = id;
            NodeId = nodeId;
            AvatarUrl = avatarUrl;
            Url = url;
            HtmlUrl = htmlUrl;
            FollowersUrl = followersUrl;
            FollowingUrl = followingUrl;
            GistsUrl = gistsUrl;
            Type = type;
            StarredUrl = starredUrl;
            SubscriptionsUrl = subscriptionsUrl;
            OrganizationsUrl = organizationsUrl;
            ReposUrl = reposUrl;
            EventsUrl = eventsUrl;
            ReceivedEventsUrl = receivedEventsUrl;
            SiteAdmin = siteAdmin;
        }

        public string Login { get; protected set; }

        public int Id { get; protected set; }

        /// <summary>GraphQL Node Id</summary>
        public string NodeId { get; protected set; }

        public string AvatarUrl { get; protected set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string FollowersUrl { get; protected set; }

        public string FollowingUrl { get; protected set; }

        public string GistsUrl { get; protected set; }

        public string StarredUrl { get; protected set; }

        public string SubscriptionsUrl { get; protected set; }

        public string OrganizationsUrl { get; protected set; }

        public string ReposUrl { get; protected set; }

        public string EventsUrl { get; protected set; }

        public string ReceivedEventsUrl { get; protected set; }

        public string Type { get; protected set; }

        public bool SiteAdmin { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Author: Id: {0} Login: {1}", Id, Login);

        public static Author Mapped(Octokit.Author author)
        {
            return new Author
            {
                Login = author.Login,
                Id = author.Id,
                NodeId = author.NodeId,
                AvatarUrl = author.AvatarUrl,
                Url = author.Url,
                HtmlUrl = author.HtmlUrl,
                FollowersUrl = author.FollowersUrl,
                FollowingUrl = author.FollowingUrl,
                GistsUrl = author.GistsUrl,
                Type = author.Type,
                StarredUrl = author.StarredUrl,
                SubscriptionsUrl = author.SubscriptionsUrl,
                OrganizationsUrl = author.OrganizationsUrl,
                ReposUrl = author.ReposUrl,
                EventsUrl = author.EventsUrl,
                ReceivedEventsUrl = author.ReceivedEventsUrl,
                SiteAdmin = author.SiteAdmin
            };
        }
    }
}