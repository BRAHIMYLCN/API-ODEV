using KutuphaneAPI.Models;
using KutuphaneAPI.DTOs;

namespace KutuphaneAPI.Interfaces;

public interface IUserService {
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id); // Tekil kullanıcı bulma
    Task<User> CreateUserAsync(UserCreateDto userDto);
    Task<User> UpdateUserAsync(int id, UserCreateDto userDto); // Güncelleme
    Task<bool> DeleteUserAsync(int id); // Silme
}