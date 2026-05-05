using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeTipoUsuario {
        public Task<List<TipoUsuario>> ObterTodosTiposUsuario();
    }
}
