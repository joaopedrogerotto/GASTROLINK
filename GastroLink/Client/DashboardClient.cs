using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Client {
    public class DashboardClient {
        private HttpClient _httpClient;

        public DashboardClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<ResumoVendasDTO> SelecionarResumoVenda() {
            var response = await _httpClient.GetAsync("Dashboard");
            if (response.IsSuccessStatusCode) {
                var resumo = await response.Content.ReadFromJsonAsync<ResumoVendasDTO>();
                return resumo?? new ResumoVendasDTO();
            }
            throw new InvalidOperationException($"Falha ao recuperar os mais vendidos: {(int)response.StatusCode} - {response.Content.ReadAsStringAsync()}");
        }
    }
}
