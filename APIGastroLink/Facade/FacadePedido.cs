using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;

namespace APIGastroLink.Facade {
    public class FacadePedido : IFacadePedido {
        private readonly IPedidoService _pedidoService;
        private readonly IDAOPedido _daoPedido;

        public FacadePedido(IPedidoService pedidoService, IDAOPedido daoPedido) {
            _pedidoService = pedidoService;
            _daoPedido = daoPedido;
        }

        public async Task<Pedido> CadastrarPedido(PedidoCreateDTO pedido) {
            pedido.ValorTotal = _pedidoService.CalcularValorTotalPedido(pedido.Itens);
            int idPedido = await _daoPedido.CadastrarPedido(pedido);
            return await _daoPedido.SelecionarPedidoPorId(idPedido);
        }
    }
}
