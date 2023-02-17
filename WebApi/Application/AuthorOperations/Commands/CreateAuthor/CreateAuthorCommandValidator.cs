using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(2).MaximumLength(30).NotEmpty();
            RuleFor(x => x.Model.Surname).MinimumLength(2).MaximumLength(30).NotEmpty();
            RuleFor(x => x.Model.DateOfBirth).LessThan(new DateTime(2011,01,01));
        }
    }
}