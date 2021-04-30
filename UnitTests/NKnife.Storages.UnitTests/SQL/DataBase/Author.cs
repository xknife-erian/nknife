using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NKnife.Storages.UnitTests.SQL.DataBase
{

	[Table(TABLE_NAME)]
	public class Author
    {
        public const string TABLE_NAME = "tab_authors";

		[PrimaryKey, IgnoreInsert, IgnoreUpdate]
		public int ID { get; set; }

		[IgnoreInsert]
		public DateTime Created_At { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

	}

}
