using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
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

        public async Task<List<Prato>> SelectAll() {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_TODOS_PRATOS", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            return MontarListaPratos(reader);
                        }
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Falha ao buscar todos os pratos: " + ex.Message);
            }
        }

        public async Task<Prato> SelectById(int id) {
            var prato = new Prato();

            if (id == 0) {
                throw new ArgumentException("Id informado não pode ser 0");
            }

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_PRATO_POR_ID", conn)) {
                        cmd.Parameters.AddWithValue("@ID_PRATO", id);

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                prato = new Prato() {
                                    Id = reader.GetInt32(reader.GetOrdinal("PRT_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("NOME")),
                                    Descricao = reader.GetString(reader.GetOrdinal("DESCRICAO")),
                                    Preco = reader.GetDecimal(reader.GetOrdinal("PRECO")),
                                    Disponibilidades = reader.GetBoolean(reader.GetOrdinal("DISPONIBILIDADE")),
                                    TempoMedioPreparo = reader.GetInt32(reader.GetOrdinal("TEMPO")),
                                    UrlImagem = reader.GetString(reader.GetOrdinal("IMAGEM")),
                                    CategoriaPrato = new CategoriaPrato {
                                        Id = reader.GetInt32(reader.GetOrdinal("CTP_ID")),
                                        Categoria = reader.GetString(reader.GetOrdinal("CATEGORIA"))
                                    }
                                };
                            }
                        }
                    }
                }
                return prato;
            } catch (Exception ex) {
                throw new Exception("Falha ao buscar o prato: " + ex.Message);
            }
        }

        public void UpdateDisponibilidade(Prato Prato) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_U_DISPONIBILIDADE_PRATO", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_PRATO", Prato.Id);
                        cmd.Parameters.AddWithValue("@DISPONIBILIDADE", Prato.Disponibilidades);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Falha em atualizar a disponibilidade do prato: " + ex.Message);
            }
        }

        public async Task<List<Prato>> SelectWithFilters(FiltroPesquisaDTO filtro) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_PESQUISAR_PRATOS", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        if (!string.IsNullOrEmpty(filtro.Nome)) {
                            cmd.Parameters.AddWithValue("@NOME", filtro.Nome);
                        }

                        if (!string.IsNullOrEmpty(filtro.Descricao)) {
                            cmd.Parameters.AddWithValue("@DESCRICAO", filtro.Descricao);
                        }

                        if (filtro.Preco > 0.0m) {
                            cmd.Parameters.AddWithValue("@PRECO", filtro.Preco);
                        }

                        if (filtro.IdCategoria > 0) {
                            cmd.Parameters.AddWithValue("@ID_CATEGORIA", filtro.IdCategoria);
                        }

                        if (filtro.Disponibilidade != null) {
                            cmd.Parameters.AddWithValue("@DISPONIBILIDAE", filtro.Disponibilidade);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            return MontarListaPratos(reader);
                        }
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Falha ao buscar todos os pratos: " + ex.Message);
            }
        }
        public void UpdatePrato(PratoEditarDTO Prato) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_U_PRATO", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_PRATO", Prato.Id);
                        cmd.Parameters.AddWithValue("@NOME", Prato.Nome);
                        cmd.Parameters.AddWithValue("@DESCRICAO", Prato.Descricao);
                        cmd.Parameters.AddWithValue("@PRECO", Prato.Preco);
                        cmd.Parameters.AddWithValue("@TEMPO_MEDIO_PREPARO", Prato.TempoMedioPreparo);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA_PRATO", Prato.IdCategoriaPrato);
                        cmd.Parameters.AddWithValue("@URL_IMAGEM", Prato.UrlImagem);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Erro ao inserir prato: " + ex.Message);
            }
        }

        private List<Prato> MontarListaPratos(SqlDataReader reader) {
            var listPratos = new List<Prato>();
            while (reader.Read()) {
                listPratos.Add(new Prato {
                    Id = reader.GetInt32(reader.GetOrdinal("PRT_ID")),
                    Nome = reader.GetString(reader.GetOrdinal("NOME")),
                    Descricao = reader.GetString(reader.GetOrdinal("DESCRICAO")),
                    Preco = reader.GetDecimal(reader.GetOrdinal("PRECO")),
                    Disponibilidades = reader.GetBoolean(reader.GetOrdinal("DISPONIBILIDADE")),
                    TempoMedioPreparo = reader.GetInt32(reader.GetOrdinal("TEMPO")),
                    UrlImagem = reader.GetString(reader.GetOrdinal("IMAGEM")),
                    CategoriaPrato = new CategoriaPrato {
                        Id = reader.GetInt32(reader.GetOrdinal("CTP_ID")),
                        Categoria = reader.GetString(reader.GetOrdinal("CATEGORIA"))
                    }
                });
            }
            return listPratos;
        }
    }
}