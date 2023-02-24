using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Title = "Budala",
                    GenreId = 3,
                    PageCount = 1050,
                    AuthorId = 1,
                    PublishDate = new DateTime(1997, 06, 05)
                },
                new Book
                {
                    Id = 2,
                    Title = "Fareler ve Insanlar",
                    GenreId = 4,
                    PageCount = 350,
                    AuthorId = 2,
                    PublishDate = new DateTime(2002, 05, 14)
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    GenreId = 2,
                    PageCount = 300,
                    AuthorId = 3,
                    PublishDate = new DateTime(1991, 04, 23)
                }
            );        
        }
    }
}