using GastroLink.Client;
using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Mapper;
using GastroLink.ViewModel;

namespace GastroLink.Facade {
    public class FacadeDashboard : IFacadeDashboard {
        private DashboardClient _dashboardClient;

        public FacadeDashboard(DashboardClient dashboardClient) {
            _dashboardClient = dashboardClient;
        }

        public async Task<ResumoVendaViewModel> SeleiconarPratoMaisVendidos() {
            var resumoVendasDto = await _dashboardClient.SelecionarResumoVenda();
            var resumoViewModel = ResumoVendasMapper.ToViewModel(resumoVendasDto);
            resumoViewModel.ResumoFaturamento.TicketMedio = TicketMedio(resumoViewModel);
            return resumoViewModel;
        }

        private decimal TicketMedio(ResumoVendaViewModel ResumoVendaViewModel) {
            return ResumoVendaViewModel.ResumoFaturamento.Faturamento / ResumoVendaViewModel.ResumoFaturamento.TotalVendidos; 
        }
    }
}
