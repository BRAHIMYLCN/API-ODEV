using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase {
    private readonly IBookService _bookService;
    public BookController(IBookService bookService) { _bookService = bookService; }

    [HttpGet]
    public async Task<IActionResult> GetBooks() => 
        Ok(ApiResponse<object>.CreateSuccess(await _bookService.GetAllBooksAsync(), "Kitaplar raflardan getirildi."));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id) {
        try {
            return Ok(ApiResponse<object>.CreateSuccess(await _bookService.GetBookByIdAsync(id), "Kitap detayları bulundu."));
        } catch (Exception ex) {
            return NotFound(ApiResponse<object>.CreateFail(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookCreateDto dto) => 
        Ok(ApiResponse<object>.CreateSuccess(await _bookService.CreateBookAsync(dto), "Yeni kitap kütüphaneye eklendi."));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, BookCreateDto dto) {
        try {
            return Ok(ApiResponse<object>.CreateSuccess(await _bookService.UpdateBookAsync(id, dto), "Kitap bilgileri güncellendi."));
        } catch (Exception ex) {
            return BadRequest(ApiResponse<object>.CreateFail(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id) {
        var result = await _bookService.DeleteBookAsync(id);
        if (result) return Ok(ApiResponse<object>.CreateSuccess(true, "Kitap kütüphaneden kalıcı olarak silindi."));
        return NotFound(ApiResponse<object>.CreateFail("Kitap bulunamadığı için silinemedi."));
    }
}