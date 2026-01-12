using KutuphaneAPI.Context;
using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace KutuphaneAPI.Services;
public class BorrowService : IBorrowService {
    private readonly AppDbContext _context;
    public BorrowService(AppDbContext context) { _context = context; }

    public async Task<List<BorrowRecord>> GetAllBorrowsAsync() => 
        await _context.BorrowRecords.Include(b => b.User).Include(b => b.Book).ToListAsync();

    public async Task<BorrowRecord> BorrowBookAsync(BorrowCreateDto borrowDto) {
        var record = new BorrowRecord {
            UserId = borrowDto.UserId,
            BookId = borrowDto.BookId,
            BorrowDate = DateTime.UtcNow
        };
        _context.BorrowRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }
}