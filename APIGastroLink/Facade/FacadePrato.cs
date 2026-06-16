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

        public void CadastrarPrato(PratoCreateDTO pratoCreateDTO) => _daoPrato.Insert(PratoMapper.ToEntidade(pratoCreateDTO));

    }
}
