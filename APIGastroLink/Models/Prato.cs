namespace APIGastroLink.Models {
    public class Prato {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int TempoMedioPreparo { get; set; }
        public bool Disponibilidades { get; set; }
        public CategoriaPrato CategoriaPrato { get; set; }
        public string UrlImagem { get; set; }
        public List<HistoricoDisponibilidade> HistoricoDisponibilidade { get; set; }
    }
}
