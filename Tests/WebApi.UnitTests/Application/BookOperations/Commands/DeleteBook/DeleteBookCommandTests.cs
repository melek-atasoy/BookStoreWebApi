using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        public BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            Book book = new Book();
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 4;
            book.Id = command.BookId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().WithMessage("Silinecek kitap bulunamadi");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted(){
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();

            FluentActions
                .Invoking(() => command.Handle()).Invoke();
        }
    }
}