using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Mapper {
    public class PratoMapper {
        public static PratoEditarDTO ToPratoEditarDTO(Prato prato) {
            return new PratoEditarDTO {
                Id = prato.Id,
                Nome = prato.Nome,
                Descricao = prato.Descricao,
                Preco = prato.Preco,
                TempoMedioPreparo = prato.TempoMedioPreparo,
                IdCategoriaPrato = prato.CategoriaPrato.Id,
                UrlImagem = prato.UrlImagem
            };
        }
    }
}
