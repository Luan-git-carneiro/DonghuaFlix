using DonghuaFlix.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.src.Core.Aplication.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssembly(typeof(AddFavoriteCommand).Assembly));

builder.Services.AddScoped<IUserRepository, UsuarioRepository>();
builder.Services.AddScoped<IDonghuaRepository, DonghuaRepository>();

var app = builder.Build();

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




