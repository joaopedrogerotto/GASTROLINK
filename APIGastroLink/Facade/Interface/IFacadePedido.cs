using APIGastroLink.DTO;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePedido {
        public Task CadastrarPedido(PedidoCreateDTO pedido);
    }
}
