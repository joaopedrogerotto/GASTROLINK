using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOFormaPagamento : IDAOFormaPagamento {
        private readonly IDAODatabase _database;

        public DAOFormaPagamento(IDAODatabase database) {
            _database = database;
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
                throw new Exception(ex.Message);
            }
        }
    }
}
