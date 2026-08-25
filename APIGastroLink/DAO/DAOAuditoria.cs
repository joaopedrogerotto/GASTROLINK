using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOAuditoria : IDAOAuditoria {
        private readonly IDAODatabase _database;

        public DAOAuditoria(IDAODatabase database) {
            _database = database;
        }

        public async Task RegisterAudit(Auditoria Auditoria) { 
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_AUDITORIA", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ACAO", Auditoria.Acao);
                        cmd.Parameters.AddWithValue("@DESCRICAO", Auditoria.Descricao);
                        cmd.Parameters.AddWithValue("@ID_USUARIO", Auditoria.Usuario.Id);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }
    }
}
