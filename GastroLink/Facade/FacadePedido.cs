using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;

namespace GastroLink.Facade {
    public class FacadePedido : IFacadePedido {
        private readonly PedidoClient _client;

        public FacadePedido(PedidoClient client) {
            _client = client;
        }

        public async Task<bool> CadastrarPedido(PedidoCreateDTO pedido) => await _client.CadastrarPedido(pedido);
    }
}
