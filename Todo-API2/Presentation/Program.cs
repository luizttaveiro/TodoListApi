using Microsoft.EntityFrameworkCore;
using Todo_API2.Application.Interfaces;
using Todo_API2.Application.Mapping;
using Todo_API2.Application.Services;
using Todo_API2.Domain.Interfaces;
using Todo_API2.Infrastructure.Data;
using Todo_API2.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoContext>(opt =>
                                               opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
