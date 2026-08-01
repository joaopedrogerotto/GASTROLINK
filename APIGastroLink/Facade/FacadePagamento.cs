using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;

namespace APIGastroLink.Facade {
    public class FacadePagamento : IFacadePagamento {
        private readonly IDAOPagamento _daoPagamento;

        public FacadePagamento(IDAOPagamento daoPagamento) {
            _daoPagamento = daoPagamento;
        }

        public async Task<bool> RegistrarPagamento(PagamentoRequestDTO pagamentoRequestDTO) => await _daoPagamento.Insert(pagamentoRequestDTO);
       
    }
}
