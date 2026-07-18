namespace GastroLink.DTO {
    public class FiltroPesquisaDTO {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public int? IdCategoria { get; set; }
        public bool? Disponibilidade { get; set; }
    }
}
