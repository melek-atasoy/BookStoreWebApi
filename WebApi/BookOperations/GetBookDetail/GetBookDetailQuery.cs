using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            BookDetailViewModel model = new BookDetailViewModel();
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadi");

            model.Title = book.Title;
            model.PageCount = book.PageCount;
            model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            model.Genre = ((GenreEnum)book.GenreId).ToString();
            return model;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}