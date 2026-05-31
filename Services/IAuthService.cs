using ApiUsuarios.DTOs;
using ApiUsuarios.Models;

namespace ApiUsuarios.Services;

public interface IAuthService
{
    Task<Usuario?> AuthenticateAsync(LoginRequestDTO request);
}
