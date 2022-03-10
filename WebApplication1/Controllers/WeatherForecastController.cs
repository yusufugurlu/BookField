using Business;
using Business.Abstract;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Parameters;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]

    public class WeatherForecastController : ControllerBase
    {
        private readonly IAuthorService _authorService;


        public WeatherForecastController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet()]
        public IActionResult GetAuthor()
        {
            var list = _authorService.GetAuthors();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorParameter authorDto)
        {
            var result = _authorService.AddAuthor(authorDto);
            return Ok(result);
        }
        /*
          [HttpGet()]
          public IEnumerable<Book> GetBooks()
          {
              var list = _bookStoreDbContext.Books.ToList();
              return list;
          }

          [HttpPost]
          public IActionResult AddBook([FromBody] BookDto bookDto)
          {
              var author = new Book();
              author.Name = bookDto.Name;
              _bookStoreDbContext.Books.Add(author);
              _bookStoreDbContext.SaveChanges();
              return Ok(author);
          }

          [HttpPost]
          public IActionResult BookAuthor([FromBody] BookAuthorDto bookAuthorDto)
          {
              var autor = _bookStoreDbContext.Authors.FirstOrDefault(x => x.Id == bookAuthorDto.AuthorId);
              if (autor != null)
              {
                  var book = _bookStoreDbContext.Books.FirstOrDefault(x => x.Id == bookAuthorDto.BookId);
                  book.Author = autor;
                  _bookStoreDbContext.Books.Update(book);
                  _bookStoreDbContext.SaveChanges();
              }
              return Ok();
          }
          */
    }
}