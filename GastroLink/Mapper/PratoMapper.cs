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

        public static List<PratoChatbotDTO> ToListPratoChatbotDTO(List<Prato> listPrato) {
            var list = new List<PratoChatbotDTO>();
            foreach (var prato in listPrato) {
                list.Add(new PratoChatbotDTO {
                    Id = prato.Id,
                    Nome = prato.Nome,
                    Descricao = prato.Descricao,
                    Preco = prato.Preco
                });
            }

            return list;
        }
    }
}
