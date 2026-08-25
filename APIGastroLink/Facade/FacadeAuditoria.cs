using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Factory.Interfaces;
using APIGastroLink.Models;
using System.Security.Claims;

namespace APIGastroLink.Facade {
    public class FacadeAuditoria : IFacadeAuditoria {
        private readonly IDAOAuditoria _daoAuditoria;
        private readonly IAuditoriaFactory _auditoriaFactory;
        private readonly IUsuarioFactory _usuarioFactory;

        public FacadeAuditoria(IDAOAuditoria daoAuditoria, IAuditoriaFactory auditoriaFactory, IUsuarioFactory usuarioFactory) {
            _daoAuditoria = daoAuditoria;
            _auditoriaFactory = auditoriaFactory;
            _usuarioFactory = usuarioFactory;
        }

        public async Task RegistrarAuditoria(AcaoAuditoriaEnum acao, string descicao, ClaimsPrincipal claim) {
            var usuario = _usuarioFactory.CriarUsuarioLogado(claim);
            var auditoria = _auditoriaFactory.Criar(acao, descicao, usuario);
            await _daoAuditoria.RegisterAudit(auditoria); 
        }
    }
}
