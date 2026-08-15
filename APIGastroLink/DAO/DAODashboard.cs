using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAODashboard : IDAODashboard {
        private readonly IDAODatabase _database;

        public DAODashboard(IDAODatabase database) {
            _database = database;
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
                throw new Exception("Falha ao gerar dashboard de vendas por cateogoria: " + ex.Message);
            }
        }
    }
}
