using System.Text.Json.Serialization;

namespace APIGastroLink.DTO {
    public class PaymentMethodDTO {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("ticket_url")]
        public string TicketUrl { get; set; }

        [JsonPropertyName("qr_code")]
        public string QrCode { get; set; }

        [JsonPropertyName("qr_code_base64")]
        public string QrCodeBase64 { get; set; }
    }
}