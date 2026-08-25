using APIGastroLink.Models;
using System.Security.Claims;

namespace APIGastroLink.Factory.Interfaces {
    public interface IUsuarioFactory {
        public Usuario CriarUsuarioLogado(ClaimsPrincipal claim);
    }
}
