using APIGastroLink.DAO.Interfaces;
using Microsoft.Data.SqlClient;

namespace APIGastroLink.DAO {
    public class DAOSQLServer : IDAODatabase {
        private string _strConexao { get; set; }

        public DAOSQLServer(IConfiguration configuration) {
            _strConexao = configuration.GetConnectionString("DefaultConnection");
        }

        public void CloseConnection(SqlConnection connection) {
            if(connection !=  null && connection.State == System.Data.ConnectionState.Open) {
                connection.Close();
            }
        }

        public SqlConnection OpenConnection() {
            var conn = new SqlConnection(_strConexao);
            conn.Open();
            return conn;
        }
    }
}
