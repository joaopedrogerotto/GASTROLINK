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

        public void InserirUsuario(UsuarioCreateDTO UsuarioCreateDTO) {
            var Usuario = UsuarioMapper.ToEntidade(UsuarioCreateDTO);
            _daoUsuario.Insert(Usuario);
        }

        public List<Usuario> ObterTodosUsuarios() {
            return _daoUsuario.SelectAll();
        }
    }
}
