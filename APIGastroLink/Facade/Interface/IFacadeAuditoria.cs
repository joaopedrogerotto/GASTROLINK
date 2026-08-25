using APIGastroLink.Enums;
using APIGastroLink.Models;
using System.Security.Claims;

namespace APIGastroLink.Facade.Interface {
    public interface IFacadeAuditoria {
        public Task RegistrarAuditoria(AcaoAuditoriaEnum acao, string descicao, ClaimsPrincipal claim);
    }
}
