using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOFormaPagamento : IDAOFormaPagamento {
        private readonly IDAODatabase _database;
        private readonly ILogService _logService;

        public DAOFormaPagamento(IDAODatabase database, ILogService logService) {
            _database = database;
            _logService = logService;
        }

        public async Task<List<FormaPagamento>> SelectAll() {
            try {
                var listaFormaPag = new List<FormaPagamento>();

                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_FORMAS_PAGAMENTO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
                            while (reader.Read()) {
                                listaFormaPag.Add(new FormaPagamento {
                                    Id = reader.GetInt32(reader.GetOrdinal("FPG_ID")),
                                    Forma = reader.GetString(reader.GetOrdinal("FPG_FORMA"))
                                });
                            }
                        }
                    }
                }
                return listaFormaPag;
            }catch (Exception ex) {
                _logService.Error(ex, "Erro ao selecionar formas de pagamento: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
