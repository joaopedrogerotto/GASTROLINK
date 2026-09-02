using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOTipoUsuario : IDAOTipoUsuario {
        private readonly IDAODatabase _database;
        private readonly ILogService _logService;

        public DAOTipoUsuario(IDAODatabase database, ILogService logService) {
            _database = database;
            _logService = logService;
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
                _logService.Error(ex, "Erro ao selecionar tipos de usuários: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
