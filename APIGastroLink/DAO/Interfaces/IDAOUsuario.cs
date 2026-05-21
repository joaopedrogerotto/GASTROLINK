using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOUsuario {
        public void Insert(Usuario Usuario);
        public List<Usuario> SelectAll();
        public Usuario SelectById(int idUsuario);
        public void Update(Usuario Usuario);
    }
}
