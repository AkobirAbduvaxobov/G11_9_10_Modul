

using BookSystem.Api.Entities;

namespace BookSystem.Api.Contracts;

public interface IBookRepository
{
    Task<long> AddAsync(Book book);
    Task<List<Book>> GetAllAsync();
}
