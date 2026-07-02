using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Mapper;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadePrato : IFacadePrato {
        private readonly IDAOPrato _daoPrato;
        private readonly IDAOHistoricoDisponibilidade _daoHistoricoDisponibilidade;

        public FacadePrato(IDAOPrato daoPrato, IDAOHistoricoDisponibilidade daoHistoricoDisponibilidade) {
            _daoPrato = daoPrato;
            _daoHistoricoDisponibilidade = daoHistoricoDisponibilidade;
        }

        public void AtualizarDisponibilidade(PratoStatusUpdateDTO pratoStatusUpdateDTO) {
            var prato = PratoMapper.ToEntidade(pratoStatusUpdateDTO);
            var historico = new HistoricoDisponibilidade {
                Prato = new Prato { Id = pratoStatusUpdateDTO.Id },
                Disponivel = pratoStatusUpdateDTO.Status,
                Justificativa = pratoStatusUpdateDTO.Justificativa,
                Usuario = new Usuario { Id = pratoStatusUpdateDTO.IdUsuario }
            };

            _daoPrato.UpdateDisponibilidade(prato);
            _daoHistoricoDisponibilidade.Insert(historico);
        }

        public async Task AtualizarPrato(PratoEditarDTO pratoEditarDTO) => _daoPrato.UpdatePrato(pratoEditarDTO);

        public void CadastrarPrato(PratoCreateDTO pratoCreateDTO, string urlImagem) {
            var prato = PratoMapper.ToEntidade(pratoCreateDTO);

            prato.UrlImagem = urlImagem;

            _daoPrato.Insert(prato);
        }

        public async Task<List<Prato>> PesquisarPrato(FiltroPesquisaDTO filtroPesquisaDTO) => await _daoPrato.SelectWithFilters(filtroPesquisaDTO);

        public async Task<List<Prato>> SelcionarTodosPratos() => await _daoPrato.SelectAll();

        public async Task<Prato> SelecionarPratoPorId(int Id) {
            var prato = await _daoPrato.SelectById(Id);
            prato.HistoricoDisponibilidade = await _daoHistoricoDisponibilidade.SelectByIdPrato(Id);
            return prato;
        }

    }
}
