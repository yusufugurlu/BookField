using AutoMapper;
using DataAccess.Model;
using ViewModel.ViewModels;

namespace WebApplication1.AutoMapping.MappingActions
{
    public class AuthorAction : IMappingAction<Author, AuthorViewModel>
    {
        public void Process(Author source, AuthorViewModel destination, ResolutionContext context)
        {
            destination.Genre = source.GenreId.ToString();
        }
    }
}
