using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Exceptions;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOCategoriaPrato : IDAOCategoriaPrato {
        private readonly IDAODatabase _database;

        public DAOCategoriaPrato(IDAODatabase database) {
            _database = database;
        }

        public void Insert(CategoriaPrato categoriaPrato) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_CATEGORIA_PRATO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CATEGORIA", categoriaPrato.Categoria);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) {
                throw new EntityAlreadyExistsException("Categoria já cadastrada");
            } catch (SqlException ex) {
                throw new Exception("Erro ao inserir categoria de prato: " + ex.Message);
            }
        }
    }
}
