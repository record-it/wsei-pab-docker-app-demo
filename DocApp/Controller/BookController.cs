using DocApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocApp.Controller;

[ApiController]
[Route("/api/books")]
public class BookController: ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMessageProducer _messageProducer;
    public BookController(AppDbContext context, IMessageProducer messageProducer)
    {
        _context = context;
        _messageProducer = messageProducer;
    }
    
    [HttpGet]
    public IAsyncEnumerable<Book> GetAllBooks(int page = 0, int size = 100)
    {
        _messageProducer.SendMessage(new RequestStatistic()
        {
            Path = "/api/books",
        });
        var response = _context.Books
            .OrderBy(e => e.Title)
            .Skip(page * size)
            .Take(size)
            .AsAsyncEnumerable();
        return response;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = _context.Books.Find(id);
        return book == null ? NotFound() : Ok(book);
    }
}