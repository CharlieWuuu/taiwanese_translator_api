// 處理應用程式的啟動邏輯
using Microsoft.OpenApi.Models; // 引入 OpenAPI (Swagger) 的命名空間

var builder = WebApplication.CreateBuilder(args); // 創建 Web 應用程式的建構器

// 加入 Swagger 服務
builder.Services.AddEndpointsApiExplorer(); // 讓 Minimal API 也能產生 OpenAPI 規格（即 Swagger 文件）
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo // 設定 Swagger 文件的基本資訊
    {
        Title = "My API", // Swagger 文件標題
        Version = "v1", // API 版本
        Description = "ASP.NET Core 8 的 API 文件" // API 文件描述
    });
});

builder.Services.AddControllers(); // 註冊 Controllers 服務，使 MVC 模式的 API 控制器可用
builder.Services.AddHttpClient(); // 註冊 HttpClient 讓 Controller 可以使用

var app = builder.Build(); // 建立 Web 應用程式

// 啟用 Swagger（API 文件）
app.UseSwagger(); // 讓應用程式提供 Swagger 文件
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"); // 設定 Swagger UI 的端點
    options.RoutePrefix = string.Empty; // 讓 Swagger UI 成為應用程式的根路徑（"/"）
});

// 啟用 HTTPS 強制跳轉（如果應用程式使用 HTTPS）
app.UseHttpsRedirection();

// 啟用授權中介軟體（此時並未真正啟用認證機制，只是預留）
app.UseAuthorization();

// 設定 API 路由對應到 Controllers
app.MapControllers();

app.Run(); // 啟動應用程式
