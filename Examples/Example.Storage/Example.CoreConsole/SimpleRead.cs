using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Example.Common;
using NKnife.Storages;

namespace Example.CoreConsole
{
    public class SimpleRead
    {
        private readonly BaseStorageRead<Publisher> _publisherRead;
        private readonly BaseStorageRead<BuyingRecord> _buyingRecordRead;
        private readonly BaseStorageRead<Person> _personRead;
        private readonly BaseStorageRead<Book> _bookRead;

        public SimpleRead(BaseStorageRead<Publisher> publisherRead, BaseStorageRead<BuyingRecord> buyingRecordRead, BaseStorageRead<Person> personRead, BaseStorageRead<Book> bookRead)
        {
            _publisherRead = publisherRead;
            _buyingRecordRead = buyingRecordRead;
            _personRead = personRead;
            _bookRead = bookRead;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishers()
        {
            var result = await _publisherRead.FindAllAsync();
            return result;
        }

        public async Task<IEnumerable<BuyingRecord>> GetAllBuyingRecords()
        {
            var result = await _buyingRecordRead.FindAllAsync();
            return result;
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            var result = await _personRead.FindAllAsync();
            return result;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var result = await _bookRead.FindAllAsync();
            return result;
        }
    }
}
