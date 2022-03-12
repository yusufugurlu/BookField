using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace Business.Abstract
{
    public interface IBookService
    {
        List<BookViewModel> GetBooks();
        BookViewModel GetBook(int id);
        BookViewModel Add(BookViewModel view);
        BookViewModel Update(BookViewModel view);
        BookViewModel Delete(int id);
    }
}
