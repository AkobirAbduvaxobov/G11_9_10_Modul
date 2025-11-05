using Microsoft.AspNetCore.Mvc;
using BookSystem.Api.Services;
using BookSystem.Api.Models;
using BookContracts;

namespace BookSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookServiceGRPC _bookService;

    public BooksController(IBookServiceGRPC bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Models.Book book)
    {
        var id = await _bookService.AddAsync(book);
        return Ok(new { BookId = id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }
}
