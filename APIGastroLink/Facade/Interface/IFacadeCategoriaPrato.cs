using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeCategoriaPrato {
        public void CadastrarCategoriaPrato(CategoriaPrato categoriaPrato);
        public List<CategoriaPrato> SelecionarTodasCategorias();
        public List<CategoriaPratoDTO> SelecionarCategoriaQuantidadePratos();
    }
}
