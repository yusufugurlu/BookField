using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestsSetup
{
    public class CommandTestFeature
    {
        public BookStoreDbContext content { get; set; }
        public IBookService bookService { get; set; }
        public IMapper mapper { get; set; }
        public CommandTestFeature()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseSqlServer("Server=PIKACU;Database=Book2DB;Trusted_Connection=True").Options;
            content = new BookStoreDbContext(options);
            mapper = new MapperConfiguration(x => { x.AddProfile<WebApplication1.AutoMapping.Mapping>(); }).CreateMapper();
            bookService = new BookManager(content, mapper);
        }
    }
}
