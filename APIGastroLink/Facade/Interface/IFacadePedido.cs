using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePedido {
        public Task<Pedido> CadastrarPedido(PedidoCreateDTO pedido);
        public Task<List<Pedido>> SelecionarPedidosCozinha();
        public Task AtualizarStatus(StatusPedidoUpdateDTO StatusPedidoUpdateDTO);
    }
}
