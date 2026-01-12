using KutuphaneAPI.Models;
using KutuphaneAPI.DTOs;
namespace KutuphaneAPI.Interfaces;
public interface IBorrowService {
    Task<List<BorrowRecord>> GetAllBorrowsAsync();
    Task<BorrowRecord> BorrowBookAsync(BorrowCreateDto borrowDto);
}