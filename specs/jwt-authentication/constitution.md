# constitution.md

## Regras Obrigatórias

- Nunca expor secrets no código
- Sempre utilizar tipagem forte
- Utilizar código assíncrono quando aplicável
- Não acessar banco diretamente dos endpoints
- Seguir separação de responsabilidades
- Não modificar endpoints existentes sem necessidade

---

## Padrões

- Utilizar DTOs para entrada e saída
- Validar dados recebidos
- Seguir convenções REST
- Manter nomes consistentes

---

## Segurança

- Nunca retornar senha ou hash
- Validar token JWT em endpoints protegidos
- Utilizar RequireAuthorization() em endpoints protegidos
- Não armazenar chave JWT hardcoded
- Centralizar geração de token no JwtTokenService
