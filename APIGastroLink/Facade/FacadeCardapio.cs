using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeCardapio : IFacadeCardapio {
        private readonly IDAOCategoriaPrato _daoCategoriaPrato;
        public FacadeCardapio(IDAOCategoriaPrato daoCategoriaPrato) {
            _daoCategoriaPrato = daoCategoriaPrato;
        }
        public async Task<List<CategoriaPrato>> SelecionarCardapio() => await _daoCategoriaPrato.SelectCardapio();
    }
}
