using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.ViewModel {
    public class EditarPratoViewModel {
        public PratoEditarDTO Prato { get; set; } = new PratoEditarDTO();
        public List<CategoriaPrato> ListCategorias { get; set; } = new List<CategoriaPrato>();
    }
}
