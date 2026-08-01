using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeFormaPagamento : IFacadeFormaPagamento {
        private readonly IDAOFormaPagamento _daoFormaPagamento;

        public FacadeFormaPagamento(IDAOFormaPagamento daoFormaPagamento) {
            _daoFormaPagamento = daoFormaPagamento;
        }

        public async Task<List<FormaPagamento>> SelecionaTodos() => await _daoFormaPagamento.SelectAll();
    }
}
