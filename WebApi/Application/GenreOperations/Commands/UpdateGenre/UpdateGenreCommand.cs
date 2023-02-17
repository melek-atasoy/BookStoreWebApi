using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }

        public UpdateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Güncellenecek kitap türü bulunamadı");
            
            if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimde bir kitap türü zaten mevcut");

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) || Model.Name.ToLower() == "string" ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}