using GastroLink.DTO;
using GastroLink.ViewModel;

namespace GastroLink.Mapper {
    public class ResumoVendasMapper {
        public static ResumoVendaViewModel ToViewModel(ResumoVendasDTO ResumoVendasDTO) {
            return new ResumoVendaViewModel {
                VendasPrato = ResumoVendasDTO.VendasPratos,
                ResumoFaturamento = ResumoVendasDTO.ResumoFaturamento,
            };
        }
    }
}
