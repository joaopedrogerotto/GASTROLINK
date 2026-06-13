using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;

namespace GastroLink.Facade {
    public class FacadeCategoriaPrato : IFacadeCategoriaPrato {
        private readonly CategoriaPratoClient _client;
        public FacadeCategoriaPrato(CategoriaPratoClient client) {
            _client = client;
        }
        public async Task<List<CategoriaPratoQuantidadeDTO>> SelecionarCategoriasComQuantiadadePratos() => await _client.CategoriasQuantidadePratos();
    }
}
