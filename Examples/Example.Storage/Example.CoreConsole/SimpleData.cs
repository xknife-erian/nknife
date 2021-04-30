using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Example.Common;
using Example.Logic;
using Example.View;
using NKnife.Storages;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using NKnife.Storages.Util;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Example.CoreConsole
{
    public class SimpleData
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private readonly BookManagerLogic _bookManagerLogic;
        private readonly PersonManagerLogic _personManagerLogic;
        private readonly BuyLogic _buyLogic;

        public SimpleData(BookManagerLogic bookManagerLogic, PersonManagerLogic personManagerLogic, BuyLogic buyLogic)
        {
            _bookManagerLogic = bookManagerLogic;
            _personManagerLogic = personManagerLogic;
            _buyLogic = buyLogic;
        }

        #region AddBooks
        
        public async Task AddBooks()
        {
            _Logger.Debug($"{nameof(AddBooks)}...");
            //新增书籍
            var bookList = await GetBooksAsync();
            _bookManagerLogic.AddBookAsync(bookList.ToArray());
        }

        /// <summary>
        /// 从Excel文件中获取所有书籍
        /// </summary>
        private async Task<IEnumerable<BookVo>> GetBooksAsync()
        {
            var list = new List<BookVo>();
            var bs = Resources.图书目录资料;
            IWorkbook workbook = new XSSFWorkbook(new MemoryStream(bs));
            ISheet sheet = workbook.GetSheetAt(0); //获取第一个工作表
            for (int i = 1; i < sheet.LastRowNum; i++) //对工作表每一行
            {
                var row = sheet.GetRow(i);
                if (row != null)
                {
                    var book = new BookVo
                    {
                        Id = row.GetCell(0).ToString(),
                        Name = row.GetCell(1).ToString(),
                        ISBN = row.GetCell(2).ToString(),
                        Price = (float) Convert.ToDouble(row.GetCell(3).NumericCellValue)
                    };
                    var pubName = row.GetCell(4).ToString().Trim();
                    book.Publisher = new PublisherVo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = pubName
                    };
                    list.Add(book);
                }
            }
            workbook.Close();
            return list;
        }

        #endregion

        #region AddPeople

        public async Task AddPeople()
        {
            _Logger.Debug($"{nameof(AddPeople)}...");
            var people = await GetPeopleAsync();
            await _personManagerLogic.AddPersonAsync(people.ToArray());
        }

        private async Task<IEnumerable<PersonVo>> GetPeopleAsync()
        {
            var list = new List<PersonVo>();
            var bs = Resources.人员名单资料;
            IWorkbook workbook = new XSSFWorkbook(new MemoryStream(bs));
            ISheet sheet = workbook.GetSheetAt(0); //获取第一个工作表
            for (int i = 1; i < sheet.LastRowNum; i++) //对工作表每一行
            {
                var row = sheet.GetRow(i);
                if (row != null)
                {
                    var person = new PersonVo()
                    {
                        Id = row.GetCell(0).ToString(),
                        Name = row.GetCell(2).ToString(),
                        IDCardNumber = row.GetCell(5).ToString(),
                        ExaminationNumber = row.GetCell(1).ToString(),
                        School = row.GetCell(8).ToString(),
                        Class = row.GetCell(6).ToString(),
                        StudentNumber = row.GetCell(7).ToString(),
                        HasCollection = false
                    };
                    if (DateTime.TryParseExact(row.GetCell(4).ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                        person.Birthday = dt;
                    list.Add(person);
                }
            }

            workbook.Close();
            return list;
        }

        #endregion

        #region BuyBook

        public async Task OneRandomTransaction()
        {
            var person = await _personManagerLogic.GetRandomPerson();
            var book = await _bookManagerLogic.GetRandomBook();
            await _buyLogic.BuyBook(person, book);
            Console.WriteLine($"{person.Name}\t buy \t{book.Name}.");
        }

        #endregion

        public async Task ViewPersonBuyingRecord()
        {
            throw new NotImplementedException();
        }
    }
}