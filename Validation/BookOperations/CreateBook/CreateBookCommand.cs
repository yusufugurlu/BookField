using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace Validation.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public BookViewModel BookView { get; set; }
        private readonly IBookService _bookService;
        public CreateBookCommand(IBookService bookService)
        {
            _bookService = bookService;
        }

        public BookViewModel Handle()
        {
            _bookService.Add(BookView);
            return BookView;
        }

    }
}
