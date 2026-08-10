using APIGastroLink.DTO;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePagamento {
        public Task<bool> RegistrarPagamento(RegistrarPagamentoDTO pagamentoRequestDTO);
        public Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoPixDTO pagamentoRequestDTO);
        public Task<bool> VerificarQrCode(PedidoPixDTO pedidoPixDTO);
    }
}
