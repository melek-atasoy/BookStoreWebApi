using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;

namespace Application.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 12;
            
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().WithMessage("Kitap bulunamadi");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeRead(){
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 2;
            
            FluentActions.Invoking(() => query.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == query.BookId);
            book.Should().NotBeNull();
        }
    }
}