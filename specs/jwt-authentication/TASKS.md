# TASKS.md

## T1
Configurar JWT Authentication no Program.cs

### Objetivo
Adicionar autenticação JWT Bearer na aplicação.

### Validação
- Aplicação inicia sem erros
- Middleware JWT configurado

---

## T2
Criar JwtTokenService

### Objetivo
Gerar tokens JWT válidos contendo claims do usuário.

### Validação
- Token gerado corretamente
- Claims presentes

---

## T3
Criar endpoint de login

### Objetivo
Permitir autenticação via email e senha.

### Implementação
- Criar LoginRequestDTO
- Criar LoginResponseDTO

### Validação
- Login retorna token válido
- Credenciais inválidas retornam 401

---

## T4
Proteger endpoints

### Objetivo
Adicionar autorização nas rotas protegidas.

### Validação
- Endpoint sem token retorna 401
- Endpoint com token válido retorna 200

---

## T5
Testar fluxo completo

### Objetivo
Validar autenticação ponta a ponta.

### Validação
- Login funciona
- Token funciona
- Expiração funciona
