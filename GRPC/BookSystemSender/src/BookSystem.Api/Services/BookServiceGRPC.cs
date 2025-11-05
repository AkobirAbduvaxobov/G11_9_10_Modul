using BookSystem.Api.Models;
using BookContracts;

namespace BookSystem.Api.Services;

public class BookServiceGRPC : IBookServiceGRPC
{
    private readonly BookService.BookServiceClient _client;

    public BookServiceGRPC(BookService.BookServiceClient client)
    {
        _client = client;
    }

    public async Task<long> AddAsync(Models.Book book)
    {
        var request = new CreateRequest
        {
            Book = new BookContracts.Book
            {
                Title = book.Title,
                Author = book.Author,
                Isbn = book.ISBN,
                Publisher = book.Publisher,
                Pages = book.Pages,
                Genre = book.Genre,
                Language = book.Language,
                Price = (double)book.Price
            }
        };
        CreateResponse response = new CreateResponse();
        try
        {
            response = await _client.AddAsync(request);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
        
        return response.BookId;
    }


    public async Task<List<Models.Book>> GetAllAsync()
    {
        var response = await _client.GetAllAsync(new GetAllRequest());

        var books = response.Items.Select(b => new Models.Book
        {
            BookId = b.BookId,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.Isbn,
            Publisher = b.Publisher,
            Pages = b.Pages,
            Genre = b.Genre,
            Language = b.Language,
            Price = (decimal)b.Price
        }).ToList();

        return books;
    }

}
