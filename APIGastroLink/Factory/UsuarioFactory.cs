using APIGastroLink.Factory.Interfaces;
using APIGastroLink.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIGastroLink.Factory {
    public class UsuarioFactory : IUsuarioFactory {
        public Usuario CriarUsuarioLogado(ClaimsPrincipal claim) {
            return new Usuario {
                Id = int.Parse(claim.FindFirstValue(ClaimTypes.NameIdentifier)),
                Login = claim.FindFirstValue(ClaimTypes.Name)
            };
        }
    }
}
