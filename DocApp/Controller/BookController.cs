using DocApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocApp.Controller;

[ApiController]
[Route("/api/books")]
public class BookController: ControllerBase
{
    private readonly AppDbContext _context;
    private int? _bookIdWithMaxPages;

    public BookController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IAsyncEnumerable<Book> GetAllBooks(int page = 0, int size = 100)
    {
        return _context.Books
            .OrderBy(e => e.Title)
            .Skip(page * size)
            .Take(size)
            .AsAsyncEnumerable();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = _context.Books.Find(id);
        return book == null ? NotFound() : Ok(book);
    }
    
    [HttpGet]
    [Route("pages/max")]
    public ActionResult<Book> GetBookWithMaxPages()
    {
        var book = _context.Books.OrderByDescending(b => b.NumPages).Take(1).SingleOrDefault();
        return book == null ? NotFound() : Ok(book);
    }
}