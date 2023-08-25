# Biblioteca

Api de gerenciamento de Biblioteca

## 🔋 Status

![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)

## ✅ Estrutura padrão

Foi seguida a estrutura padrão do estilo RestFul

- GET: lista ou consulta dados
- POST: criação de dados
- PUT: atualização de dados
- DELETE: remoção de dados

## 🛠️ Ferramentas Utilizadas

![AutoMapper](https://img.shields.io/badge/Biblioteca-AutoMapper-red)
![EF Core](https://img.shields.io/badge/Framework-EF%20Core-purple)
![Newtonsoft.Json](https://img.shields.io/badge/Biblioteca-Newtonsoft.Json-orange)
![MySQL](https://img.shields.io/badge/Banco%20de%20dados-MySQL-blue)
![C#](https://img.shields.io/badge/Linguagem-C%23-blue)
![.NET](https://img.shields.io/badge/Plataforma-.NET-blue)

## 📝 DESCRIÇÃO

API RESTfull que se comunica com o banco de dados MySQL com entidades de relacionamentos a partir de rotas que realizam CRUD

##### Entidades

- Autor
- Categoria
- Livro
- CategoriaLivro

##### Relacionamentos

| 1 : N             | N : N          |
| ----------------- | -------------- |
| Autor : Livro     | CategoriaLivro |
| Categoria : Livro |                |

### ℹ️ INFORMAÇÕES

- As entidades possuem camadas de DTOs e são mapeadas pela biblioteca AutoMapper
- As entidades possuem rotas em CRUD
  - POST, GET, GET {id}, PATCH, DELETE
- A prática de API RESTfull estão em todos os controller
- O projeto possui camadas de Repository aplicados no Controller

> **Rotas:** O projeto possui documentação de suas rotas pelo swegger
