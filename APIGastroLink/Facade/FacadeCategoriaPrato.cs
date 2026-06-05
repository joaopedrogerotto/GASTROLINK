using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeCategoriaPrato : IFacadeCategoriaPrato {
        private readonly IDAOCategoriaPrato _dao;

        public FacadeCategoriaPrato(IDAOCategoriaPrato dao) {
            _dao = dao;
        }

        public void CadastrarCategoriaPrato(CategoriaPrato categoriaPrato) => _dao.Insert(categoriaPrato);
    }
}
