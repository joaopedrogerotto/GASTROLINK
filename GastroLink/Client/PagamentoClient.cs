using GastroLink.DTO;

namespace GastroLink.Client {
    public class PagamentoClient {
        private readonly HttpClient _httpClient;

        public PagamentoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<bool> RegistrarPagamento(PagamentoRequestDTO pagamentoRequestDTO) {
            var response = await _httpClient.PostAsJsonAsync("Pagamento", pagamentoRequestDTO);
            return response.IsSuccessStatusCode;
        }
    }
}
