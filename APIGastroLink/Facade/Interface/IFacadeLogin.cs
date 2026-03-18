using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeLogin {
        public Usuario ValidarLogin(Login Login);
    }
}
