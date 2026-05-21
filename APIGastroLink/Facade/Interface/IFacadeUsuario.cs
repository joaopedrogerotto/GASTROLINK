using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeUsuario {
        public void InserirUsuario(UsuarioCreateDTO Usuario);
        public List<Usuario> ObterTodosUsuarios();
        public Usuario ObterUsuarioPeloId(int usuarioId);
        public void AtualizarUsuario(UsuarioUpdateDTO UsuarioUpdateDTO);
        public void ExcluirUsuario(UsuarioDeleteDTO UsuarioDeleteDTO);
    }
}
