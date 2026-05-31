using ApiUsuarios.Models;

namespace ApiUsuarios.Services;

public interface IJwtTokenService
{
    string GenerateToken(Usuario usuario);
}
