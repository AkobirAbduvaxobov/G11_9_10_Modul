
using BookSystem.Api.Contracts;
using BookSystem.Api.Entities;
using BookSystem.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Api.Implementations;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book.BookId;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        var books = await _context.Books.ToListAsync();
        return books;
    }   
}
