using Microsoft.Data.SqlClient;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;


//using Mikrotik_Administrador.Model;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace Mikrotik_Administrador.Data
{
    public class AppRepository
    {
        public string MikrotikConnection { get; set; }
        public AppRepository(bool isUnitOfWork = false)
        {
             MikrotikConnection = "Data Source=.;Initial Catalog=Mikrotiks;User ID=sa;Password=admin123;Trust Server Certificate=True";
        }
        public void Dispose()
        {
            GC.Collect();
        }
        #region ActionsUsers
        public async Task<UserModel> GetUser(string Usuario, string Password)
        {
            UserModel response = new UserModel();
            List<UserModel> list = new List<UserModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserbyNameAndPassword", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                        cmd.Parameters.Add(new SqlParameter("@Password", Password));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToUser(reader));
                            }
                            response = list.Count()> 0 ? list[0]: null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }
        private UserModel MapToUser(SqlDataReader reader)
        {
            return new UserModel()
            {
                Id = (int)reader["Id"],
                Usuario = (string)reader["Usuario"],
                Password = (string)reader["Password"],
                Estatus = (bool)reader["Estatus"],
                Id_TipoUsuario = Convert.IsDBNull(reader["Id_TipoUsuario"]) ? 0 : (int)reader["Id_TipoUsuario"],
            };
        }

        #endregion
    }
}
