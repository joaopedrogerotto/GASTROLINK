using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;

namespace APIGastroLink.DAO {
    public class DAOPrato : IDAOPrato {
        private readonly IDAODatabase _database;

        public DAOPrato(IDAODatabase database) {
            _database = database;
        }

        public void Insert(Prato Prato) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_PRATOS", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NOME", Prato.Nome);
                        cmd.Parameters.AddWithValue("@DESCRICAO", Prato.Descricao);
                        cmd.Parameters.AddWithValue("@PRECO", Prato.Preco);
                        cmd.Parameters.AddWithValue("@TEMPO_MEDIO_PREPARO", Prato.TempoMedioPreparo);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA_PRATO", Prato.CategoriaPrato.Id);
                        cmd.Parameters.AddWithValue("@URL_IMAGEM", Prato.UrlImagem);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) { 
                throw new Exception("Erro ao inserir prato: " + ex.Message);
            }
        }
    }
}
