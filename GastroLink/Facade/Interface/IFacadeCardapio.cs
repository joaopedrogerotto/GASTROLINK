using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeCardapio {
        public Task<List<CategoriaPrato>> SelecionarCardapio();
    }
}
