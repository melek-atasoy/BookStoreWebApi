using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}