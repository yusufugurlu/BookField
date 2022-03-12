using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace Validation.BookOperations.CreateBook
{
    public class CreateBookValidator:AbstractValidator<BookViewModel>
    {
        public CreateBookValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez.");
        }
    }
}
