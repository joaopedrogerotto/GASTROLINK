using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeMesa {
        public Task<List<Mesa>> BuscarMesasMapeamento();
        public Task<bool> CadastrarMesa(MesaRequestDTO mesaRequestDto);
        public Task<bool> SalvarLayoutMesa(List<LayoutMesaDTO> listLayoutMesa);

    }
}
