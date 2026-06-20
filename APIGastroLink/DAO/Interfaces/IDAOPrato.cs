using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOPrato {
        public void Insert(Prato Prato);
        public Task<List<Prato>> SelectAll();
        public Task<Prato> SelectById(int id);
        public void UpdateDisponibilidade(Prato Prato);
    }
}
