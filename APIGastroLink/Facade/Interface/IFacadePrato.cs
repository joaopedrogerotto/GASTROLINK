using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePrato {
        public void CadastrarPrato(PratoCreateDTO pratoCreateDTO, string urlImagem);
        public Task<List<Prato>> SelcionarTodosPratos();
        public Task<Prato> SelecionarPratoPorId(int Id);
    }
}
