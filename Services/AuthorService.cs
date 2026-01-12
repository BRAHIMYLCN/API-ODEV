using KutuphaneAPI.Context;
using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneAPI.Services;

public class AuthorService : IAuthorService {
    private readonly AppDbContext _context;
    public AuthorService(AppDbContext context) { _context = context; }

    public async Task<List<Author>> GetAllAuthorsAsync() => await _context.Authors.ToListAsync();

    public async Task<Author> GetAuthorByIdAsync(int id) {
        var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        if (author == null) throw new Exception("Yazar bulunamadı!");
        return author;
    }

    public async Task<Author> CreateAuthorAsync(AuthorCreateDto authorDto) {
        var author = new Author { Name = authorDto.Name };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<Author> UpdateAuthorAsync(int id, AuthorCreateDto authorDto) {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) throw new Exception("Güncellenecek yazar bulunamadı!");
        
        author.Name = authorDto.Name;
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAuthorAsync(int id) {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return false;

        // KORUMA KALKANI: Yazara ait kitap kontrolü
        var hasBooks = await _context.Books.AnyAsync(b => b.AuthorId == id);
        if (hasBooks) {
            throw new Exception("Bu yazara ait sistemde kayıtlı kitaplar var. Önce kitapları silmelisin!");
        }
        
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }
}