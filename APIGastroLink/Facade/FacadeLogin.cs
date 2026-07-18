using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using APIGastroLink.Services;

namespace APIGastroLink.Facade {
    public class FacadeLogin : IFacadeLogin {
        private readonly IDAOLogin _daoLogin;
        private readonly PasswordService _passwordService;
        public FacadeLogin(IDAOLogin daoLogin, PasswordService passwordService) {
            _daoLogin = daoLogin;
            _passwordService = passwordService;
        }

        public Usuario ValidarLogin(Login Login) {
            var usuario = _daoLogin.Autenticar(Login);
            if (usuario == null) {
                return null;
            }
            if (!_passwordService.VerifyPassword(Login.Senha, usuario.Password)) {
                return null;
            }
            return usuario;
        }
    }
}
