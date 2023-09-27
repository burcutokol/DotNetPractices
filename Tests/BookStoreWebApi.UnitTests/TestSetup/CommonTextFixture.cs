using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApi.DbOperations;

namespace BookStoreWebApi.UnitTests.TestSetup
{
    public class CommonTextFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTextFixture() 
        {

        }

    }
}
