using System.Text.Json.Serialization;

namespace APIGastroLink.DTO {
    public class TransactionsDTO {
        [JsonPropertyName("payments")]
        public List<PaymentDTO> Payments { get; set; }
    }
}
