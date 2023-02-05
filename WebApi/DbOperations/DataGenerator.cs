using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

                context.Books.AddRange(
                    new Book
                    {
                        Id = 1,
                        Title = "Budala",
                        GenreId = 1,
                        PageCount = 1050,
                        PublishDate = new DateTime(1997, 06, 05)
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Fareler ve Insanlar",
                        GenreId = 2,
                        PageCount = 350,
                        PublishDate = new DateTime(2002, 05, 14)
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "1984",
                        GenreId = 3,
                        PageCount = 300,
                        PublishDate = new DateTime(1991, 04, 23)
                    }
                );

                context.SaveChanges();
            }

        }
    }
}