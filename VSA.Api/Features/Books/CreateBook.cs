using Carter;
using MediatR;
using VSA.Api.Contracts;
using VSA.Api.Database;
using VSA.Api.Entities;
using static VSA.Api.Features.Books.CreateBook;

namespace VSA.Api.Features.Books
{
    public static class CreateBook
    {
        public sealed class CreateBookCommand : IRequest<long>
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }
        internal sealed class Handler : IRequestHandler<CreateBookCommand, long>
        {
            private readonly ApplicationDbContext _dbContext;
            public Handler(ApplicationDbContext dbContext) => _dbContext = dbContext;
         
            public async Task<long> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                var book = new Book
                {
                    Name = request.Name,
                    Description = request.Description
                };
                await _dbContext.AddAsync(book, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return book.Id;
            }
        }
    }
    public class CreateBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/createbook", async (CreateBookCommand request, ISender sender) =>
            {
                var bookId = await sender.Send(request);
                return Results.Ok(bookId);
            });
        }
    }
}
