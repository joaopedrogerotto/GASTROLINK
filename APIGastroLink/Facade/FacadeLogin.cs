using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeLogin : IFacadeLogin {
        private readonly IDAOLogin _daoLogin;
        public FacadeLogin(IDAOLogin daoLogin) {
            _daoLogin = daoLogin;
        }

        public Usuario ValidarLogin(Login Login) => _daoLogin.Autenticar(Login);
    }
}
