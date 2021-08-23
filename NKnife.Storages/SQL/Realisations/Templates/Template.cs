using System.Collections.Generic;
using System.Text.RegularExpressions;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Templates;

namespace NKnife.Storages.SQL.Realisations.Templates
{
    public class Template : ITemplate
    {
        private const string ESC_START = "{{";
        private const string ESC_END = "}}";

        private readonly List<ITemplateSnippet> _expressions;

        public Template(string pattern)
        {
            Pattern = pattern;
            Parameters = SuperSql.DefaultFormatter;
            _expressions = new List<ITemplateSnippet>();
        }

        public IFormatter Parameters { get; set; }

        public string Pattern { get; set; }

        public IEnumerable<ITemplateSnippet> Snippets => _expressions;

        public ITemplate Append(params ITemplateSnippet[] snippets)
        {
            foreach (var snippet in snippets)
                _expressions.Add(snippet);
            return this;
        }

        public ITemplate Append(string name, string code, string prefix = "", string postfix = "")
        {
            ITemplateSnippet snippet = new Snippet(name, code, prefix, postfix);
            Append(snippet);
            return this;
        }

        public string GetSql(bool endOfStatement = true)
        {
            var pattern = Pattern;

            if (endOfStatement)
                Append(SnippetLibrary.End(Parameters.EndOfStatement.ToString()));

            foreach (var snippet in _expressions)
            {
                var text = ESC_START + snippet.Name + ESC_END;
                if (pattern.Contains(text)) pattern = pattern.Replace(text, snippet.Prefix + snippet.Code + snippet.Postfix);
            }

            pattern = Regex.Replace(pattern, ESC_START + "([A-Za-z0-9_]+)" + ESC_END, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            return pattern;
        }
    }
}