using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Model;
using FluentAssertions;
using FluentValidation;
using System;
using Test.TestsSetup;
using Validation.BookOperations.CreateBook;
using ViewModel.ViewModels;
using Xunit;

namespace Test.Application.BookOperations.Commands.BookCommands
{
    public class CreateBookCommandTests: IClassFixture<CommandTestFeature>
    {
        private readonly IBookService _bookService;
        public BookStoreDbContext content;
        public IMapper mapper;
        public CreateBookCommandTests(CommandTestFeature feature)
        {
            content = feature.content;
            mapper = feature.mapper;
            _bookService = feature.bookService;
        }
        [Fact]
        public void FirstTest()
        {
            // Arrenge -Hazırlık
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.Name = "test";
            CreateBookCommand createBookCommand = new CreateBookCommand(_bookService);
            createBookCommand.BookView = bookViewModel;

            //Çalıştırma Doğrulama
            FluentActions.Invoking(() => createBookCommand.Handle())
                .Should().Throw<InvalidCastException>().And.Message.Should().Be("Kitap zaten mevcut");
        }

        [Fact]
        public void FirstTestValidation()
        {
            // Arrenge -Hazırlık
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.Name = "test";
            CreateBookCommand createBookCommand = new CreateBookCommand(_bookService);
            createBookCommand.BookView = bookViewModel;
            CreateBookValidator validationRules =new CreateBookValidator(); 
            var result= validationRules.Validate(bookViewModel);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("test",0)]
        [InlineData("", 0)]
        [InlineData("Map Code", 1)]
        [InlineData("Map Code", 5)]
        public void FirstTestValidationWithTheory(string name, int id)
        {
            // Arrenge -Hazırlık
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.Name = name;
            bookViewModel.Id = id;
            CreateBookCommand createBookCommand = new CreateBookCommand(_bookService);
            createBookCommand.BookView = bookViewModel;
            CreateBookValidator validationRules = new CreateBookValidator();
            var result = validationRules.Validate(bookViewModel);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
