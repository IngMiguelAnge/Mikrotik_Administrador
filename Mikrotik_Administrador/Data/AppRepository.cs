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
        #region ActionsMikrotiks
        public async Task<bool> DesactivarMikrotik(int Id)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("DesactivarMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InsertandUpdateMikrotik(MikrotikModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertandUpdateMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", obj.Id));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", obj.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@IP", obj.IP));
                        cmd.Parameters.Add(new SqlParameter("@Port", obj.Port));
                        cmd.Parameters.Add(new SqlParameter("@Usuario", obj.Usuario));
                        cmd.Parameters.Add(new SqlParameter("@Password", obj.Password));
                        cmd.Parameters.Add(new SqlParameter("@IpMax", obj.IpMax));
                        cmd.Parameters.Add(new SqlParameter("@IpMin", obj.IpMin));
                        cmd.Parameters.Add(new SqlParameter("@Estatus", obj.Estatus));
                        cmd.Parameters.Add(new SqlParameter("@Limite_Alcanzado", obj.Limite_Alcanzado));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<MikrotikModel> GetMikrotikById(int Id)
        {
            MikrotikModel response = new MikrotikModel();
            List<MikrotikModel> list = new List<MikrotikModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetMikrotikById", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToMikrotik(reader));
                            }
                            response = list.Count() > 0 ? list[0] : null;
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
        public async Task<List<ListMikrotikModel>> GetMikrotiks()
        {
            List<ListMikrotikModel> list = new List<ListMikrotikModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetMikrotiks", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToMikrotiks(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        private MikrotikModel MapToMikrotik(SqlDataReader reader)
        {
            return new MikrotikModel()
            {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                IP = (string)reader["IP"],
                Port = (string)reader["Port"],
                Usuario = (string)reader["Usuario"],
                Password = (string)reader["Password"],
                IpMax = (string)reader["IpMax"],
                IpMin = (string)reader["IpMin"],
                Estatus = (bool)reader["Estatus"],
                Limite_Alcanzado = (bool)reader["Limite_Alcanzado"],
            };
        }
        private ListMikrotikModel MapToMikrotiks(SqlDataReader reader)
        {
            return new ListMikrotikModel()
            {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                IP = (string)reader["IP"],
                Estatus = (string)reader["Estatus"],
                Limite_Alcanzado = (string)reader["Limite"],
            };
        }

        #endregion
    }
}
