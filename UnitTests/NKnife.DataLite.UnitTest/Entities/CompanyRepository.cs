using System;
using System.IO;

namespace NKnife.DataLite.UnitTest.Entities
{
    public class CompanyRepository : PagingAndSortingRepositoryBase<Company, string>
    {
        public CompanyRepository() 
            : base(BuildDefaultDatabaseName())
        {
        }

        public CompanyRepository(string repositoryPath) 
            : base(repositoryPath)
        {
        }
    }

}
