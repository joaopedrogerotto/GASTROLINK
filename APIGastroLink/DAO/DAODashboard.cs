using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAODashboard : IDAODashboard {
        private readonly IDAODatabase _database;
        private readonly ILogService _logService;

        public DAODashboard(IDAODatabase database, ILogService logService) {
            _database = database;
            _logService = logService;
        }

        public IndicadorDashboardDTO DashboardVendasCategoria(DashboardFiltroDTO DashboardFiltroDTO) {
            try {
                var indicadorDashboard = new IndicadorDashboardDTO();
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_DASHBOARD_CATEGORIAS", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (DashboardFiltroDTO.DataInicio != null) {
                            cmd.Parameters.AddWithValue("@DATA_INICIO", DashboardFiltroDTO.DataInicio);
                        }

                        if (DashboardFiltroDTO.DataFim != null) {
                            cmd.Parameters.AddWithValue("@DATA_FIM", DashboardFiltroDTO.DataFim);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            indicadorDashboard.Nome = "Vendas por categoria";
                            indicadorDashboard.Tipo = "bar";
                            while (reader.Read()) {
                                indicadorDashboard.Dados.Add(new DadosDashboardDTO {
                                    Label = reader.GetString(reader.GetOrdinal("CTP_CATEGORIA")),
                                    Valor = reader.GetDecimal(reader.GetOrdinal("VALOR_VENDIDO"))
                                });
                            }
                        }
                    }
                }
                return indicadorDashboard;
            } catch (Exception ex) {
                _logService.Error(ex,"Falha ao gerar dashboard de vendas por categoria: " + ex.Message);
                throw new Exception("Falha ao gerar dashboard de vendas por cateogoria: " + ex.Message);
            }
        }

        public IndicadorDashboardDTO DashboardVendasFormaPagamento(DashboardFiltroDTO DashboardFiltroDTO) {
            try {
                var indicadorDashboard = new IndicadorDashboardDTO();
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_DASHBOARD_FORMA_PAGAMENTO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (DashboardFiltroDTO.DataInicio != null) {
                            cmd.Parameters.AddWithValue("@DATA_INICIO", DashboardFiltroDTO.DataInicio);
                        }

                        if (DashboardFiltroDTO.DataFim != null) {
                            cmd.Parameters.AddWithValue("@DATA_FIM", DashboardFiltroDTO.DataFim);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            indicadorDashboard.Nome = "Vendas por categoria";
                            indicadorDashboard.Tipo = "line";
                            while (reader.Read()) {
                                indicadorDashboard.Dados.Add(new DadosDashboardDTO {
                                    Label = reader.GetString(reader.GetOrdinal("FORMA_PAGAMENTO")),
                                    Valor = reader.GetDecimal(reader.GetOrdinal("VALOR_TOTAL")),
                                    Data = reader.GetDateTime(reader.GetOrdinal("DATA"))
                                });
                            }
                        }
                    }
                }
                return indicadorDashboard;
            } catch (Exception ex) {
                _logService.Error(ex, "Falha ao gerar dashboard de vendas por categoria: " + ex.Message);
                throw new Exception("Falha ao gerar dashboard de vendas por cateogoria: " + ex.Message);
            }
        }

        public async Task<List<VendasPratosDTO>> PratosMaisVendidos() {
            try {
                var listVendas = new List<VendasPratosDTO>();
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_PRODUTOS_MAIS_VENDIDOS", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
                            while (await reader.ReadAsync()) {
                                listVendas.Add(new VendasPratosDTO {
                                    Nome = reader.GetString(reader.GetOrdinal("NOME")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("QUANTIDADE"))
                                });
                            }
                        }
                    }
                }
                return listVendas;
            }catch (Exception ex) { 
                _logService.Error(ex, "Falha ao gerar relatório de pratos mais vendidos: " + ex.Message);
                throw new Exception(ex.Message); 
            }
        }

        public async Task<ResumoFaturamentoDTO> ResumoFaturamento() {
            try {
                var resumoFat = new ResumoFaturamentoDTO();
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_RESUMO_FATURAMENTO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
                            while (await reader.ReadAsync()) {
                                resumoFat.TotalVendidos = reader.GetInt32(reader.GetOrdinal("TOTAL_VENDIDOS"));
                                resumoFat.Faturamento = reader.GetDecimal(reader.GetOrdinal("FATURAMENTO"));
                            }
                        }
                    }
                }
                return resumoFat;
            }catch (Exception ex) {
                _logService.Error(ex, "Falha ao gerar resumo de faturamento: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
