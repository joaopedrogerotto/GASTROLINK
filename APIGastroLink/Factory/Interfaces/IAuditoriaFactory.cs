using APIGastroLink.Enums;
using APIGastroLink.Models;

namespace APIGastroLink.Factory.Interfaces {
    public interface IAuditoriaFactory {
        public Auditoria Criar(AcaoAuditoriaEnum acao, string descricao, Usuario usuario);
    }
}
