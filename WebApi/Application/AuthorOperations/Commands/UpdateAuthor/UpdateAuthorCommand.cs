using System;
using AutoMapper;
using WebApi.DbOperations;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }

        public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı");
            
            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) || Model.Name.ToLower() == "string" ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) || Model.Surname.ToLower() == "string" ? author.Surname : Model.Surname;
            author.DateOfBirth = Model.DateOfBirth.Date;
            _context.SaveChanges();
        }

    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}