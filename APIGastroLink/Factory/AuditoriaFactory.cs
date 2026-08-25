using APIGastroLink.Enums;
using APIGastroLink.Factory.Interfaces;
using APIGastroLink.Models;

namespace APIGastroLink.Factory {
    public class AuditoriaFactory : IAuditoriaFactory {
        public Auditoria Criar(AcaoAuditoriaEnum acao, string descricao, Usuario usuario) {
            return new Auditoria {
                Acao = acao.ToString(),
                Descricao = descricao,
                Usuario = usuario,
                DataHora = DateTime.Now
            };
        }
    }
}
