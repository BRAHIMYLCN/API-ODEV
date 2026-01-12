using KutuphaneAPI.Models;
using KutuphaneAPI.DTOs;

namespace KutuphaneAPI.Interfaces;

public interface IAuthorService {
    Task<List<Author>> GetAllAuthorsAsync();
    Task<Author> GetAuthorByIdAsync(int id);
    Task<Author> CreateAuthorAsync(AuthorCreateDto authorDto);
    Task<Author> UpdateAuthorAsync(int id, AuthorCreateDto authorDto);
    Task<bool> DeleteAuthorAsync(int id);
}