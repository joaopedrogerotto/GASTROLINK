using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOUsuario : IDAOUsuario {
        private readonly IDAODatabase _database;

        public DAOUsuario(IDAODatabase database) {
            _database = database;
        }

        public void Insert(Usuario Usuario) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd =  new SqlCommand("PR_I_CADASTRO_USUARIO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOME", Usuario.Nome);
                        cmd.Parameters.AddWithValue("@LOGIN", Usuario.Login);
                        cmd.Parameters.AddWithValue("@SENHA", Usuario.Password);
                        cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", Usuario.Tipo.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception sqlEx) {
                throw new Exception(sqlEx.Message);
            }
        }
    }
}
