using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using WebApi.Entities;
using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.IsActive).NotNull();
        }
    }
}