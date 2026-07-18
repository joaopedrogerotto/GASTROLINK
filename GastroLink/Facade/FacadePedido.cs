using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadePedido : IFacadePedido {
        private readonly PedidoClient _pedidoClient;
        private readonly CozinhaClient _cozinhaClient;

        public FacadePedido(PedidoClient pedidoClient, CozinhaClient cozinhaClient) {
            _pedidoClient = pedidoClient;
            _cozinhaClient = cozinhaClient;
        }

        public async Task<bool> CadastrarPedido(PedidoCreateDTO pedido) => await _pedidoClient.CadastrarPedido(pedido);

        public async Task<List<Pedido>> PedidosCozinhaPendente() => await _cozinhaClient.PedidosPendentes();
    }
}
