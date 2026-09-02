using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOLogin : IDAOLogin {
        private readonly IDAODatabase _database;
        private readonly ILogService _logService;

        public DAOLogin(IDAODatabase database, ILogService logService) {
            _database = database;
            _logService = logService;
        }

        public Usuario Autenticar(Login Login) {
            Usuario Usuario = null;
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_VALIDAR_LOGIN", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LOGIN", Login.LoginStr);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    Usuario = new Usuario();

                                    Usuario.Id = reader.GetInt32(reader.GetOrdinal("USU_ID"));
                                    Usuario.Nome = reader.GetString(reader.GetOrdinal("USU_NOME"));
                                    Usuario.Login = reader.GetString(reader.GetOrdinal("USU_LOGIN"));
                                    Usuario.Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS"));
                                    Usuario.Password = reader.GetString(reader.GetOrdinal("USU_SENHA"));
                                    Usuario.Tipo.Id = reader.GetInt32(reader.GetOrdinal("USU_TPU_ID"));
                                    Usuario.Tipo.Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"));
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                _logService.Error(ex,$"Erro ao autenticar usuário: {ex.Message}");
            }
            return Usuario;
        }
    }
}
