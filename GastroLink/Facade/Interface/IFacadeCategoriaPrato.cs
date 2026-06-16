using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeCategoriaPrato {
        public Task<List<CategoriaPratoQuantidadeDTO>> SelecionarCategoriasComQuantiadadePratos();
        public Task<List<CategoriaPrato>> SelecionarCategorias();
    }
}
