using APIGastroLink.DTO;
using APIGastroLink.Services.Interfaces;

namespace APIGastroLink.Services {
    public class PedidoService : IPedidoService {
        public decimal CalcularValorTotalPedido(List<ItemPedidoCreateDTO> itens) => itens.Sum(item => item.Quantidade * item.Preco);
    }
}
