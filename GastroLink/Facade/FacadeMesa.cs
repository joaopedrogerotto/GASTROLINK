using GastroLink.Client;
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
    }
}
