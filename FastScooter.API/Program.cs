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
// Dependency Injection: AddScoped Infrastructure and Domain
builder.Services.AddScoped<IUserInfrastructure, UserMySQLInfrastructure>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IScooterInfrastructure, ScooterMySQLInfrastructure>();
builder.Services.AddScoped<IScooterDomain, ScooterDomain>();
builder.Services.AddScoped<IRentInfrastructure, RentMySQLInfrastructure>();
builder.Services.AddScoped<IRentDomain, RentDomain>();
builder.Services.AddScoped<IFavoriteInfrastructure, FavoritesMySQLInfrastructure>();
builder.Services.AddScoped<IFavoriteDomain, FavoriteDomain>();
builder.Services.AddScoped<IPaymentInfrastructure, PaymentMySQLInfrastructure>();
builder.Services.AddScoped<IPaymentDomain, PaymentDomain>();
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