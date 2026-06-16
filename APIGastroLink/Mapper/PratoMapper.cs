using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Mapper {
    public class PratoMapper {
        public static Prato ToEntidade (PratoCreateDTO pratoCreateDTO) {
            return new Prato {
                Nome = pratoCreateDTO.Nome,
                Descricao = pratoCreateDTO.Descricao,
                Preco = pratoCreateDTO.Preco,
                TempoMedioPreparo = pratoCreateDTO.TempoMedioPreparo,
                CategoriaPrato = new CategoriaPrato { Id = pratoCreateDTO.IdCategoriaPrato }
            };
        }
    }
}
