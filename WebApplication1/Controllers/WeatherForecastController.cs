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

        [HttpPost]
        public IActionResult UpdateAuthor([FromBody] AuthorParameter authorDto)
        {
            var result = _authorService.UpdateAuthor(authorDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult UpdateAuthor(int id)
        {
            var result = _authorService.GetAuthorById(id);
            return Ok(result);
        }
    }
}