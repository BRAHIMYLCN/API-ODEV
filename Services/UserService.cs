using KutuphaneAPI.Context;
using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneAPI.Services;

public class UserService : IUserService {
    private readonly AppDbContext _context;
    public UserService(AppDbContext context) { _context = context; }

    public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

    public async Task<User> GetUserByIdAsync(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new Exception("Kullanıcı bulunamadı!");
        return user;
    }

    public async Task<User> CreateUserAsync(UserCreateDto userDto) {
        // DTO'dan gelen veriyi Name ve LastName olarak ayırıyoruz veya DTO'yu ona göre güncelliyoruz
        // Şimdilik hata vermemesi için FullName'i Name'e atıyoruz:
        var user = new User { 
            Name = userDto.FullName, // DTO'ndaki ismi Name'e veriyoruz
            LastName = "", // Boş bırakabiliriz veya DTO'nu güncelleyebilirsin
            Email = userDto.Email,
            Address = "Belirtilmedi" 
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(int id, UserCreateDto userDto) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new Exception("Güncellenecek kullanıcı bulunamadı!");
        
        user.Name = userDto.FullName;
        user.Email = userDto.Email;
        
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        var hasActiveBorrow = await _context.BorrowRecords
            .AnyAsync(b => b.UserId == id && b.ReturnDate == null);

        if (hasActiveBorrow) {
            throw new Exception("Bu kullanıcı üzerinde iade edilmemiş kitap görünüyor. Önce kitapları iade almalısın!");
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}