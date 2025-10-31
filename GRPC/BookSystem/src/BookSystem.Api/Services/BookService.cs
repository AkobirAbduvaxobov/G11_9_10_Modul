using BookContracts;
using BookSystem.Api.Contracts;
using Grpc.Core;


namespace BookSystem.Api.Services;

public class BookService : BookContracts.BookService.BookServiceBase
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public override async Task<CreateResponse> Add(CreateRequest request, ServerCallContext context)
    {
        var book = new Api.Entities.Book
        {
            Title = request.Book.Title,
            Author = request.Book.Author,
            ISBN = request.Book.Isbn,
            Publisher = request.Book.Publisher,
            Pages = request.Book.Pages,
            Genre = request.Book.Genre,
            Language = request.Book.Language,
            Price = (decimal)request.Book.Price
        };


        var res = await _repository.AddAsync(book);
        return new CreateResponse { BookId = res };
    }

    public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
    {
        var books = await _repository.GetAllAsync();

        GetAllResponse response = new GetAllResponse();
        foreach (var book in books)
        {
            response.Items.Add(new Book()
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.ISBN,
                Publisher = book.Publisher,
                Pages = book.Pages,
                Genre = book.Genre,
                Language = book.Language,
                Price = (double)book.Price
            });
        }

        return response;
    }
}
