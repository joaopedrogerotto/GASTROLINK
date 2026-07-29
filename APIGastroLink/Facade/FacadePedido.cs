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

        public async Task AtualizarStatus(StatusPedidoUpdateDTO StatusPedidoUpdateDTO) => await _daoPedido.UpdateStatus(StatusPedidoUpdateDTO);

        public async Task<Pedido> CadastrarPedido(PedidoCreateDTO pedido) {
            pedido.ValorTotal = _pedidoService.CalcularValorTotalPedido(pedido.Itens);
            int idPedido = await _daoPedido.InsertPedido(pedido);
            return await _daoPedido.SelectPedidoById(idPedido);
        }

        public async Task<List<Pedido>> SelecionaPedidosProntos() => await _daoPedido.SelectAllPronto();

        public async Task<List<Pedido>> SelecionarPedidosCozinha() => await _daoPedido.SelectPedidosEmPreparo();

        public async Task<Pedido> SelecionarPeloId(int idPedido) => await _daoPedido.SelectPedidoById(idPedido);
    }
}
