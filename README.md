# 🧾 SIEGFiscal

Sistema de gestão fiscal desenvolvido em **.NET 8** com arquitetura **DDD (Domain-Driven Design)**, utilizando **Entity Framework Core** para persistência, **SQL Server**, **MongoDB** (para event store, ainda não implementado) e comunicação extensível com mensageria (RabbitMQ).

---

## 🚀 Como rodar a aplicação

### 🧰 Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)

### 📦 Restaurar dependências
```bash
dotnet restore
```

### 🗃️ Aplicar migrações do Entity Framework
Execute no diretório raiz do projeto:

```bash
dotnet ef database update --project SIEGFiscal.Infrastructure --startup-project SIEGFiscal
```

> Se quiser gerar novamente as migrações:
> ```bash
> dotnet ef migrations add InitialCreate --project SIEGFiscal.Infrastructure --startup-project SIEGFiscal
> ```

### ▶️ Rodar a aplicação
```bash
dotnet run --project SIEGFiscal
```

A API estará disponível em:
```
https://localhost:7080/swagger
```

---

## 🧪 Executar os testes automatizados

```bash
dotnet test
```

Os testes unitários utilizam:
- **xUnit** para orquestração dos testes;
- **FluentAssertions** para asserts legíveis;
- **Moq** para simulação de dependências (repositórios e serviços).

---

## 🏗️ Decisões de Arquitetura e Modelagem

### 🔹 Padrão DDD (Domain-Driven Design)
A solução foi modelada em **camadas independentes**, respeitando os princípios de **alta coesão e baixo acoplamento**:

| Camada | Responsabilidade |
|--------|------------------|
| **Domain** | Entidades, Value Objects e Interfaces de Repositório. Não depende de nada externo. |
| **Application** | Casos de uso e regras de negócio de aplicação. Depende apenas da camada de domínio. |
| **Infrastructure** | Implementação de repositórios (EF Core, MongoDB, etc.) e configuração do banco. |
| **API** | Exposição dos endpoints REST e integração com o restante do sistema. |

### 🔹 Banco de Dados e ORM
- Utiliza **Entity Framework Core 8.0** com SQL Server.
- Mapeamento fluente em `AppDbContext`.
- Suporte a **migrações automáticas** via CLI.
- Campos decimais (`TotalValue`) configurados com precisão via `HasPrecision(18,2)` para evitar truncamentos.

### 🔹 Comunicação e Extensões
- Suporte à mensageria via **RabbitMQ** (através de `RabbitMQ.Client`).
- Resiliência configurável via **Polly**.
- Integração com **MongoDB** para armazenamento de logs e eventos.

### 🔹 API RESTful
A camada de API segue boas práticas:
- Endpoints bem definidos e sem verbos (`/api/fiscaldocument`).
- Métodos HTTP corretos (`GET`, `PUT`, `DELETE`).
- Paginação, filtros e ordenação aplicados via query string.
- Versionamento e Swagger habilitado por padrão.

---

## 🔐 Tratamento de Dados Sensíveis

Os dados sensíveis (como **CNPJ**, **conexão com banco**, **segredos de autenticação**) foram tratados da seguinte forma:

- 🔸 **Configurações isoladas** em `appsettings.json` e `appsettings.Development.json`.
- 🔸 **Segredos locais** armazenados via [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets?view=aspnetcore-8.0):
  ```bash
  dotnet user-secrets init
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=SIEGFiscal;User Id=sa;Password=StrongPassword123;"
  ```

---

## 💡 Possíveis Melhorias Futuras

Se houvesse mais tempo, poderíamos evoluir o projeto com:

| Categoria | Melhoria |
|------------|-----------|
| **Performance** | Implementar cache distribuído (Redis) para consultas de documentos fiscais. |
| **Escalabilidade** | Orquestrar os microsserviços com **Kubernetes** e usar **Kafka** para mensageria. |
| **Segurança** | Adicionar autenticação e autorização via **JWT** e roles de acesso. |
| **Observabilidade** | Adicionar **Serilog** com sinks para ElasticSearch e monitoramento no Kibana. |
| **Testes** | Cobertura completa de testes de integração com `WebApplicationFactory`. |
| **Infraestrutura** | Docker Compose com SQL Server + Mongo + RabbitMQ. |
| **Frontend** | Criar dashboard Angular para acompanhar status e relatórios fiscais. |

---

## 📁 Estrutura do Projeto

```
SIEGFiscal
├── SIEGFiscal.API                → Endpoints REST + Swagger + Configuração
├── SIEGFiscal.Application        → Casos de uso, DTOs e Services
├── SIEGFiscal.Domain             → Entidades, Interfaces e Regras de Domínio
├── SIEGFiscal.Infrastructure     → Repositórios e Contexto EF Core
└── SIEGFiscal.Tests              → Testes unitários e de integração
```

---

## 👨‍💻 Autor

**Raul Araújo**  
Desenvolvedor .NET | Arquitetura DDD | Fintechs e Sistemas Fiscais  
📧 [raulluiz_araujo_12@hotmail.com](mailto:raulluiz_araujo_12@hotmail.com)

---

🛠️ *“Código limpo é aquele que expressa claramente a intenção do desenvolvedor.” — Robert C. Martin*
