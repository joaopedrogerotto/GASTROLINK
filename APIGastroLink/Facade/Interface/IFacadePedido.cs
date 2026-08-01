using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePedido {
        public Task<Pedido> CadastrarPedido(PedidoCreateDTO pedido);
        public Task<List<Pedido>> SelecionarPedidosCozinha();
        public Task AtualizarStatus(StatusPedidoUpdateDTO StatusPedidoUpdateDTO);
        public Task<List<Pedido>> SelecionaPedidosProntos();
        public Task<List<Pedido>> SelecionaPedidosCaixa();
        public Task<Pedido> SelecionarPeloId(int idPedido);
    }
}
