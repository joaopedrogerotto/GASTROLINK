using APIGastroLink.DTO;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAODashboard {
        public IndicadorDashboardDTO DashboardVendasCategoria(DashboardFiltroDTO DashboardFiltroDTO);
    }
}
