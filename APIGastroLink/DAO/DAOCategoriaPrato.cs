using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
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

        public List<CategoriaPrato> SelectAll() {
            var categorias = new List<CategoriaPrato>();
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_CATEGORIAS_PRATO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                categorias.Add(new CategoriaPrato {
                                    Id = reader.GetInt32(reader.GetOrdinal("CTP_ID")),
                                    Categoria = reader.GetString(reader.GetOrdinal("CTP_CATEGORIA"))
                                });
                            }
                        }
                    }
                }
            } catch (SqlException ex) {
                throw new Exception("Erro ao selecionar categorias de prato: " + ex.Message);
            }
            return categorias;
        }

        public List<CategoriaPratoDTO> SelectAllDTOQuantidadePratos() {
            var categorias = new List<CategoriaPratoDTO>();
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_QTD_PRATOS_CATEGORIA", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                categorias.Add(new CategoriaPratoDTO {
                                    Id = reader.GetInt32(reader.GetOrdinal("CTP_ID")),
                                    Categoria = reader.GetString(reader.GetOrdinal("CTP_CATEGORIA")),
                                    TotalPratos = reader.GetInt32(reader.GetOrdinal("QTD_PRATOS"))
                                });
                            }
                        }
                    }
                }
            } catch (SqlException ex) {
                throw new Exception("Erro ao selecionar categorias de prato com quantidade: " + ex.Message);
            }
            return categorias;
        }
    }   
}
