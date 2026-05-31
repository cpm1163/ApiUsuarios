# PLAN.md

## Arquitetura

- ASP.NET Core Web API
- Minimal APIs
- Entity Framework Core
- SQLite
- JWT Bearer Authentication

---

## Estratégia de Segurança

- Tokens assinados com chave simétrica
- Expiração padrão de 1 hora
- Claims contendo UserId e Email
- Endpoints protegidos com RequireAuthorization()

---

## Estrutura Técnica

### Serviços
- JwtTokenService
- AuthService

### DTOs
- LoginRequestDTO
- LoginResponseDTO

### Endpoints
- POST /api/auth/login

### Middleware
- Authentication Middleware
- Authorization Middleware

---

## Dependências

- Microsoft.AspNetCore.Authentication.JwtBearer

---

## Configuração

- Configurações JWT via IConfiguration
- Chave JWT armazenada em appsettings.json

---

## Decisões Técnicas

- Utilizar PasswordHasher<T>
- Seguir padrão de Services e DTOs
- Utilizar autenticação baseada em Bearer Token
