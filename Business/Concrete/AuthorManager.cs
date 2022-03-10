using Business.Abstract;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Parameters;
using ViewModel.ViewModels;

namespace Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private readonly BookStoreDbContext _bookStoreDbContext;


        public AuthorManager(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public AuthorViewModel AddAuthor(AuthorParameter authorParameter)
        {
            Author author = new Author();
            author.GenreId = authorParameter.Genre;
            author.Name = authorParameter.Name;
            _bookStoreDbContext.Authors.Add(author);
            _bookStoreDbContext.SaveChanges();

            AuthorViewModel authorViewModel = new AuthorViewModel();
            authorViewModel.Name = authorParameter.Name;
            authorViewModel.Genre = authorParameter.Genre.ToString();

            return authorViewModel;
        }

        public List<AuthorViewModel> GetAuthors()
        {
            var AuthorViewModels = _bookStoreDbContext.Authors.Select(x => new AuthorViewModel
            {
                Genre = x.GenreId.ToString(),
                Name = x.Name
            }).ToList();
            return AuthorViewModels;
        }
    }
}
