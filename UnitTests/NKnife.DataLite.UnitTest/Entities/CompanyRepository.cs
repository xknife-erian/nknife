﻿using System;
using System.IO;

namespace NKnife.DataLite.UnitTest.Entities
{
    public class CompanyRepository : PagingAndSortingRepositoryBase<Company, string>
    {
        public CompanyRepository() :
            base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nameof(CompanyRepository)}.litedb"))
        {
        }

        public CompanyRepository(string repositoryPath) :
            base(repositoryPath)
        {
        }
    }

}
