<h1 align="center">🍽️ GastroLink</h1>

O GastroLink é um sistema de gestão de restaurantes desenvolvido para automatizar e organizar o fluxo de pedidos em tempo real, permitindo a comunicação entre atendimento, cozinha e caixa.

Inicialmente, o sistema será uma aplicação Web MVC, com possibilidade de expansão futura para uma aplicação mobile.

## 🛠️ Tecnologias
### Backend
- ASP.NET Core MVC(.NET 9)
- ADO.NET
- SignalR

### Frontend
- HTML 5
- CSS3
- Bootstrap 5
- TypeScript

### Banco de Dados
- SQL Server 2022
- Redis (Cache)


## 📐 Padrões de Projeto
- Facade
- DAO (Data Access Object)
- Service Layer
- DTO (Data Transfer Object)
- Mapper
  
## ⚙️ Recursos Utilizados

- **SignalR** para comunicação em tempo real entre atendimento, cozinha e caixa.
- **Redis** para cache e armazenamento temporário dos pedidos até a confirmação do mesmo, melhorando o desempenho e reduzindo acessos ao banco de dados.


## 📋 Principais Funcionalidades

- Autenticação e controle de usuários;
- Gestão de mesas e pedidos;
- Painel da cozinha em tempo real;
- Gestão de pagamentos;
- Relatórios de vendas;
- Controle de disponibilidade dos pratos.


## 📚 Documentação
- [📄 Documento de Requisitos](./Documentação/REQUISITOS%20GASTROLINK.pdf)


## 📄 Licença
Este projeto foi desenvolvido para fins de estudo e aprendizado.