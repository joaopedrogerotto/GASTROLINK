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
    }
}
