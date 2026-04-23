using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeMesa : IFacadeMesa {
        private readonly IDAOMesa _daoMesa;

        public FacadeMesa(IDAOMesa daoMesa) {
            _daoMesa = daoMesa;
        }

        public void AtualizarLayoutMesas(List<Mesa> listMesa) => _daoMesa.UpdateLayout(listMesa);
        public void CadastrarMesa(string Numero) => _daoMesa.Insert(Numero);

        public List<Mesa> SelecionarTodasMesas() => _daoMesa.SelectAll();
    }
}
