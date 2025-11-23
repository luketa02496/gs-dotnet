## Link publico do deploy:
https://gs-dotnet-x0ec.onrender.com/

## Autenticacao no Postman:
antes de tentar acessar os endpoints é preciso fazer a autenticacao

Basic Auth
Username: lucas@workwell.com
senha: 654321
# 📘 WorkWell API --- Plataforma de Bem-Estar no Trabalho

API RESTful desenvolvida em **.NET 8**, utilizando **Dapper** para
acesso ao banco **Oracle**.\
O sistema fornece CRUD de usuários e avaliações de bem-estar (humor,
estresse, produtividade).

------------------------------------------------------------------------

## 📌 Tecnologias Utilizadas

-   **.NET 8 Web API**
-   **Dapper**
-   **Oracle Database**
-   **Dependency Injection**
-   **RESTful Best Practices**
-   **Paginação**
-   **Retornos HATEOAS (links de navegação)**

------------------------------------------------------------------------

## 📁 Estrutura do Projeto

    WorkWell.Api/
    │── Controllers/
    │     ├── UseresController.cs
    │     └── AssessmentsController.cs
    │
    │── Models/
    │     ├── Useres.cs
    │     └── Assessment.cs
    │
    │── Repositories/
    │     ├── IUseresRepository.cs
    │     ├── IAssessmentRepository.cs
    │     ├── UserRepository.cs
    │     └── AssessmentRepository.cs
    │
    │── Program.cs
    │── appsettings.json

------------------------------------------------------------------------

## 🗄️ Estrutura do Banco de Dados (Oracle)

### **Tabela USERES**

``` sql
CREATE TABLE useres (
    id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL,
    idade NUMBER(3)
);
```

### **Tabela ASSESSMENT**

``` sql
CREATE TABLE assessment (
    id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    humor NUMBER(1) NOT NULL,
    estresse NUMBER(1) NOT NULL,
    produtividade NUMBER(1) NOT NULL,
    useres_id NUMBER NOT NULL REFERENCES useres(id)
);
```

------------------------------------------------------------------------

## 🔌 Dependência de Conexão (appsettings.json)

``` json
{
  "ConnectionStrings": {
    "OracleDb": "User Id=SEU_USER;Password=SUA_SENHA;Data Source=SEU_HOST:1521/SEU_SERVICE"
  }
}
```

------------------------------------------------------------------------

## 🚀 Endpoints da API

### 🟦 **Users**

#### **GET /api/v1/useres?page=1&pageSize=10**

Retorna todos os usuários com paginação.

#### **GET /api/v1/useres/{id}**

Retorna um usuário específico.

#### **POST /api/v1/useres**

Cria um usuário.

**Exemplo:**

``` json
{
  "nome": "Lucas",
  "idade": 25
}
```

#### **DELETE /api/v1/useres/{id}**

Remove um usuário.

------------------------------------------------------------------------

### 🟧 **Assessments**

#### **GET /api/v1/assessments/user/{userId}**

Lista todas as avaliações de um usuário.

#### **GET /api/v1/assessments/{id}**

Retorna uma avaliação específica.

#### **POST /api/v1/assessments**

Cria uma avaliação.

**Exemplo:**

``` json
{
  "humor": 4,
  "estresse": 2,
  "produtividade": 5,
  "useresId": 1
}
```

#### **DELETE /api/v1/assessments/{id}**

Exclui uma avaliação.

------------------------------------------------------------------------

## 📦 Paginação (Users)

A API retorna:

``` json
{
  "data": [],
  "pagination": {
    "page": 1,
    "pageSize": 10,
    "totalItems": 45,
    "totalPages": 5
  },
  "links": {
    "self": "...",
    "next": "...",
    "prev": null
  }
}
```

------------------------------------------------------------------------

## 🧩 Funcionamento Interno

### ✔ Repositories com Dapper

Toda a comunicação com Oracle usa Dapper e SQL puro.

### ✔ Injeção de Dependência

Registrada em `Program.cs`:

``` csharp
builder.Services.AddScoped<IUseresRepository, UserRepository>();
builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
```

------------------------------------------------------------------------

## ▶ Como Rodar o Projeto

### 1️⃣ Restaurar dependências

    dotnet restore

### 2️⃣ Rodar o projeto

    dotnet run

### 3️⃣ Testar no Postman usando os endpoints acima.

------------------------------------------------------------------------

## 🧪 Testes Sugeridos

-   Criar usuário\
-   Buscar usuário específico\
-   Criar assessment\
-   Buscar assessments de um usuário\
-   Excluir usuário\
-   Validar erros (404, 500 etc.)

------------------------------------------------------------------------

## 🏁 Conclusão

Este projeto implementa uma API RESTful simples, limpa e objetiva
utilizando .NET + Oracle, seguindo boas práticas de arquitetura,
organização e uso eficiente do Dapper.




