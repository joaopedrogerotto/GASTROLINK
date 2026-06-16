using System.ComponentModel.DataAnnotations;

namespace GastroLink.DTO {
    public class PratoCreateDTO {
        [Required(ErrorMessage = "O nome do prato é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do prato é obrigatória.")]
        public string Descricao { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço do prato deve ser maior que zero.")]
        public decimal Preco { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "O tempo médio de preparo do prato deve ser maior que zero.")]
        public int TempoMedioPreparo { get; set; }
        [Required(ErrorMessage = "A categoria do prato é obrigatória.")]
        public int IdCategoriaPrato { get; set; }
    }
}
