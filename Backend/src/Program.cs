using DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.GetDonghua;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Infrastructure.Persistence;
using DonghuaFlix.Backend.src.Infrastructure.Persistence.Repositories;
using DonghuaFlix.Backend.src.Infrastructure.Security;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using DonghuaFlix.Backend.src.Core.Application.Behaviors;
using System.Reflection;
using Microsoft.OpenApi.Models;
using DonghuaFlix.Backend.src.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
// ou o Assembly da camada Application

builder.Services.AddScoped<IEpisodeRepository , EpisodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDonghuaRepository, DonghuaRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>
builder.Services.AddScoped<AbstractValidator<GetDonghuaByIdQuery>, GetDonghuaByIdQueryValidator>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();


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





builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


// Configuração do Swagger
// Configuração do Swagger com suporte ao JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    // Define o esquema de segurança JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite 'Bearer' [espaço] e então o seu token JWT.\n\nExemplo: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\""
    });

    // Exige o token nos endpoints protegidos
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// ===== [1] CONFIGURAÇÃO DO SEED DO BANCO DE DADOS =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        // Aplica migrações pendentes
        await context.Database.MigrateAsync();
        
        // Executa o seed do usuário admin
        await UserSeed.SeedAdminUserAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao aplicar migrações ou executar seed");
    }
}

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

// IMPORTANTE: A ordem é crucial!
app.UseAuthentication(); // Deve vir ANTES de UseAuthorization
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();




