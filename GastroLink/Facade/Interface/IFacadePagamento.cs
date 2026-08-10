using GastroLink.DTO;

namespace GastroLink.Facade.Interface {
    public interface IFacadePagamento {
        public Task<DadosPagamentoDTO> ObterDadosParaPagamento(int idPedido);
        public Task<bool> EfetuarPagamento(RegistrarPagamentoDTO pagamentoRequest);
        public Task<PixQrCodeResponseDTO> GerarQrCodePix(PagamentoPixDTO pagamentoRequest);
        public Task<int> VerificarStatusPagamentoQrCode(PedidoPixDTO pedidoPix);
    }
}
