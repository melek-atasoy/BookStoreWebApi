using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace Application.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {

        [Fact]
        public void WhenLessThanOrEqualToZeroBookIdIsGiven_Validator_ShouldBeReturnError(){
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 0;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnError(){
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 1;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}