using APIGastroLink.DTO;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOPedido {
        public Task CadastrarPedido(PedidoCreateDTO pedido);
    }
}
