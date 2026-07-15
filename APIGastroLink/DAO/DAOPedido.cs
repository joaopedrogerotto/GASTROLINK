using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOPedido : IDAOPedido {
        private readonly IDAODatabase _database;

        public DAOPedido(IDAODatabase database) {
            _database = database;
        }

        public async Task<int> CadastrarPedido(PedidoCreateDTO pedido) {
            int idPedido = 0;
            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand cmd = new SqlCommand("PR_I_PEDIDO", conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MESA_ID", pedido.IdMesa);
                    cmd.Parameters.AddWithValue("@USUARIO_ID", pedido.IdUsuario);
                    cmd.Parameters.AddWithValue("@VALOR_TOTAL", pedido.ValorTotal);
                    cmd.Parameters.AddWithValue("@ITENS_PEDIDO", ConvertForDataTable(pedido.Itens));
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
                        while (reader.Read()) {
                            idPedido = reader.GetInt32(reader.GetOrdinal("ID_PEDIDO"));
                        }
                    }
                }
            }
            return idPedido;
        }

        public async Task<Pedido> SelecionarPedidoPorId(int idPedido) {
            using (SqlConnection conn = _database.OpenConnection()) {
                using (SqlCommand cmd = new SqlCommand("PR_S_PEDIDO_POR_ID", conn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PEDIDO_ID", idPedido);
                    var pedidosMap = new Dictionary<int, Pedido>();

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            int id = reader.GetInt32(reader.GetOrdinal("NUMERO_PEDIDO"));
                            var pedido = new Pedido();

                            if (!pedidosMap.ContainsKey(id)) {
                                pedido = new Pedido() {
                                    Id = id,
                                    ValorTotal = reader.GetDecimal(reader.GetOrdinal("TOTAL")),
                                    Status = new StatusPedido() {
                                        Id = reader.GetInt32(reader.GetOrdinal("ID_STATUS")),
                                        Status = reader.GetString(reader.GetOrdinal("STATUS"))
                                    },
                                    Mesa = new Mesa() {
                                        Id = reader.GetInt32(reader.GetOrdinal("ID_MESA")),
                                        Numero = reader.GetString(reader.GetOrdinal("NUMERO_MESA"))
                                    },
                                    Usuario = new Usuario() {
                                        Id = reader.GetInt32(reader.GetOrdinal("ID_USUARIO")),
                                        Nome = reader.GetString(reader.GetOrdinal("NOME_USUARIO"))
                                    },
                                    dataCriacao = reader.GetDateTime(reader.GetOrdinal("DATA"))
                                };


                                pedidosMap[id] = pedido;
                            }

                            pedido = pedidosMap[id];

                            pedido.Itens.Add(
                                new ItemPedido {
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("QUANTIDADE")),
                                    Observacao = reader.GetString(reader.GetOrdinal("OBSERVACAO")),
                                    Prato = new Prato() {
                                        Id = reader.GetInt32(reader.GetOrdinal("ID_PRATO")),
                                        Nome = reader.GetString(reader.GetOrdinal("NOME_PRATO"))
                                    }
                                }
                              );
                        }
                    }
                    return pedidosMap[idPedido];
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
