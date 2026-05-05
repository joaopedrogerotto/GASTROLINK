using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOTipoUsuario {
        public List<TipoUsuario> SelectAll();
    }
}
