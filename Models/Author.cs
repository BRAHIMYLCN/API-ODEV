using System.Collections.Generic;

namespace KutuphaneAPI.Models;

public class Author : BaseEntity {
    public string Name { get; set; }
    public List<Book> Books { get; set; } = new();
}