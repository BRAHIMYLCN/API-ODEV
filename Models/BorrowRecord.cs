using System;

namespace KutuphaneAPI.Models;

public class BorrowRecord : BaseEntity {
    public int UserId { get; set; }
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTimeOffset BorrowDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ReturnDate { get; set; }
}