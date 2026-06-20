using AutoMapper;
using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Mappings {
    public class UsuarioMapper {
        public static UsuarioCreateDTO ToCreateDTO (Usuario entidade) {
            if(entidade == null) {
                return null;
            }

            return new UsuarioCreateDTO {
                Nome = entidade.Nome,
                Login = entidade.Login,
                Password = entidade.Password,
                TipoUsuarioId = entidade.Tipo.Id
            };
        }
    }
}
