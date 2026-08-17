using APIGastroLink.DTO;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAODashboard {
        public IndicadorDashboardDTO DashboardVendasCategoria(DashboardFiltroDTO DashboardFiltroDTO);
        public IndicadorDashboardDTO DashboardVendasFormaPagamento(DashboardFiltroDTO DashboardFiltroDTO);
    }
}
