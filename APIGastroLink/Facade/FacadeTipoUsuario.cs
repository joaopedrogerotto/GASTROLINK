using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;

namespace APIGastroLink.Facade {
    public class FacadeTipoUsuario : IFacadeTipoUsuario {
        private readonly IDAOTipoUsuario _daoTipoUsuario;

        public FacadeTipoUsuario(IDAOTipoUsuario daoTipoUsuario) {
            _daoTipoUsuario = daoTipoUsuario;
        }

        public List<TipoUsuario> SelectAll() {
            return _daoTipoUsuario.SelectAll();
        }
    }
}
