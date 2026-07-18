using GastroLink.Client;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadeTipoUsuario : IFacadeTipoUsuario {
        private readonly TipoUsuarioClient _tipoUsuarioClient;

        public FacadeTipoUsuario(TipoUsuarioClient tipoUsuarioClient) {
            _tipoUsuarioClient = tipoUsuarioClient;
        }
        public async Task<List<TipoUsuario>> ObterTodosTiposUsuario() {
            return await _tipoUsuarioClient.SelecionarTodosTipoUsuario();
        }
    }
}
