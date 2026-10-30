using ApiUsuarios.Data;
using ApiUsuarios.DTOs;
using ApiUsuarios.Models;
using ApiUsuarios.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Configuração 'Jwt:Key' não encontrada.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            // sem tolerância: token expirado é rejeitado imediatamente
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Usuarios.Any())
    {
        db.Usuarios.AddRange(
            new Usuario { Nome = "Ana Silva", Email = "ana.silva@email.com", Senha = "senha123" },
            new Usuario { Nome = "Bruno Costa", Email = "bruno.costa@email.com", Senha = "senha456" },
            new Usuario { Nome = "Carla Souza", Email = null, Senha = "senha789" }
        );
        db.SaveChanges();
    }

    app.MapOpenApi();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/openapi/v1.json", "ApiUsuarios v1"));
}

app.UseAuthentication();
app.UseAuthorization();

// POST /api/auth/login
app.MapPost("/api/auth/login", async (LoginRequestDTO request, IAuthService authService, IJwtTokenService tokenService) =>
{
    if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
        return Results.BadRequest("Email e Senha são obrigatórios.");

    var usuario = await authService.AuthenticateAsync(request);
    if (usuario is null)
        return Results.Unauthorized();

    var token = tokenService.GenerateToken(usuario);
    return Results.Ok(new LoginResponseDTO(token));
})
.WithName("Login")
.WithTags("Auth");

// GET /usuarios
app.MapGet("/usuarios", async (AppDbContext db) =>
    await db.Usuarios.ToListAsync())
    .WithName("GetUsuarios")
    .WithTags("Usuarios")
    .RequireAuthorization();

// GET /usuarios/{id}
app.MapGet("/usuarios/{id}", async (int id, AppDbContext db) =>
    await db.Usuarios.FindAsync(id) is Usuario usuario
        ? Results.Ok(usuario)
        : Results.NotFound())
    .WithName("GetUsuarioById")
    .WithTags("Usuarios")
    .RequireAuthorization();

// POST /usuarios
app.MapPost("/usuarios", async (Usuario usuario, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(usuario.Nome))
        return Results.BadRequest("O campo Nome é obrigatório.");

    if (string.IsNullOrWhiteSpace(usuario.Senha))
        return Results.BadRequest("O campo Senha é obrigatório.");

    db.Usuarios.Add(usuario);
    await db.SaveChangesAsync();
    return Results.Created($"/usuarios/{usuario.Id}", usuario);
})
.WithName("CreateUsuario")
.WithTags("Usuarios")
.RequireAuthorization();

// PUT /usuarios/{id}
app.MapPut("/usuarios/{id}", async (int id, Usuario input, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(input.Nome))
        return Results.BadRequest("O campo Nome é obrigatório.");

    if (string.IsNullOrWhiteSpace(input.Senha))
        return Results.BadRequest("O campo Senha é obrigatório.");

    var usuario = await db.Usuarios.FindAsync(id);
    if (usuario is null) return Results.NotFound();

    usuario.Nome = input.Nome;
    usuario.Email = input.Email;
    usuario.Senha = input.Senha;

    await db.SaveChangesAsync();
    return Results.Ok(usuario);
})
.WithName("UpdateUsuario")
.WithTags("Usuarios")
.RequireAuthorization();

// DELETE /usuarios/{id}
app.MapDelete("/usuarios/{id}", async (int id, AppDbContext db) =>
{
    var usuario = await db.Usuarios.FindAsync(id);
    if (usuario is null) return Results.NotFound();

    db.Usuarios.Remove(usuario);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteUsuario")
.WithTags("Usuarios")
.RequireAuthorization();

app.Run();

public partial class Program { }
