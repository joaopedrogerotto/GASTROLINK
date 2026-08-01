using GastroLink.DTO;
using GastroLink.Models;

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

        public async Task<Pedido> ObterPedidoPorId(int id) {
            var response = await _httpClient.GetAsync($"Pedido/{id}");

            if (response.IsSuccessStatusCode) {
                var pedido = await response.Content.ReadFromJsonAsync<Pedido>();
                return pedido ?? new Pedido();
            }

            throw new InvalidOperationException($"Falha ao buscar pedido pelo id {id}: {(int)response.StatusCode} - {response.Content.ReadAsStringAsync()}");
        }
    }
}
