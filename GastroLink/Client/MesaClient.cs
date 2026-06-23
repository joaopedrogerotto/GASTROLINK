using GastroLink.DTO;
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

        public async Task<bool> SalvarMesa(MesaRequestDTO mesaRequestDTO) {
            var response = await _HttpClient.PostAsJsonAsync("Mesa/SalvarMesa", mesaRequestDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SalvarLayoutMesas (List<LayoutMesaDTO> listLayout) {
            var response = await _HttpClient.PostAsJsonAsync("Mesa/SalvarLayout", listLayout);
            return response.IsSuccessStatusCode;
        }
    }
}
