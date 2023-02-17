using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using WebApi.Entities;
using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator(){
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}