# Lead Management System

Um sistema completo de gerenciamento de leads desenvolvido como parte do desafio Full Stack .NET. O sistema permite visualizar, aceitar e recusar leads através de uma interface web moderna e responsiva.

## 🏗️ Arquitetura

Este projeto implementa as melhores práticas de arquitetura de software:

- **Domain-Driven Design (DDD)**: Estrutura em camadas bem definidas
- **CQRS (Command Query Responsibility Segregation)**: Separação clara entre operações de leitura e escrita
- **MediatR**: Implementação do padrão Mediator para desacoplamento
- **Clean Architecture**: Dependências apontando para o domínio
- **Repository Pattern**: Abstração da camada de dados

## 🚀 Tecnologias Utilizadas

### Backend
- **.NET 6.0**: Framework principal
- **Entity Framework Core**: ORM para acesso a dados
- **SQL Server**: Banco de dados (container Docker)
- **MediatR**: Implementação de CQRS
- **Swagger**: Documentação da API
- **xUnit + FluentAssertions + Moq**: Testes unitários

### Frontend
- **Angular**: Framework SPA
- **TypeScript**: Linguagem principal
- **RxJS**: Programação reativa
- **CSS3**: Estilização responsiva

## 📁 Estrutura do Projeto

```
LeadManagementChallenge/
├── src/
│   ├── LeadManager.Domain/          # Entidades e regras de negócio
│   ├── LeadManager.Application/     # Casos de uso (Commands/Queries)
│   ├── LeadManager.Infrastructure/  # Implementações (EF, Repositórios)
│   ├── LeadManager.API/            # Controllers e configuração da API
│   └── LeadManager.WebApp/         # Aplicação Angular
├── tests/
│   └── LeadManager.Tests/          # Testes unitários
└── README.md
```

## ⚙️ Pré-requisitos

- **.NET SDK 6.0** ou superior
- **Node.js 18+** e **npm**
- **Angular CLI** (`npm install -g @angular/cli`)
- **Git** (opcional, para controle de versão)
- **Docker** (para o banco de dados local)

## 🛠️ Instalação e Execução

### 1. Clone o Repositório
```bash
git clone <url-do-repositorio>
cd LeadManagementChallenge
```

### 2. Configurar e Executar o Backend

```bash
# Subir o SQL Server local (primeiro terminal na raiz do projeto)
docker compose up -d sqlserver

# Navegar para o projeto da API
cd src/LeadManager.API

# Restaurar dependências
dotnet restore

# Aplicar as migrações no SQL Server
dotnet ef database update

# Executar a API apontando para o SQL Server
$env:DOTNET_ENVIRONMENT="Development"
dotnet run --urls "http://localhost:5001"
```

A API estará disponível em: `http://localhost:5001`
Documentação Swagger: `http://localhost:5001/swagger`

Precisa encerrar o container? Use `docker compose down`.

### 3. Configurar e Executar o Frontend

```bash
# Em um novo terminal, navegar para o projeto Angular
cd src/LeadManager.WebApp

# Instalar dependências
npm install

# Executar o servidor de desenvolvimento
ng serve --open
```

A aplicação web estará disponível em: `http://localhost:4200`

### 4. Executar Testes (Opcional)

```bash
# Na raiz do projeto
dotnet test
```

## 🎯 Funcionalidades

### ✅ Implementadas

1. **Visualização de Leads Convidados**
   - Lista todos os leads com status "Invited"
   - Exibe informações básicas: nome, categoria, localização, preço, descrição
   - Interface responsiva com estados de loading e erro

2. **Ações de Aceitar/Recusar Leads**
   - Botões "Accept" e "Decline" em cada lead
   - Aplicação automática de desconto de 10% para leads > $500
   - Notificação por arquivo de texto (`notifications.log`)
   - Feedback visual imediato

3. **Cadastro de Novos Leads**
   - Formulário validado para registrar leads direto do painel
   - Armazenamento automático no SQL Server via API
   - Feedback visual de sucesso e tratamento de erros

4. **Visualização de Leads Aceitos**
   - Aba separada para leads aceitos
   - Exibe informações completas incluindo contato
   - Navegação fluida entre abas

5. **Sistema de Notificação**
   - Implementação via arquivo de texto
   - Registro de todas as ações de aceitar leads
   - Formato estruturado com timestamp

### 🏗️ Arquitetura Técnica

- **API RESTful** com endpoints bem definidos
- **Padrão CQRS** com separação de Commands e Queries
- **Injeção de Dependência** configurada adequadamente
- **Testes Unitários** abrangentes (95%+ cobertura)
- **Tratamento de Erros** robusto
- **CORS** configurado para desenvolvimento

## 📋 Endpoints da API

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/leads/invited` | Lista leads convidados |
| GET | `/api/leads/accepted` | Lista leads aceitos |
| POST | `/api/leads` | Cadastra um novo lead |
| PUT | `/api/leads/{id}/accept` | Aceita um lead |
| PUT | `/api/leads/{id}/decline` | Recusa um lead |

## 🧪 Testes

O projeto inclui testes unitários abrangentes:

- **Testes de Domínio**: Validação das regras de negócio
- **Testes de Application**: Verificação dos handlers
- **Testes de Infrastructure**: Validação dos serviços
- **Mocks e Stubs**: Isolamento de dependências

```bash
# Executar todos os testes
dotnet test

# Executar com relatório de cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 🎨 Interface do Usuário

- **Design Responsivo**: Funciona em desktop e mobile
- **Estados Visuais**: Loading, erro, vazio
- **Navegação Intuitiva**: Abas claras e bem definidas
- **Feedback Imediato**: Ações refletidas instantaneamente
- **Acessibilidade**: Seguindo boas práticas de UX

## 🔧 Configuração Avançada

### Banco de Dados
O projeto utiliza SQL Server 2022 em Docker.

1. Inicie o container com `docker compose up -d sqlserver`.
2. Garanta que a string de conexão em `appsettings.json` aponte para `Server=localhost,1433;Database=LeadManagerDb;User Id=sa;Password=SenhaForte123!;`.
3. Aplique as migrações com `dotnet ef database update` a partir de `src/LeadManager.API`.
4. Opcionalmente, execute `database/seed.sql` com o `sqlcmd` para popular dados adicionais:
   ```bash
   sqlcmd -S localhost,1433 -U sa -P "SenhaForte123!" -i database/seed.sql
   ```

As migrações em `src/LeadManager.Infrastructure/Migrations` estão ajustadas para SQL Server.

### Notificações
Para implementar notificação por e-mail real:

1. Crie uma implementação de `INotificationService`
2. Configure SMTP settings
3. Registre no DI container

## 🚀 Deploy

### Backend
```bash
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
ng build --prod
```

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 👨‍💻 Desenvolvedor

Desenvolvido como parte do desafio técnico Full Stack .NET.

---

**Nota**: Este projeto demonstra a implementação de um sistema completo seguindo as melhores práticas de desenvolvimento de software, incluindo arquitetura limpa, testes abrangentes e interface moderna.

