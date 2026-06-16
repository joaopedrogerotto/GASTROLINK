using GastroLink.DTO;

namespace GastroLink.Facade.Interface {
    public interface IFacadePrato {
        public Task<bool> CadastrarPrato(PratoCreateDTO pratoCreateDTO);
    }
}
