# Spec-Driven Development com Claude Code no .NET

Projeto e material de apoio da videoaula sobre **Spec-Driven Development (SDD)** utilizando **Claude Code** em um projeto **ASP.NET Core Web API** com autenticação JWT.

---

# 📚 Sobre a Aula

Nesta aula mostramos como utilizar o conceito de **Spec-Driven Development** para desenvolver software com IA de forma mais previsível, organizada e profissional.

Ao invés de simplesmente pedir código para a IA, criamos:

- especificações
- planejamento técnico
- regras globais
- tarefas sequenciais

E então utilizamos o **Claude Code** para implementar cada etapa incrementalmente.

---

# 🚀 Cenário do Projeto

Temos uma aplicação ASP.NET Core Web API que já realiza:

- CRUD de clientes
- persistência de dados
- operações básicas

E o objetivo é implementar:

✅ autenticação JWT  
✅ autorização de endpoints  
✅ login seguro  
✅ proteção de rotas  

Tudo isso utilizando o fluxo do **Spec-Driven Development**.

---

# 🧠 O que é Spec-Driven Development

Spec-Driven Development é uma abordagem onde a especificação se torna a principal fonte de verdade do projeto.

Ao invés da IA improvisar soluções, ela trabalha baseada em:

- requisitos claros
- arquitetura definida
- regras de engenharia
- tarefas pequenas e verificáveis

---

# ⚡ Fluxo do SDD

```text
Specify → Design → Tasks → Execute
```

## Specify
Definição do problema, requisitos e regras de negócio.

## Design
Definição da arquitetura e decisões técnicas.

## Tasks
Quebra da implementação em tarefas menores.

## Execute
Execução incremental pelos agentes de IA.

---

# 📁 Estrutura dos Artefatos

```text
/specs
   /jwt-authentication
      SPEC.md
      PLAN.md
      TASKS.md
      constitution.md
```

---

# 📄 SPEC.md

Responsável por definir:

- objetivos
- requisitos
- regras de negócio
- restrições
- critérios de aceitação

O SPEC.md define o **O QUÊ** precisa ser construído.

---

# 🏗️ PLAN.md

Responsável por definir:

- arquitetura
- stack tecnológico
- endpoints
- decisões técnicas
- serviços
- estratégias de segurança

O PLAN.md define o **COMO** o sistema será implementado.

---

# 📋 TASKS.md

Responsável por quebrar a implementação em tarefas:

- pequenas
- sequenciais
- verificáveis
- ideais para execução por IA

Exemplo:

```text
T1 - Configurar JWT
T2 - Criar JwtTokenService
T3 - Criar endpoint de login
T4 - Proteger endpoints
```

---

# 🛡️ constitution.md

Arquivo contendo as regras globais do projeto.

Exemplos:

- Nunca expor secrets
- Utilizar tipagem forte
- Seguir separação de responsabilidades
- Utilizar código assíncrono
- Não acessar banco diretamente dos controllers

---

# 🤖 Execução com Claude Code

Após a criação dos artefatos, utilizamos o Claude Code para implementar as tarefas incrementalmente.

Exemplo:

```text
Leia:
- SPEC.md
- PLAN.md
- constitution.md
- TASKS.md

Implemente apenas a T1.
Não implemente outras tarefas.
```

---

# ✅ Benefícios do SDD

- Redução de improviso da IA
- Maior previsibilidade
- Melhor controle arquitetural
- Menos regressões
- Melhor uso do contexto
- Desenvolvimento incremental
- Maior qualidade do código gerado

---

# 🔥 Conceitos Importantes

## Human-in-the-loop

A IA executa tarefas.

O desenvolvedor supervisiona, revisa e valida.

---

## Engenharia de Contexto

A qualidade da IA depende diretamente da qualidade do contexto fornecido.

---

## Vibe Coding vs Engenharia

SDD substitui desenvolvimento baseado em tentativa e erro por engenharia orientada por especificação.

---

# 🛠️ Tecnologias Utilizadas

- ASP.NET Core
- .NET
- JWT Authentication
- Entity Framework Core
- Claude Code
- Spec-Driven Development

---

# 🎯 Conclusão

Spec-Driven Development não é apenas uma técnica para usar IA.

É uma nova forma de estruturar engenharia de software na era dos agentes inteligentes.

Quando fornecemos:

- contexto
- arquitetura
- regras
- tarefas claras

A IA deixa de improvisar e passa a colaborar de forma muito mais previsível e profissional.

---

# 📌 Objetivo da Aula

Demonstrar na prática como utilizar:

- especificações
- arquitetura
- governança
- execução incremental

para transformar IA em uma ferramenta de engenharia de software real.

---
