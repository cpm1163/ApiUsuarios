using ApiUsuarios.Data;
using ApiUsuarios.DTOs;
using ApiUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUsuarios.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Usuario?> AuthenticateAsync(LoginRequestDTO request)
    {
        var usuario = await _db.Usuarios
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (usuario is null || usuario.Senha != request.Senha)
            return null;

        return usuario;
    }
}
