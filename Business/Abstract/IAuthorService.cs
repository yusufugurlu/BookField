using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Parameters;
using ViewModel.ViewModels;

namespace Business.Abstract
{
    public interface IAuthorService
    {
        List<AuthorViewModel> GetAuthors();
        AuthorViewModel AddAuthor(AuthorParameter authorParameter);
    }
}
