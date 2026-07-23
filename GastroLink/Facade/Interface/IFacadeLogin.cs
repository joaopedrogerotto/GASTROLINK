using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeLogin {
        public Task<UsuarioLoginDTO> ValidarLogin(Login Login);
    }
}
