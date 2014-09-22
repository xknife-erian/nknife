using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.KeywordResonator.Client
{
    public enum UrlState
    {
        Unvisited = 0,
        Visited = 1,
        Visiting = 2
    }

    public class UrlItem
    {

        /// <summary>
        /// Url值
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Url状态
        /// </summary>
        private UrlState State { get; set; }

        public UrlItem(string url)
        {
            Url = url;
            State = UrlState.Unvisited;
        }
    }
}
