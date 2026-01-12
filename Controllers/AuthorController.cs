using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase {
    private readonly IAuthorService _authorService;
    public AuthorController(IAuthorService authorService) { _authorService = authorService; }

    [HttpGet]
    public async Task<IActionResult> GetAuthors() => 
        Ok(ApiResponse<object>.CreateSuccess(await _authorService.GetAllAuthorsAsync(), "Yazarlar listelendi."));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id) {
        try {
            return Ok(ApiResponse<object>.CreateSuccess(await _authorService.GetAuthorByIdAsync(id), "Yazar bulundu."));
        } catch (Exception ex) {
            return NotFound(ApiResponse<object>.CreateFail(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorCreateDto dto) => 
        Ok(ApiResponse<object>.CreateSuccess(await _authorService.CreateAuthorAsync(dto), "Yazar sisteme kaydedildi."));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorCreateDto dto) {
        try {
            return Ok(ApiResponse<object>.CreateSuccess(await _authorService.UpdateAuthorAsync(id, dto), "Yazar ismi güncellendi."));
        } catch (Exception ex) {
            return BadRequest(ApiResponse<object>.CreateFail(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id) {
        var result = await _authorService.DeleteAuthorAsync(id);
        if (result) return Ok(ApiResponse<object>.CreateSuccess(true, "Yazar ve eserleri sistemden silindi."));
        return NotFound(ApiResponse<object>.CreateFail("Yazar bulunamadı."));
    }
}