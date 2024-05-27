using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocApp.Data;
[Table("book")]
public class Book
{
    [Key()]
    [Column("book_id")]
    public int BookId { get; set; }
    
    [StringLength(400)]
    [Column("title")]
    public string Title { get; set; }
    
    [StringLength(13)]
    [Column("isbn13")]
    public string Isbn13 { get; set; }
    
    [Column("language_id")]
    public int? LanguageId { get; set; }
    
    [Column("num_pages")]
    public int? NumPages { get; set; }
    
    [Column("publication_date")]
    public DateTime? PublicationDate { get; set; }
    
    [Column("publisher_id")]
    public int? PublisherId { get; set; }
}

public class BookLanguage
{
}