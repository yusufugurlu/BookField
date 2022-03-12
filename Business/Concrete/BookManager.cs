using AutoMapper;
using Business.Abstract;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public BookManager(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public BookViewModel Add(BookViewModel view)
        {
            _bookStoreDbContext.Database.EnsureCreated();
            var newBook = _mapper.Map<Book>(view);

            _bookStoreDbContext.Books.Add(newBook);
            _bookStoreDbContext.SaveChanges();
            return view;
        }

        public BookViewModel Delete(int id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            var oldBook = _bookStoreDbContext.Books.Where(x => x.Id == id).FirstOrDefault();
            if (oldBook != null)
            {
                var newBook = _mapper.Map<BookViewModel>(oldBook);
                _bookStoreDbContext.Books.Remove(oldBook);
                _bookStoreDbContext.SaveChanges();
                bookViewModel = newBook;
            }
            return bookViewModel;
        }

        public List<BookViewModel> GetBooks()
        {
            var list = _bookStoreDbContext.Books.Select(x => new BookViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return list;
        }

        public BookViewModel GetBook(int id)
        {
            var list = _bookStoreDbContext.Books.Where(x => x.Id == id).Select(x => new BookViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault();
            return list;
        }

        public BookViewModel Update(BookViewModel view)
        {
            var oldBook = _bookStoreDbContext.Books.Where(x => x.Id == view.Id).FirstOrDefault();
            if (oldBook != null)
            {
                Book newBook = _mapper.Map<Book>(view);
                _bookStoreDbContext.Books.Update(newBook);
                _bookStoreDbContext.SaveChanges();
            }
            return view;
        }
    }
}
