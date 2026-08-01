using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeFormaPagamento {
        public Task<List<FormaPagamento>> SelecionaTodos();
    }
}
