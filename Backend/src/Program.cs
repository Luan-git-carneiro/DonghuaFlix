using DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Infrastructure.Persistence.Repositories;
using FluentValidation;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = "wwwroot",
    Args = args
});

// Add services to the container.
builder.Services.AddControllersWithViews();
// Registre MediatR e AutoMapper

builder.Services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssembly(typeof(AddFavoriteCommand).Assembly));

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




