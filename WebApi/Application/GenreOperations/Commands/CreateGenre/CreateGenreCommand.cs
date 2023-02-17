using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreModel Model { get; set; }
        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if(genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut");
            
            genre = _mapper.Map<Genre>(Model);
            _context.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}