using GastroLink.DTO;

namespace GastroLink.Client {
    public class CategoriaPratoClient {
        private HttpClient _httpClient;

        public CategoriaPratoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<CategoriaPratoQuantidadeDTO>> CategoriasQuantidadePratos() {
            var response = await _httpClient.GetAsync("CategoriaPrato/QuantidadePratos");
            if(response.IsSuccessStatusCode) {
                var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaPratoQuantidadeDTO>>();
                return categorias ?? new List<CategoriaPratoQuantidadeDTO>();
            }
            throw new InvalidOperationException("Falha ao recuperar categorias e suas contagens de pratos.");
        }
    }
}
