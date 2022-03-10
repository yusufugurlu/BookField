using AutoMapper;
using Common.Entities;
using DataAccess.Model;
using ViewModel.Parameters;
using ViewModel.ViewModels;

namespace WebApplication1.AutoMapping
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<AuthorParameter, Author>();
            //Burada kaynaktaki entityde bulunan enumu hedeftedeki enumu string halini view aktarması
            //Propertylere custom özellikler eklenebilir.
            CreateMap<Author, AuthorParameter>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<AuthorParameter, AuthorViewModel>();
        }
        
    }
}
