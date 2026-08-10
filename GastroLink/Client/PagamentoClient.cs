using GastroLink.DTO;

namespace GastroLink.Client {
    public class PagamentoClient {
        private readonly HttpClient _httpClient;

        public PagamentoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<bool> RegistrarPagamento(RegistrarPagamentoDTO pagamentoRequestDTO) {
            var response = await _httpClient.PostAsJsonAsync("Pagamento", pagamentoRequestDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoPixDTO pagamentoRequestDTO) {
            var response = await _httpClient.PostAsJsonAsync($"Pagamento/GerarQrCodePix", pagamentoRequestDTO);
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<PixQrCodeResponseDTO>();
            }
            throw new Exception("Erro ao gerar QR Code Pix");
        }

        public async Task<int> VerificarQrCode(PedidoPixDTO pedidoPixDTO) {
            var response = await _httpClient.PostAsJsonAsync($"Pagamento/VerificarQrCode", pedidoPixDTO);
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            throw new Exception("Erro ao verificar QR Code Pix");
        }
    }
}
