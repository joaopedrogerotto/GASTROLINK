using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeMesa {
        public List<Mesa> SelecionarTodasMesas();
        public void CadastrarMesa(string Numero);
        public void AtualizarLayoutMesas(List<Mesa> listMesa);
    }
}
