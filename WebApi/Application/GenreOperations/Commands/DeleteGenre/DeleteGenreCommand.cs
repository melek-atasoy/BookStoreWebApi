using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly IBookStoreDbContext _context;
        public int GenreId { get; set; }

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Silinecek kitap türü bulunamadı");
            
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }

    public class DeleteGenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}