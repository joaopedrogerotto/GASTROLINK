using APIGastroLink.Models;

namespace APIGastroLink.DTO {
    public class PratoCreateDTO {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int TempoMedioPreparo { get; set; }
        public int IdCategoriaPrato { get; set; }
    }
}
