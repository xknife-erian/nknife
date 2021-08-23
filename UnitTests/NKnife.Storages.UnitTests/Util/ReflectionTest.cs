using NKnife.Storages.SQL.Common;
using NKnife.Storages.UnitTests.SQL.DataBase;
using NKnife.Storages.Util;
using Xunit;

namespace NKnife.Storages.UnitTests.Util
{
    public class ReflectionTest
    {
        [Fact]
        public void GetForeignKeys()
        {
            var fk1 = Reflection.GetForeignKeys<Book>();
            var fk2 = Reflection.GetForeignKeys<Config>();
            Assert.Equal(3, fk1.Length);
            Assert.Equal(0, fk2.Length);
            Assert.Equal("ID_Author,ID_Publisher,ID_Shop", string.Join(',', fk1));
        }

        [Fact]
        public void GetPrimaryKey()
        {
            var pk_books = Reflection.GetPrimaryKey<Book>().ToLower();
            Assert.Equal("id", pk_books);

            var pk_authors = Reflection.GetPrimaryKey<Author>().ToLower();
            Assert.Equal("id", pk_authors);

            //Assert.ThrowsException<Exceptions.PrimaryKeyNotFoundException>(() => { reflection.GetPrimaryKey<DataBaseDemo.Config>(); });
        }

        [Fact]
        public void GetTableName()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var tabBooks = Reflection.GetTableName<Book>();
            Assert.Equal(Book.TABLE_NAME, tabBooks);

            var tabAuthors = Reflection.GetTableName<Author>();
            Assert.Equal(Author.TABLE_NAME, tabAuthors);

            var config = Reflection.GetTableName<Config>();
            Assert.Equal(nameof(Config), config);
        }
    }
}