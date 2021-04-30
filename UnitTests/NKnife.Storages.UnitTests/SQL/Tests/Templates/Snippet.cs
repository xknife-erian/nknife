using NKnife.Storages.SQL.Realisations.Templates;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Templates
{

	
	public class Snippets
	{

		[Fact]
		public void SnippetNameValidation()
		{
			Snippet s1 = new Snippet("CODE", "S1");
			Snippet s2 = new Snippet("WHERE", "S2");
			Snippet s3 = new Snippet("ORDERBY", "S3");
			Snippet s4 = new Snippet("LIMIT", "S4");
			Snippet s5 = new Snippet("TEST_THIS", "S5");
		}

		[Fact]
		public void SnippetNameValidationException()
		{
			// Assert.ThrowsException<Exceptions.SnippetNameValidationException>(() => { new Snippet("@WHERE", ""); });
			// Assert.ThrowsException<Exceptions.SnippetNameValidationException>(() => { new Snippet(" ", ""); });
			// Assert.ThrowsException<Exceptions.SnippetNameValidationException>(() => { new Snippet("123sass=", ""); });
			// Assert.ThrowsException<Exceptions.SnippetNameValidationException>(() => { new Snippet("BLA!BLA", ""); });
			// Assert.ThrowsException<Exceptions.SnippetNameValidationException>(() => { new Snippet("Foo Bar", ""); });
		}

	}

}