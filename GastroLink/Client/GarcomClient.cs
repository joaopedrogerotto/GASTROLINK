using GastroLink.Models;

namespace GastroLink.Client {
    public class GarcomClient {
        private HttpClient _httpClient;

        public GarcomClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Pedido>> TodosPedidosProntos() {
            var response = await _httpClient.GetAsync("Garcom");
            if (response.IsSuccessStatusCode) {
                var pedidos = await response.Content.ReadFromJsonAsync<List<Pedido>>();
                return pedidos ?? new List<Pedido>();
            }

            throw new InvalidOperationException($"Falha ao recuperar pedidos para o garçom: {(int)response.StatusCode} - {response.Content.ReadAsStringAsync()}");
        }
    }
}
