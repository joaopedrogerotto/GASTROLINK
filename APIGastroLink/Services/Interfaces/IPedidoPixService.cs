using APIGastroLink.DTO;

namespace APIGastroLink.Services.Interfaces {
    public interface IPedidoPixService {
        public Task<bool> SalvarPedidoPix(PedidoPixDTO pedidoPix);
    }
}
