using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthorManager(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public AuthorViewModel AddAuthor(AuthorParameter authorParameter)
        {
            var author = _mapper.Map<Author>(authorParameter);
            _bookStoreDbContext.Authors.Add(author);
            _bookStoreDbContext.SaveChanges();

            AuthorViewModel authorViewModel = _mapper.Map<AuthorViewModel>(authorParameter);
            return authorViewModel;
        }

        public AuthorViewModel GetAuthorById(int id)
        {
            var authorViewModel = _bookStoreDbContext.Authors.Select(x => new AuthorViewModel
            {
                Genre = x.GenreId.ToString(),
                Name = x.Name
            }).FirstOrDefault();
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

        public AuthorViewModel UpdateAuthor(AuthorParameter authorParameter)
        {
            AuthorViewModel authorViewModel = new AuthorViewModel();

            var author = _bookStoreDbContext.Authors.FirstOrDefault(x => x.Id == authorParameter.Id);
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
