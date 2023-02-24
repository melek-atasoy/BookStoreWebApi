using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            
            var book = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).FirstOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadi");

            BookDetailViewModel model = _mapper.Map<BookDetailViewModel>(book);
            // model.Title = book.Title;
            // model.PageCount = book.PageCount;
            // model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            // model.Genre = ((GenreEnum)book.GenreId).ToString();
            return model;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }
    }
}