using Microsoft.EntityFrameworkCore;
using KutuphaneAPI.Context;
using KutuphaneAPI.Interfaces;
using KutuphaneAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Bağlantısı (SQLite kullanarak kurulum derdinden kurtuluyoruz)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=kutuphane.db"));

// 2. Servisleri Tanımlama (Dependency Injection)
// Hocanın projesindeki IOrderService yapısına uygun şekilde ekledik
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger arayüzü için gerekli
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();

var app = builder.Build();

// 3. Swagger'ı Aktifleştirme (Ödevin görsel testi için şart)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Madde 2.5: Global Exception Handling Middleware
app.Use(async (context, next) => {
    try {
        await next();
    }
    catch (Exception ex) {
        // Hata durumunda 500 Internal Server Error dönüyoruz (Madde 2.6)
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        // Hata mesajını bizim standart ApiResponse formatına paketliyoruz (Madde 2.4)
        var response = KutuphaneAPI.Common.ApiResponse<object>.CreateFail($"Beklenmedik bir hata oluştu: {ex.Message}");
        
        await context.Response.WriteAsJsonAsync(response);
    }
});
app.MapControllers(); // Controller'ları dışarı açar

app.Run();