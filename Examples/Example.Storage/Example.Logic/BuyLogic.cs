using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Example.Common;
using Example.View;
using NKnife.Storages;

namespace Example.Logic
{
    public class BuyLogic
    {
        private static readonly Random _Random = new Random((int)DateTime.Now.Ticks);
        private readonly IMapper _mapper;
        private readonly BaseStorageWrite<Publisher> _publisherWrite;
        private readonly BaseStorageRead<Publisher> _publisherRead;
        private readonly BaseStorageWrite<Person> _personWrite;
        private readonly BaseStorageRead<Person> _personRead;
        private readonly BaseStorageWrite<Book> _bookWrite;
        private readonly BaseStorageRead<Book> _bookRead;
        private readonly BaseStorageWrite<BuyingRecord> _buyingRecordWrite;
        private readonly BaseStorageRead<BuyingRecord> _buyingRecordRead;

        public BuyLogic(IMapper mapper, BaseStorageWrite<Publisher> publisherWrite, BaseStorageRead<Publisher> publisherRead, BaseStorageWrite<Person> personWrite, 
            BaseStorageRead<Person> personRead, BaseStorageWrite<Book> bookWrite, BaseStorageRead<Book> bookRead, BaseStorageWrite<BuyingRecord> buyingRecordWrite, BaseStorageRead<BuyingRecord> buyingRecordRead)
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

        public async Task BuyBook(PersonVo person, BookVo book)
        {
            var buyingRecord = new BuyingRecord();
            buyingRecord.Id = Guid.NewGuid().ToString();
            buyingRecord.TradingPrice = (float) (book.Price * 0.8);
            buyingRecord.CurrentOwner = person.Id;
            buyingRecord.TradingTime = DateTime.Now.AddMinutes(-_Random.Next(100, 10000));
            buyingRecord.Book = book.Id;
            await _buyingRecordWrite.InsertAsync(buyingRecord);
        }
    }
}
