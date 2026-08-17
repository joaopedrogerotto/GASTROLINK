using GastroLink.DTO;
using GastroLink.ViewModel;

namespace GastroLink.Facade.Interface {
    public interface IFacadeDashboard {
        public Task<ResumoVendaViewModel> SeleiconarPratoMaisVendidos();
    }
}
