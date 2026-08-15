using APIGastroLink.DTO;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeDashboard {
        public IndicadorDashboardDTO GerarIndicadores(DashboardFiltroDTO DashboardFiltroDTO);
    }
}
