using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOHistoricoDisponibilidade : IDAOHistoricoDisponibilidade {
        private readonly IDAODatabase _database;
        private readonly ILogService _logService;

        public DAOHistoricoDisponibilidade(IDAODatabase database, ILogService logService) {
            _database = database;
            _logService = logService;
        }

        public void Insert(HistoricoDisponibilidade historicoDisponibilidade) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_HISTORICO_DISPONIBILIDADE", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_PRATO", historicoDisponibilidade.Prato.Id);
                        cmd.Parameters.AddWithValue("@ID_USUARIO", historicoDisponibilidade.Usuario.Id);
                        cmd.Parameters.AddWithValue("@JUSTIFICATIVA", historicoDisponibilidade.Justificativa);
                        cmd.Parameters.AddWithValue("@DISPONIBILIDADE", historicoDisponibilidade.Disponivel);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Erro ao inserir histórico de disponibilidade.", ex);
            }
        }

        public async Task<List<HistoricoDisponibilidade>> SelectByIdPrato(int idPrato) {
            var historicos = new List<HistoricoDisponibilidade>();

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_HISTORICO_POR_ID_PRATO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_PRATO", idPrato);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                var historico = new HistoricoDisponibilidade {
                                    Data = reader.GetDateTime("DATA"),
                                    Justificativa = reader.GetString("JUSTIFICATIVA"),
                                    Usuario = new Usuario { Nome = reader.GetString("NOME") },
                                    Prato = new Prato { Id = idPrato },
                                    Disponivel = reader.GetBoolean("DISPONIBILIDADE")
                                };
                                historicos.Add(historico);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                _logService.Error(ex, "Erro ao selecionar histórico de disponibilidade: " + ex.Message);
                throw new Exception("Erro ao selecionar histórico de disponibilidade.", ex);
            }

            return historicos;
        }
    }
}

