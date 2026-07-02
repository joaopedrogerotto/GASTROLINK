namespace APIGastroLink.DTO {
    public class PratoStatusUpdateDTO {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int IdUsuario { get; set; }
        public string Justificativa { get; set; }
    }
}
