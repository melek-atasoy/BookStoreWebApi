using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",100,0)]
        [InlineData("",0,1)]
        [InlineData("Lord of the rings",0,0)]
        [InlineData("Lord of the rings",100,0)]
        [InlineData("Lord of the rings",0,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.UpdatedBook = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };
            command.BookId = 1;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenLessThanOrEqualToZeroBookIdIsGiven_Validator_ShouldBeReturnError(){
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.UpdatedBook = new UpdateBookModel()
            {
                Title = "Lord of the rings",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };
            command.BookId = -10;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError(){
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.UpdatedBook = new UpdateBookModel()
            {
                Title = "Lord of the rings",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now
            };
            command.BookId = 1;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(){
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.UpdatedBook = new UpdateBookModel()
            {
                Title = "Lord of the rings",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-10)
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

    }
}