using GastroLink.DTO;

namespace GastroLink.Facade.Interface {
    public interface IFacadePedido {
        public Task<bool> CadastrarPedido(PedidoCreateDTO pedido);
    }
}
