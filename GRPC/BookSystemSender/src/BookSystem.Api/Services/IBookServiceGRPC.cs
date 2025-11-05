using BookSystem.Api.Models;

namespace BookSystem.Api.Services;

public interface IBookServiceGRPC
{
    public Task<long> AddAsync(Book book);
    public Task<List<Book>> GetAllAsync();
}