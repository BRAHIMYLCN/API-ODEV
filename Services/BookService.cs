using KutuphaneAPI.Context;
using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneAPI.Services;

public class BookService : IBookService {
    private readonly AppDbContext _context;
    public BookService(AppDbContext context) { _context = context; }

    public async Task<List<Book>> GetAllBooksAsync() => 
        await _context.Books.Include(b => b.Author).ToListAsync();

    public async Task<Book> GetBookByIdAsync(int id) {
        var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) throw new Exception("Kitap bulunamadı!");
        return book;
    }

    public async Task<Book> CreateBookAsync(BookCreateDto bookDto) {
        var book = new Book { 
            Title = bookDto.Title, 
            ISBN = bookDto.ISBN, 
            AuthorId = bookDto.AuthorId 
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBookAsync(int id, BookCreateDto bookDto) {
        var book = await _context.Books.FindAsync(id);
        if (book == null) throw new Exception("Güncellenecek kitap bulunamadı!");

        book.Title = bookDto.Title;
        book.ISBN = bookDto.ISBN;
        book.AuthorId = bookDto.AuthorId;

        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBookAsync(int id) {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}