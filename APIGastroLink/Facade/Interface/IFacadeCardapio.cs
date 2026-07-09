using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeCardapio {
        public Task<List<CategoriaPrato>> SelecionarCardapio();
    }
}
