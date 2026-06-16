using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.ViewModel {
    public class CadastroPratoViewModel {
        public PratoCreateDTO Prato { get; set; } = new PratoCreateDTO();
        public List<CategoriaPrato> ListCategorias { get; set; } = new List<CategoriaPrato>();
    }
}
