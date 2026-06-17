using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadePrato {
        public Task<bool> CadastrarPrato(PratoCreateDTO pratoCreateDTO);
        public Task<List<Prato>> SelecionarTodosPratos();
    }
}
