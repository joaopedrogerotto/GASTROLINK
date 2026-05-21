using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Mapper;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeUsuario : IFacadeUsuario {
        private readonly IDAOUsuario _daoUsuario;

        public FacadeUsuario(IDAOUsuario daoUsuario) {
            _daoUsuario = daoUsuario;
        }

        public void AtualizarUsuario(UsuarioUpdateDTO UsuarioUpdateDTO) {
            var usuario = UsuarioMapper.ToEntidade(UsuarioUpdateDTO);
            _daoUsuario.Update(usuario);
        }

        public void ExcluirUsuario(UsuarioDeleteDTO UsuarioDeleteDTO) {
            _daoUsuario.Delete(UsuarioMapper.ToEntidade(UsuarioDeleteDTO));
        }

        public void InserirUsuario(UsuarioCreateDTO UsuarioCreateDTO) {
            var Usuario = UsuarioMapper.ToEntidade(UsuarioCreateDTO);
            _daoUsuario.Insert(Usuario);
        }

        public List<Usuario> ObterTodosUsuarios() => _daoUsuario.SelectAll();
      
        public Usuario ObterUsuarioPeloId(int usuarioId) => _daoUsuario.SelectById(usuarioId);
    }
}
