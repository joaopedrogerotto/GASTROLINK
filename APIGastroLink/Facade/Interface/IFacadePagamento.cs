using APIGastroLink.DTO;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadePagamento {
        public Task<bool> RegistrarPagamento(PagamentoRequestDTO pagamentoRequestDTO);
        public Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoRequestDTO pagamentoRequestDTO);
    }
}
