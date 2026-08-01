using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using Microsoft.Data.SqlClient;

namespace APIGastroLink.DAO {
    public class DAOPagamento : IDAOPagamento {
        private readonly IDAODatabase _database;

        public DAOPagamento(IDAODatabase database) {
            _database = database;
        }

        public async Task<bool> Insert(PagamentoRequestDTO PagamentoRequestDTO) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_PAGAMENTO", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DESCONTO", PagamentoRequestDTO.Desconto);
                        cmd.Parameters.AddWithValue("@VALOR_PAGO", PagamentoRequestDTO.ValorPago);
                        cmd.Parameters.AddWithValue("@VALOR_TOTAL", PagamentoRequestDTO.ValorTotal);
                        cmd.Parameters.AddWithValue("@ID_PEDIDO", PagamentoRequestDTO.IdPedido);
                        cmd.Parameters.AddWithValue("@ID_FORMA_PAGAMENTO", PagamentoRequestDTO.IdFormaPagamento);
                        cmd.Parameters.AddWithValue("@ID_USUARIO", PagamentoRequestDTO.IdUsuario);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            } catch (Exception ex) {
                throw new Exception("Falha ao inserir o pagamento.", ex);
            }
        }
    }
}
