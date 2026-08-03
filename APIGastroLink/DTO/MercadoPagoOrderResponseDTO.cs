using System.Text.Json.Serialization;

namespace APIGastroLink.DTO {
    public class MercadoPagoOrderResponseDTO {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("transactions")]
        public TransactionsDTO Transactions { get; set; }
    }
}
