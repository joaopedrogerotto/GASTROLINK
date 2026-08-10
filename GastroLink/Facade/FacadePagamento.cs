using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;

namespace GastroLink.Facade {
    public class FacadePagamento : IFacadePagamento {
        private readonly FormaPagamentoClient _formaPagamentoClient;
        private readonly PedidoClient _pedidoClient;
        private readonly PagamentoClient _pagamentoClient;

        public FacadePagamento(FormaPagamentoClient formaPagamentoClient, PedidoClient pedidoClient, PagamentoClient pagamentoClient) {
            _formaPagamentoClient = formaPagamentoClient;
            _pedidoClient = pedidoClient;
            _pagamentoClient = pagamentoClient;
        }

        public async Task<bool> EfetuarPagamento(RegistrarPagamentoDTO pagamentoRequest) {
            return await _pagamentoClient.RegistrarPagamento(pagamentoRequest);
        }

        public async Task<PixQrCodeResponseDTO> GerarQrCodePix(PagamentoPixDTO pagamentoRequest) => await _pagamentoClient.GerarQRCodePix(pagamentoRequest);

        public async Task<DadosPagamentoDTO> ObterDadosParaPagamento(int idPedido) {
            var pedido  = await _pedidoClient.ObterPedidoPorId(idPedido);
            var formasPagamento = await _formaPagamentoClient.ObterFormasPagamento();
            var dadosPagamento = new DadosPagamentoDTO {
                Pedido = pedido,
                FormasPagamento = formasPagamento
            };
            return dadosPagamento;
        }

        public async Task<int> VerificarStatusPagamentoQrCode(PedidoPixDTO pedidoPix) {
            return await _pagamentoClient.VerificarQrCode(pedidoPix);
        }
    }
}
