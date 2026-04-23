using GastroLink.Models;

namespace GastroLink.Facade.Interface {
    public interface IFacadeMesa {
        public Task<List<Mesa>> BuscarMesasMapeamento();
    }
}
