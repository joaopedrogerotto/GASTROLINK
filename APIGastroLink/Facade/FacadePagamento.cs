using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Services.Interfaces;

namespace APIGastroLink.Facade {
    public class FacadePagamento : IFacadePagamento {
        private readonly IDAOPagamento _daoPagamento;
        private readonly IMercadoPagoService _mercadoPagoService;

        public FacadePagamento(IDAOPagamento daoPagamento, IMercadoPagoService mercadoPagoService) {
            _daoPagamento = daoPagamento;
            _mercadoPagoService = mercadoPagoService;
        }

        public async Task<PixQrCodeResponseDTO> GerarQRCodePix(PagamentoPixDTO pagamentoRequestDTO) => await _mercadoPagoService.GerarQRCodePix(pagamentoRequestDTO);

        public async Task<bool> RegistrarPagamento(RegistrarPagamentoDTO pagamentoRequestDTO) => await _daoPagamento.Insert(pagamentoRequestDTO);

        public async Task<bool> VerificarQrCode(PedidoPixDTO pedidoPixDTO) => await _mercadoPagoService.VerificarQrCode(pedidoPixDTO);
    }
}
