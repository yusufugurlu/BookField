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

        public AuthorViewModel GetAuthorById(int id)
        {
            var AuthorViewModels = _bookStoreDbContext.Authors.Select(x => new AuthorViewModel
            {
                Genre = x.GenreId.ToString(),
                Name = x.Name
            }).FirstOrDefault();
            return AuthorViewModels;
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

        public AuthorViewModel UpdateAuthor(AuthorParameter authorParameter)
        {
            AuthorViewModel authorViewModel = new AuthorViewModel();

            var author=  _bookStoreDbContext.Authors.FirstOrDefault(x => x.Id == authorParameter.Id);
            if (author != null)
            {
                author.Name = authorParameter.Name;
                author.GenreId = authorParameter.Genre;
                _bookStoreDbContext.Authors.Update(author);
                _bookStoreDbContext.SaveChanges();

                authorViewModel.Name = authorParameter.Name;
                authorViewModel.Genre = authorParameter.Genre.ToString();
            }
            return authorViewModel;
        }
    }
}
