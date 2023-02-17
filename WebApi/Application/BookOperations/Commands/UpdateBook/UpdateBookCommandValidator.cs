using System;
using System.Linq;
using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.UpdatedBook.GenreId).GreaterThan(0);
            RuleFor(command => command.UpdatedBook.Title).NotEmpty().MinimumLength(1);
            RuleFor(command => command.UpdatedBook.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.UpdatedBook.PageCount).NotEmpty().GreaterThan(0);
        }
    }
}