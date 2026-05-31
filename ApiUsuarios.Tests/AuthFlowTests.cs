using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using ApiUsuarios.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace ApiUsuarios.Tests;

public class AuthFlowTests : IClassFixture<ApiUsuariosWebAppFactory>
{
    private readonly HttpClient _client;

    // credenciais do seed definido em Program.cs
    private const string EmailValido = "ana.silva@email.com";
    private const string SenhaValida = "senha123";

    public AuthFlowTests(ApiUsuariosWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ── Login funciona ─────────────────────────────────────────────────────────

    [Fact]
    public async Task Login_ComCredenciaisValidas_Retorna200EToken()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login",
            new { Email = EmailValido, Senha = SenhaValida });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrWhiteSpace(body.Token));
    }

    [Fact]
    public async Task Login_ComSenhaErrada_Retorna401()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login",
            new { Email = EmailValido, Senha = "senha_errada" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_ComEmailInexistente_Retorna401()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login",
            new { Email = "naoexiste@email.com", Senha = SenhaValida });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // ── Token funciona ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetUsuarios_SemToken_Retorna401()
    {
        var response = await _client.GetAsync("/usuarios");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUsuarios_ComTokenValido_Retorna200()
    {
        var token = await ObterTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/usuarios");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUsuarioById_ComTokenValido_Retorna200()
    {
        var token = await ObterTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/usuarios/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ── Expiração funciona ─────────────────────────────────────────────────────

    [Fact]
    public async Task GetUsuarios_ComTokenExpirado_Retorna401()
    {
        var tokenExpirado = GerarTokenExpirado();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenExpirado);

        var response = await _client.GetAsync("/usuarios");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    private async Task<string> ObterTokenAsync()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login",
            new { Email = EmailValido, Senha = SenhaValida });

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        return body!.Token;
    }

    private static string GerarTokenExpirado()
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(ApiUsuariosWebAppFactory.JwtKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "ApiUsuarios",
            audience: "ApiUsuarios",
            claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, "1") },
            expires: DateTime.UtcNow.AddHours(-2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
