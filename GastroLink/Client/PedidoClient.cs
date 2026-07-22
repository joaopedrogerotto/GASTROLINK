using GastroLink.DTO;

namespace GastroLink.Client {
    public class PedidoClient {
        private HttpClient _httpClient;

        public PedidoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<bool> CadastrarPedido(PedidoCreateDTO pedido) {
            var response = await _httpClient.PostAsJsonAsync("Pedido", pedido);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarStatusPedido (StatusPedidoUpdateDTO StatusPedidoUpdateDTO) {
            var response = await _httpClient.PutAsJsonAsync("Pedido", StatusPedidoUpdateDTO);
            return response.IsSuccessStatusCode;
        }
    }
}
