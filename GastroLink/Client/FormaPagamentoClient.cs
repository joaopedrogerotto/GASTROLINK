using GastroLink.Models;

namespace GastroLink.Client {
    public class FormaPagamentoClient {
        private readonly HttpClient _httpClient;

        public FormaPagamentoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<FormaPagamento>> ObterFormasPagamento() {
            var response = await _httpClient.GetAsync("FormaPagamento");
            if (response.IsSuccessStatusCode) {
                var formasPagamento = await response.Content.ReadFromJsonAsync<List<FormaPagamento>>();
                return formasPagamento ?? new List<FormaPagamento>();
            }
            throw new InvalidOperationException($"Falha ao recuperar formas de pagamento: {(int)response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }
}
