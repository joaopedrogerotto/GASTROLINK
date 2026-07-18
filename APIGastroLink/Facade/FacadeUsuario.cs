using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Mapper;
using APIGastroLink.Models;
using APIGastroLink.Services;

namespace APIGastroLink.Facade {
    public class FacadeUsuario : IFacadeUsuario {
        private readonly IDAOUsuario _daoUsuario;
        private readonly PasswordService _passwordService;

        public FacadeUsuario(IDAOUsuario daoUsuario, PasswordService passwordService) {
            _daoUsuario = daoUsuario;
            _passwordService = passwordService;
        }

        public void AtualizarUsuario(UsuarioUpdateDTO UsuarioUpdateDTO) {
            var usuario = UsuarioMapper.ToEntidade(UsuarioUpdateDTO);
            _daoUsuario.Update(usuario);
        }

        public void AlterarStatusUsuario(UsuarioStatusUpdateDTO UsuarioStatusUpdateDTO) {
            _daoUsuario.UpdateStatus(UsuarioMapper.ToEntidade(UsuarioStatusUpdateDTO));
        }

        public void InserirUsuario(UsuarioCreateDTO UsuarioCreateDTO) {
            var Usuario = UsuarioMapper.ToEntidade(UsuarioCreateDTO);
            Usuario.Password = _passwordService.HashPassword(Usuario.Password);
            _daoUsuario.Insert(Usuario);
        }

        public Usuario ObterUsuarioPeloId(int usuarioId) => _daoUsuario.SelectById(usuarioId);

        public List<Usuario> ObterTodosUsuarios() => _daoUsuario.SelectAll();
    }
}
