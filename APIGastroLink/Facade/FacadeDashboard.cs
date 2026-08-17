using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;

namespace APIGastroLink.Facade {
    public class FacadeDashboard : IFacadeDashboard {
        private readonly IDAODashboard _daoDashboard;

        public FacadeDashboard(IDAODashboard daoDashboard) {
            _daoDashboard = daoDashboard;
        }
        public IndicadorDashboardDTO GerarIndicadores(DashboardFiltroDTO DashboardFiltroDTO) {
            return DashboardFiltroDTO.Indicador switch {
                "vendas-categoria" => _daoDashboard.DashboardVendasCategoria(DashboardFiltroDTO),
                "vendas-forma-pagamento" => _daoDashboard.DashboardVendasFormaPagamento(DashboardFiltroDTO)
            };
        }
    }
}
