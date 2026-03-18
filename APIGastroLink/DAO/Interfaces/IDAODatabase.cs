using Microsoft.Data.SqlClient;

namespace APIGastroLink.DAO.Interfaces {
    public interface IDAODatabase {
        public SqlConnection OpenConnection();
        public void CloseConnection(SqlConnection connection);
    }
}
