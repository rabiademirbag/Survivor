# 🏝️ Pratik - Survivor

Bu proje, **Survivor yarışmasına özel** bir ASP.NET Core Web API uygulamasıdır. Uygulama, yarışmacılar (Competitors) ve kategoriler (Categories) arasında bire-çok (one-to-many) ilişkisini içerir. Temel CRUD (Create, Read, Update, Delete) işlemleri desteklenmektedir.

---

## 📌 Proje Amacı

- Category ve Competitor tabloları arasında bire-çok ilişki kurmak
- CRUD işlemlerini RESTful endpoint'ler ile gerçekleştirmek
- Soft delete mantığını uygulamak
- DTO ve Entity ayrımı ile katmanlı mimariyi desteklemek

---

## 🧪 Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger (API dokümantasyonu)
- C# 10 / .NET 6+

---

## 🧱 Veri Tabanı Tasarımı

### 🗂️ Category (Kategoriler)

| Alan Adı     | Tip     |
|--------------|---------|
| Id           | int     |
| Name         | string  |
| CreatedDate  | DateTime |
| ModifiedDate | DateTime |
| IsDeleted    | bool    |

### 🧍 Competitor (Yarışmacılar)

| Alan Adı     | Tip     |
|--------------|---------|
| Id           | int     |
| FirstName    | string  |
| LastName     | string  |
| CategoryId   | int     |
| CreatedDate  | DateTime |
| ModifiedDate | DateTime |
| IsDeleted    | bool    |
