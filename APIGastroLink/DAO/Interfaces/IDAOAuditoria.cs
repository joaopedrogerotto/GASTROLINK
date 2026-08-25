using APIGastroLink.Models;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAOAuditoria {
        public Task RegisterAudit(Auditoria Auditoria);
    }
}
