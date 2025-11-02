using Application.Abstraction;
using Application.Service;
using Domain.Entity;
using Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Usuario
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Photographer
builder.Services.AddScoped<IPhotographerService, PhotographerService>();
builder.Services.AddScoped<IPhotographerRepository, PhotographerRepository>();
builder.Services.AddScoped<IPhotoSessionService, PhotoSessionService>();
builder.Services.AddScoped<IPhotographerRepository, PhotographerRepository>();
builder.Services.AddScoped<IPhotoSessionRepository, PhotoSessionRepository>();
// Location
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
