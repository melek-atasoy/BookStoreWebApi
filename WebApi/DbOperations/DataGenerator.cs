using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return; //data was already seeded
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Fyodor",
                        Surname = "Dostoyevski",
                        DateOfBirth = new DateTime(1821, 11, 11)
                    },
                    new Author
                    {
                        Name = "John",
                        Surname = "Steinbeck",
                        DateOfBirth = new DateTime(1902, 02, 27)
                    },
                    new Author
                    {
                        Name = "George",
                        Surname = "Orwell",
                        DateOfBirth = new DateTime(1903, 06, 25)
                    }
                );
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Novel"
                    },
                    new Genre
                    {
                        Name = "Tragedy"
                    }
                );
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

                context.SaveChanges();
            }

        }
    }
}