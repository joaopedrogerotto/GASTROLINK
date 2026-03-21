using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIGastroLink.DAO {
    public class DAOMesa : IDAOMesa {
        private readonly IDAODatabase _database;

        public DAOMesa(IDAODatabase database) {
            _database = database;
        }

        public void Delete(Mesa Mesa) {
            throw new NotImplementedException();
        }

        public void Insert(string Numero) {
            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_I_CADASTRAR_MESA", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NUMERO_MESA",Numero);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<Mesa> SelectAll() {
            List<Mesa> listMesa = new List<Mesa>();

            try {
                using (SqlConnection conn = _database.OpenConnection()) {
                    using (SqlCommand cmd = new SqlCommand("PR_S_CONSULTAR_MESAS", conn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    Mesa mesa = new Mesa();

                                    mesa.Id = reader.GetInt32(reader.GetOrdinal("MSA_ID"));
                                    mesa.Numero = reader.GetString(reader.GetOrdinal("MSA_NUMERO"));
                                    mesa.Status.Id = reader.GetInt32(reader.GetOrdinal("STM_ID"));
                                    mesa.Status.Status = reader.GetString(reader.GetOrdinal("STM_STATUS"));

                                    listMesa.Add(mesa);
                                }
                            }
                        }
                    }
                }
                return listMesa;
            } catch (Exception ex) { 
                throw new Exception(ex.ToString());
            }
        }

        public void Update(Mesa Mesa) {
            throw new NotImplementedException();
        }
    }
}
