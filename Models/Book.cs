namespace KutuphaneAPI.Models;

public class Book : BaseEntity {
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}