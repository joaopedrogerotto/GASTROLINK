using APIGastroLink.DTO;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOPagamento {
        public Task<bool> Insert(PagamentoRequestDTO PagamentoRequestDTO);
    }
}
