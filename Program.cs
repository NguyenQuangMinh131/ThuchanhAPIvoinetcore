using ett1_web_api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ appsettings.json (hoặc dùng mặc định nếu null)
var connectionString = builder.Configuration.GetConnectionString("StudentDBConnectionString")
    ?? "Server=DESKTOP-CI4SVFI\\SQLEXPRESS;Database=StudentDB;Integrated Security=True;TrustServerCertificate=True;";

// In ra chuỗi kết nối để debug
Console.WriteLine($"🔗 Connection String: {connectionString}");

try
{
    // Thêm DbContext vào DI container
    builder.Services.AddDbContext<StudentContext>(options =>
        options.UseSqlServer(connectionString));

    Console.WriteLine("✅ Kết nối Database thành công!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Lỗi khi kết nối Database: {ex.Message}");
}

// Bật CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Thêm dịch vụ Controller + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Sử dụng Routing trước khi Mapping Controllers
app.UseRouting();

// Bật CORS
app.UseCors("AllowAll");

// Bật Swagger nếu ở chế độ Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Bật Authorization (nếu có)
app.UseAuthorization();

// Ánh xạ Controller
app.MapControllers();

app.Run();
