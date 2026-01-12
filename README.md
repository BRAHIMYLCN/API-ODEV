# 📚 Kütüphane Yönetim Sistemi API

Bu proje, modern .NET 9 teknolojileri ve katmanlı mimari (Layered Architecture) kullanılarak geliştirilmiş profesyonel bir kütüphane yönetim sistemi REST API'sidir.

## 🛠️ Teknik Özellikler
* **Framework:** .NET 9 (Minimal API & Controllers)
* **ORM:** Entity Framework Core
* **Veritabanı:** SQLite
* **Mimari:** Katmanlı Mimari (Controller -> Service -> Data)

## ✨ Önemli Gereksinim Uygulamaları
* **BaseEntity:** Tüm tablolar için `Id`, `CreatedAt` ve `UpdatedAt` alanları otomatik yönetilmektedir.
* **Global Exception Handling:** Tüm beklenmedik hatalar merkezi bir middleware ile yakalanıp `ApiResponse` formatında dönülmektedir.
* **Standardized Response:** Tüm API çıktıları `{ success, message, data }` kalıbına uygundur.
* **Veri Bütünlüğü:** Üzerinde kitap olan kullanıcıların veya kitabı olan yazarların silinmesi engellenerek veri tutarlılığı sağlanmıştır.

## 🚀 Başlatma
1. `dotnet restore`
2. `dotnet ef database update`
3. `dotnet run`

API dokümantasyonuna `/swagger` adresinden erişebilirsiniz.