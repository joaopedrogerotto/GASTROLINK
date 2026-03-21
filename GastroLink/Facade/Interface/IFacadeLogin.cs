using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeLogin {
        public Task<Usuario> ValidarLogin(Login Login);
    }
}
