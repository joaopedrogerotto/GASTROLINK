<h1 align="center">🍽️ GastroLink</h1>

O GastroLink é um sistema de gestão de restaurantes desenvolvido para automatizar e organizar o fluxo de pedidos em tempo real, permitindo a comunicação entre atendimento, cozinha e caixa.

Inicialmente, o sistema será uma aplicação Web MVC, com possibilidade de expansão futura para uma aplicação mobile.

## 🛠️ Tecnologias
### Backend
- ASP.NET Core MVC (.NET 10.0)
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
- **Google Gemini** para processamento de linguagem natural, permitindo que o chatbot compreenda as solicitações dos clientes e interaja de forma contextualizada.
- **Chatbot de atendimento** para recomendação de pratos, gerenciamento do carrinho e auxílio na finalização dos pedidos

## 🤖 Chatbot - Gastrobot

O GastroLink possui um chatbot, o Gastrobot, que é integrado ao Google Gemini, desenvolvido para atuar como um assistente virtual de atendimento do restaurante.

Entre suas funcionalidades estão:

- Recomendação de pratos com base nas preferências do cliente;
- Consulta ao cardápio disponível;
- Adição de pratos ao carrinho por meio de linguagem natural;
- Gerenciamento do carrinho durante a conversa;
- Identificação do número da mesa;
- Confirmação dos itens antes da finalização do pedido;
- Envio do pedido confirmado para o fluxo normal do restaurante.

O chatbot utiliza o histórico da conversa para manter o contexto da interação, permitindo que o cliente faça solicitações de forma natural, sem precisar seguir comandos rígidos.


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