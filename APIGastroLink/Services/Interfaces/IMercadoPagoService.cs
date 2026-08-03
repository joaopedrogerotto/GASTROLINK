using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Services.Interfaces {
    public interface IMercadoPagoService {
        Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoRequestDTO pagamentoRequestDTO);
        Task<bool> VerificarQrCode(PedidoPixDTO pedidoPixDTO);
    }
}
