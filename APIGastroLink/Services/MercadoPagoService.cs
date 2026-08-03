using APIGastroLink.DTO;
using APIGastroLink.Services.Interfaces;
using APIGastroLink.Settings;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.Http.Headers;

namespace APIGastroLink.Services {
    public class MercadoPagoService : IMercadoPagoService {
        private readonly HttpClient _httpClient;
        private readonly MercadoPagoOptions _mercadoPagoOptions;

        public MercadoPagoService(HttpClient httpClient, IOptions<MercadoPagoOptions> mercadoPagoOptions) {
            _httpClient = httpClient;
            _mercadoPagoOptions = mercadoPagoOptions.Value;
        }


        public async Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoRequestDTO pagamentoRequestDTO) {
            Console.WriteLine(_mercadoPagoOptions.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _mercadoPagoOptions.AccessToken);

            var idempotencyKey = Guid.NewGuid().ToString();
            _httpClient.DefaultRequestHeaders.Remove("X-Idempotency-Key");
            _httpClient.DefaultRequestHeaders.Add("X-Idempotency-Key", idempotencyKey);

            var body = new {
                type = "online",
                total_amount = pagamentoRequestDTO.ValorPago.ToString("F2", CultureInfo.InvariantCulture),
                external_reference = pagamentoRequestDTO.IdPedido.ToString(),
                processing_mode = "automatic",
                transactions = new {
                    payments = new[] {
                new {
                    amount = pagamentoRequestDTO.ValorTotal.ToString("F2", CultureInfo.InvariantCulture),
                    payment_method = new {
                        id = "pix",
                        type = "bank_transfer"
                    }
                }
            }
                },
                payer = new {
                    email = _mercadoPagoOptions.TestBuyerEmail
                }
            };

            var response = await _httpClient.PostAsJsonAsync("v1/orders", body);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseBody);

            var resultado = await response.Content.ReadFromJsonAsync<MercadoPagoOrderResponseDTO>();

            var pagamento = resultado!.Transactions.Payments.First();

            return new PixQrCodeResponseDTO {
                CodigoPix = pagamento.PaymentMethod.QrCode,
                QrCodeBase64 = pagamento.PaymentMethod.QrCodeBase64
            };

        }
    }
}
