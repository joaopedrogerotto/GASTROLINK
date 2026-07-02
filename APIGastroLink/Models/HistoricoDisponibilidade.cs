namespace APIGastroLink.Models {
    public class HistoricoDisponibilidade {
        public Prato Prato { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }
        public string Justificativa { get; set; }
        public bool Disponivel { get; set;}
    }
}
