using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // 对于 MySqlServerVersion
using Microsoft.EntityFrameworkCore; // 对于 UseMySql
using ToDoListApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 添加 CORS 服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // 允许来自 React 应用的请求
              .AllowAnyMethod() // 允许所有 HTTP 方法 (GET, POST, PUT, DELETE 等)
              .AllowAnyHeader(); // 允许所有请求头
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 3, 0)) // 替换为你的 MySQL 版本
    ));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

var app = builder.Build();

app.UseRouting();

//使用 CORS 必须在 UseRouting 和 UseEndpoints 之间
app.UseCors("AllowReactApp");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseAuthorization();

app.MapControllers();

app.Run();
