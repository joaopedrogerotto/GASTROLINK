using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOPedido : IDAOPedido {
        private readonly IDAODatabase _database;

        public DAOPedido(IDAODatabase database) {
            _database = database;
        }

        public async Task CadastrarPedido(PedidoCreateDTO pedido) {
            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand cmd = new SqlCommand("PR_I_PEDIDO", conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MESA_ID", pedido.IdMesa);
                    cmd.Parameters.AddWithValue("@USUARIO_ID", pedido.IdUsuario);
                    cmd.Parameters.AddWithValue("@VALOR_TOTAL", pedido.ValorTotal);
                    cmd.Parameters.AddWithValue("@ITENS_PEDIDO", ConvertForDataTable(pedido.Itens));
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private DataTable ConvertForDataTable(List<ItemPedidoCreateDTO> itens) {
            var table = new DataTable();

            table.Columns.Add("ID_PRATO", typeof(int));
            table.Columns.Add("OBSERVACAO", typeof(string));
            table.Columns.Add("QUANTIDADE", typeof(int));

            foreach (var item in itens) {
                table.Rows.Add(item.IdPrato, item.Observacao, item.Quantidade);
            }

            return table;
        }
    }
}
