using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NKnife.Storages.UnitTests.SQL.DataBase
{
    [Table(TABLE_NAME)]
    public class Book
    {
        public const string TABLE_NAME = "tab_books";

        [PrimaryKey]
        [IgnoreInsert]
        [IgnoreUpdate]
        public int ID { get; set; }

        [Column("created_at")]
        [InsertDefault("NOW()")]
        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey] public int ID_Author { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey] public int ID_Publisher { get; set; }

        [System.ComponentModel.DataAnnotations.ForeignKey] public int ID_Shop { get; set; }
    }
}