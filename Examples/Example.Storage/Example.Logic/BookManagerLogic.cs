using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Example.Common;
using Example.View;
using NKnife.Storages;

namespace Example.Logic
{
    public class BookManagerLogic
    {
        private static readonly Random _Random = new Random((int) DateTime.Now.Ticks);
        private readonly IMapper _mapper;
        private readonly BaseStorageWrite<Publisher> _publisherWrite;
        private readonly BaseStorageRead<Publisher> _publisherRead;
        private readonly BaseStorageWrite<Person> _personWrite;
        private readonly BaseStorageRead<Person> _personRead;
        private readonly BaseStorageWrite<Book> _bookWrite;
        private readonly BaseStorageRead<Book> _bookRead;
        private readonly BaseStorageWrite<BuyingRecord> _buyingRecordWrite;
        private readonly BaseStorageRead<BuyingRecord> _buyingRecordRead;

        public BookManagerLogic(IMapper mapper, BaseStorageWrite<Publisher> publisherWrite, BaseStorageRead<Publisher> publisherRead, BaseStorageWrite<Person> personWrite, BaseStorageRead<Person> personRead, 
            BaseStorageWrite<Book> bookWrite, BaseStorageRead<Book> bookRead, BaseStorageWrite<BuyingRecord> buyingRecordWrite, BaseStorageRead<BuyingRecord> buyingRecordRead)
        {
            _mapper = mapper;

            _publisherWrite = publisherWrite;
            _publisherRead = publisherRead;
            _personWrite = personWrite;
            _personRead = personRead;
            _bookWrite = bookWrite;
            _bookRead = bookRead;
            _buyingRecordWrite = buyingRecordWrite;
            _buyingRecordRead = buyingRecordRead;
        }

        /// <summary>
        /// 书籍管理逻辑：新增书籍，同时看是否是新的出版社，如果是，则新增该书的出版社
        /// 因仅为Demo程序，为表达<see cref="NKnife.Storages"/>模块的设计与使用，在业务逻辑上并没有进行十分严谨的进行设计。
        /// </summary>
        public async void AddBookAsync(params BookVo[] books)
        {
            var pubList = new List<Publisher>();
            var bookList = new List<Book>();
            foreach (var book in books)
            {
                var has = pubList.Find((p) => p.Name == book.Publisher.Name);
                if (has == null)
                {
                    var pub = _mapper.Map<Publisher>(book.Publisher);
                    pubList.Add(pub);
                }
                else
                {
                    book.Publisher.Id = has.Id;
                }
                bookList.Add(_mapper.Map<Book>(book));
            }

            await _publisherWrite.InsertManyAsync(pubList);
            Console.WriteLine($"新增出版社，计{pubList.Count}个，成功。");
            await _bookWrite.InsertManyAsync(bookList);
            Console.WriteLine($"新增书籍，计{bookList.Count}本，成功。");
        }

        public async Task<BookVo> GetRandomBook()
        {
            var count = await _bookRead.CountAsync();
            var index = _Random.Next(0, (int)count);
            var book = await _bookRead.PageAsync(index, 1);
            return _mapper.Map<BookVo>(book.ElementAt(0));
        }
    }
}
