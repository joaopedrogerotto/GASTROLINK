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

        public async Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoRequestDTO pagamentoRequestDTO) {
            var response = await _httpClient.PostAsJsonAsync($"Pagamento/GerarQrCodePix", pagamentoRequestDTO);
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<PixQrCodeResponseDTO>();
            }
            throw new Exception("Erro ao gerar QR Code Pix");
        }
    }
}
