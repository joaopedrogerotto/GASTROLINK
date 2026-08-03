using System.Text.Json.Serialization;

namespace APIGastroLink.DTO {
    public class PaymentDTO {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethodDTO PaymentMethod { get; set; }

    }
}
