namespace KutuphaneAPI.DTOs;
public class BookCreateDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int AuthorId { get; set; } // Hangi yazara ait olduğunu buradan bağlayacağız
}