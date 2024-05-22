using Microsoft.EntityFrameworkCore;

using FastScooter.API.Mapper;
using FastScooter.Domain.Domain;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dependency Injection: AddScoped
builder.Services.AddScoped<IUserInfrastructure, UserMySQLInfrastructure>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
// Dependency Injection: AddAutoMapper
builder.Services.AddAutoMapper(
    // typeof(ModelToResponse),
    typeof(RequestToModel)
    );
// Database Connection and Dependency Injection: AddDbContext
var connectionString = builder.Configuration.GetConnectionString("FastScooterConnectionString");
builder.Services.AddDbContext<FastScooterContext>(
    dbContextOptions => dbContextOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    );

var app = builder.Build();
// Create database if not exists
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<FastScooterContext>())
{
    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();