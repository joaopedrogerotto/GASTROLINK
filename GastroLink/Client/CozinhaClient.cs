using GastroLink.Models;

namespace GastroLink.Client {
    public class CozinhaClient {
        private HttpClient _httpClient;

        public CozinhaClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Pedido>> PedidosPendentes() {
            var response = await _httpClient.GetAsync("Cozinha/Pedidos");
            if (response.IsSuccessStatusCode) {
                var pedidos = await response.Content.ReadFromJsonAsync<List<Pedido>>();
                return pedidos ?? new List<Pedido>();
            }
            throw new InvalidOperationException($"Falha ao recuperar pedidos para a cozinha: {(int)response.StatusCode} - {response.Content.ReadAsStringAsync()}");
        }
    }
}
