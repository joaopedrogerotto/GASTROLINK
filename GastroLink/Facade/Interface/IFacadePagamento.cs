using GastroLink.DTO;

namespace GastroLink.Facade.Interface {
    public interface IFacadePagamento {
        public Task<DadosPagamentoDTO> ObterDadosParaPagamento(int idPedido);
        public Task<bool> EfetuarPagamento(PagamentoRequestDTO pagamentoRequest);
        public Task<PixQrCodeResponseDTO> GerarQrCodePix(PagamentoRequestDTO pagamentoRequest);
    }
}
