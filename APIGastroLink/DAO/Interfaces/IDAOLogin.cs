using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOLogin {
        public Usuario Autenticar(Login Login);
    }
}
