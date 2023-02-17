using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x=> x.AuthorId).GreaterThan(0);
            RuleFor(x => x.Model.DateOfBirth).LessThan(new DateTime(2011,01,01));
            RuleFor(x => x.Model.Name).MinimumLength(2).MaximumLength(30).When(x => x.Model.Name != string.Empty);
            RuleFor(x => x.Model.Surname).MinimumLength(2).MaximumLength(30).When(x => x.Model.Name != string.Empty);
        }
    }
}