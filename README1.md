# Lead Manager 

## 📌 Tecnologias Utilizadas

- **Backend**
  - [.NET 6](https://dotnet.microsoft.com/) - API RESTful
  - **Entity Framework Core** - ORM para acesso ao banco de dados
  - **SQL Server** (rodando via Docker)
  - **MediatR** (CQRS) - separação de comandos e queries (opcional/implementado conforme desafio)
  - **xUnit** - testes unitários

- **Frontend**
  - [React.js](https://reactjs.org/) - SPA (Single Page Application)
  - **Axios** - consumo da API
  - **React Router** - roteamento de abas
  - **Bootstrap / Font Awesome** - estilização e ícones

- **Infraestrutura**
  - **Docker & Docker Compose** - para orquestrar API, banco de dados e dependências
  - **Mail Service Fake** - simulação de envio de e-mails em arquivo/texto

---

## 🏗️ Arquitetura do Sistema

O sistema segue uma arquitetura em **camadas**, organizada da seguinte forma:

- **LeadManager.API**  
  Contém a Web API construída em ASP.NET Core, responsável por expor os endpoints RESTful.

- **LeadManager.Application**  
  Contém regras de negócio, handlers CQRS (via MediatR) e validações.

- **LeadManager.Domain**  
  Entidades do domínio, interfaces e contratos.

- **LeadManager.Infrastructure**  
  Implementação de persistência (Entity Framework Core, SQL Server) e serviços auxiliares.

- **LeadManager.WebApp**  
  Aplicação em React que consome a API e fornece a interface do usuário.

O fluxo segue:  
**Frontend (React) → API (.NET Core) → Infra (EF Core + SQL Server)**.  
Eventos como *Accept* e *Decline* são tratados pela API, persistidos no banco e notificados por serviço de e-mail falso.

---

## ⚙️ Pré-requisitos (Windows)

Antes de executar o sistema, é necessário instalar/configurar:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)  
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)  
- [Node.js 18+](https://nodejs.org/) (para rodar o frontend)  
- [Git](https://git-scm.com/) (para clonar o repositório)

Para executar os **testes unitários**:
- **xUnit** já está configurado no projeto, basta rodar o comando `dotnet test`.

---

## ▶️ Instruções de Execução

### 1. Clonar o repositório
```powershell
git clone https://github.com/LucasSanzio/LeadManager.git
cd LeadManager
```

### 2. Subir containers Docker (API + SQL Server)
```powershell
docker compose up -d
```

### 3. Aplicar migrações e criar banco
```powershell
cd src/LeadManager.API
dotnet ef database update
```

### 4. Executar a API
```powershell
$env:DOTNET_ENVIRONMENT="Development"
dotnet run --urls "http://localhost:5001"
```

### 5. Executar o Frontend
```powershell
cd src/LeadManager.WebApp
npm install
npm start
```

### 6. Rodar os testes
```powershell
dotnet test
```

---

## 🚀 Funcionalidades do Sistema

- **Tab Invited**  
  - Listagem de leads no status *Invited*  
  - Botão **Accept** → muda status para *Accepted*, aplica 10% desconto se preço > $500 e registra notificação de e-mail  
  - Botão **Decline** → muda status para *Declined*

- **Tab Accepted**  
  - Listagem de leads aceitos  
  - Exibe informações adicionais (telefone, e-mail, nome completo)

- **Banco de Dados SQL Server**  
  - Persistência de leads, histórico de ações (Accept/Decline)

- **Serviço de E-mail Fake**  
  - Simulação de envio de e-mail para `vendas@test.com`

- **Testes Unitários**  
  - Casos de teste para garantir integridade das regras de negócio

---

## 📂 Estrutura do Projeto

```
LeadManager/
 ├── src/
 │   ├── LeadManager.API/         # API .NET Core
 │   ├── LeadManager.Application/ # Camada de aplicação (CQRS/Serviços)
 │   ├── LeadManager.Domain/      # Entidades e contratos
 │   ├── LeadManager.Infrastructure/ # Persistência e serviços externos
 │   └── LeadManager.WebApp/    # SPA React
 ├── tests/
 │   └── LeadManager.Tests/       # Testes unitários
 ├── docker-compose.yml
 └── README.md
```



---

## 🔄 Resetar o Banco de Dados

Caso queira **dropar o banco de dados** e reiniciá-lo com os **leads iniciais**, siga os passos:

### 1. Dropar o banco existente
```powershell
cd src/LeadManager.API
dotnet ef database drop -f
```

### 2. Recriar o banco com as migrações aplicadas
```powershell
dotnet ef database update
```

Isso irá recriar todas as tabelas e popular novamente os dados iniciais de leads (seeding configurado no projeto).


## ✅ Conclusão

Este projeto implementa um sistema de **gerenciamento de leads** baseado em .NET 6 + React + SQL Server.  
A arquitetura modular facilita a manutenção, expansão e aplicação de boas práticas como **CQRS** e **DDD**.  

---
