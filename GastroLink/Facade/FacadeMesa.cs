using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadeMesa : IFacadeMesa {
        private readonly MesaClient _mesaClient;

        public FacadeMesa(MesaClient mesaClient) {
            _mesaClient = mesaClient;
        }

        public async Task<List<Mesa>> BuscarMesasMapeamento() {
            return await _mesaClient.SelecionarMesasMapeamento();
        }

        public async Task<bool> CadastrarMesa(MesaRequestDTO mesaRequestDto) => await _mesaClient.SalvarMesa(mesaRequestDto);

        public async Task<bool> SalvarLayoutMesa(List<LayoutMesaDTO> listLayoutMesa) => await _mesaClient.SalvarLayoutMesas(listLayoutMesa);
    }
}
