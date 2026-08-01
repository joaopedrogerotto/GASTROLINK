using GastroLink.Models;

namespace GastroLink.Client {
    public class CaixaClient {
        private readonly HttpClient _httpClient;

        public CaixaClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Pedido>> SelecionaPedidosCaixa() {
            var response = await _httpClient.GetAsync("Caixa");
            if (response.IsSuccessStatusCode) {
                var pedidos = await response.Content.ReadFromJsonAsync<List<Pedido>>();
                return pedidos ?? new List<Pedido>();
            }
            throw new InvalidOperationException($"Falha ao recuperar pedidos para o caixa: {(int)response.StatusCode} - {response.Content.ReadAsStringAsync()}");
        }
    }
}
