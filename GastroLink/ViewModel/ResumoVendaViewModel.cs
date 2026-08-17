using GastroLink.DTO;

namespace GastroLink.ViewModel {
    public class ResumoVendaViewModel {
        public List<VendasPratosDTO> VendasPrato { get; set; } = new List<VendasPratosDTO>();
        public ResumoFaturamentoDTO ResumoFaturamento { get; set; }
    }
}
