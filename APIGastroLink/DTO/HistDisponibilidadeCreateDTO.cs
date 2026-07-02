namespace APIGastroLink.DTO {
    public class HistDisponibilidadeCreateDTO {
        public int IdPrato { get; set; }
        public int IdUsuario { get; set; }
        public string Justificativa { get; set; }
        public bool Disponivel { get; set; }

    }
}
