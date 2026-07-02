using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOHistoricoDisponibilidade {
        public void Insert(HistoricoDisponibilidade historicoDisponibilidade);
        public Task<List<HistoricoDisponibilidade>> SelectByIdPrato(int idPrato);
    }
}
