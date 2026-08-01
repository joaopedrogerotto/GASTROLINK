using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOPedido {
        public Task<int> InsertPedido(PedidoCreateDTO pedido);
        public Task<Pedido> SelectPedidoById(int idPedido);
        public Task<List<Pedido>> SelectPedidosEmPreparo();
        public Task<List<Pedido>> SelectAllPronto();
        public Task<List<Pedido>> SelectAllCaixa();
        public Task UpdateStatus(StatusPedidoUpdateDTO StatusPedidoUpdateDTO);
    }
}
