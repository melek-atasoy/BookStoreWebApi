using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle(){
            var authors = _context.Authors.Include(x => x.Books).ToList();
            var vm = _mapper.Map<List<AuthorsViewModel>>(authors);
            return vm;
        }
    }

    public class AuthorsViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}