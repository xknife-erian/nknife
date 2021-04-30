using System.Collections.Generic;

namespace NKnife.Storages.SQL.Interfaces.Templates
{
    public interface ITemplate
    {
        string Pattern { get; set; }

        IFormatter Parameters { get; set; }

        IEnumerable<ITemplateSnippet> Snippets { get; }

        ITemplate Append(params ITemplateSnippet[] snippets);

        ITemplate Append(string name, string code, string prefix = "", string postfix = "");

        string GetSql(bool endOfStatement = true);
    }
}