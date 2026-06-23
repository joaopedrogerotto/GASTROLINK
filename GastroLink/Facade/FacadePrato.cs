using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Mapper;
using GastroLink.Models;

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

        public async Task<List<Prato>> SelecionarTodosPratos() => await _pratoClient.TodosPratos();

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

        public Task<Prato> BuscarPratoPorId(int id) => _pratoClient.BuscarPratoPorId(id);

        public async Task<bool> AtualizarDisponibilidade(PratoStatusUpdateDTO pratoStatusUpdateDTO) => await _pratoClient.AtualizarDisponibilidade(pratoStatusUpdateDTO);

        public Task<bool> SalvarLayoutMesa(List<LayoutMesaDTO> listLayoutMesa) {
            throw new NotImplementedException();
        }
    }
}
