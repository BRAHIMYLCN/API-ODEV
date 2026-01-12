using KutuphaneAPI.DTOs;
using KutuphaneAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace KutuphaneAPI.Controllers;
[ApiController] [Route("api/[controller]")]
public class BorrowController : ControllerBase {
    private readonly IBorrowService _borrowService;
    public BorrowController(IBorrowService borrowService) { _borrowService = borrowService; }

    [HttpGet] public async Task<IActionResult> GetBorrows() => Ok(await _borrowService.GetAllBorrowsAsync());
    [HttpPost] public async Task<IActionResult> BorrowBook(BorrowCreateDto dto) => Ok(await _borrowService.BorrowBookAsync(dto));
}