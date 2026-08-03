using APIGastroLink.DTO;
using APIGastroLink.Services.Interfaces;
using APIGastroLink.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace APIGastroLink.Services {
    public class MercadoPagoService : IMercadoPagoService, IPedidoPixService {
        private readonly HttpClient _httpClient;
        private readonly MercadoPagoOptions _mercadoPagoOptions;
        private readonly IDatabase _redisDatabase;

        public MercadoPagoService(HttpClient httpClient, IOptions<MercadoPagoOptions> mercadoPagoOptions, IConnectionMultiplexer redis) {
            _httpClient = httpClient;
            _mercadoPagoOptions = mercadoPagoOptions.Value;
            _redisDatabase = redis.GetDatabase();
        }


        public async Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoRequestDTO pagamentoRequestDTO) {
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
            
            var pedidoPix = new PedidoPixDTO {
                IdPedido = pagamentoRequestDTO.IdPedido,
                IdOrderMercadoPago = resultado!.Id,
                ValorPago = pagamentoRequestDTO.ValorPago
            };

            if (!await SalvarPedidoPix(pedidoPix)) {
                throw new Exception("Erro ao salvar pedido PIX");
            }

            return new PixQrCodeResponseDTO {
                IdOrderMercadoPago = resultado!.Id,
                CodigoPix = pagamento.PaymentMethod.QrCode,
                QrCodeBase64 = pagamento.PaymentMethod.QrCodeBase64
            };

        }

        public async Task<bool> VerificarQrCode(PedidoPixDTO pedidoPixDTO) {
            await _redisDatabase.KeyDeleteAsync($"pedido_pix:{pedidoPixDTO.IdPedido}-{pedidoPixDTO.IdOrderMercadoPago}");
            return true;//Para fins didaticos e de testes ele sempre retorna true, mas poderia ser implementado uma verificação real do status do pagamento no MercadoPago
        }

        public async Task<bool> SalvarPedidoPix(PedidoPixDTO pedidoPix) {
            try {
                var jsonPedidoPix = JsonSerializer.Serialize(pedidoPix);

                await _redisDatabase.StringSetAsync($"pedido_pix:{pedidoPix.IdPedido}-{pedidoPix.IdOrderMercadoPago}", jsonPedidoPix);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}
