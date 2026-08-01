using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOFormaPagamento {
        public Task<List<FormaPagamento>>SelectAll();
    }
}
