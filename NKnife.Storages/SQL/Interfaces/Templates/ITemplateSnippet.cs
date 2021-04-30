namespace NKnife.Storages.SQL.Interfaces.Templates
{

	public interface ITemplateSnippet
	{

		string Name { get; set; }

		string Code { get; set; }

		string Prefix { get; set; }

		string Postfix { get; set; }

	}

}