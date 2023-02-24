using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord of the rings", 0, 0)]
        [InlineData("Lord of the rings", 0, 1)]
        [InlineData("Lord of the rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){

            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel() {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel() {
                Title = "Lord of the rings",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel() {
                Title = "Lord of the rings",
                PageCount = 100,
                GenreId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);
        }
    }
}