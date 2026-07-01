namespace APIGastroLink.DTO {
    public class PratoEditarDTO {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int TempoMedioPreparo { get; set; }
        public int IdCategoriaPrato { get; set; }
        public IFormFile? formFile { get; set; }
        public string UrlImagem { get; set; }
    }
}
