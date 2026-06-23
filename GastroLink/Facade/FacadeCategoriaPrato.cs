using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadeCategoriaPrato : IFacadeCategoriaPrato {
        private readonly CategoriaPratoClient _client;
        public FacadeCategoriaPrato(CategoriaPratoClient client) {
            _client = client;
        }

        public async Task<bool> SalvarCategoria(CategoriaPrato CategoriaPrato) => await _client.SalvarCategoriaPrato(CategoriaPrato);

        public async Task<List<CategoriaPrato>> SelecionarCategorias() => await _client.SelecionarCategorias();

        public async Task<List<CategoriaPratoQuantidadeDTO>> SelecionarCategoriasComQuantiadadePratos() => await _client.CategoriasQuantidadePratos();
    }
}
