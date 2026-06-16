using GastroLink.DTO;

namespace GastroLink.Client {
    public class PratoClient {
        private HttpClient _httpClient;

        public PratoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<bool> CadastrarPrato(PratoCreateDTO pratoCreateDTO) {
            var response = await _httpClient.PostAsJsonAsync("Prato", pratoCreateDTO);
            if(response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }
    }
}
