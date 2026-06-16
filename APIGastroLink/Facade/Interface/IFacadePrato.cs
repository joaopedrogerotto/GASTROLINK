using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePrato {
        public void CadastrarPrato(PratoCreateDTO pratoCreateDTO);
    }
}
