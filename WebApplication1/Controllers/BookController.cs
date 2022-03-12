using Business.Abstract;
using Common.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Validation.BookOperations.CreateBook;
using ViewModel.ViewModels;
using WebApplication1.Resource;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _authorService;
        private readonly IStringLocalizer<WebApplication1.Resource.Resource> _stringLocalizer;

        public BookController(IBookService authorService, IStringLocalizer<WebApplication1.Resource.Resource> stringLocalizer)
        {
            _authorService = authorService;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet()]
        public IActionResult GetBooks()
        {
            var value = _stringLocalizer["Hello"];
            var d = value.Value;
            var list = _authorService.GetBooks();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook([FromQuery] int id)
        {
            var result = _authorService.GetBook(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookViewModel authorDto)
        {

            CreateBookCommand createBookCommand = new CreateBookCommand(_authorService);
            createBookCommand.BookView = authorDto;
            CreateBookValidator validator = new CreateBookValidator();
            validator.ValidateAndThrow(authorDto);
            var result = createBookCommand.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook2([FromBody] BookViewModel authorDto)
        {
            try
            {
                CreateBookCommand createBookCommand = new CreateBookCommand(_authorService);
                createBookCommand.BookView = authorDto;
                CreateBookValidator validator = new CreateBookValidator();
                ValidationResult results = validator.Validate(authorDto);
                if (!results.IsValid)
                {
                    return BadRequest(FValidator.Errors(results.Errors));
                }

                var result = createBookCommand.Handle();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateAuthor([FromBody] BookViewModel authorDto)
        {
            var result = _authorService.Update(authorDto);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteBook(int id)
        {
            var result = _authorService.Delete(id);
            return Ok(result);
        }
    }
}
