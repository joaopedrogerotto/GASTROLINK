using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOPedido {
        public Task<int> CadastrarPedido(PedidoCreateDTO pedido);
        public Task<Pedido> SelecionarPedidoPorId(int idPedido);
    }
}
