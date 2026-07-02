namespace GastroLink.Models {
    public class HistoricoDisponibilidade {
        public DateTime Data { get; set; }
        public string Justificativa { get; set; }
        public bool Disponivel { get; set; }
        public Usuario Usuario { get; set; }
        public Prato Prato { get; set; }
    }
}
