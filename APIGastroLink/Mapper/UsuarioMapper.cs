using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Mapper {
    public class UsuarioMapper {
        public static Usuario ToEntidade(UsuarioCreateDTO entidade) {
            if (entidade == null) {
                return null;
            }

            return new Usuario {
                Nome = entidade.Nome,
                Login = entidade.Login,
                Password = entidade.Password,
                Tipo = new TipoUsuario { Id = entidade.TipoUsuarioId },
                Status = true
            };
        }

        public static Usuario ToEntidade(UsuarioUpdateDTO entidade) {
            if (entidade == null) {
                return null;
            }
            return new Usuario {
                Id = entidade.Id,
                Nome = entidade.Nome,
                Login = entidade.Login,
                Tipo = new TipoUsuario { Id = entidade.TipoUsuarioId },
            };
        }
    }
}
