using DonghuaFlix.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = "wwwroot",
    Args = args
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssembly(typeof(AddFavoriteCommand).Assembly));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDonghuaRepository, DonghuaRepository>();

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




