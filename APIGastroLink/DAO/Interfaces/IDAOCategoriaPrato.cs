using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOCategoriaPrato {
        public void Insert(CategoriaPrato categoriaPrato);
        public List<CategoriaPrato> SelectAll();
        public List<CategoriaPratoDTO> SelectAllDTOQuantidadePratos();
        public Task<List<CategoriaPrato>> SelectCardapio();
    }
}
