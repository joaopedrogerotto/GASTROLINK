using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeUsuario {
        public Task<bool> CadastrarUsuario(Usuario Usuario);
        public Task<List<Usuario>> ObterTodosUsuarios();
        public Task<Usuario> ObterUsuarioId(int idUsuario);
        public Task<bool> AtualizarStatusUsuario(UsuarioStatusUpdateDTO UsuarioStatusUpdateDTO);
    }
}
