using GastroLink.Client;
using GastroLink.Facade.Interface;
using GastroLink.Models;

namespace GastroLink.Facade {
    public class FacadeCardapio : IFacadeCardapio {
        private readonly CardapioClient _client;
        public FacadeCardapio(CardapioClient client) {
            _client = client;
        }
        public async Task<List<CategoriaPrato>> SelecionarCardapio() => await _client.SelecionarCardapio();
    }
}
