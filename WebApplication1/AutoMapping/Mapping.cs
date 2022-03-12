using AutoMapper;
using Common.Entities;
using DataAccess.Model;
using ViewModel.Parameters;
using ViewModel.ViewModels;
using WebApplication1.AutoMapping.MappingActions;

namespace WebApplication1.AutoMapping
{
    //Bu sınıf bizim için Entity ve dto sınıfları arasında mapping tablosu görevi görmektedir. 
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<AuthorParameter, AuthorViewModel>();
            CreateMap<AuthorParameter, Author>();
            //Burada kaynaktaki entityde bulunan enumu hedeftedeki enumu string halini view aktarması
            //Propertylere custom özellikler eklenebilir.
            CreateMap<Author, AuthorParameter>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

            //Yukarıdaki maplamelerde kendine has özellikleri maplemek için IMappingAction interface kullanarak bunları farklı
            //classlara bölebiliriz.
            CreateMap<Author, AuthorViewModel>().AfterMap<AuthorAction>();
            CreateMap<Author, AuthorViewModel>().BeforeMap<AuthorAction>();

            //ReverseMap mapping işleminin iki yönlü olduğunu belirtir.
            CreateMap<Author, AuthorViewModel>().ReverseMap();

            CreateMap<BookViewModel,Book >().ReverseMap();

        }
        
    }
}
