using System;
using System.ComponentModel.DataAnnotations;

namespace NKnife.Storages.UnitTests.SQL.DataBase
{

	[System.ComponentModel.DataAnnotations.Schema.Table(TABLE_NAME)]
	public class Author
    {
        public const string TABLE_NAME = "tab_authors";

		[PrimaryKey, Dapper.IgnoreInsert, Dapper.IgnoreUpdate]
		public int ID { get; set; }

		[Dapper.IgnoreInsert]
		public DateTime Created_At { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

	}

}
