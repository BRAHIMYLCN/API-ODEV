using KutuphaneAPI.Models;
using KutuphaneAPI.DTOs;

namespace KutuphaneAPI.Interfaces;

public interface IBookService {
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(BookCreateDto bookDto);
    Task<Book> UpdateBookAsync(int id, BookCreateDto bookDto);
    Task<bool> DeleteBookAsync(int id);
}