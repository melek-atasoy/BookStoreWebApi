using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 4;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().WithMessage("Kitap bulunamadi");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated(){
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 2;
            command.UpdatedBook = new UpdateBookModel() {Title = "Lord of the rings", PublishDate = DateTime.Now.Date.AddYears(-10), PageCount = 100, GenreId = 2};
            
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
            book.Title.Should().Be(command.UpdatedBook.Title);
            book.GenreId.Should().Be(command.UpdatedBook.GenreId);
            book.PageCount.Should().Be(command.UpdatedBook.PageCount);
            book.PublishDate.Should().Be(command.UpdatedBook.PublishDate);

        }
    }
}