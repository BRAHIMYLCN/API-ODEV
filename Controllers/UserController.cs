using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;
    public UserController(IUserService userService) { _userService = userService; }

    [HttpGet]
    public async Task<IActionResult> GetUsers() => 
        Ok(ApiResponse<object>.CreateSuccess(await _userService.GetAllUsersAsync(), "Kullanıcılar listelendi."));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id) {
        try {
            return Ok(ApiResponse<object>.CreateSuccess(await _userService.GetUserByIdAsync(id), "Kullanıcı bulundu."));
        } catch (Exception ex) {
            return NotFound(ApiResponse<object>.CreateFail(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateDto dto) => 
        Ok(ApiResponse<object>.CreateSuccess(await _userService.CreateUserAsync(dto), "Kullanıcı başarıyla oluşturuldu."));

    [HttpPut("{id}")] // GÜNCELLEME (PUT)
    public async Task<IActionResult> UpdateUser(int id, UserCreateDto dto) {
        try {
            return Ok(ApiResponse<object>.CreateSuccess(await _userService.UpdateUserAsync(id, dto), "Kullanıcı güncellendi."));
        } catch (Exception ex) {
            return BadRequest(ApiResponse<object>.CreateFail(ex.Message));
        }
    }

    [HttpDelete("{id}")] // SİLME (DELETE)
    public async Task<IActionResult> DeleteUser(int id) {
        var result = await _userService.DeleteUserAsync(id);
        if (result) return Ok(ApiResponse<object>.CreateSuccess(true, "Kullanıcı sistemden uçuruldu!"));
        return NotFound(ApiResponse<object>.CreateFail("Kullanıcı bulunamadığı için silinemedi."));
    }
}