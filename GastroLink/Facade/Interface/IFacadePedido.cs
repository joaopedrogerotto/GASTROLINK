using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadePedido {
        public Task<bool> CadastrarPedido(PedidoCreateDTO pedido);
        public Task<List<Pedido>> PedidosCozinhaPendente();
    }
}
