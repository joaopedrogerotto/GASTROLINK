using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;
using GastroLink.Service;

namespace GastroLink.Facade {
    public class FacadeLogin : IFacadeLogin {
        private readonly LoginClient _loginCliente;

        public FacadeLogin(LoginClient loginCliente) {
            _loginCliente = loginCliente;
        }

        public async Task<UsuarioLoginDTO> ValidarLogin(Login Login) {
            return await _loginCliente.Login(Login);
        }
    }
}
