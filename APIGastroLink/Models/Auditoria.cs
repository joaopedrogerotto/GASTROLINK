namespace APIGastroLink.Models {
    public class Auditoria {
        public string Acao { get; set; }
        public string Descricao { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataHora { get; set; }
    }
}
