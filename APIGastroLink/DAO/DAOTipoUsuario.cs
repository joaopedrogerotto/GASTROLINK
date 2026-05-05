using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOTipoUsuario : IDAOTipoUsuario {
        private readonly IDAODatabase _database;

        public DAOTipoUsuario(IDAODatabase database) {
            _database = database;
        }

        public List<TipoUsuario> SelectAll() {
            var listTiposUsuarios = new List<TipoUsuario>();
            try {
                using (var connection = _database.OpenConnection()) {
                    using (var command = new SqlCommand("PR_S_CONSULTA_TIPOS_USUARIOS", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                var tipoUsuario = new TipoUsuario {
                                    Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                    Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                };
                                listTiposUsuarios.Add(tipoUsuario);
                            }
                        }
                    }
                }
                return listTiposUsuarios;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
