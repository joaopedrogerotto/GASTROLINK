using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOUsuario : IDAOUsuario {
        private readonly IDAODatabase _database;

        public DAOUsuario(IDAODatabase database) {
            _database = database;
        }

        public void Insert(Usuario Usuario) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd =  new SqlCommand("PR_I_CADASTRO_USUARIO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOME", Usuario.Nome);
                        cmd.Parameters.AddWithValue("@LOGIN", Usuario.Login);
                        cmd.Parameters.AddWithValue("@SENHA", Usuario.Password);
                        cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", Usuario.Tipo.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception sqlEx) {
                throw new Exception(sqlEx.Message);
            }
        }

        public List<Usuario> SelectAll() {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_TODOS_USUARIOS", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            List<Usuario> usuarios = new List<Usuario>();
                            while (reader.Read()) {
                                Usuario usuario = new Usuario {
                                    Id = reader.GetInt32(reader.GetOrdinal("USU_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("USU_NOME")),
                                    Login = reader.GetString(reader.GetOrdinal("USU_LOGIN")),
                                    Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS")),
                                    Tipo = new TipoUsuario {
                                        Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                        Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                    }
                                };
                                usuarios.Add(usuario);
                            }
                            return usuarios;
                        }
                    }   
                }
            }catch (Exception sqlEx) { 
                throw new Exception( sqlEx.Message); 
            }
        }

        public Usuario SelectById(int usuarioId) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_USUARIO_POR_ID", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_USUARIO", usuarioId);
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                return new Usuario {
                                    Id = reader.GetInt32(reader.GetOrdinal("USU_ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("USU_NOME")),
                                    Login = reader.GetString(reader.GetOrdinal("USU_LOGIN")),
                                    Status = reader.GetBoolean(reader.GetOrdinal("USU_STATUS")),
                                    Tipo = new TipoUsuario {
                                        Id = reader.GetInt32(reader.GetOrdinal("TPU_ID")),
                                        Tipo = reader.GetString(reader.GetOrdinal("TPU_TIPO"))
                                    }
                                };
                            } else {
                                return null;
                            }
                        }
                    }
                }
            } catch (Exception sqlEx) {
                throw new Exception(sqlEx.Message);
            }
        }

        public void Update(Usuario Usuario) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_U_USUARIO", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_USUARIO", Usuario.Id);
                        cmd.Parameters.AddWithValue("@NOME", Usuario.Nome);
                        cmd.Parameters.AddWithValue("@LOGIN", Usuario.Login);
                        cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", Usuario.Tipo.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception sqlEx) {
                throw new Exception(sqlEx.Message);
            }
        }
    }
}
