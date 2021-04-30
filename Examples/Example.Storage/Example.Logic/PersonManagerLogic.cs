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
    public class PersonManagerLogic
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

        public PersonManagerLogic(IMapper mapper, BaseStorageWrite<Publisher> publisherWrite, BaseStorageRead<Publisher> publisherRead, BaseStorageWrite<Person> personWrite, 
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

        public async Task AddPersonAsync(PersonVo[] people)
        {
            var pub = _mapper.Map<Person[]>(people);
            await _personWrite.InsertManyAsync(pub);
            Console.WriteLine($"新增读者{pub.Length}个，完成。");
        }

        public async Task<PersonVo> GetRandomPerson()
        {
            var count = await _personRead.CountAsync();
            var index = _Random.Next(0, (int)count);
            var personArray = await _personRead.PageAsync(index, 1);
            return _mapper.Map<PersonVo>(personArray.ElementAt(0));
        }

    }
}
