using GastroLink.Models;

namespace GastroLink.Client {
    public class MesaClient {
        private HttpClient _HttpClient;

        public MesaClient(HttpClient httpClient) {
            _HttpClient = httpClient;
        }

        public async Task<List<Mesa>?> SelecionarMesasMapeamento() {
            var response = await _HttpClient.GetAsync("Mesa");

            if (!response.IsSuccessStatusCode) {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<Mesa>>();
        }
    }
}
