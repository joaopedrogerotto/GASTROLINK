using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Mapper {
    public class PedidoMapper {
        public static PedidoCreateDTO RascunhoToPedidoCreateDTO(RascunhoPedido rascunho) {
            return new PedidoCreateDTO {
                IdMesa = rascunho.MesaId,
                Itens = rascunho.Itens.Select(item => new ItemPedidoCreateDTO {
                    IdPrato = item.Prato.Id,
                    Quantidade = item.Quantidade,
                    Observacao = item.Observacao,
                    Preco = item.Preco
                }).ToList()
            };
        }

        public static PedidoCreateDTO PedidoChatbotToPedicoCreate(PedidoCreateChatbotDTO PedidoCreateChatbotDTO) {
            return new PedidoCreateDTO {
                Itens = PedidoCreateChatbotDTO.itens
            };
        }
    }
}
