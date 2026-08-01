using GastroLink.Models;

namespace GastroLink.Client {
    public class CardapioClient {
        private HttpClient _httpClient;

        public CardapioClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<CategoriaPrato>> SelecionarCardapio() {
            var response = await _httpClient.GetAsync("Cardapio");
            if (response.IsSuccessStatusCode) {
                var cardapio = await response.Content.ReadFromJsonAsync<List<CategoriaPrato>>();
                return cardapio ?? new List<CategoriaPrato>();
            }
            throw new InvalidOperationException($"Falha ao recuperar cardapio: {(int)response.StatusCode} - {response.Content.ReadAsStringAsync()}");
        }
    }
}
