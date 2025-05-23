using DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.GetDonghua;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Infrastructure.Persistence;
using DonghuaFlix.Backend.src.Infrastructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = "wwwroot",
    Args = args
});

builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlite("Data Source=donghuaflix.db") );

// Add services to the container.
builder.Services.AddControllersWithViews();
// Registre MediatR e AutoMapper

builder.Services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssembly(typeof(AddFavoriteCommand).Assembly));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
// ou o Assembly da camada Application

builder.Services.AddScoped<IEpisodeRepository , EpisodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDonghuaRepository, DonghuaRepository>();
builder.Services.AddScoped<AbstractValidator<GetDonghuaByIdQuery>, GetDonghuaByIdQueryValidator>();


builder.Services.AddCors(options => 
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseCors("Frontend");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




