using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı");
            
            if(author.Books.Any())
                throw new InvalidOperationException("Öncelikle yazarın tüm kitapları silinmelidir");
            
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }

}