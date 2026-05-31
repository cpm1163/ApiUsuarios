# SPEC.md

## Feature
Implementar autenticação JWT na API ASP.NET Core.

---

## Objetivo

Adicionar autenticação e autorização baseada em JWT
para proteger os endpoints da aplicação.

---

## Requisitos de Negócio

- Usuários devem conseguir realizar login
- Endpoints protegidos devem exigir autenticação
- Tokens devem possuir expiração
- Apenas usuários autenticados podem acessar rotas protegidas

---

## Restrições

- Utilizar JWT Bearer Authentication
- Não utilizar OAuth
- Não utilizar ASP.NET Identity
- Não alterar funcionalidades existentes do CRUD

---

## Critérios de Aceitação

- Endpoint /login retorna token JWT válido
- Rotas protegidas retornam 401 sem token
- Token válido permite acesso aos endpoints
- Token expirado deve ser rejeitado
