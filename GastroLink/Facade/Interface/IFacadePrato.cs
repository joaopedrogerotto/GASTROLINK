using GastroLink.DTO;
using GastroLink.Mapper;
using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadePrato {
        public Task<bool> CadastrarPrato(PratoCreateDTO pratoCreateDTO);
        public Task<List<Prato>> SelecionarTodosPratos();
        public Task<Prato> BuscarPratoPorId(int id);
        public Task<bool> AtualizarDisponibilidade(PratoStatusUpdateDTO pratoStatusUpdateDTO);
        public Task<List<Prato>> SelecionarPratosPesquisa(FiltroPesquisaDTO filtroPesquisaDTO);
    }
}
