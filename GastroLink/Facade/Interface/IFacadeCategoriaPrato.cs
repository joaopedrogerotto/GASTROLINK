using GastroLink.DTO;

namespace GastroLink.Facade.Interface {
    public interface IFacadeCategoriaPrato {
        public Task<List<CategoriaPratoQuantidadeDTO>> SelecionarCategoriasComQuantiadadePratos();
    }
}
