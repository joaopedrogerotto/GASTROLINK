using GastroLink.Client;
using GastroLink.Facade.Interface;
using GastroLink.Mappings;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadeUsuario : IFacadeUsuario {
        private readonly UsuarioClient _usuarioClient;

        public FacadeUsuario(UsuarioClient usuarioClient) {
            _usuarioClient = usuarioClient;
        }

        public async Task<bool> CadastrarUsuario(Usuario Usuario) {
            var usuarioCreateDTO = UsuarioMapper.ToCreateDTO(Usuario);

            return await _usuarioClient.CadastrarUsuario(usuarioCreateDTO);
        }

        public async Task<List<Usuario>> ObterTodosUsuarios() {
            return await _usuarioClient.ObterTodosUsuarios();
        }

        public async Task<Usuario> ObterUsuarioId(int idUsuario) => await _usuarioClient.ObterUsuarioPeloId(idUsuario);
    }
}
