using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;

namespace GastroLink.Facade {
    public class FacadePrato : IFacadePrato {
        public readonly PratoClient _pratoClient;

        public FacadePrato(PratoClient pratoClient) {
            _pratoClient = pratoClient;
        }

        public async Task<bool> CadastrarPrato(PratoCreateDTO pratoCreateDTO) {
            ValidarPrato(pratoCreateDTO);
            return await _pratoClient.CadastrarPrato(pratoCreateDTO);
        }

        private static void ValidarPrato(PratoCreateDTO pratoCreateDTO) {
            if (string.IsNullOrEmpty(pratoCreateDTO.Nome)) {
                throw new ArgumentException("O nome do prato é obrigatório.");
            }
            if (pratoCreateDTO.Preco <= 0) {
                throw new ArgumentException("O preço do prato deve ser maior que zero.");
            }
            if (pratoCreateDTO.TempoMedioPreparo <= 0) {
                throw new ArgumentException("O tempo médio de preparo do prato deve ser maior que zero.");
            }
        }
    }
}
