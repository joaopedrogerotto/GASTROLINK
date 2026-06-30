namespace APIGastroLink.DTO {
    public class FiltroPesquisaDTO {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public int? IdCategoria { get; set; }
        
        public bool PossuiFiltro() {
            return GetType().GetProperties().All(p => p.GetValue(this) == null);
        }
    }
}
