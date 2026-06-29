using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Mapper;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadePrato : IFacadePrato {
        private readonly IDAOPrato _daoPrato;

        public FacadePrato(IDAOPrato daoPrato) {
            _daoPrato = daoPrato;
        }

        public void AtualizarDisponibilidade(PratoStatusUpdateDTO pratoStatusUpdateDTO) {
            var prato = PratoMapper.ToEntidade(pratoStatusUpdateDTO);
            _daoPrato.UpdateDisponibilidade(prato); 
        }

        public void CadastrarPrato(PratoCreateDTO pratoCreateDTO, string urlImagem) {
            var prato = PratoMapper.ToEntidade(pratoCreateDTO);

            prato.UrlImagem = urlImagem;

            _daoPrato.Insert(prato);
        }

        public async Task<List<Prato>> PesquisarPrato(FiltroPesquisaDTO filtroPesquisaDTO) => await _daoPrato.SelectWithFilters(filtroPesquisaDTO);

        public async Task<List<Prato>> SelcionarTodosPratos() => await _daoPrato.SelectAll();

        public async Task<Prato> SelecionarPratoPorId(int Id) => await _daoPrato.SelectById(Id);

    }
}
