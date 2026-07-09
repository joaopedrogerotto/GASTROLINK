using APIGastroLink.DTO;

namespace APIGastroLink.Services.Interfaces {
    public interface IPedidoService {
        public decimal CalcularValorTotalPedido(List<ItemPedidoCreateDTO> itens);
    }
}
