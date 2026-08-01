using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadePedido : IFacadePedido {
        private readonly PedidoClient _pedidoClient;
        private readonly CozinhaClient _cozinhaClient;
        private readonly GarcomClient _garcomClient;
        private readonly CaixaClient _caixaClient;

        public FacadePedido(PedidoClient pedidoClient, CozinhaClient cozinhaClient, GarcomClient garcomClient, CaixaClient caixaClient) {
            _pedidoClient = pedidoClient;
            _cozinhaClient = cozinhaClient;
            _garcomClient = garcomClient;
            _caixaClient = caixaClient;
        }

        public async Task<bool> AtualizarPedido(StatusPedidoUpdateDTO StatusPedidoUpdateDTO) => await _pedidoClient.AtualizarStatusPedido(StatusPedidoUpdateDTO);

        public async Task<bool> CadastrarPedido(PedidoCreateDTO pedido) => await _pedidoClient.CadastrarPedido(pedido);

        public async Task<List<Pedido>> PedidosCaixa() => await _caixaClient.SelecionaPedidosCaixa();
        public async Task<List<Pedido>> PedidosCozinhaPendente() => await _cozinhaClient.PedidosPendentes();

        public async Task<List<Pedido>> PedidosProntos() => await _garcomClient.TodosPedidosProntos();

        public async Task<Pedido> SelecionaPedidoPeloId(int id) => await _pedidoClient.ObterPedidoPorId(id);
    }
}
