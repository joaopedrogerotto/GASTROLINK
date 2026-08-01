using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadePedido {
        public Task<bool> CadastrarPedido(PedidoCreateDTO pedido);
        public Task<List<Pedido>> PedidosCozinhaPendente();
        public Task<bool> AtualizarPedido(StatusPedidoUpdateDTO StatusPedidoUpdateDTO);
        public Task<Pedido> SelecionaPedidoPeloId(int id);
        public Task<List<Pedido>> PedidosProntos();
        public Task<List<Pedido>> PedidosCaixa();
    }
}
