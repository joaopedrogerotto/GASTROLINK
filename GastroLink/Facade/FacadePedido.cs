using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadePedido : IFacadePedido {
        private readonly PedidoClient _pedidoClient;
        private readonly CozinhaClient _cozinhaClient;
        private readonly GarcomClient _garcomClient;

        public FacadePedido(PedidoClient pedidoClient, CozinhaClient cozinhaClient, GarcomClient garcomClient) {
            _pedidoClient = pedidoClient;
            _cozinhaClient = cozinhaClient;
            _garcomClient = garcomClient;
        }

        public async Task<bool> AtualizarPedido(StatusPedidoUpdateDTO StatusPedidoUpdateDTO) => await _pedidoClient.AtualizarStatusPedido(StatusPedidoUpdateDTO);

        public async Task<bool> CadastrarPedido(PedidoCreateDTO pedido) => await _pedidoClient.CadastrarPedido(pedido);

        public async Task<List<Pedido>> PedidosCozinhaPendente() => await _cozinhaClient.PedidosPendentes();

        public async Task<List<Pedido>> PedidosProntos() => await _garcomClient.TodosPedidosProntos();
    }
}
